/*******************************************************************************
/* ODBCSQL: a sample program that implements an ODBC command line interpreter.
/*
/* USAGE:   ODBCSQL DSN=<dsn name>   or
/*          ODBCSQL FILEDSN=<file dsn> or
/*          ODBCSQL DRIVER={driver name}
/*
/*
/* Copyright(c) Microsoft Corporation.   This is a WDAC sample program and
/* is not suitable for use in production environments.   
/*
/******************************************************************************/
/* Modules:
/*      Main                Main driver loop, executes queries.
/*      DisplayResults      Display the results of the query if any
/*      AllocateBindings    Bind column data
/*      DisplayTitles       Print column titles
/*      SetConsole          Set console display mode
/*      HandleError         Show ODBC error messages
/******************************************************************************/

#include <windows.h>
#include <sql.h>
#include <sqlext.h>
#include <stdio.h>
#include <conio.h>
#include <tchar.h>
#include <stdlib.h>
#include <sal.h>
#include <minmax.h>
#include <string>
#include <locale>
#include <utility>
#include <codecvt>
#include <vector>

/*******************************************/
/* Macro to call ODBC functions and        */
/* report an error on failure.             */
/* Takes handle, handle type, and stmt     */
/*******************************************/

#define TRYODBC(h, ht, x)   {   RETCODE rc = x;\
	if (rc != SQL_SUCCESS) \
								{ \
								HandleDiagnosticRecord (h, ht, rc); \
								} \
								if (rc == SQL_ERROR) \
								{ \
								fwprintf(stderr, L"Error in " L#x L"\n"); \
								goto Exit;  \
								}  \
							}

namespace ModDb
{
	/******************************************/
	/* Structure to store information about   */
	/* a column.
	/******************************************/

	typedef struct STR_BINDING {
		SQLSMALLINT         cDisplaySize;           /* size to display  */
		WCHAR               *wszBuffer;             /* display buffer   */
		SQLLEN              indPtr;                 /* size or null     */
		BOOL                fChar;                  /* character col?   */
		struct STR_BINDING  *sNext;                 /* linked list      */
	} BINDING;

	typedef struct SCENARIO_BINDING {
		int				ScenarioId;
		std::string		Name;
		float			BinSize;
	} SCENARIO;

	typedef struct ITEM_BINDING {
		int				ItemId;
		int				RunId;
		std::string		Label;
		int				Quantity;
		float			Size;
	} ITEM;

	typedef struct BINITEM_BINDING {
		int				BinItemId;
		int				BinId;
		std::string		Label;
		float			Size;
	} BINITEM;

	typedef struct BIN_BINDING {
		int				BinId;
		int				PopulationId;
		float			Filled;
		float			Capacity;
	} BIN;

	typedef struct POPULATION_BINDING {
		int				PopulationId;
		int				GenerationId;
		int				Number;
		float			Fitness;
		int				BinCount;
	} POPULATION;

	typedef struct GENERATION_BINDING {
		int				GenerationId;
		int				RunId;
		int				Number;
	} GENERATION;

	typedef struct RUN_BINDING {
		int				RunId;
		int				ScenarioId;
	} RUN;

	typedef struct SQL_CONNECTION {
		SQLHENV     hEnv;
		SQLHDBC     hDbc;
		SQLHSTMT    hStmt;
	};



	/******************************************/
	/* Forward references                     */
	/******************************************/

	void Connect ();
	void Disconnect ();
	SCENARIO GetScenario(int id);
	std::vector<ITEM> GetItems(int runId);
	RUN InsertRun(int scenarioId);
	GENERATION InsertGeneration(GENERATION generation);
	POPULATION InsertPopulation(POPULATION population);
	BIN InsertBin(BIN bin);
	BINITEM InsertBinItem(BINITEM binItem);


	void HandleDiagnosticRecord (SQLHANDLE      hHandle,    
		SQLSMALLINT    hType,  
		RETCODE        RetCode);

	void DisplayResults(HSTMT       hStmt,
		SQLSMALLINT cCols);

	void AllocateBindings(HSTMT         hStmt,
		SQLSMALLINT   cCols,
		BINDING**     ppBinding,
		SQLSMALLINT*  pDisplay);

	void DisplayTitles(HSTMT    hStmt,
		DWORD    cDisplaySize,
		BINDING* pBinding);

	void SetConsole(DWORD   cDisplaySize,
		BOOL    fInvert);

	/*****************************************/
	/* Some constants                        */
	/*****************************************/


#define DISPLAY_MAX 50          // Arbitrary limit on column width to display
#define DISPLAY_FORMAT_EXTRA 3  // Per column extra display bytes (| <data> )
#define DISPLAY_FORMAT      L"%c %*.*s "
#define DISPLAY_FORMAT_C    L"%c %-*.*s "
#define NULL_SIZE           6   // <NULL>
#define SQL_QUERY_SIZE      1000 // Max. Num characters for SQL Query passed in.

#define PIPE                L'|'

	SHORT   gHeight = 80;       // Users screen height
	WCHAR* defaultConnectionString = L"Driver={SQL Server Native Client 11.0};Server=localhost\\SQLEXPRESS;Database=modelosIIIT;Trusted_Connection=yes;";
	SQL_CONNECTION _sqlConnection;

	std::wstring s2ws(const std::string& str)
	{
		typedef std::codecvt_utf8<wchar_t> convert_typeX;
		std::wstring_convert<convert_typeX, wchar_t> converterX;

		return converterX.from_bytes(str);
	}

	std::string ws2s(const std::wstring& wstr)
	{
		typedef std::codecvt_utf8<wchar_t> convert_typeX;
		std::wstring_convert<convert_typeX, wchar_t> converterX;

		return converterX.to_bytes(wstr);
	}

	void Connect(){

		SQL_CONNECTION sqlConnection = SQL_CONNECTION();
		WCHAR*      pwszConnStr;
		WCHAR       wszInput[SQL_QUERY_SIZE];

		sqlConnection.hEnv = NULL;
		sqlConnection.hDbc = NULL;
		sqlConnection.hStmt = NULL;

		// Allocate an environment

		if (SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &sqlConnection.hEnv) == SQL_ERROR)
		{
			fwprintf(stderr, L"Unable to allocate an environment handle\n");
			exit(-1);
		}

		// Register this as an application that expects 3.x behavior,
		// you must register something if you use AllocHandle

		TRYODBC(sqlConnection.hEnv,
			SQL_HANDLE_ENV,
			SQLSetEnvAttr(sqlConnection.hEnv,
			SQL_ATTR_ODBC_VERSION,
			(SQLPOINTER)SQL_OV_ODBC3,
			0));

		// Allocate a connection
		TRYODBC(sqlConnection.hEnv,
			SQL_HANDLE_ENV,
			SQLAllocHandle(SQL_HANDLE_DBC, sqlConnection.hEnv, &sqlConnection.hDbc));

		pwszConnStr = defaultConnectionString;

		// Connect to the driver.  Use the connection string if supplied
		// on the input, otherwise let the driver manager prompt for input.

		TRYODBC(sqlConnection.hDbc,
			SQL_HANDLE_DBC,
			SQLDriverConnect(sqlConnection.hDbc,
			GetDesktopWindow(),
			pwszConnStr,
			SQL_NTS,
			NULL,
			0,
			NULL,
			SQL_DRIVER_COMPLETE));

		fwprintf(stderr, L"Connected!\n");

		TRYODBC(sqlConnection.hDbc,
			SQL_HANDLE_DBC,
			SQLAllocHandle(SQL_HANDLE_STMT, sqlConnection.hDbc, &sqlConnection.hStmt));

		_sqlConnection = sqlConnection;

Exit:
		{}
		//Disconnect();
	};

	void Disconnect () {
		// Free ODBC handles and exit

		if (_sqlConnection.hStmt)
		{
			SQLFreeHandle(SQL_HANDLE_STMT, _sqlConnection.hStmt);
		}

		if (_sqlConnection.hDbc)
		{
			SQLDisconnect(_sqlConnection.hDbc);
			SQLFreeHandle(SQL_HANDLE_DBC, _sqlConnection.hDbc);
		}

		if (_sqlConnection.hEnv)
		{
			SQLFreeHandle(SQL_HANDLE_ENV, _sqlConnection.hEnv);
		}

		wprintf(L"\nDisconnected.");
	};

	SCENARIO GetScenario(int id) {
		RETCODE     retCode;
		SQLSMALLINT sNumResults;
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         qRetCode = SQL_SUCCESS;
		SCENARIO result;

		/*SQL_CONNECTION sqlConnection = Connect();*/
		std::wstring s = std::wstring(L"SELECT * FROM Scenarios WHERE ScenarioId=");
		s += std::wstring(std::to_wstring(id));
		WCHAR* query = const_cast<wchar_t*>(s.c_str());

		retCode = SQLExecDirect(_sqlConnection.hStmt, query, SQL_NTS);

		switch(retCode)
		{
		case SQL_SUCCESS_WITH_INFO:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				// fall through
			}
		case SQL_SUCCESS:
			{
				// If this is a row-returning query, display
				// results
				TRYODBC(_sqlConnection.hStmt,
					SQL_HANDLE_STMT,
					SQLNumResultCols(_sqlConnection.hStmt,&sNumResults));

				if (sNumResults > 0)
				{
					// Allocate memory for each column 
					AllocateBindings(_sqlConnection.hStmt, sNumResults, &pFirstBinding, &cDisplaySize);

					// Fetch and display the data
					bool fNoData = false;

					// Fetch a row
					TRYODBC(_sqlConnection.hStmt, SQL_HANDLE_STMT, qRetCode = SQLFetch(_sqlConnection.hStmt));

					if (qRetCode == SQL_NO_DATA_FOUND)
					{
						fNoData = true;
					}
					else
					{
						result = SCENARIO();
						pThisBinding = pFirstBinding;
						result.ScenarioId = wcstol(pThisBinding->wszBuffer, NULL, 10);
						pThisBinding = pThisBinding->sNext;
						result.Name = ws2s(pThisBinding->wszBuffer);
						pThisBinding = pThisBinding->sNext;
						result.BinSize = wcstod(pThisBinding->wszBuffer, NULL);
					}

					while (pFirstBinding)
					{
						pThisBinding = pFirstBinding->sNext;
						free(pFirstBinding->wszBuffer);
						free(pFirstBinding);
						pFirstBinding = pThisBinding;
					}
				} 
				break;
			}

		case SQL_ERROR:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				break;
			}

		default:
			fwprintf(stderr, L"Unexpected return code %hd!\n", retCode);

		}
		TRYODBC(_sqlConnection.hStmt,
			SQL_HANDLE_STMT,
			SQLFreeStmt(_sqlConnection.hStmt, SQL_CLOSE));

		//Disconnect();

		return result;

Exit:
		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
		Disconnect();
	};

	std::vector<ITEM> GetItems(int runId) {
		RETCODE     retCode;
		SQLSMALLINT sNumResults;
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         qRetCode = SQL_SUCCESS;
		std::vector<ITEM> results;

		//SQL_CONNECTION sqlConnection = Connect();
		std::wstring s = std::wstring(L"SELECT * FROM Items WHERE ScenarioId=");
		s += std::wstring(std::to_wstring(runId));
		WCHAR* query = const_cast<wchar_t*>(s.c_str());

		retCode = SQLExecDirect(_sqlConnection.hStmt, query, SQL_NTS);

		switch(retCode)
		{
		case SQL_SUCCESS_WITH_INFO:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				// fall through
			}
		case SQL_SUCCESS:
			{
				// If this is a row-returning query, display
				// results
				TRYODBC(_sqlConnection.hStmt,
					SQL_HANDLE_STMT,
					SQLNumResultCols(_sqlConnection.hStmt,&sNumResults));

				if (sNumResults > 0)
				{
					// Allocate memory for each column 
					AllocateBindings(_sqlConnection.hStmt, sNumResults, &pFirstBinding, &cDisplaySize);

					// Fetch and display the data
					bool fNoData = false;

					do {
						// Fetch a row
						TRYODBC(_sqlConnection.hStmt, SQL_HANDLE_STMT, qRetCode = SQLFetch(_sqlConnection.hStmt));

						if (qRetCode == SQL_NO_DATA_FOUND)
						{
							fNoData = true;
						}
						else
						{
							ITEM row = ITEM();
							pThisBinding = pFirstBinding;
							row.ItemId = wcstol(pThisBinding->wszBuffer, NULL, 10);
							pThisBinding = pThisBinding->sNext;
							row.RunId = wcstol(pThisBinding->wszBuffer, NULL, 10);
							pThisBinding = pThisBinding->sNext;
							row.Label = ws2s(pThisBinding->wszBuffer);
							pThisBinding = pThisBinding->sNext;
							row.Quantity = wcstol(pThisBinding->wszBuffer, NULL, 10);
							pThisBinding = pThisBinding->sNext;
							row.Size = wcstod(pThisBinding->wszBuffer, NULL);

							results.push_back(row);
						}
					} while (!fNoData);

					while (pFirstBinding)
					{
						pThisBinding = pFirstBinding->sNext;
						free(pFirstBinding->wszBuffer);
						free(pFirstBinding);
						pFirstBinding = pThisBinding;
					}
				} 
				break;
			}

		case SQL_ERROR:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				break;
			}

		default:
			fwprintf(stderr, L"Unexpected return code %hd!\n", retCode);

		}
		TRYODBC(_sqlConnection.hStmt,
			SQL_HANDLE_STMT,
			SQLFreeStmt(_sqlConnection.hStmt, SQL_CLOSE));

		//Disconnect();

		return results;

Exit:
		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
		Disconnect();
	};

	RUN InsertRun(int scenarioId){
		RETCODE     retCode;
		SQLSMALLINT sNumResults;
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         qRetCode = SQL_SUCCESS;
		RUN result;

		result.ScenarioId = scenarioId;

		//SQL_CONNECTION sqlConnection = Connect();
		std::wstring s = std::wstring(L"SET NOCOUNT ON; INSERT INTO runs(scenarioId) VALUES(");
		s += std::wstring(std::to_wstring(scenarioId));
		s += std::wstring(L");SELECT @@IDENTITY;");

		WCHAR* query = const_cast<wchar_t*>(s.c_str());

		retCode = SQLExecDirect(_sqlConnection.hStmt, query, SQL_NTS);

		switch(retCode)
		{
		case SQL_SUCCESS_WITH_INFO:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				// fall through
			}
		case SQL_SUCCESS:
			{

				// If this is a row-returning query, display
				// results
				TRYODBC(_sqlConnection.hStmt,
					SQL_HANDLE_STMT,
					SQLNumResultCols(_sqlConnection.hStmt,&sNumResults));

				
				if (sNumResults > 0)
				{
					// Allocate memory for each column 
					AllocateBindings(_sqlConnection.hStmt, sNumResults, &pFirstBinding, &cDisplaySize);

					// Fetch and display the data
					bool fNoData = false;

					// Fetch a row
					TRYODBC(_sqlConnection.hStmt, SQL_HANDLE_STMT, qRetCode = SQLFetch(_sqlConnection.hStmt));

					if (qRetCode == SQL_NO_DATA_FOUND)
					{
						fNoData = true;
					}
					else
					{
						pThisBinding = pFirstBinding;
						result.RunId = wcstol(pThisBinding->wszBuffer, NULL, 10);
					}

					while (pFirstBinding)
					{
						pThisBinding = pFirstBinding->sNext;
						free(pFirstBinding->wszBuffer);
						free(pFirstBinding);
						pFirstBinding = pThisBinding;
					}
				} 
				break;
			}

		case SQL_ERROR:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				break;
			}

		default:
			fwprintf(stderr, L"Unexpected return code %hd!\n", retCode);

		}
		TRYODBC(_sqlConnection.hStmt,
			SQL_HANDLE_STMT,
			SQLFreeStmt(_sqlConnection.hStmt, SQL_CLOSE));

		//Disconnect();

		return result;

Exit:
		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
		Disconnect();
	}

	GENERATION InsertGeneration(GENERATION generation){
		RETCODE     retCode;
		SQLSMALLINT sNumResults;
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         qRetCode = SQL_SUCCESS;
		GENERATION result;

		result.RunId = generation.RunId;
		result.Number = generation.Number;

		//SQL_CONNECTION sqlConnection = Connect();
		std::wstring s = std::wstring(L"SET NOCOUNT ON; INSERT INTO generations(runId,number) VALUES(");
		s += std::wstring(std::to_wstring(generation.RunId));
		s += std::wstring(L",");
		s += std::wstring(std::to_wstring(generation.Number));
		s += std::wstring(L");SELECT @@IDENTITY;");

		WCHAR* query = const_cast<wchar_t*>(s.c_str());

		retCode = SQLExecDirect(_sqlConnection.hStmt, query, SQL_NTS);

		switch(retCode)
		{
		case SQL_SUCCESS_WITH_INFO:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				// fall through
			}
		case SQL_SUCCESS:
			{

				// If this is a row-returning query, display
				// results
				TRYODBC(_sqlConnection.hStmt,
					SQL_HANDLE_STMT,
					SQLNumResultCols(_sqlConnection.hStmt,&sNumResults));

				
				if (sNumResults > 0)
				{
					// Allocate memory for each column 
					AllocateBindings(_sqlConnection.hStmt, sNumResults, &pFirstBinding, &cDisplaySize);

					// Fetch and display the data
					bool fNoData = false;

					// Fetch a row
					TRYODBC(_sqlConnection.hStmt, SQL_HANDLE_STMT, qRetCode = SQLFetch(_sqlConnection.hStmt));

					if (qRetCode == SQL_NO_DATA_FOUND)
					{
						fNoData = true;
					}
					else
					{
						pThisBinding = pFirstBinding;
						result.GenerationId = wcstol(pThisBinding->wszBuffer, NULL, 10);
					}

					while (pFirstBinding)
					{
						pThisBinding = pFirstBinding->sNext;
						free(pFirstBinding->wszBuffer);
						free(pFirstBinding);
						pFirstBinding = pThisBinding;
					}
				} 
				break;
			}

		case SQL_ERROR:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				break;
			}

		default:
			fwprintf(stderr, L"Unexpected return code %hd!\n", retCode);

		}
		TRYODBC(_sqlConnection.hStmt,
			SQL_HANDLE_STMT,
			SQLFreeStmt(_sqlConnection.hStmt, SQL_CLOSE));

		//Disconnect();

		return result;

Exit:
		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
		Disconnect();
	}

	POPULATION InsertPopulation(POPULATION population){
		RETCODE     retCode;
		SQLSMALLINT sNumResults;
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         qRetCode = SQL_SUCCESS;
		POPULATION result;

		result.GenerationId = population.GenerationId;
		result.Number = population.Number;
		result.Fitness = population.Fitness;
		result.BinCount = population.BinCount;

		//SQL_CONNECTION sqlConnection = Connect();
		std::wstring s = std::wstring(L"SET NOCOUNT ON; INSERT INTO populations(generationId,number,fitness,binCount) VALUES(");
		s += std::wstring(std::to_wstring(population.GenerationId));
		s += std::wstring(L",");
		s += std::wstring(std::to_wstring(population.Number));
		s += std::wstring(L",");
		s += std::wstring(std::to_wstring(population.Fitness));
		s += std::wstring(L",");
		s += std::wstring(std::to_wstring(population.BinCount));
		s += std::wstring(L");SELECT @@IDENTITY;");

		WCHAR* query = const_cast<wchar_t*>(s.c_str());

		retCode = SQLExecDirect(_sqlConnection.hStmt, query, SQL_NTS);

		switch(retCode)
		{
		case SQL_SUCCESS_WITH_INFO:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				// fall through
			}
		case SQL_SUCCESS:
			{

				// If this is a row-returning query, display
				// results
				TRYODBC(_sqlConnection.hStmt,
					SQL_HANDLE_STMT,
					SQLNumResultCols(_sqlConnection.hStmt,&sNumResults));

				
				if (sNumResults > 0)
				{
					// Allocate memory for each column 
					AllocateBindings(_sqlConnection.hStmt, sNumResults, &pFirstBinding, &cDisplaySize);

					// Fetch and display the data
					bool fNoData = false;

					// Fetch a row
					TRYODBC(_sqlConnection.hStmt, SQL_HANDLE_STMT, qRetCode = SQLFetch(_sqlConnection.hStmt));

					if (qRetCode == SQL_NO_DATA_FOUND)
					{
						fNoData = true;
					}
					else
					{
						pThisBinding = pFirstBinding;
						result.PopulationId = wcstol(pThisBinding->wszBuffer, NULL, 10);
					}

					while (pFirstBinding)
					{
						pThisBinding = pFirstBinding->sNext;
						free(pFirstBinding->wszBuffer);
						free(pFirstBinding);
						pFirstBinding = pThisBinding;
					}
				} 
				break;
			}

		case SQL_ERROR:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				break;
			}

		default:
			fwprintf(stderr, L"Unexpected return code %hd!\n", retCode);

		}
		TRYODBC(_sqlConnection.hStmt,
			SQL_HANDLE_STMT,
			SQLFreeStmt(_sqlConnection.hStmt, SQL_CLOSE));

		//Disconnect();

		return result;

Exit:
		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
		Disconnect();
	}

	BIN InsertBin(BIN bin){
		RETCODE     retCode;
		SQLSMALLINT sNumResults;
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         qRetCode = SQL_SUCCESS;
		BIN result;

		result.PopulationId = bin.PopulationId;
		result.Filled = bin.Filled;
		result.Capacity = bin.Capacity;

		//SQL_CONNECTION sqlConnection = Connect();
		std::wstring s = std::wstring(L"SET NOCOUNT ON; INSERT INTO bins(populationId,filled,capacity) VALUES(");
		s += std::wstring(std::to_wstring(bin.PopulationId));
		s += std::wstring(L",");
		s += std::wstring(std::to_wstring(bin.Filled));
		s += std::wstring(L",");
		s += std::wstring(std::to_wstring(bin.Capacity));
		s += std::wstring(L");SELECT @@IDENTITY;");

		WCHAR* query = const_cast<wchar_t*>(s.c_str());

		retCode = SQLExecDirect(_sqlConnection.hStmt, query, SQL_NTS);

		switch(retCode)
		{
		case SQL_SUCCESS_WITH_INFO:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				// fall through
			}
		case SQL_SUCCESS:
			{

				// If this is a row-returning query, display
				// results
				TRYODBC(_sqlConnection.hStmt,
					SQL_HANDLE_STMT,
					SQLNumResultCols(_sqlConnection.hStmt,&sNumResults));

				
				if (sNumResults > 0)
				{
					// Allocate memory for each column 
					AllocateBindings(_sqlConnection.hStmt, sNumResults, &pFirstBinding, &cDisplaySize);

					// Fetch and display the data
					bool fNoData = false;

					// Fetch a row
					TRYODBC(_sqlConnection.hStmt, SQL_HANDLE_STMT, qRetCode = SQLFetch(_sqlConnection.hStmt));

					if (qRetCode == SQL_NO_DATA_FOUND)
					{
						fNoData = true;
					}
					else
					{
						pThisBinding = pFirstBinding;
						result.BinId = wcstol(pThisBinding->wszBuffer, NULL, 10);
					}

					while (pFirstBinding)
					{
						pThisBinding = pFirstBinding->sNext;
						free(pFirstBinding->wszBuffer);
						free(pFirstBinding);
						pFirstBinding = pThisBinding;
					}
				} 
				break;
			}

		case SQL_ERROR:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				break;
			}

		default:
			fwprintf(stderr, L"Unexpected return code %hd!\n", retCode);

		}
		TRYODBC(_sqlConnection.hStmt,
			SQL_HANDLE_STMT,
			SQLFreeStmt(_sqlConnection.hStmt, SQL_CLOSE));

		//Disconnect();

		return result;

Exit:
		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
		Disconnect();
	}

	BINITEM InsertBinItem(BINITEM binItem){
		RETCODE     retCode;
		SQLSMALLINT sNumResults;
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         qRetCode = SQL_SUCCESS;
		BINITEM result;

		result.BinId = binItem.BinId;
		result.Label = binItem.Label;
		result.Size = binItem.Size;

		//SQL_CONNECTION sqlConnection = Connect();
		std::wstring s = std::wstring(L"SET NOCOUNT ON; INSERT INTO binItems(binId,label,size) VALUES(");
		s += std::wstring(std::to_wstring(binItem.BinId));
		s += std::wstring(L",'");
		s += std::wstring(s2ws(binItem.Label));
		s += std::wstring(L"',");
		s += std::wstring(std::to_wstring(binItem.Size));
		s += std::wstring(L");SELECT @@IDENTITY;");

		WCHAR* query = const_cast<wchar_t*>(s.c_str());

		retCode = SQLExecDirect(_sqlConnection.hStmt, query, SQL_NTS);

		switch(retCode)
		{
		case SQL_SUCCESS_WITH_INFO:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				// fall through
			}
		case SQL_SUCCESS:
			{

				// If this is a row-returning query, display
				// results
				TRYODBC(_sqlConnection.hStmt,
					SQL_HANDLE_STMT,
					SQLNumResultCols(_sqlConnection.hStmt,&sNumResults));

				
				if (sNumResults > 0)
				{
					// Allocate memory for each column 
					AllocateBindings(_sqlConnection.hStmt, sNumResults, &pFirstBinding, &cDisplaySize);

					// Fetch and display the data
					bool fNoData = false;

					// Fetch a row
					TRYODBC(_sqlConnection.hStmt, SQL_HANDLE_STMT, qRetCode = SQLFetch(_sqlConnection.hStmt));

					if (qRetCode == SQL_NO_DATA_FOUND)
					{
						fNoData = true;
					}
					else
					{
						pThisBinding = pFirstBinding;
						result.BinItemId = wcstol(pThisBinding->wszBuffer, NULL, 10);
					}

					while (pFirstBinding)
					{
						pThisBinding = pFirstBinding->sNext;
						free(pFirstBinding->wszBuffer);
						free(pFirstBinding);
						pFirstBinding = pThisBinding;
					}
				} 
				break;
			}

		case SQL_ERROR:
			{
				HandleDiagnosticRecord(_sqlConnection.hStmt, SQL_HANDLE_STMT, retCode);
				break;
			}

		default:
			fwprintf(stderr, L"Unexpected return code %hd!\n", retCode);

		}
		TRYODBC(_sqlConnection.hStmt,
			SQL_HANDLE_STMT,
			SQLFreeStmt(_sqlConnection.hStmt, SQL_CLOSE));

		//Disconnect();

		return result;

Exit:
		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
		Disconnect();
	}

	/************************************************************************
	/* DisplayResults: display results of a select query
	/*
	/* Parameters:
	/*      hStmt      ODBC statement handle
	/*      cCols      Count of columns
	/************************************************************************/

	void DisplayResults(HSTMT       hStmt,
		SQLSMALLINT cCols)
	{
		BINDING         *pFirstBinding, *pThisBinding;          
		SQLSMALLINT     cDisplaySize;
		RETCODE         RetCode = SQL_SUCCESS;
		int             iCount = 0;

		// Allocate memory for each column 

		AllocateBindings(hStmt, cCols, &pFirstBinding, &cDisplaySize);

		// Set the display mode and write the titles

		DisplayTitles(hStmt, cDisplaySize+1, pFirstBinding);


		// Fetch and display the data

		bool fNoData = false;

		do {
			// Fetch a row

			if (iCount++ >= gHeight - 2)
			{
				int     nInputChar;
				bool    fEnterReceived = false;

				while(!fEnterReceived)
				{   
					wprintf(L"              ");
					SetConsole(cDisplaySize+2, TRUE);
					wprintf(L"   Press ENTER to continue, Q to quit (height:%hd)", gHeight);
					SetConsole(cDisplaySize+2, FALSE);

					nInputChar = _getch();
					wprintf(L"\n");
					if ((nInputChar == 'Q') || (nInputChar == 'q'))
					{
						goto Exit;
					}
					else if ('\r' == nInputChar)
					{
						fEnterReceived = true;
					}
					// else loop back to display prompt again
				}

				iCount = 1;
				DisplayTitles(hStmt, cDisplaySize+1, pFirstBinding);
			}

			TRYODBC(hStmt, SQL_HANDLE_STMT, RetCode = SQLFetch(hStmt));

			if (RetCode == SQL_NO_DATA_FOUND)
			{
				fNoData = true;
			}
			else
			{

				// Display the data.   Ignore truncations

				for (pThisBinding = pFirstBinding;
					pThisBinding;
					pThisBinding = pThisBinding->sNext)
				{
					if (pThisBinding->indPtr != SQL_NULL_DATA)
					{
						wprintf(pThisBinding->fChar ? DISPLAY_FORMAT_C:DISPLAY_FORMAT,
							PIPE,
							pThisBinding->cDisplaySize,
							pThisBinding->cDisplaySize,
							pThisBinding->wszBuffer);
					} 
					else
					{
						wprintf(DISPLAY_FORMAT_C,
							PIPE,
							pThisBinding->cDisplaySize,
							pThisBinding->cDisplaySize,
							L"<NULL>");
					}
				}
				wprintf(L" %c\n",PIPE);
			}
		} while (!fNoData);

		SetConsole(cDisplaySize+2, TRUE);
		wprintf(L"%*.*s", cDisplaySize+2, cDisplaySize+2, L" ");
		SetConsole(cDisplaySize+2, FALSE);
		wprintf(L"\n");

Exit:
		// Clean up the allocated buffers

		while (pFirstBinding)
		{
			pThisBinding = pFirstBinding->sNext;
			free(pFirstBinding->wszBuffer);
			free(pFirstBinding);
			pFirstBinding = pThisBinding;
		}
	}

	/************************************************************************
	/* AllocateBindings:  Get column information and allocate bindings
	/* for each column.  
	/*
	/* Parameters:
	/*      hStmt      Statement handle
	/*      cCols       Number of columns in the result set
	/*      *lppBinding Binding pointer (returned)
	/*      lpDisplay   Display size of one line
	/************************************************************************/

	void AllocateBindings(HSTMT         hStmt,
		SQLSMALLINT   cCols,
		BINDING       **ppBinding,
		SQLSMALLINT   *pDisplay)
	{
		SQLSMALLINT     iCol;
		BINDING         *pThisBinding, *pLastBinding = NULL;
		SQLLEN          cchDisplay, ssType;
		SQLSMALLINT     cchColumnNameLength;

		*pDisplay = 0;

		for (iCol = 1; iCol <= cCols; iCol++)
		{
			pThisBinding = (BINDING *)(malloc(sizeof(BINDING)));
			if (!(pThisBinding))
			{
				fwprintf(stderr, L"Out of memory!\n");
				exit(-100);
			}

			if (iCol == 1)
			{
				*ppBinding = pThisBinding;
			}
			else
			{
				pLastBinding->sNext = pThisBinding;
			}
			pLastBinding = pThisBinding;


			// Figure out the display length of the column (we will
			// bind to char since we are only displaying data, in general
			// you should bind to the appropriate C type if you are going
			// to manipulate data since it is much faster...)

			TRYODBC(hStmt,
				SQL_HANDLE_STMT,
				SQLColAttribute(hStmt,
				iCol,
				SQL_DESC_DISPLAY_SIZE,
				NULL,
				0,
				NULL,
				&cchDisplay));


			// Figure out if this is a character or numeric column; this is
			// used to determine if we want to display the data left- or right-
			// aligned.

			// SQL_DESC_CONCISE_TYPE maps to the 1.x SQL_COLUMN_TYPE. 
			// This is what you must use if you want to work
			// against a 2.x driver.

			TRYODBC(hStmt,
				SQL_HANDLE_STMT,
				SQLColAttribute(hStmt,
				iCol,
				SQL_DESC_CONCISE_TYPE,
				NULL,
				0,
				NULL,
				&ssType));


			pThisBinding->fChar = (ssType == SQL_CHAR ||
				ssType == SQL_VARCHAR ||
				ssType == SQL_LONGVARCHAR);

			pThisBinding->sNext = NULL;

			// Arbitrary limit on display size
			if (cchDisplay > DISPLAY_MAX)
				cchDisplay = DISPLAY_MAX;

			// Allocate a buffer big enough to hold the text representation
			// of the data.  Add one character for the null terminator

			pThisBinding->wszBuffer = (WCHAR *)malloc((cchDisplay+1) * sizeof(WCHAR));

			if (!(pThisBinding->wszBuffer))
			{
				fwprintf(stderr, L"Out of memory!\n");
				exit(-100);
			}

			// Map this buffer to the driver's buffer.   At Fetch time,
			// the driver will fill in this data.  Note that the size is 
			// count of bytes (for Unicode).  All ODBC functions that take
			// SQLPOINTER use count of bytes; all functions that take only
			// strings use count of characters.

			TRYODBC(hStmt,
				SQL_HANDLE_STMT,
				SQLBindCol(hStmt,
				iCol,
				SQL_C_TCHAR,
				(SQLPOINTER) pThisBinding->wszBuffer,
				(cchDisplay + 1) * sizeof(WCHAR),
				&pThisBinding->indPtr));


			// Now set the display size that we will use to display
			// the data.   Figure out the length of the column name

			TRYODBC(hStmt,
				SQL_HANDLE_STMT,
				SQLColAttribute(hStmt,
				iCol,
				SQL_DESC_NAME,
				NULL,
				0,
				&cchColumnNameLength,
				NULL));

			pThisBinding->cDisplaySize = max((SQLSMALLINT)cchDisplay, cchColumnNameLength);
			if (pThisBinding->cDisplaySize < NULL_SIZE)
				pThisBinding->cDisplaySize = NULL_SIZE;

			*pDisplay += pThisBinding->cDisplaySize + DISPLAY_FORMAT_EXTRA;

		}

		return;

Exit:

		exit(-1);

		return;
	}


	/************************************************************************
	/* DisplayTitles: print the titles of all the columns and set the 
	/*                shell window's width
	/*
	/* Parameters:
	/*      hStmt          Statement handle
	/*      cDisplaySize   Total display size
	/*      pBinding        list of binding information
	/************************************************************************/

	void DisplayTitles(HSTMT     hStmt,
		DWORD     cDisplaySize,
		BINDING   *pBinding)
	{
		WCHAR           wszTitle[DISPLAY_MAX];
		SQLSMALLINT     iCol = 1;

		SetConsole(cDisplaySize+2, TRUE);

		for (; pBinding; pBinding = pBinding->sNext)
		{
			TRYODBC(hStmt,
				SQL_HANDLE_STMT,
				SQLColAttribute(hStmt,
				iCol++,
				SQL_DESC_NAME,
				wszTitle,
				sizeof(wszTitle), // Note count of bytes!
				NULL,
				NULL));

			wprintf(DISPLAY_FORMAT_C,
				PIPE,
				pBinding->cDisplaySize,
				pBinding->cDisplaySize,
				wszTitle);
		}

Exit:

		wprintf(L" %c", PIPE);
		SetConsole(cDisplaySize+2, FALSE);
		wprintf(L"\n");

	}


	/************************************************************************
	/* SetConsole: sets console display size and video mode
	/*
	/*  Parameters
	/*      siDisplaySize   Console display size
	/*      fInvert         Invert video?
	/************************************************************************/

	void SetConsole(DWORD dwDisplaySize,
		BOOL  fInvert)
	{
		HANDLE                          hConsole;
		CONSOLE_SCREEN_BUFFER_INFO      csbInfo;

		// Reset the console screen buffer size if necessary

		hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

		if (hConsole != INVALID_HANDLE_VALUE)
		{
			if (GetConsoleScreenBufferInfo(hConsole, &csbInfo))
			{
				if (csbInfo.dwSize.X <  (SHORT) dwDisplaySize)
				{
					csbInfo.dwSize.X =  (SHORT) dwDisplaySize;
					SetConsoleScreenBufferSize(hConsole, csbInfo.dwSize);
				}

				gHeight = csbInfo.dwSize.Y;
			}

			if (fInvert)
			{
				SetConsoleTextAttribute(hConsole, (WORD)(csbInfo.wAttributes | BACKGROUND_BLUE));
			}
			else
			{
				SetConsoleTextAttribute(hConsole, (WORD)(csbInfo.wAttributes & ~(BACKGROUND_BLUE)));
			}
		}
	}


	/************************************************************************
	/* HandleDiagnosticRecord : display error/warning information
	/*
	/* Parameters:
	/*      hHandle     ODBC handle
	/*      hType       Type of handle (HANDLE_STMT, HANDLE_ENV, HANDLE_DBC)
	/*      RetCode     Return code of failing command
	/************************************************************************/

	void HandleDiagnosticRecord (SQLHANDLE      hHandle,    
		SQLSMALLINT    hType,  
		RETCODE        RetCode)
	{
		SQLSMALLINT iRec = 0;
		SQLINTEGER  iError;
		WCHAR       wszMessage[1000];
		WCHAR       wszState[SQL_SQLSTATE_SIZE+1];


		if (RetCode == SQL_INVALID_HANDLE)
		{
			fwprintf(stderr, L"Invalid handle!\n");
			return;
		}

		while (SQLGetDiagRec(hType,
			hHandle,
			++iRec,
			wszState,
			&iError,
			wszMessage,
			(SQLSMALLINT)(sizeof(wszMessage) / sizeof(WCHAR)),
			(SQLSMALLINT *)NULL) == SQL_SUCCESS)
		{
			// Hide data truncated..
			if (wcsncmp(wszState, L"01004", 5))
			{
				fwprintf(stderr, L"[%5.5s] %s (%d)\n", wszState, wszMessage, iError);
			}
		}

	}
}

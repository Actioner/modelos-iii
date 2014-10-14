using BE.ModelosIII.Tasks.Commands.Utility;
using SharpArch.Domain.Commands;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Linq;
using System.Xml.Schema;

namespace BE.ModelosIII.Mvc.Models.Screen
{
    public class ScreenMapModel : CommandBase
    {
        private bool _isValidXml;

        public List<RowModel> Rows { get; set; }
        public string Error { get; set; }

        public bool ExistRow(string row)
        {
           return Rows.Any(r => r.Name == row);
        }

        public bool HasSeat(string row, int seat)
        {
            if (Rows == null)
            {
                return false;
                
            }
            var selrow = Rows.FirstOrDefault(r => r.Name == row);

            return selrow != null && selrow.Seats.Any(s => s.Number == seat);
        }

        public void ValidateSchema(string filename, string xsd)
        {
            _isValidXml = true;

            var settings = new XmlReaderSettings
                               {
                                   ValidationType = ValidationType.Schema,
                                   ValidationFlags = 
                                        XmlSchemaValidationFlags.ProcessInlineSchema |
                                        XmlSchemaValidationFlags.ProcessSchemaLocation |
                                        XmlSchemaValidationFlags.ReportValidationWarnings
                               };
            settings.ValidationEventHandler += ValidationCallBack;

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(xsd, settings);

            // Parse the file. 
            while (reader.Read())
            {
            }
        }

        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if(args.Severity == XmlSeverityType.Error)
            {
                _isValidXml = false;
            }
        }

        public void InitRowsFromXml(XmlDocument xml)
        {
            if (_isValidXml == false)
            {
                Error = "Formato del XML invalido";
            }

            try
            {
                Rows = new List<RowModel>();

                var xmlrows = xml.GetElementsByTagName("fila");

                foreach (XmlNode xmlrow in xmlrows)
                {
                    if (xmlrow.Attributes == null)
                    {
                        continue;
                    }

                    var row = new RowModel
                                  {
                                      Name = xmlrow.Attributes["nombre"].Value.ToUpper()
                                  };

                    if (ExistRow(row.Name))
                    {
                        throw new Exception("La fila " + row.Name + " se encuentra repetida");
                    }

                    var xmlseats = xmlrow.ChildNodes;

                    foreach (XmlNode xmlseat in xmlseats)
                    {
                        if (xmlseat.Name == "asiento")
                        {
                            int seat = int.Parse(xmlseat.InnerText);
                            AddSeatToRow(seat, row);
                        }

                        if (xmlseat.Name == "segmento" && xmlseat.Attributes != null)
                        {
                            int start = int.Parse(xmlseat.Attributes["desde"].Value);
                            int end = int.Parse(xmlseat.Attributes["hasta"].Value);
                            for (int i = start; i <= end; i++)
                            {
                                AddSeatToRow(i, row);
                            }
                        }
                    }

                    Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Rows = null;
                Error = "Error: "+ ex.Message;
            }
        }

        private void AddSeatToRow(int seat, RowModel row)
        {
            if (row.Seats.Any(s => s.Number == seat))
            {
                throw new Exception("El asiento " + row.Name + seat + " se encuentra repetido");
            }

            row.Seats.Add(new SeatModel
                              {
                                  Number = seat
                              });
        }
    }
}

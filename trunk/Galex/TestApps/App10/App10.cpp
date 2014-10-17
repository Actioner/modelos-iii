

#include "..\..\source\Initialization.h"
#include "..\..\Problems\BPP.h"
#include "..\..\source\Crossovers.h"
#include "..\..\source\SimpleStub.h"
#include "..\..\source\Matings.h"
#include "..\..\source\PopulationStatistics.h"
#include "..\..\source\StopCriteria.h"

#include "..\..\source\Selections.h"
#include "..\..\source\Couplings.h"
#include "..\..\source\Replacements.h"
#include "..\..\source\FitnessSharing.h"
#include "..\..\source\Scalings.h"
#include "..\..\source\StopCriteria.h"
#include "..\..\SqlConnection\ModDb.cpp"

#include <iostream>
#include <iomanip>
#include <vector>

enum WorkflowDataIDs
{
	WDID_POPULATION,
	WDID_POPULATION_STATS
};

Problems::BPP::BinInitializator initializator;
Problems::BPP::BinCrossoverOperation crossover;
Problems::BPP::BinMutationOperation mutation;
Problems::BPP::BinFitnessOperation fitnessOperation;
Fitness::Comparators::GaSimpleComparator fitnessComparator;

Population::GaCombinedFitnessOperation populationFitnessOperation( &fitnessOperation );

Chromosome::MatingOperations::GaBasicMatingOperation mating;

Population::GaPopulationSizeTracker sizeTracker;
Population::GaRawFitnessTracker rawTracker;
Population::GaScaledFitnessTracker scaledTracker;

Population::SelectionOperations::GaTournamentSelection selection;
Population::CouplingOperations::GaSimpleCoupling coupling;
Population::ReplacementOperations::GaWorstReplacement replacement;
Population::ScalingOperations::GaNoScaling scaling;

Algorithm::StopCriteria::GaStatsChangesCriterion stopCriterion;

int scenarioId;
int runId;

void GACALL MyHandler(int id, Common::Observing::GaEventData& data)
{
	const Population::GaPopulation& population = ( (Population::GaPopulationEventData&)data ).GetPopulation();

	const Problems::BPP::BinChromosome& chromosome = (const Problems::BPP::BinChromosome&)*population[ 0 ].GetChromosome();
	const Problems::BPP::BinConfigBlock& ccb = (const Problems::BPP::BinConfigBlock&)*chromosome.GetConfigBlock();

	const Statistics::GaStatistics& stats = population.GetStatistics();

	if( stats.GetCurrentGeneration() != 1 && !stats.GetValue<Fitness::GaFitness>( Population::GADV_BEST_FITNESS ).IsChanged( 2 ) )
		return;

	const Common::Data::GaSingleDimensionArray<Problems::BPP::BinConfigBlock::Item>& items = ccb.GetItems();

	ModDb::GENERATION generation = ModDb::GENERATION();

	generation.RunId = runId;
	generation.Number = stats.GetCurrentGeneration();
	generation = ModDb::InsertGeneration(generation);

	for( int i = 0; i < population.GetCount(); i++ )
	{
		const Problems::BPP::BinChromosome& chromosome = (const Problems::BPP::BinChromosome&)*population[ i ].GetChromosome();

		const Problems::BPP::BinFitness f = (const Problems::BPP::BinFitness&)population[ i ].GetFitness( Population::GaChromosomeStorage::GAFT_RAW );
		ModDb::POPULATION population = ModDb::POPULATION();
		population.GenerationId = generation.GenerationId;
		population.Number = i + 1;
		population.Fitness = f.GetValue();
		population.BinCount = chromosome.GetStructure().GetCount();
		population = ModDb::InsertPopulation(population);

		for( const Problems::BPP::BinList::GaNodeType* bNode = chromosome.GetGenes().GetHead(); bNode != NULL; bNode = bNode->GetNext() )
		{
			Problems::BPP::Bin bBin = bNode->GetValue();

			ModDb::BIN bin = ModDb::BIN();
			bin.PopulationId = population.PopulationId;
			bin.Filled = bBin.GetFill();
			bin.Capacity = bBin.GetCapacity();
			bin = ModDb::InsertBin(bin);

			for( const Problems::BPP::Bin::ItemList::GaNodeType* iNode = bBin.GetItems().GetHead(); iNode != NULL; iNode = iNode->GetNext() ){
				ModDb::BINITEM binItem = ModDb::BINITEM();
				binItem.BinId = bin.BinId;
				binItem.Label = items[iNode->GetValue()]._label;
				binItem.Size = items[iNode->GetValue()]._size;
				ModDb::InsertBinItem(binItem);
			}
		}
	}
}

Common::Observing::GaNonmemberEventHandler newGenHandler( MyHandler );

int main(int argc, const char* argv[])
{
	scenarioId = 1;
	ModDb::Connect();

	ModDb::SCENARIO scenario = ModDb::GetScenario(scenarioId);
	std::vector<ModDb::ITEM> dbItems = ModDb::GetItems(scenario.ScenarioId);

	GaInitialize();

	{
		int itemCount = 0;
		for (std::vector<ModDb::ITEM>::iterator it = dbItems.begin(); it != dbItems.end(); ++it)
			itemCount += it->Quantity;

		Common::Data::GaSingleDimensionArray<Problems::BPP::BinConfigBlock::Item> items(itemCount);

		int index = 0;
		for (std::vector<ModDb::ITEM>::iterator it = dbItems.begin(); it != dbItems.end(); ++it) {
			for (int i = 0; i < it->Quantity; i++) {
				items[index++] = Problems::BPP::BinConfigBlock::Item(it->Label, it->Size);
			}
		}

		Chromosome::GaMatingConfig matingConfiguration(
			Chromosome::GaCrossoverSetup( &crossover, &Chromosome::GaCrossoverParams( 1.0f, 2 ), NULL ),
			Chromosome::GaMutationSetup( &mutation, &Chromosome::GaMutationSizeParams( 0.66f, true, 2L ), NULL ) );

		Chromosome::GaInitializatorSetup initializatorSetup( &initializator, NULL, &Chromosome::GaInitializatorConfig(
			&Problems::BPP::BinConfigBlock( items, scenario.BinSize ) ) );
		Fitness::GaFitnessComparatorSetup fitnessComparatorSetup( &fitnessComparator,
			&Fitness::Comparators::GaSimpleComparatorParams( Fitness::Comparators::GACT_MAXIMIZE_ALL ), NULL );

		Algorithm::Stubs::GaSimpleGAStub::GaStatTrackersCollection trackers;
		trackers[ Population::GaPopulationSizeTracker::TRACKER_ID ] =  &sizeTracker;
		trackers[ Population::GaRawFitnessTracker::TRACKER_ID ] =  &rawTracker;
		trackers[ Population::GaScaledFitnessTracker::TRACKER_ID ] =  &scaledTracker;

		Population::GaSelectionSetup selectionSetup( &selection,
			&Population::SelectionOperations::GaTournamentSelectionParams( 2, -1, 2, 2, Population::SelectionOperations::GaTournamentSelectionParams::GATST_ROULETTE_WHEEL_SELECTION ),
			&Population::SelectionOperations::GaTournamentSelectionConfig( fitnessComparatorSetup, Chromosome::GaMatingSetup() ) );

		Population::GaCouplingSetup couplingSetup( &coupling, &Population::GaCouplingParams( 50, 1 ),
			&Population::GaCouplingConfig( Chromosome::GaMatingSetup( &mating, NULL, &matingConfiguration ) ) );

		Population::GaReplacementSetup replacementSetup( &replacement, &Population::GaReplacementParams( 50 ), &Population::GaReplacementConfig() );
		Population::GaScalingSetup scalingSetup( &scaling, NULL, &Population::GaScalingConfig() );

		Algorithm::Stubs::GaSimpleGAStub simpleGA( WDID_POPULATION, WDID_POPULATION_STATS,
			initializatorSetup,
			Population::GaPopulationFitnessOperationSetup( &populationFitnessOperation, &Problems::BPP::BinFitnessOperationParams( 2 ),
			&Fitness::GaFitnessOperationConfig( NULL ) ),
			fitnessComparatorSetup,
			Population::GaPopulationParams( 100, 0, Population::GaPopulationParams::GAPFO_FILL_ON_INIT ),
			trackers,
			Chromosome::GaMatingSetup(),
			selectionSetup,
			couplingSetup,
			replacementSetup,
			scalingSetup,
			Population::GaFitnessComparatorSortingCriteria( fitnessComparatorSetup, Population::GaChromosomeStorage::GAFT_RAW ) );
		simpleGA.SetBranchCount( 2 );

		Common::Workflows::GaWorkflow workflow( NULL );

		workflow.RemoveConnection( *workflow.GetFirstStep()->GetOutboundConnections().begin(), true );

		Common::Workflows::GaWorkflowBarrier* br1 = new Common::Workflows::GaWorkflowBarrier();
		simpleGA.Connect( workflow.GetFirstStep(), br1 );

		Common::Workflows::GaBranchGroup* bg1 = (Common::Workflows::GaBranchGroup*)workflow.ConnectSteps( br1, workflow.GetLastStep(), 0 );

		Algorithm::StopCriteria::GaStopCriterionStep* stopStep = new Algorithm::StopCriteria::GaStopCriterionStep(
			Algorithm::StopCriteria::GaStopCriterionSetup( &stopCriterion,
			&Algorithm::StopCriteria::GaStatsChangesCriterionParams(
			Population::GADV_BEST_FITNESS, 100), NULL ), workflow.GetWorkflowData(), WDID_POPULATION_STATS );

		Common::Workflows::GaBranchGroupTransition* bt1 = new Common::Workflows::GaBranchGroupTransition();

		bg1->GetBranchGroupFlow()->SetFirstStep( stopStep );
		bg1->GetBranchGroupFlow()->ConnectSteps( stopStep, bt1, 0 );
		workflow.ConnectSteps( bt1, simpleGA.GetStubFlow().GetFirstStep(), 1 );

		Common::Workflows::GaDataCache<Population::GaPopulation> population( workflow.GetWorkflowData(), WDID_POPULATION );

		population.GetData().GetEventManager().AddEventHandler( Population::GaPopulation::GAPE_NEW_GENERATION, &newGenHandler );

		ModDb::RUN run = ModDb::InsertRun(scenarioId);
		runId = run.RunId;

		workflow.Start();
		workflow.Wait();

	}

	ModDb::Disconnect();
	GaFinalize();

	//std::cin.get();
	return 0;
}

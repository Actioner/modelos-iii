﻿using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Scenario
{
    public class CreateScenarioHandler : ScenarioHandler<CreateScenarioCommand>
    {
        public CreateScenarioHandler(
                IRepository<Domain.Scenario> scenarioRepository,
                IMappingEngine mappingEngine)
            : base(scenarioRepository, mappingEngine)
        {
        }

        protected override Domain.Scenario MapCommandToScenario(CreateScenarioCommand command)
        {
            return MappingEngine.Map<Domain.Scenario>(command);
        }
    }
}

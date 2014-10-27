using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Infrastructure;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Scenario
{
    public abstract class ScenarioHandler<T> : ICommandHandler<T>
        where T : ScenarioCommand
    {
        protected IRepository<Domain.Scenario> ScenarioRepository;
        protected IMappingEngine MappingEngine;

        protected ScenarioHandler(
            IRepository<Domain.Scenario> scenarioRepository,
            IMappingEngine mappingEngine)
        {
            this.ScenarioRepository = scenarioRepository;
            this.MappingEngine = mappingEngine;
        }

        public virtual void Handle(T command)
        {
            var scenario = MapCommandToScenario(command);
            var result = ScenarioRepository.SaveOrUpdate(scenario);
            
            command.Id = result.Id;
        }

        protected abstract Domain.Scenario MapCommandToScenario(T command);
    }
}

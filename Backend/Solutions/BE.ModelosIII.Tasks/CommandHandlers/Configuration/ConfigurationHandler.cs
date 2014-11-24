using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Infrastructure;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using BE.ModelosIII.Tasks.Commands.Configuration;

namespace BE.ModelosIII.Tasks.CommandHandlers.Configuration
{
    public class ConfigurationHandler : ICommandHandler<ConfigurationCommand>
    {
        private readonly IRepository<Domain.Scenario> _scenarioRepository;
        private readonly IMappingEngine _mappingEngine;

        public ConfigurationHandler(
            IRepository<Domain.Scenario> scenarioRepository,
            IMappingEngine mappingEngine)
        {
            this._scenarioRepository = scenarioRepository;
            this._mappingEngine = mappingEngine;
        }

        public virtual void Handle(ConfigurationCommand command)
        {
            var scenario = MapCommandToScenario(command);
            var result = _scenarioRepository.SaveOrUpdate(scenario);
            
            command.ScenarioId = result.Id;
        }

        protected Domain.Scenario MapCommandToScenario(ConfigurationCommand command) 
        {
            return _mappingEngine.Map(command, _scenarioRepository.Get(command.ScenarioId));
        }
    }
}

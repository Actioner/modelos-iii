using System.Collections.Generic;
using AutoMapper;
using BE.ModelosIII.Tasks.Commands.Run;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Run
{
    public abstract class RunHandler<T> : ICommandHandler<T>
        where T : RunCommand
    {
        protected IRepository<Domain.Run> RunRepository;
        protected IMappingEngine MappingEngine;

        protected RunHandler(
            IRepository<Domain.Run> scenarioRepository,
            IMappingEngine mappingEngine)
        {
            this.RunRepository = scenarioRepository;
            this.MappingEngine = mappingEngine;
        }

        public virtual void Handle(T command)
        {
            var scenario = MapCommandToRun(command);
            var result = RunRepository.SaveOrUpdate(scenario);
            
            command.Id = result.Id;
        }

        protected abstract Domain.Run MapCommandToRun(T command);
    }
}

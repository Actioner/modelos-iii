using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Run;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Run
{
    public class CreateRunHandler : RunHandler<CreateRunCommand>
    {
        public CreateRunHandler(
                IRepository<Domain.Run> runRepository,
                IMappingEngine mappingEngine)
            : base(runRepository, mappingEngine)
        {
        }

        protected override Domain.Run MapCommandToRun(CreateRunCommand command)
        {
            return MappingEngine.Map<Domain.Run>(command);
        }
    }
}

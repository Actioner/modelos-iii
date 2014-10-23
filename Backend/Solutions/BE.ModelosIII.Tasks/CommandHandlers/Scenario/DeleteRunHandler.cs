using BE.ModelosIII.Tasks.Commands.Run;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Run
{
    public class DeleteRunHandler : ICommandHandler<DeleteRunCommand>
    {
        private readonly IRepository<Domain.Run> _runRepository;

        public DeleteRunHandler(
            IRepository<Domain.Run> runRepository)
        {
            _runRepository = runRepository;
        }

        public void Handle(DeleteRunCommand command)
        {
            var run = _runRepository.Get(command.Id);
            if (run != null)
            {
                _runRepository.Delete(run);
            }
        }
    }
}

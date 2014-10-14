using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Screening;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Screening
{
    public class DeleteScreeningHandler : ICommandHandler<DeleteScreeningCommand>
    {
        private readonly IScreeningRepository _screeningRepository;

        public DeleteScreeningHandler(
            IScreeningRepository screeningRepository)
        {
            _screeningRepository = screeningRepository;
        }

        public void Handle(DeleteScreeningCommand command)
        {
            var screening = _screeningRepository.Get(command.Id);
            if (screening != null)
            {
                _screeningRepository.Delete(screening);
            }
        }
    }
}

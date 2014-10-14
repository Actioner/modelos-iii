using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex.Screen
{
    public class DeleteScreenHandler : ICommandHandler<DeleteScreenCommand>
    {
        private readonly IScreenRepository _screenRepository;

        public DeleteScreenHandler(
            IScreenRepository screenRepository)
        {
            _screenRepository = screenRepository;
        }

        public void Handle(DeleteScreenCommand command)
        {
            var screen = _screenRepository.Get(command.Id);
            if (screen != null)
            {
                _screenRepository.Delete(screen);
            }
        }
    }
}

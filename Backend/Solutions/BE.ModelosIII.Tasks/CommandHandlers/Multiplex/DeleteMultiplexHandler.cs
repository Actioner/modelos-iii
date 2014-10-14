using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex
{
    public class DeleteMultiplexHandler : ICommandHandler<DeleteMultiplexCommand>
    {
        private readonly IMultiplexRepository _multiplexRepository;

        public DeleteMultiplexHandler(
            IMultiplexRepository multiplexRepository)
        {
            _multiplexRepository = multiplexRepository;
        }

        public void Handle(DeleteMultiplexCommand command)
        {
            var multiplex = _multiplexRepository.Get(command.Id);
            if (multiplex != null)
            {
                _multiplexRepository.Delete(multiplex);
            }
        }
    }
}

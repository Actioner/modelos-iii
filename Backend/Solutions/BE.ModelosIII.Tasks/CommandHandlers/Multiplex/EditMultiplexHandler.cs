using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex
{
    public class EditMultiplexHandler : MultiplexHandler<EditMultiplexCommand>
    {
        public EditMultiplexHandler(
                IMultiplexRepository multiplexRepository,
                IMappingEngine mappingEngine)
            : base(multiplexRepository, mappingEngine)
        {
        }

        protected override Domain.Multiplex MapCommandToMultiplex(EditMultiplexCommand command)
        {
            return MappingEngine.Map(command, MultiplexRepository.Get(command.Id));
        }
    }
}
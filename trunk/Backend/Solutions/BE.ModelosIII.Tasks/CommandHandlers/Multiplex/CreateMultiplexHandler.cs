using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex
{
    public class CreateMultiplexHandler :MultiplexHandler<CreateMultiplexCommand>
    {
        public CreateMultiplexHandler(
                IMultiplexRepository multiplexRepository,
                IMappingEngine mappingEngine)
            : base(multiplexRepository, mappingEngine)
        {
        }

        protected override Domain.Multiplex MapCommandToMultiplex(CreateMultiplexCommand command)
        {
            return MappingEngine.Map<Domain.Multiplex>(command);
        }
    }
}

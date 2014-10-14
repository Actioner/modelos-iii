using System.Collections.Generic;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex.Screen
{
    public class CreateScreenHandler : ScreenHandler<CreateScreenCommand>
    {
        private readonly IMultiplexRepository _multiplexRepository; 

        public CreateScreenHandler(
                IScreenRepository screenRepository,
                IMultiplexRepository multiplexRepository,
                IMappingEngine mappingEngine)
            : base(screenRepository, mappingEngine)
        {
            _multiplexRepository = multiplexRepository;
        }

        protected override Domain.Screen MapCommandToScreen(CreateScreenCommand command)
        {
            var screen = MappingEngine.Map<Domain.Screen>(command);
            screen.Multiplex = _multiplexRepository.Get(command.MultiplexId);
            screen.Rows = new List<Row>();
            var rows = MappingEngine.Map<IList<Row>>(command.Rows);

            foreach (var row in rows)
            {
                row.Screen = screen;

                foreach (var seat in row.Seats)
                {
                    seat.Row = row;
                }

                screen.Rows.Add(row);
            }
            return screen;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex.Screen
{
    public class EditScreenHandler : ScreenHandler<EditScreenCommand>
    {
        public EditScreenHandler(
                IScreenRepository screenRepository,
                IMappingEngine mappingEngine)
            : base(screenRepository, mappingEngine)
        {
        }

        protected override Domain.Screen MapCommandToScreen(EditScreenCommand command)
        {
            var screen = MappingEngine.Map(command, ScreenRepository.Get(command.Id));

            if (command.Rows.Any(r => r.Id == 0))
            {
                screen.Rows.Clear();
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
            }

            return screen;
        }
    }
}
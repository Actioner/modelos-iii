using System.Collections.Generic;
using BE.ModelosIII.Tasks.Commands.Utility;

namespace BE.ModelosIII.Tasks.Commands.Multiplex.Screen
{
    public class CreateScreenCommand : ScreenCommand
    {
        public CreateScreenCommand()
        {
            Rows = new List<RowModel>();
        }
    }
}

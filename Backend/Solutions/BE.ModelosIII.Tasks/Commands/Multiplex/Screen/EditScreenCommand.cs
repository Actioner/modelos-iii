using System.Collections.Generic;
using BE.ModelosIII.Tasks.Commands.Utility;

namespace BE.ModelosIII.Tasks.Commands.Multiplex.Screen
{
    public class EditScreenCommand : ScreenCommand
    {
        public EditScreenCommand()
        {
            Rows = new List<RowModel>();
        }
    }
}

using System.Collections.Generic;
using SharpArch.Domain.Commands;
using BE.ModelosIII.Tasks.Commands.Utility;

namespace BE.ModelosIII.Tasks.Commands.Multiplex.Screen
{
    public class ScreenCommand : CommandBase
    {
        public int Id { get; set; }
        public int MultiplexId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public IList<RowModel> Rows { get; set; }

        //Plano
        public FileUploadModel LayoutFile { get; set; }

        public ScreenCommand()
        {
            LayoutFile = new FileUploadModel
                             {
                                 Id = "LayoutFile"
                             };

            Rows = new List<RowModel>();
        }
    }
}
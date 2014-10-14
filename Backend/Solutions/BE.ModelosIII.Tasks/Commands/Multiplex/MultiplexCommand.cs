using BE.ModelosIII.Tasks.Commands.Utility;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Multiplex
{
    public class MultiplexCommand : CommandBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Subways { get; set; }
        public string Buses { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Poster { get; set; }
        public FileUploadModel NewPoster { get; set; }

        public MultiplexCommand()
        {
            NewPoster = new FileUploadModel
                             {
                                 Id = "NewPoster"
                             };
        }
    }
}
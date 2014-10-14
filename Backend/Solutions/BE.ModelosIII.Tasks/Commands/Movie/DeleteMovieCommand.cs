using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Movie
{
    public class DeleteMovieCommand : CommandBase
    {
        public int Id { get; set; }
    }
}

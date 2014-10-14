using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Movie;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Movie
{
    public class DeleteMovieHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;

        public DeleteMovieHandler(
            IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Handle(DeleteMovieCommand command)
        {
            var movie = _movieRepository.Get(command.Id);
            if (movie != null)
            {
                _movieRepository.Delete(movie);
            }
        }
    }
}

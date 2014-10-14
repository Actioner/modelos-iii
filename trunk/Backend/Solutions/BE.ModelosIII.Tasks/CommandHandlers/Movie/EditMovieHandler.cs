using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Movie;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Movie
{
    public class EditMovieHandler : MovieHandler<EditMovieCommand>
    {
        public EditMovieHandler(
                IMovieRepository movieRepository,
                IRepository<Rating> ratingRepository,
                IRepository<Genre> genreRepository,
                IMappingEngine mappingEngine)
            : base(movieRepository, ratingRepository, genreRepository, mappingEngine)
        {
        }

        protected override Domain.Movie MapCommandToMovie(EditMovieCommand command)
        {
            return MappingEngine.Map(command, MovieRepository.Get(command.Id));
        }
    }
}
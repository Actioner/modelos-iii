using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Movie;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Movie
{
    public class CreateMovieHandler : MovieHandler<CreateMovieCommand>
    {
        public CreateMovieHandler(
                IMovieRepository movieRepository,
                IRepository<Rating> ratingRepository,
                IRepository<Genre> genreRepository,
                IMappingEngine mappingEngine)
            : base(movieRepository, ratingRepository, genreRepository, mappingEngine)
        {
        }

        protected override Domain.Movie MapCommandToMovie(CreateMovieCommand command)
        {
            return MappingEngine.Map<Domain.Movie>(command);
        }
    }
}

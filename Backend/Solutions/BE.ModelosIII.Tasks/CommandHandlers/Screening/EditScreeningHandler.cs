using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Screening;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Screening
{
    public class EditScreeningHandler : ICommandHandler<EditScreeningCommand>
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IScreenRepository _screenRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMappingEngine _mappingEngine;

        public EditScreeningHandler(
            IScreeningRepository screeningRepository,
            IScreenRepository screenRepository, 
            IMovieRepository movieRepository,
            IMappingEngine mappingEngine)
        {
            this._screeningRepository = screeningRepository;
            this._screenRepository = screenRepository;
            this._movieRepository = movieRepository;
            this._mappingEngine = mappingEngine;
        }

        public virtual void Handle(EditScreeningCommand command)
        {
            var screening = MapCommandToScreening(command);

            screening.Screen = _screenRepository.Get(command.ScreenId);
            screening.Movie = _movieRepository.Get(command.MovieId);

            var result = _screeningRepository.SaveOrUpdate(screening);
            
            command.Id = result.Id;
        }

        protected Domain.Screening MapCommandToScreening(EditScreeningCommand command)
        {
            return _mappingEngine.Map(command, _screeningRepository.Get(command.Id));
            
        }
    }
}

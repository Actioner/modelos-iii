using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Screening;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Screening
{
    public class AssignScreeningHandler : ICommandHandler<AssignScreeningCommand>
    {
        private readonly IScreeningRepository _screeningRepository;

        public AssignScreeningHandler(IScreeningRepository screeningRepository)
        {
            this._screeningRepository = screeningRepository;
        }

        public virtual void Handle(AssignScreeningCommand command)
        {
            foreach (var screening in command.Screenings)
            {
                _screeningRepository.SaveOrUpdate(screening);
            }
        }
    }
}

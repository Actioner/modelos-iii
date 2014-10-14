using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Account;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Account
{
    public class ManageAccountValidator : AbstractValidator<ManageAccountCommand>
    {
        private readonly IUserRepository _userRepository;

        public ManageAccountValidator(
            IUserRepository userRepository)
        {
            this._userRepository = userRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithName("Nombre");

            RuleFor(x => x.Email)
                .NotEmpty()
                .Must((x, y) => BeValid(x));//.WithLocalizedMessage(() => NDW.Resources.ValidationMessages.EmailAlreadyInUse);

            RuleFor(x => x.Phone)
                .NotEmpty();
        }

        /// <summary>
        /// Revisa que el usuario cambie el mail por uno que no este tomado ya
        /// </summary>
        /// <param name="command">El comando enviado por el usuario para cambiar datos.</param>
        /// <returns>Devuelve true si el usuario puede cambiar el mail.</returns>
        public bool BeValid(ManageAccountCommand command)
        {
            var user = _userRepository.Get(command.Id);
            var otherUser = _userRepository.GetByEmail(command.Email);
            return user != null && (otherUser == null || user.Id == otherUser.Id);
        }
    }
}

using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.ApplicationServices;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Security;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Security
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;

        public LoginValidator(IUserRepository userRepository, 
            IEncryptionService encryptionService)
        {
            this._userRepository = userRepository;
            this._encryptionService = encryptionService;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .EmailAddress()
                .Must((x, y) => BeEnabled(x))
                .WithLocalizedMessage(() => ValidationMessages.EmailDisabled);

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must((x, y) => BeValidCombination(x))
                .WithLocalizedMessage(() => ValidationMessages.EmailOrPasswordInvalid);
        }

        /// <summary>
        /// Revisa que el usuario esté habilitado
        /// </summary>
        /// <param name="command">El comando enviado por el usuario para loguearse.</param>
        /// <returns>Devuelve true si el usuario está habilitado. Si el usuario no existiera también devolverá true, para evitar
        /// confirmar a un usuario malicioso si el nombre de usuario ingresado existe o no.</returns>
        public bool BeEnabled(LoginCommand command)
        {
            var usuario = _userRepository.GetByEmail(command.Email);
            return usuario == null || usuario.Enabled;
        }

        public bool BeValidCombination(LoginCommand command)
        {
            var usuario = _userRepository.GetByEmail(command.Email);
            return usuario != null
                && command.Password != null
                && _encryptionService.EncryptPassword(command.Password) == usuario.Password;
        }
    }
}

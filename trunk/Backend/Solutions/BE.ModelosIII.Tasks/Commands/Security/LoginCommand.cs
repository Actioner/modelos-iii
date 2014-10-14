using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Security
{
    public class LoginCommand : CommandBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
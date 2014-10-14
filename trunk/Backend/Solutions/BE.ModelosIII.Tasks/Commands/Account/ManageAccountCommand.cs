using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Account
{
    public class ManageAccountCommand : CommandBase
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
    }
}

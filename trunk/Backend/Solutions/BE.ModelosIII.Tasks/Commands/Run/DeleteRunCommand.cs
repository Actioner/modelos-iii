using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Run
{
    public class DeleteRunCommand : CommandBase
    {
        public int Id { get; set; }
    }
}

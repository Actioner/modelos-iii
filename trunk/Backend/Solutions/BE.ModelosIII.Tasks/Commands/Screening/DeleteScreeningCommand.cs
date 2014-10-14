using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Screening
{
    public class DeleteScreeningCommand : CommandBase
    {
        public int Id { get; set; }
    }
}

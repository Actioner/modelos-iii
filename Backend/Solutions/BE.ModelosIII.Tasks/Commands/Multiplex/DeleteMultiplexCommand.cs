using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Multiplex
{
    public class DeleteMultiplexCommand : CommandBase
    {
        public int Id { get; set; }
    }
}

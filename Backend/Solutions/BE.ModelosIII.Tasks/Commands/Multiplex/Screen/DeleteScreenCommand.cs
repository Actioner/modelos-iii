using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Multiplex.Screen
{
    public class DeleteScreenCommand : CommandBase
    {
        public int Id { get; set; }
        public int MultiplexId { get; set; }
    }
}

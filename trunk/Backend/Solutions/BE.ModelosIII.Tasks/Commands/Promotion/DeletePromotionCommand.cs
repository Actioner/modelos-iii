using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Promotion
{
    public class DeletePromotionCommand : CommandBase
    {
        public int Id { get; set; }
    }
}

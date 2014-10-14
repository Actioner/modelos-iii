using BE.ModelosIII.Domain;

namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public interface IEmailService
    {
        void SendEmail(string mailFrom, string mailTo, string subject, string body);
    }
}

using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Infrastructure.Helpers;

namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string mailFrom, string mailTo, string subject, string body)
        {
            var mailObj = new MailMessage
                              {
                                  BodyEncoding = Encoding.UTF8,
                                  IsBodyHtml = true,
                                  From = new MailAddress(mailFrom),
                                  Subject = subject,
                                  Body = body
                              };

            mailObj.To.Add(mailTo);

            Task.Factory.StartNew(() =>
                    {
                        var smtp = new SmtpClient();
                        smtp.EnableSsl = smtp.Port == 587 || smtp.Port == 465;
                        smtp.Timeout = 30000;
                        smtp.Send(mailObj);
                    }, TaskCreationOptions.LongRunning);
        }
    }
}

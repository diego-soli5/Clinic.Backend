using Clinic.Core.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.EmailService
{
    public abstract class MasterMailServer
    {
        private readonly EmailServiceOptions _options;

        public MasterMailServer(IOptions<EmailServiceOptions> options)
        {
            _options = options.Value;
        }

        public void SendMail(string subject, string body, List<string> receiversMails)
        {
            using (var smtpClient = new SmtpClient())
            {
                InitializeSmtpClient(smtpClient);

                using (var mailMessage = new MailMessage())
                { 
                    mailMessage.From = new MailAddress(_options.SenderMail);

                    foreach (string mail in receiversMails)
                    {
                        mailMessage.To.Add(mail);
                    }

                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.Priority = MailPriority.Normal;

                    smtpClient.Send(mailMessage);
                }
            }
        }

        private void InitializeSmtpClient (SmtpClient client)
        {
            client.Credentials = new NetworkCredential(_options.SenderMail, _options.Password);
            client.Host = _options.Host;
            client.Port = _options.Port;
            client.EnableSsl = _options.Ssl;
        }
    }
}

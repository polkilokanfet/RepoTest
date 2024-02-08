using HVTApp.Infrastructure.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace HVTApp.Services.EmailService
{
    public class MailKitService : IEmailService
    {
        public void SendMail(string to, string subject, string body)
        {
            const string from = "kosolapov_ag@uetm.ru";
#if DEBUG
            to = "kosolapov.ep@mail.ru";
#endif

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HVTApp", from));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("mx1.uetm.ru", 25, SecureSocketOptions.None);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(from, "Qazxsw-12");

                client.Send(message);
                client.Disconnect(true);
            }
		}
    }
}
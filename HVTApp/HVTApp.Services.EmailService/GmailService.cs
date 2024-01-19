using HVTApp.Infrastructure.Interfaces.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace HVTApp.Services.EmailService
{
    public class GmailService : IEmailService
    {
        public void SendMail(string to, string subject, string body)
        {
            const string from = "apphvt@gmail.com";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HVTApp", from));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 25, false);

                // Note: only needed if the SMTP server requires authentication
                //client.Authenticate(from, "uetm741258963");
                client.Authenticate(from, "kxag dkbm qsih ajri");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
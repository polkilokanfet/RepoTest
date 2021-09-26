using System;
using HVTApp.Infrastructure.Interfaces.Services;
using System.Net;
using System.Net.Mail;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IUnityContainer _container;

        public EmailService(IUnityContainer container)
        {
            _container = container;
        }

        public void SendMail(string to, string subject, string body)
        {
            const string from = "apphvt@gmail.com";

            var mailAddressTo = new MailAddress(to);
            var mailAddressFrom = new MailAddress(from, "HVTApp");

            //сообщение
            var message = new MailMessage(mailAddressFrom, mailAddressTo)
            {
                Subject = subject,
                Body = body
            };

            //клиент
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, "uetm741258963"),
                EnableSsl = true
            };

            try
            {
                //отправка клиентом сообщения
                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Error", e.PrintAllExceptions());
            }
        }
    }
}

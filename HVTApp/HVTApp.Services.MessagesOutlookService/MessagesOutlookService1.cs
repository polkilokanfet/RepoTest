using System.Collections.Generic;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Services;
using OpenMcdf;

namespace HVTApp.Services.MessagesOutlookService
{
    public class MessagesOutlookService1 : IMessagesOutlookService
    {
        public MessageOutlook GetOutlookMessage(string path)
        {
            try
            {
                MessageOutlook message;

                using (var msg = new MsgReader.Outlook.Storage.Message(path))
                {
                    message = new MessageOutlook
                    {
                        FilePath = path,
                        Subject = msg.Subject,
                        BodyText = msg.BodyText,
                        SentOnDate = msg.SentOn,
                        Sender = new UserOutlook(msg.Sender.Email, msg.Sender.DisplayName),
                        Recipients = msg.Recipients.Select(recipient => new UserOutlook(recipient.Email, recipient.DisplayName)).ToList()
                    };

                    //var recipientsTo = msg.GetEmailRecipients(MsgReader.Outlook.RecipientType.To, false, false);
                    //var recipientsCc = msg.GetEmailRecipients(MsgReader.Outlook.RecipientType.Cc, false, false);
                    //var subject = msg.Subject;
                    //var htmlBody = msg.BodyHtml;
                    // etc...
                }

                return message;
            }
            catch (CFFileFormatException e)
            {
                throw;
            }
        }

        public IEnumerable<MessageOutlook> GetOutlookMessages(string path)
        {
            List<MessageOutlook> result = new List<MessageOutlook>();

            var fileNames = System.IO.Directory.GetFiles(path, "*.msg");

            foreach (var fileName in fileNames)
            {
                var filePath = Path.Combine(path, fileName);
                try
                {
                    result.Add(this.GetOutlookMessage(filePath));
                }
                catch (CFFileFormatException e)
                {
                }
            }

            return result;
        }
    }
}

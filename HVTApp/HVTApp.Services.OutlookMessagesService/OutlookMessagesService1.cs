using System;
using System.Collections.Generic;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Services.OutlookMessagesService
{
    public class OutlookMessagesService1 : IOutlookMessagesService
    {
        public IEnumerable<OutlookMessage> GetOutlookMessages(string path)
        {
            using (var msg = new MsgReader.Outlook.Storage.Message(@"C:\test\test.msg"))
            {
                var from = msg.Sender;
                var sentOn = msg.SentOn;
                var recipientsTo = msg.GetEmailRecipients(MsgReader.Outlook.RecipientType.To, false, false);
                var recipientsCc = msg.GetEmailRecipients(MsgReader.Outlook.RecipientType.Cc, false, false);
                var subject = msg.Subject;
                var htmlBody = msg.BodyHtml;
                // etc...
            }

            return new List<OutlookMessage>();
        }
    }
}

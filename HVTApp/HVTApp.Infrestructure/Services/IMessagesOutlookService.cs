using System;
using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IMessagesOutlookService
    {
        IEnumerable<MessageOutlook> GetOutlookMessages(string path);
    }

    public class MessageOutlook
    {
        public DateTime? SentOnDate { get; set; }
        public string Subject { get; set; }
        public UserOutlook Sender { get; set; }
        public List<UserOutlook> Recipients { get; set; }
        public string FilePath { get; set; }
    }

    public class UserOutlook
    {
        public string Email { get; }
        public string DisplayName { get; }

        public UserOutlook(string email, string displayName)
        {
            Email = email;
            DisplayName = displayName;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.DisplayName))
                return Email;

            return $"{DisplayName} <{Email}>";
        }
    }
}
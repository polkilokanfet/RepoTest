using System;
using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IMessagesOutlookService
    {
        MessageOutlook GetOutlookMessage(string path);
        IEnumerable<MessageOutlook> GetOutlookMessages(string path);
    }

    public class MessageOutlook
    {
        public DateTime? SentOnDate { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public UserOutlook Sender { get; set; }
        public List<UserOutlook> Recipients { get; set; }
        public string FilePath { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is MessageOutlook other)
            {
                if (!Equals(this.SentOnDate, other.SentOnDate)) return false;
                if (!Equals(this.Subject, other.Subject)) return false;
                if (!Equals(this.BodyText, other.BodyText)) return false;
                if (!Equals(this.Sender, other.Sender)) return false;

                return true;
            }

            return false;
        }

        protected bool Equals(MessageOutlook other)
        {
            return Nullable.Equals(SentOnDate, other.SentOnDate) && Subject == other.Subject && BodyText == other.BodyText && Equals(Sender, other.Sender) && Equals(Recipients, other.Recipients) && FilePath == other.FilePath;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SentOnDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BodyText != null ? BodyText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Sender != null ? Sender.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Recipients != null ? Recipients.GetHashCode() : 0);
                return hashCode;
            }
        }
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

        public override bool Equals(object obj)
        {
            if (obj is UserOutlook other)
            {
                return Equals(this.Email, other.Email);
            }

            return false;
        }

        protected bool Equals(UserOutlook other)
        {
            return Email == other.Email && DisplayName == other.DisplayName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Email != null ? Email.GetHashCode() : 0) * 397) ^ (DisplayName != null ? DisplayName.GetHashCode() : 0);
            }
        }
    }
}
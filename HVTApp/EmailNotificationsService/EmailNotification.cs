namespace EmailNotificationsService
{
    internal class EmailNotification
    {
        public string Address { get; }
        public string Subject { get; }
        public string Body { get; }

        public EmailNotification(string address, string subject, string body)
        {
            Address = address;
            Subject = subject;
            Body = body;
        }
    }
}
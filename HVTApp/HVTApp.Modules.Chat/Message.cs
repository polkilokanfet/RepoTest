using System;

namespace HVTApp.Modules.Messenger
{
    public class Message
    {
         public string Author { get; }
        public DateTime Moment { get; }
        public string Text { get; }

       public Message(string author, DateTime moment, string text)
        {
            Author = author;
            Moment = moment;
            Text = text;
        }
    }
}
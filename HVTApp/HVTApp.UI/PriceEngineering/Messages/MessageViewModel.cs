using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class MessageViewModel
    {
        public string Message { get; }
        public User Author { get; }
        public DateTime Moment { get; }

        public MessageViewModel(string message, User author, DateTime moment)
        {
            Message = message;
            Author = author;
            Moment = moment;
        }
    }
}
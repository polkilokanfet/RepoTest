using System;
using System.Data;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class Letter : LookupItem<Document>
    {
        public string RegNumber => Entity.RegNumber;
        public DateTime Date => Entity.Date;
        public string Comment => Entity.Comment;
        public string SenderEmployee => Entity.SenderEmployee?.ToString();
        public string Author => Entity.Author?.ToString();
        public string RecipientEmployee => Entity.RecipientEmployee?.ToString();
        public string RequestDocument => Entity.RequestDocument?.ToString();
        public string Direction => Entity.Direction == DocumentDirection.Incoming
            ? "Входящий"
            : "Исходящий";

        /// <summary>
        /// Компания-отправитель
        /// </summary>
        public string CompanySender => Entity.SenderEmployee?.Company.ToString();

        /// <summary>
        /// Компания-получатель
        /// </summary>
        public string CompanyRecipient => Entity.RecipientEmployee?.Company.ToString();

        public Letter(Document entity) : base(entity)
        {
            if (entity == null) throw new NoNullAllowedException();
        }
    }
}
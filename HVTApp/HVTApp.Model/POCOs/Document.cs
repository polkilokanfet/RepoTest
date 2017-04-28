using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// «апрос, ответ на который дан насто€щим документом.
        /// </summary>
        public virtual Document RequestDocument { get; set; }

        public virtual Employee Author { get; set; }
        public virtual Employee SenderEmployee { get; set; }
        public virtual Employee RecipientEmployee { get; set; }
        public virtual List<Employee> CopyToRecipients { get; set; }
        public virtual RegistrationDetails RegistrationDetailsOfSender { get; set; }
        public virtual RegistrationDetails RegistrationDetailsOfRecipient{ get; set; }

        public string Comment { get; set; }
    }

    public class RegistrationDetails : BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

}
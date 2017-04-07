using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// «апрос, ответ на который дан насто€щим документом.
        /// </summary>
        public virtual Document RequestDocument { get; set; }

        public virtual User Author { get; set; }
        public virtual Employee SenderEmployee { get; set; }
        public virtual Employee RecipientEmployee { get; set; }
        public virtual List<Employee> CopyToRecipients { get; set; }
        public virtual RegistrationDetails RegistrationDetailsOfSender { get; set; }
        public virtual RegistrationDetails RegistrationDetailsOfRecipient{ get; set; }
    }

    public class RegistrationDetails : BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

}
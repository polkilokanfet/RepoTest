using System.Collections.Generic;

namespace HVTApp.Model
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// «апрос, ответ на который дан насто€щим документом.
        /// </summary>
        public virtual Document RequestDocument { get; set; }

        public virtual Company Sender { get; set; }
        public virtual Employee SenderEmployee { get; set; }
        public virtual Company RecipientCompany => RecipientEmployee?.Company;
        public virtual Employee RecipientEmployee { get; set; }
        public virtual List<Employee> CopyToRecipients { get; set; }
        public virtual RegistrationDetails RegistrationDetailsOfSender { get; set; }
        public virtual RegistrationDetails RegistrationDetailsOfRecipient{ get; set; }
    }
}
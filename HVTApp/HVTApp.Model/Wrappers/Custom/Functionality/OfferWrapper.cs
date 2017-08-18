using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrappers
{
    public partial class OfferWrapper
    {
        public void ChangeOffer(OfferWrapper templateOffer)
        {
            this.ValidityDate = DateTime.Today.AddDays(30);
            //this.Document.RegistrationDetailsOfSender.RegistrationDate = DateTime.Today;
            //this.Document.RecipientEmployee = templateOffer.Document.RecipientEmployee;

            //foreach (var copyToRecipient in Document.CopyToRecipients)
            //    this.Document.CopyToRecipients.Add(copyToRecipient);

            //foreach (var productOfferUnit in templateOffer.OfferUnits.Select(x => x.Model))
            //    this.OfferUnits.Add(GetWrapper<OfferUnitWrapper, OfferUnit>(productOfferUnit));
        }
    }
}

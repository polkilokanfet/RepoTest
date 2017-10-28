using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapper
    {
        public double Sum => OfferUnits.Sum(x => x.Cost);

        public void ChangeOffer(OfferWrapper templateOffer)
        {
            this.ValidityDate = DateTime.Today.AddDays(30);
            //this.DocumentId.RegistrationDetailsOfSender.RegistrationDate = DateTime.Today;
            //this.DocumentId.RecipientEmployee = templateOffer.DocumentId.RecipientEmployee;

            //foreach (var copyToRecipient in DocumentId.CopyToRecipients)
            //    this.DocumentId.CopyToRecipients.Add(copyToRecipient);

            //foreach (var productOfferUnit in templateOffer.OfferUnits.Select(x => x.Model))
            //    this.OfferUnits.Add(GetWrapper<OfferUnitWrapper, OfferUnit>(productOfferUnit));
        }
    }
}

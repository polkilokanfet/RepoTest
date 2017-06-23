using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrappers
{
    public partial class OfferWrapper
    {
        public void ChangeOffer(OfferWrapper templateOffer)
        {
            this.Project = templateOffer.Project;
            this.Tender = templateOffer.Tender;
            this.ValidityDate = DateTime.Today.AddDays(30);
            this.RegistrationDetailsOfSender.RegistrationDate = DateTime.Today;
            this.RecipientEmployee = templateOffer.RecipientEmployee;

            foreach (var copyToRecipient in CopyToRecipients)
                this.CopyToRecipients.Add(copyToRecipient);

            foreach (var productOfferUnit in templateOffer.ProductOfferUnits.Select(x => x.Model))
                this.ProductOfferUnits.Add(WrappersFactory.GetWrapper<ProductOfferUnit, ProductOfferUnitWrapper>(productOfferUnit));
        }
    }
}

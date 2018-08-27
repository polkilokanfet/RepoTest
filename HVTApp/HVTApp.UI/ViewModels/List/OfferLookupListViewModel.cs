using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferLookupListViewModel
    {
        protected override Offer GetNewItem()
        {
            return new Offer {ValidityDate = DateTime.Today.AddDays(60)};
        }
    }
}
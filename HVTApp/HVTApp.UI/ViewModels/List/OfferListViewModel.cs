using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferListViewModel
    {
        protected override Offer GetNewItem()
        {
            return new Offer {ValidityDate = DateTime.Today.AddDays(60)};
        }
    }
}
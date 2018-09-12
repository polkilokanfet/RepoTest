using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel : WrapperWithUnitsViewModel<OfferWrapper, Offer, OfferUnit, OfferUnitWrapper, AfterSaveOfferEvent>
    {
        protected override void AddCommand_Execute()
        {
            throw new NotImplementedException();
        }

        protected override DateTime GetDate()
        {
            throw new NotImplementedException();
        }
    }
}
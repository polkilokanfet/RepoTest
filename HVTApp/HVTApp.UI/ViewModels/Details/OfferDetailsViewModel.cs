using System;
using System.Collections.Generic;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel : WrapperWithUnitsViewModel<IUnitsGroup, OfferWrapper, Offer, OfferUnit, OfferUnitWrapper, AfterSaveOfferEvent>
    {
        protected override void AddCommand_Execute()
        {
            throw new NotImplementedException();
        }

        protected override DateTime GetDate()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IUnitsGroup> GetGroups()
        {
            throw new NotImplementedException();
        }
    }
}
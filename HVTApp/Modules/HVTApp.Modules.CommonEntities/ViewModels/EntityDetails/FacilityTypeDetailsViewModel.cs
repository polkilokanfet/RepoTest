﻿using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper>
    {
        public FacilityTypeDetailsViewModel(FacilityTypeWrapper item) : base(item)
        {
        }
    }
}
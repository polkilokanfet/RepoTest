﻿using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper>
    {
        public CompanyFormDetailsViewModel(CompanyFormWrapper item) : base(item)
        {
        }
    }
}
﻿using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    internal class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper, Contract>
    {
        public ContractDetailsViewModel(ContractWrapper item) : base(item)
        {
        }
    }
}
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilitiesViewModel : EditableBase<FacilityWrapper, FacilityDetailsViewModel, Facility>
    {
        public FacilitiesViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
        }
    }

    public class FacilityDetailsViewModel : BaseDetailsViewModel<FacilityWrapper, Facility>
    {
        public FacilityDetailsViewModel(FacilityWrapper item) : base(item)
        {
        }
    }
}

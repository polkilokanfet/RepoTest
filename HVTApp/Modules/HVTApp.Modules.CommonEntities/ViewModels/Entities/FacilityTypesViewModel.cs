﻿using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilityTypesViewModel : BaseListViewModel<FacilityTypeWrapper, FacilityTypeDetailsViewModel>
    {
        public FacilityTypesViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            unitOfWork.FacilityTypes.GetAll().ForEach(x => Items.Add(x));
        }
    }
}
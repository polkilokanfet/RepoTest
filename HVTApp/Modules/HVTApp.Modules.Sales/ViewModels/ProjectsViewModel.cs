using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : BaseListViewModel<ProjectWrapper, ProjectDetailsViewModel, Project>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;

            unitOfWork.Projects.GetAll().ForEach(Items.Add);
        }

        public ICollection<ProjectWrapper> Projects => Items;
        public ProjectWrapper SelectedProject { get; set; }
    }
}

using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class ProjectUnitGroupViewModel : IDialogRequestClose
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;
        private readonly IGetProductService _getProductService;

        public ProjectUnitGroup ProjectUnitGroup { get; }

        public ICommand EditFacilityCommand { get; }
        public ICommand EditProductCommand { get; }

        public ProjectUnitGroupViewModel(ProjectUnitGroup projectUnitGroup, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _selectService = container.Resolve<ISelectService>();
            _getProductService = container.Resolve<IGetProductService>();

            ProjectUnitGroup = projectUnitGroup;

            EditFacilityCommand = new DelegateCommand(EditFacilityCommand_Execute);
            EditProductCommand = new DelegateCommand(EditProductCommand_Execute);
        }

        private async void EditProductCommand_Execute()
        {
            var product = await _getProductService.GetProductAsync(ProjectUnitGroup.Product.Model);
            if (product != null && product.Id != ProjectUnitGroup.Product?.Id)
            {
                ProjectUnitGroup.Product = new ProductWrapper(product);
            }
        }

        private async void EditFacilityCommand_Execute()
        {
            var facilities = (await _unitOfWork.GetRepository<Facility>().GetAllAsync());
            var facility = _selectService.SelectItem(facilities);
            if (facility != null && facility.Id != ProjectUnitGroup.Facility?.Id)
            {
                ProjectUnitGroup.Facility = new FacilityWrapper(facility);
            }
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
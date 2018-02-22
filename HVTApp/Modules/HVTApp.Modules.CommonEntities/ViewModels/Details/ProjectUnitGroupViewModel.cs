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

        public UnitGroupGroup UnitGroupGroup { get; }

        public ICommand EditFacilityCommand { get; }
        public ICommand EditProductCommand { get; }

        public ProjectUnitGroupViewModel(UnitGroupGroup unitGroupGroup, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _selectService = container.Resolve<ISelectService>();
            _getProductService = container.Resolve<IGetProductService>();

            UnitGroupGroup = unitGroupGroup;

            EditFacilityCommand = new DelegateCommand(EditFacilityCommand_Execute);
            EditProductCommand = new DelegateCommand(EditProductCommand_Execute);
        }

        private async void EditProductCommand_Execute()
        {
            var product = await _getProductService.GetProductAsync(UnitGroupGroup.Product.Model);
            if (product != null && product.Id != UnitGroupGroup.Product?.Id)
            {
                UnitGroupGroup.Product = new ProductWrapper(await _unitOfWork.GetRepository<Product>().GetByIdAsync(product.Id));
            }
        }

        private async void EditFacilityCommand_Execute()
        {
            var facilities = (await _unitOfWork.GetRepository<Facility>().GetAllAsync());
            var facility = _selectService.SelectItem(facilities);
            if (facility != null && facility.Id != UnitGroupGroup.Facility?.Id)
            {
                UnitGroupGroup.Facility = new FacilityWrapper(facility);
            }
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
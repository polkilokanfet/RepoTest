using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class OfferUnitsGroupDetailsViewModel : IDialogRequestClose
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;
        private readonly IGetProductService _getProductService;

        public OfferUnitsGroup OfferUnitsGroup { get; }

        public ICommand SelectFacilityCommand { get; }
        public ICommand SelectProductCommand { get; }
        public ICommand SelectPaymentConditionSetCommand { get; }

        public ICommand OkCommand { get; }

        public OfferUnitsGroupDetailsViewModel(OfferUnitsGroup offerUnitsGroup, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _selectService = container.Resolve<ISelectService>();
            _getProductService = container.Resolve<IGetProductService>();

            OfferUnitsGroup = offerUnitsGroup;

            SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute);
            SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
            SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute);

            OkCommand = new DelegateCommand(() => CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true)));
        }

        private async void SelectPaymentConditionSetCommand_Execute()
        {
            var sets = await _unitOfWork.GetRepository<PaymentConditionSet>().GetAllAsync();
            var set = _selectService.SelectItem(sets);
            if (set != null && set.Id != OfferUnitsGroup.PaymentConditionSet?.Id)
            {
                OfferUnitsGroup.PaymentConditionSet = new PaymentConditionSetWrapper(set);
            }
        }

        private async void SelectProductCommand_Execute()
        {
            var product = await _getProductService.GetProductAsync(OfferUnitsGroup.Product.Model);
            if (product != null && product.Id != OfferUnitsGroup.Product?.Id)
            {
                OfferUnitsGroup.Product = new ProductWrapper(await _unitOfWork.GetRepository<Product>().GetByIdAsync(product.Id));
            }
        }

        private async void SelectFacilityCommand_Execute()
        {
            var facilities = (await _unitOfWork.GetRepository<Facility>().GetAllAsync());
            var facility = _selectService.SelectItem(facilities);
            if (facility != null && facility.Id != OfferUnitsGroup.Facility?.Id)
            {
                OfferUnitsGroup.Facility = new FacilityWrapper(facility);
            }
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
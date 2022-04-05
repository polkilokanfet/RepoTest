using System;
using System.ComponentModel;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering.BlockChooser
{
    public class ProductBlockStructureCostViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private ProductBlockWrapper1 _productBlockWrapper;

        public ProductBlockWrapper1 ProductBlockWrapper
        {
            get => _productBlockWrapper;
            private set
            {
                if (_productBlockWrapper != null)
                    _productBlockWrapper.PropertyChanged -= ProductBlockWrapperOnPropertyChanged;

                _productBlockWrapper = value;

                if (_productBlockWrapper != null)
                    _productBlockWrapper.PropertyChanged += ProductBlockWrapperOnPropertyChanged;

                OkCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        private void ProductBlockWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OkCommand.RaiseCanExecuteChanged();
        }

        public int Amount { get; set; } = 1;

        public bool Result { get; private set; } = false;

        public DelegateLogCommand SelectBlockCommand { get; }
        public DelegateLogCommand OkCommand { get; }

        public event Action OkCommandExecuted;

        public ProductBlockStructureCostViewModel(IUnityContainer container, ProductBlock productBlock, DesignDepartmentParameters requiredParameters)
        {
            _container = container;
            _unitOfWork = container.Resolve<IUnitOfWork>();

            ProductBlockWrapper = new ProductBlockWrapper1(_unitOfWork.Repository<ProductBlock>().GetById(productBlock.Id));

            SelectBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var getProductService = _container.Resolve<IGetProductService>();
                    var selectedProductBlock = getProductService.GetProductBlock(ProductBlockWrapper.Model, requiredParameters.Parameters);
                    if (ProductBlockWrapper.Id != selectedProductBlock.Id)
                    {
                        ProductBlockWrapper = new ProductBlockWrapper1(_unitOfWork.Repository<ProductBlock>().GetById(selectedProductBlock.Id));
                    }
                });

            OkCommand = new DelegateLogCommand(
                () =>
                {
                    if (ProductBlockWrapper.IsChanged)
                    {
                        ProductBlockWrapper.AcceptChanges();
                        _unitOfWork.SaveChanges();
                    }

                    Result = true;
                    OkCommandExecuted?.Invoke();
                },
                () => ProductBlockWrapper != null && ProductBlockWrapper.IsValid);
        }

        public ProductBlock Run()
        {
            new ProductBlockStructureCostWindow(this).ShowDialog();
            return this.ProductBlockWrapper.Model;
        }
    }
}
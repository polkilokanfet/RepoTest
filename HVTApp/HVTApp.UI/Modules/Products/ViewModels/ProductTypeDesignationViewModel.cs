using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class ProductTypeDesignationViewModel : ViewModelBase
    {
        private ParameterWrapper _selectedParameter;
        public ProductTypeDesignationWrapper ProductTypeDesignationWrapper { get; }

        public ParameterWrapper SelectedParameter
        {
            get => _selectedParameter;
            set
            {
                _selectedParameter = value;
                RemoveParameterCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogCommand AddParameterCommand { get; }
        public DelegateLogCommand RemoveParameterCommand { get; }

        public ProductTypeDesignationViewModel(IUnityContainer container) : base(container)
        {
            ProductTypeDesignationWrapper = new ProductTypeDesignationWrapper(new ProductTypeDesignation())
            {
                ProductType = new ProductTypeWrapper(new ProductType())
                {
                    Name = "Новый тип продукта"
                }
            };

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    if (UnitOfWork.SaveEntity(ProductTypeDesignationWrapper.Model).OperationCompletedSuccessfully)
                    {
                        ProductTypeDesignationWrapper.AcceptChanges();
                        Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductTypeDesignationEvent>().Publish(ProductTypeDesignationWrapper.Model);
                    }

                    SaveCommand.RaiseCanExecuteChanged();
                },
                () => ProductTypeDesignationWrapper.IsValid && ProductTypeDesignationWrapper.IsChanged);

            AddParameterCommand = new DelegateLogCommand(
                () =>
                {
                    var parameters = UnitOfWork.Repository<Parameter>().Find(x => true);
                    var parameter = Container.Resolve<ISelectService>().SelectItem(parameters);
                    if (parameter != null)
                    {
                        ProductTypeDesignationWrapper.Parameters.Add(new ParameterWrapper(parameter));
                    }
                });

            RemoveParameterCommand = new DelegateLogCommand(
                () => { ProductTypeDesignationWrapper.Parameters.Remove(SelectedParameter); },
                () => SelectedParameter != null);

            ProductTypeDesignationWrapper.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
            };
        }
    }
}
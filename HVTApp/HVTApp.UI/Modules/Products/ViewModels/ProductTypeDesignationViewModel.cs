using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class ProductTypeDesignationViewModel : ViewModelBase
    {
        private ParameterWrapper _selectedParameter;
        public ProductTypeDesignationWrapper ProductTypeDesignationWrapper { get; }

        public ParameterWrapper SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                _selectedParameter = value;
                ((DelegateCommand) RemoveParameterCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand AddParameterCommand { get; }
        public ICommand RemoveParameterCommand { get; }

        public ProductTypeDesignationViewModel(IUnityContainer container) : base(container)
        {
            ProductTypeDesignationWrapper = new ProductTypeDesignationWrapper(new ProductTypeDesignation())
            {
                ProductType = new ProductTypeWrapper(new ProductType())
                {
                    Name = "New product type"
                }
            };

            SaveCommand = new DelegateCommand(
                () =>
                {
                    UnitOfWork.Repository<ProductTypeDesignation>().Add(ProductTypeDesignationWrapper.Model);
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductTypeDesignationEvent>().Publish(ProductTypeDesignationWrapper.Model);
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () => ProductTypeDesignationWrapper.IsValid && ProductTypeDesignationWrapper.IsChanged);

            AddParameterCommand = new DelegateCommand(
                () =>
                {
                    var parameters = UnitOfWork.Repository<Parameter>().Find(x => true);
                    var parameter = Container.Resolve<ISelectService>().SelectItem(parameters);
                    if (parameter != null)
                    {
                        ProductTypeDesignationWrapper.Parameters.Add(new ParameterWrapper(parameter));
                    }
                });

            RemoveParameterCommand = new DelegateCommand(
                () => { ProductTypeDesignationWrapper.Parameters.Remove(SelectedParameter); },
                () => SelectedParameter != null);

            ProductTypeDesignationWrapper.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            };
        }
    }
}
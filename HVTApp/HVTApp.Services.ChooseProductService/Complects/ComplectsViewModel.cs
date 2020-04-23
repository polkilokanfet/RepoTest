using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Complects
{
    public class ComplectsViewModel : ViewModelBase
    {
        private Product _selectedItem;
        public ObservableCollection<Product> Items { get; }

        public Product SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsSelected { get; private set; } = false;

        public ICommand SelectCommand { get; }
        public ICommand NewComplectCommand { get; }

        public ComplectsViewModel(IUnityContainer container) : base(container)
        {
            var complectsParameter = GlobalAppProperties.Actual.ComplectsParameter;
            var complects = UnitOfWork.Repository<Product>().Find(x => x.ProductBlock.Parameters.ContainsById(complectsParameter));
            Items = new ObservableCollection<Product>(complects);

            SelectCommand = new DelegateCommand(
                () =>
                {
                    IsSelected = true;
                    SelectEvent?.Invoke();
                }, 
                () => SelectedItem != null);

            NewComplectCommand = new DelegateCommand(
                () =>
                {
                    var complectViewModel = Container.Resolve<ComplectViewModel>();
                    complectViewModel.ShowDialog();
                    if (complectViewModel.IsSaved)
                    {
                        Items.Add(complectViewModel.Product.Model);
                        SelectedItem = complectViewModel.Product.Model;
                    }
                });
        }

        public void ShowDialog()
        {
            var complectsWindow = new ComplectsWindow(this);
            complectsWindow.ShowDialog();
        }

        public event Action SelectEvent;
    }
}
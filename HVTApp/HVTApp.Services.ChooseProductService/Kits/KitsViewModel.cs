using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Kits
{
    public class KitsViewModel : ViewModelBase
    {
        private DesignDepartment _designDepartment;

        private Kit _selectedItem;
        public ObservableCollection<Kit> Items { get; private set; }

        public Kit SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value, () =>
                {
                    ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
                });
            }
        }

        public bool IsSelected { get; private set; } = false;

        public ICommand SelectCommand { get; }
        public ICommand NewComplectCommand { get; }

        public KitsViewModel(IUnityContainer container) : base(container)
        {
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
                    var kitViewModel = Container.Resolve<KitViewModel>();
                    kitViewModel.Load(_designDepartment);
                    kitViewModel.ShowDialog();
                    if (kitViewModel.IsSaved)
                    {
                        var kit = new Kit(kitViewModel.Product.Model);
                        Items.Add(kit);
                        SelectedItem = kit;
                    }
                });
        }

        /// <summary>
        /// Загрузка всех комплектов
        /// </summary>
        public void Load()
        {
            var kitParameter = GlobalAppProperties.Actual.ComplectsParameter;
            var kits = UnitOfWork.Repository<Product>()
                .Find(product => product.ProductBlock.Parameters.ContainsById(kitParameter))
                .Select(product => new Kit(product));
            Items = new ObservableCollection<Kit>(kits);
        }

        public void Load(DesignDepartment designDepartment)
        {
            _designDepartment = UnitOfWork.Repository<DesignDepartment>().GetById(designDepartment.Id);
            var kits = _designDepartment.Kits.Select(product => new Kit(product));
            Items = new ObservableCollection<Kit>(kits);
        }

        public void ShowDialog()
        {
            var kitsWindow = new KitsWindow(this);
            kitsWindow.ShowDialog();
        }

        public event Action SelectEvent;
    }
}
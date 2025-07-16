using System;
using System.Collections.Generic;
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
        public ObservableCollection<Kit> Items { get; } = new ObservableCollection<Kit>();

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
        public ICommand NewKitCommand { get; }
        public ICommand LoadAllKitsCommand { get; }

        public KitsViewModel(IUnityContainer container) : base(container)
        {
            LoadAllKitsCommand = new DelegateCommand(this.Load2, () => GlobalAppProperties.User.RoleCurrent == Role.Constructor);

            SelectCommand = new DelegateCommand(
                () =>
                {
                    IsSelected = true;
                    SelectEvent?.Invoke();
                }, 
                () => SelectedItem != null);

            NewKitCommand = new DelegateCommand(
                () =>
                {
                    var kitViewModel = Container.Resolve<KitViewModel>();
                    kitViewModel.Load(_designDepartment);
                    kitViewModel.ShowDialog();
                    if (kitViewModel.IsSaved)
                    {
                        var kit = new Kit(kitViewModel.Product.Model);
                        Items.Insert(0, kit);
                        SelectedItem = kit;
                    }
                }, () => GlobalAppProperties.User.RoleCurrent == Role.Constructor);
        }

        /// <summary>
        /// Загрузка всех комплектов (для менеджера)
        /// </summary>
        public void Load()
        {
            var kits = UnitOfWork.Repository<Product>()
                .Find(product => product.DesignDepartmentsKits.Any(department => department.IsKitDepartment))
                .Select(product => new Kit(product));
            RefreshItems(kits);
        }

        /// <summary>
        /// Загрузка всех комплектов
        /// </summary>
        public void Load2()
        {
            var kitParameter = GlobalAppProperties.Actual.ComplectsParameter;
            var kits = UnitOfWork.Repository<Product>()
                .Find(product => product.ProductBlock.Parameters.ContainsById(kitParameter))
                .Select(product => new Kit(product));
            RefreshItems(kits);
        }

        /// <summary>
        /// Загрузка всех комплектов
        /// </summary>
        /// <param name="designDepartment">Департамент ОГК, чьи комплекты</param>
        public void Load(DesignDepartment designDepartment)
        {
            _designDepartment = UnitOfWork.Repository<DesignDepartment>().GetById(designDepartment.Id);
            var kits = _designDepartment.Kits.Select(product => new Kit(product));
            RefreshItems(kits);
        }

        private void RefreshItems(IEnumerable<Kit> kits)
        {
            Items.Clear();
            Items.AddRange(kits.OrderBy(kit => kit.Product.DesignationSpecial));
        }

        public void ShowDialog()
        {
            var kitsWindow = new KitsWindow(this);
            kitsWindow.ShowDialog();
        }

        public event Action SelectEvent;
    }
}
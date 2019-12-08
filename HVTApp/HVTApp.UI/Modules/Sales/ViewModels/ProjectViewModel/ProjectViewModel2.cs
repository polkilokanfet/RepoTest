using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Structures;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectViewModel2 : ViewModelBase
    {
        private object _selectedItem;
        public ProjectWithGroupsWrapper Project { get; private set; }

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(PriceStructures));
            }
        }

        /// <summary>
        /// Структуры себестоимости выбранного юнита.
        /// </summary>
        public PriceStructures PriceStructures
        {
            get
            {
                if (SelectedItem == null)
                    return null;

                if (SelectedItem is SalesUnitProjectGroup)
                    return ((SalesUnitProjectGroup)SelectedItem).Unit.CostStructure.PriceStructures;

                return ((SalesUnitProjectItem)SelectedItem).CostStructure.PriceStructures;
            }
        }

        public ProjectViewModel2(IUnityContainer container) : base(container)
        {
            AddCommand = new DelegateCommand(
                () =>
                {
                    //создаем новый юнит и привязываем его к проекту
                    var salesUnit = new SalesUnitWrapper(new SalesUnit {Project = Project.Model});

                    //создаем модель для диалога
                    var viewModel = new SalesUnitsViewModel(salesUnit, Container, UnitOfWork);

                    //заполняем юнит начальными данными
                    FillingSalesUnit(viewModel.ViewModel.Item);

                    //диалог с пользователем
                    var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
                    if (!result.HasValue || !result.Value) return;

                    //клонируем юниты
                    var units = CloneSalesUnits(viewModel.ViewModel.Item.Model, viewModel.Amount);

                    var group = new SalesUnitsWrappersGroup(units.ToList());
                    Groups.Add(group);
                    RefreshPrice(group);
                    Groups.SelectedGroup = group;

                });

            AddProductIncludedCommand = new DelegateCommand(
                () =>
                {
                    var productIncludedWrapper = new ProductIncludedWrapper(new ProductIncluded());
                    var productsIncludedViewModel = new ProductsIncludedViewModel(productIncludedWrapper, UnitOfWork, Container);
                    var dr = Container.Resolve<IDialogService>().ShowDialog(productsIncludedViewModel);
                    if (!dr.HasValue || !dr.Value) return;

                    if (SelectedItem is SalesUnitProjectGroup)
                    {
                        ((SalesUnitProjectGroup)SelectedItem).AddProductIncluded(productsIncludedViewModel.ViewModel.Entity, productsIncludedViewModel.IsForEach);
                    }
                    else if (SelectedItem is SalesUnitProjectItem)
                    {
                        ((SalesUnitProjectItem)SelectedItem).ProductsIncluded.Add(new ProductIncludedWrapper(productsIncludedViewModel.ViewModel.Entity));
                    }
                }, 
                () => SelectedItem != null);
        }

        public void Load(Guid id)
        {
            var project = UnitOfWork.Repository<Project>().GetById(id);
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == id);
            Project = new ProjectWithGroupsWrapper(project, salesUnits);
        }

        #region Commands

        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public ICommand AddProductIncludedCommand { get; }
        public ICommand RemoveProductIncludedCommand { get; }

        #endregion

        protected abstract void AddCommand_Execute();

        private void RemoveProductIncludedCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            Groups.SelectedGroup.RemoveProductIncluded(Groups.SelectedProductIncluded);
            RefreshPrice(Groups.SelectedGroup);
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            //удаление из группы
            if (Groups.Contains(Groups.SelectedGroup))
            {
                Groups.Remove(Groups.SelectedGroup);
            }
            //удаление из подгруппы
            else
            {
                var group = Groups.Single(x => x.Groups != null && x.Groups.Contains(Groups.SelectedGroup as TMember));
                group.Groups.Remove(Groups.SelectedGroup as TMember);

                //если группа стала пустая - удалить
                if (!group.Groups.Any())
                {
                    Groups.Remove(group);
                }
            }

            Groups.SelectedGroup = default(TGroup);
        }

        private async void ChangeProductCommand_Execute(TGroup wrappersGroup)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(wrappersGroup.Product?.Model);
            if (product == null || product.Id == wrappersGroup.Product.Id) return;
            product = await UnitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            wrappersGroup.Product = new ProductWrapper(product);
            RefreshPrice(wrappersGroup);
        }

        private async void ChangeFacilityCommand_Execute(TGroup wrappersGroup)
        {
            var facilities = await UnitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, wrappersGroup.Facility?.Id);
            if (facility == null) return;
            facility = await UnitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            wrappersGroup.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(TGroup wrappersGroup)
        {
            var sets = await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, wrappersGroup.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await UnitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            wrappersGroup.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }

        #endregion

        /// <summary>
        /// Заполнение юнита по выбранной группе
        /// </summary>
        /// <param name="salesUnitWrapper"></param>
        private void FillingSalesUnit(SalesUnitWrapper salesUnitWrapper)
        {
            if (SelectedItem == null)
            {
                var paymentConditionSet = UnitOfWork.Repository<PaymentConditionSet>().GetById(GlobalAppProperties.Actual.PaymentConditionSet.Id);
                salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(paymentConditionSet);
                salesUnitWrapper.ProductionTerm = GlobalAppProperties.Actual.StandartTermFromStartToEndProduction;

                return;
            }

            salesUnitWrapper.Cost = Groups.SelectedGroup.Cost;
            salesUnitWrapper.Facility = Groups.SelectedGroup.Facility;
            salesUnitWrapper.PaymentConditionSet = Groups.SelectedGroup.PaymentConditionSet;
            salesUnitWrapper.ProductionTerm = Groups.SelectedGroup.ProductionTerm;
            salesUnitWrapper.Product = Groups.SelectedGroup.Product;
            salesUnitWrapper.DeliveryDateExpected = Groups.SelectedGroup.DeliveryDateExpected;

            //создаем зависимое оборудование
            foreach (var prodIncl in Groups.SelectedGroup.ProductsIncluded)
            {
                var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                salesUnitWrapper.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
            }
        }

        /// <summary>
        /// Клонирование юнитов по образцу.
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private IEnumerable<SalesUnit> CloneSalesUnits(SalesUnit salesUnit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var unit = (SalesUnit)salesUnit.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                yield return unit;
            }

        }

    }
}
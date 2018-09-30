using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitsGroupsViewModel : BaseGroupsViewModel<SalesUnitsWrappersGroup, SalesUnitsWrappersGroup, SalesUnit>, IGroupsViewModel<SalesUnit, ProjectWrapper>
    {
        private ProjectWrapper _projectWrapper;

        public SalesUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override DateTime GetPriceDate(SalesUnitsWrappersGroup grp)
        {
            return grp.OrderInTakeDate < DateTime.Today ? grp.OrderInTakeDate : DateTime.Today;
        }


        #region AddCommand

        protected override void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if(_projectWrapper != null) salesUnit.Project = _projectWrapper;

            //создаем модель для диалога
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, UnitOfWork);

            //заполняем юнит начальными данными
            FillingSalesUnit(viewModel.ViewModel.Item);

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = CloneSalesUnits(viewModel.ViewModel.Item.Model, viewModel.Amount);

            var group = new SalesUnitsWrappersGroup(units);
            Groups.Add(group);
            RefreshPrice(group);
            SelectedGroup = group;
        }

        /// <summary>
        /// Заполнение юнита по выбранной группе
        /// </summary>
        /// <param name="salesUnitWrapper"></param>
        private void FillingSalesUnit(SalesUnitWrapper salesUnitWrapper)
        {
            if (SelectedGroup == null) return;

            salesUnitWrapper.Cost = SelectedGroup.Cost;
            salesUnitWrapper.Facility = SelectedGroup.Facility;
            salesUnitWrapper.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
            salesUnitWrapper.ProductionTerm = SelectedGroup.ProductionTerm;
            salesUnitWrapper.Product = SelectedGroup.Product;
            salesUnitWrapper.DeliveryDateExpected = SelectedGroup.DeliveryDateExpected;
                
            //создаем зависимое оборудование
            foreach (var prodIncl in SelectedGroup.ProductsIncluded)
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

        #endregion


        public void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //добавляем созданные
            var added = GetAddedUnits().ToList();
            UnitOfWork.Repository<SalesUnit>().AddRange(added);

            //удаляем удаленные
            var removedModels = GetRemovedUnits().ToList();
            //сообщаем об удалении (так высоко, т.к. после удаления объект рушится)
            removedModels.ForEach(x => eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Publish(x));
            UnitOfWork.Repository<SalesUnit>().DeleteRange(removedModels);

            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).ToList();
            modified = Groups.ModifiedItems.Concat(modified).ToList();

            Groups.AcceptChanges();

            added.Concat(modified.Select(x => x.Model)).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(x));
        }

        private IEnumerable<SalesUnit> GetAddedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups);
            added = added.Concat(Groups.AddedItems);
            return added.Select(x => x.Model).Distinct();
        }

        private IEnumerable<SalesUnit> GetRemovedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).ToList();
            added = added.Concat(Groups.AddedItems).ToList();

            //удаляем удаленные
            var removed = Groups.Except(added).Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            removed = Groups.RemovedItems.Concat(removed).ToList();
            removed = removed.Concat(Groups.RemovedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups)).ToList();
            return removed.Select(x => x.Model).Distinct().ToList();
        }

        public virtual bool CanSaveChanges()
        {
            return Groups.Any() && Groups.IsValid;
        }

        public void Load(IEnumerable<SalesUnit> units, ProjectWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            _projectWrapper = parentWrapper;
            UnitOfWork = unitOfWork;

            var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new SalesUnitsWrappersGroup(x)).ToList();

            if (isNew)
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(new List<SalesUnitsWrappersGroup>());
                groups.ForEach(x => Groups.Add(x));
            }
            else
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(groups);
            }

            OnPropertyChanged(nameof(Groups));

            Groups.PropertyChanged += GroupsOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;

            Groups.ForEach(RefreshPrice);
        }

        public bool IsValid => Groups != null && Groups.Any() && Groups.IsValid;

        public bool IsChanged => Groups != null && Groups.IsChanged;

        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            GroupChanged?.Invoke();
        }

        private void GroupsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            GroupChanged?.Invoke();
        }


        public event Action GroupChanged;
    }
}
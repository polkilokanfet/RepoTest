using System;
using System.Collections.Generic;
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
    public class OfferUnitsGroupsViewModel : BaseGroupsViewModel<OfferUnitsGroup, OfferUnitsGroup, OfferUnit>, IGroupsViewModel<OfferUnit, OfferWrapper>
    {
        private OfferWrapper _offerWrapper;

        public OfferUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        public void Load(IEnumerable<OfferUnit> units, OfferWrapper offerWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            _offerWrapper = offerWrapper;
            UnitOfWork = unitOfWork;

            var groups = units.GroupBy(x => x, new OfferUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new OfferUnitsGroup(x)).ToList();
            groups.ForEach(x => x.Offer = offerWrapper);

            if (isNew)
            {
                Groups = new ValidatableChangeTrackingCollection<OfferUnitsGroup>(new List<OfferUnitsGroup>());
                groups.ForEach(x => Groups.Add(x));
            }
            else
            {
                Groups = new ValidatableChangeTrackingCollection<OfferUnitsGroup>(groups);
            }

            OnPropertyChanged(nameof(Groups));

            Groups.PropertyChanged += GroupsOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;

            Groups.ForEach(RefreshPrice);
        }


        #region Commands

        protected override void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new OfferUnitWrapper(new OfferUnit());
            if (_offerWrapper != null) salesUnit.Offer = _offerWrapper;

            //создаем модель для диалога
            var viewModel = new OfferUnitsViewModel(salesUnit, Container, UnitOfWork);

            //заполняем юнит начальными данными
            if (SelectedGroup != null)
            {
                viewModel.ViewModel.Item.Cost = SelectedGroup.Cost;
                viewModel.ViewModel.Item.Facility = SelectedGroup.Facility;
                viewModel.ViewModel.Item.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
                viewModel.ViewModel.Item.ProductionTerm = SelectedGroup.ProductionTerm;
                viewModel.ViewModel.Item.Product = SelectedGroup.Product;

                //создаем зависимое оборудование
                foreach (var prodIncl in SelectedGroup.ProductsIncluded)
                {
                    var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                    viewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
                }
            }

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);

            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = new List<OfferUnit>();
            for (int i = 0; i < viewModel.Amount; i++)
            {
                var unit = (OfferUnit)viewModel.ViewModel.Item.Model.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                units.Add(unit);
            }

            var group = new OfferUnitsGroup(units);
            Groups.Add(group);
            RefreshPrice(group);
            SelectedGroup = group;
        }

        #endregion

        public void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //добавляем созданные
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).ToList();
            added = added.Concat(Groups.AddedItems).ToList();
            UnitOfWork.Repository<OfferUnit>().AddRange(added.Select(x => x.Model).Distinct());

            //удаляем удаленные
            var removed = Groups.Except(added).Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            //removed = removed.Except(added.Where(x => x.Groups != null).SelectMany(x => x.Groups)).ToList(); // исключаем вновь добавленные
            removed = Groups.RemovedItems.Concat(removed).ToList();
            removed = removed.Concat(Groups.RemovedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups)).ToList();
            var removedModels = removed.Select(x => x.Model).Distinct().ToList();
            //сообщаем об изменениях (так высоко, т.к. после удаления объект рушится)
            removedModels.ForEach(x => eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Publish(x));
            UnitOfWork.Repository<OfferUnit>().DeleteRange(removedModels);

            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).ToList();
            modified = Groups.ModifiedItems.Concat(modified).ToList();

            Groups.AcceptChanges();

            //сообщаем об изменениях
            added.Concat(modified).Select(x => x.Model).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Publish(x));
        }


        protected override DateTime GetPriceDate(OfferUnitsGroup grp)
        {
            return grp.Offer.Date < DateTime.Today ? grp.Offer.Date : DateTime.Today;
        }
    }
}
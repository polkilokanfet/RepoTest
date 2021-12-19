using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public class OfferUnitsGroupsViewModel : BaseGroupsViewModel<OfferUnitsGroup, OfferUnitsGroup, OfferUnit, AfterSaveOfferUnitEvent, AfterRemoveOfferUnitEvent>, 
        IGroupsViewModel<OfferUnit, OfferWrapper>
    {
        private OfferWrapper _offerWrapper;

        public OfferUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<OfferUnitsGroup> GetGroups(IEnumerable<OfferUnit> units)
        {
            return units.GroupBy(offerUnit => offerUnit, new OfferUnitsGroupsComparer())
                        .OrderBy(x => x.Key, new ProductCostComparer())
                        .Select(x => new OfferUnitsGroup(x)).ToList();
        }

        public void Load(IEnumerable<OfferUnit> units, OfferWrapper offerWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            Load(units, unitOfWork, isNew);
            _offerWrapper = offerWrapper;
            if(isNew) Groups.ForEach(x => x.Offer = offerWrapper);
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
            if (Groups.SelectedGroup != null)
            {
                viewModel.ViewModel.Item.Cost = Groups.SelectedGroup.Cost;
                viewModel.ViewModel.Item.Facility = new FacilityWrapper(Groups.SelectedGroup.Facility.Model);
                viewModel.ViewModel.Item.PaymentConditionSet = new PaymentConditionSetWrapper(Groups.SelectedGroup.PaymentConditionSet.Model);
                viewModel.ViewModel.Item.ProductionTerm = Groups.SelectedGroup.ProductionTerm;
                viewModel.ViewModel.Item.Product = new ProductWrapper(Groups.SelectedGroup.Product.Model);

                //создаем зависимое оборудование
                foreach (var prodIncl in Groups.SelectedGroup.ProductsIncluded)
                {
                    var pi = new ProductIncluded { Product = prodIncl.Model.Product, Amount = prodIncl.Model.Amount };
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
            Groups.SelectedGroup = group;
        }

        #endregion

        protected override DateTime GetPriceDate(OfferUnitsGroup @group)
        {
            if(@group.Offer == null) return DateTime.Today;
            return @group.Offer.Date < DateTime.Today ? @group.Offer.Date : DateTime.Today;
        }
    }
}
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
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitsGroupsViewModel : 
        BaseGroupsViewModel<SalesUnitsWrappersGroup, SalesUnitsWrappersGroup, SalesUnit, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent>, IGroupsViewModel<SalesUnit, ProjectWrapper>
    {
        private ProjectWrapper _projectWrapper;

        public SalesUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<SalesUnitsWrappersGroup> Grouping(IEnumerable<SalesUnit> units)
        {
            return units.GroupBy(x => x, new SalesUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new SalesUnitsWrappersGroup(x.ToList())).ToList();
        }

        public void Load(IEnumerable<SalesUnit> units, ProjectWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            Load(units, unitOfWork, isNew);
            _projectWrapper = parentWrapper;
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

            var group = new SalesUnitsWrappersGroup(units.ToList());
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


    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class SalesUnitsSpecificationBase : 
        BaseContainerViewModelWithFilter<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent, Specification, SelectedSpecificationChangedEvent, SpecificationView>
    {
        public SalesUnitsSpecificationBase(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<SalesUnitLookup> GetActualLookups(Specification specification)
        {
            return AllLookups.Where(lookup => lookup.Specification != null && lookup.Specification?.Id == specification.Id);
        }

        protected override bool CanBeShown(SalesUnit calculation)
        {
            return 
                Filter != null && 
                calculation.Specification != null && 
                Filter.Id == calculation.Specification.Id;
        }
        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        /// <summary>
        /// Реакция на сохранение строки проекта
        /// </summary>
        /// <param name="salesUnit"></param>
        protected override void OnAfterSaveItemEvent(SalesUnit salesUnit)
        {
            base.OnAfterSaveItemEvent(salesUnit);
            if (CanBeShown(salesUnit))
            {
                RefreshGroups();
            }
        }

        /// <summary>
        /// Реакция на удаление строки проекта
        /// </summary>
        /// <param name="salesUnit"></param>
        protected override void OnAfterRemoveItemEvent(SalesUnit salesUnit)
        {
            base.OnAfterRemoveItemEvent(salesUnit);
            if (CanBeShown(salesUnit))
            {
                RefreshGroups();
            }
        }

        protected override void OnFilterChanged(Specification filter)
        {
            base.OnFilterChanged(filter);
            RefreshGroups();
        }

        protected override IEnumerable<SalesUnitLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return ((ISalesUnitRepository)unitOfWork.Repository<SalesUnit>())
                .GetAllOfCurrentUser()
                .Select(salesUnit => new SalesUnitLookup(salesUnit));
        }

        protected void RefreshGroups()
        {
            //очистка отображаемых групп
            Groups.Clear();

            //если нет фильтра - ничего не отображаем
            if (Filter == null) return;

            var salesUnits = GetActualLookups(Filter).Select(salesUnitLookup => salesUnitLookup.Entity);

            //группируем их
            var groups = salesUnits.GroupBy(salesUnit => salesUnit, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsGroup(x));

            //обновляем вид
            Groups.AddRange(groups.OrderByDescending(salesUnitsGroup => salesUnitsGroup.Total));
        }
    }
}
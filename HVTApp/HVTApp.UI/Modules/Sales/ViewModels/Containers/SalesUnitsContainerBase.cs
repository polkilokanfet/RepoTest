using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public abstract class SalesUnitsContainerBase<TFilt, TSelectedFiltChangedEvent> : 
        BaseContainerFilt <SalesUnit, SalesUnitLookup, SelectedSalesUnitChangedEvent, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent, TFilt, TSelectedFiltChangedEvent> 
        where TFilt : class, IBaseEntity 
        where TSelectedFiltChangedEvent : PubSubEvent<TFilt>, new()
    {
        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        protected SalesUnitsContainerBase(IUnityContainer container) : base(container)
        {
        }

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

        protected override void OnFilterChanged(TFilt filter)
        {
            base.OnFilterChanged(filter);
            RefreshGroups();
        }

        protected override IEnumerable<SalesUnitLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return ((ISalesUnitRepository)unitOfWork.Repository<SalesUnit>())
                .GetUsersSalesUnits()
                .Select(x => new SalesUnitLookup(x));
        }

        protected void RefreshGroups()
        {
            //очистка отображаемых групп
            Groups.Clear();

            //если нет фильтра - ничего не отображаем
            if (Filter == null) return;

            var salesUnits = GetActualLookups(Filter).Select(x => x.Entity);

            //группируем их
            var groups = salesUnits.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsGroup(x));

            //обновляем вид
            Groups.AddRange(groups.OrderByDescending(x => x.Total));
        }
    }
}
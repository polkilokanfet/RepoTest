using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Comparers;
using HVTApp.UI.Groups;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract class SalesUnitsContainerBase<TFilt, TSelectedFiltChangedEvent> : 
        BaseContainerFilt <SalesUnit, SalesUnitLookup, SelectedSalesUnitChangedEvent, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent, TFilt, TSelectedFiltChangedEvent> 
        where TFilt : class, IBaseEntity 
        where TSelectedFiltChangedEvent : PubSubEvent<TFilt>, new()
    {
        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        protected SalesUnitsContainerBase(IUnityContainer container) : base(container)
        {
            //реакция на смену фильтра
            container.Resolve<IEventAggregator>().GetEvent<TSelectedFiltChangedEvent>().Subscribe(filt =>
            {
                RefreshGroups();
            });

            //реакция на сохранение/удаление строки проекта
            this.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add || args.Action == NotifyCollectionChangedAction.Remove)
                {
                    RefreshGroups();
                }
            };

            container.Resolve<IEventAggregator>().GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                if (this.ContainsById(salesUnit))
                {
                    RefreshGroups();
                }
            });
        }

        protected override IEnumerable<SalesUnitLookup> GetLookups(IUnitOfWorkDisplay unitOfWork)
        {
            return unitOfWork.Repository<SalesUnit>()
                .Find(x => x.Project.Manager.IsAppCurrentUser())
                .Select(x => new SalesUnitLookup(x));
        }

        protected void RefreshGroups()
        {
            Groups.Clear();

            if (Filt == null) return;

            var salesUnits = GetActualLookups(Filt).Select(x => x.Entity);

            //группируем их
            var groups = salesUnits.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsGroup(x));

            //обновляем вид
            Groups.AddRange(groups.OrderByDescending(x => x.Total));
        }
    }
}
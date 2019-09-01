using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
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
        private TFilt _filt;

        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        protected SalesUnitsContainerBase(IUnityContainer container) : base(container)
        {
            // реакция на смену
            container.Resolve<IEventAggregator>().GetEvent<TSelectedFiltChangedEvent>().Subscribe(x =>
            {
                _filt = x;
                RefreshGroups(_filt);
            });

            // реакция на сохранение строки проекта
            container.Resolve<IEventAggregator>().GetEvent<AfterSaveSalesUnitEvent>().Subscribe(x =>
            {
                if (_filt.Id != x.Project.Id) return;

                AllItems.ReAddById(x);
                RefreshGroups(_filt);
            });

            // реакция на удаление строки проекта
            container.Resolve<IEventAggregator>().GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(x =>
            {
                if (_filt.Id != x.Project.Id) return;

                AllItems.RemoveById(x);
                RefreshGroups(_filt);
            });

        }

        protected override IEnumerable<SalesUnit> GetItems(IUnitOfWorkDisplay unitOfWork)
        {
            return unitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
        }

        private void RefreshGroups(TFilt filt)
        {
            Groups.Clear();

            if (filt == null) return;

            var salesUnits = GetActualLookups(filt).Select(x => x.Entity);

            //группируем их
            var groups = salesUnits.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsGroup(x));

            //обновляем вид
            Groups.AddRange(groups.OrderByDescending(x => x.Total));
        }
    }

    public class SalesUnitsProjectBase : SalesUnitsContainerBase<Project, SelectedProjectChangedEvent>
    {
        public SalesUnitsProjectBase(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<SalesUnitLookup> GetActualLookups(Project project)
        {
            return AllItems.Where(x => x.Project.Id == project.Id).Select(x => new SalesUnitLookup(x));
        }
    }

    public class SalesUnitsSpecificetionBase : SalesUnitsContainerBase<Specification, SelectedSpecificationChangedEvent>
    {
        public SalesUnitsSpecificetionBase(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<SalesUnitLookup> GetActualLookups(Specification specification)
        {
            return AllItems.Where(x => x.Specification?.Id == specification.Id).Select(x => new SalesUnitLookup(x));
        }
    }

}
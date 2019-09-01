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
    public class SalesUnitsContainer : BaseContainerProjectReact <SalesUnit, SalesUnitLookup, SelectedSalesUnitChangedEvent, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent>
    {
        private Project _project;

        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        public SalesUnitsContainer(IUnityContainer container) : base(container)
        {
            // Реакция на смену выбранного проекта
            container.Resolve<IEventAggregator>().GetEvent<SelectedProjectChangedEvent>().Subscribe(x =>
            {
                _project = x;
                RefreshGroups(_project);
            });

            container.Resolve<IEventAggregator>().GetEvent<AfterSaveSalesUnitEvent>().Subscribe(x =>
            {
                if (_project.Id != x.Project.Id) return;

                AllItems.ReAddById(x);
                RefreshGroups(_project);
            });

            container.Resolve<IEventAggregator>().GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(x =>
            {
                if (_project.Id != x.Project.Id) return;

                AllItems.RemoveById(x);
                RefreshGroups(_project);
            });

        }

        protected override IEnumerable<SalesUnit> GetItems(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
        }

        protected override IEnumerable<SalesUnitLookup> GetActualForProjectLookups(Project project)
        {
            return AllItems.Where(x => x.Project.Id == project.Id).Select(x => new SalesUnitLookup(x));
        }

        private void RefreshGroups(Project project)
        {
            Groups.Clear();

            if (project == null) return;

            var salesUnits = AllItems.Where(x => x.Project.Id == project.Id).ToList();

            //группируем их
            var groups = salesUnits.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsGroup(x));

            //обновляем вид
            Groups.AddRange(groups.OrderByDescending(x => x.Total));
        }
    }
}
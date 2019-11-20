using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectItem : BindableBase
    {
        public readonly ObservableCollection<SalesUnit> SalesUnits;
        public ObservableCollection<ProjectUnitsGroup> ProjectUnitsGroups { get; } = new ObservableCollection<ProjectUnitsGroup>();

        public Project Project => SalesUnits.First().Project;
        public IEnumerable<Facility> Facilities => SalesUnits.Select(x => x.Facility).Distinct();
        public string Name => SalesUnits.First().Project.Name;
        public double Sum => SalesUnits.Sum(x => x.Cost);
        public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
        public DateTime RealizationDate => SalesUnits.First().RealizationDateCalculated;
        public ProjectType ProjectType => SalesUnits.First().Project.ProjectType;
        public bool IsDone => SalesUnits.All(x => x.IsDone);
        public bool IsLoosen => SalesUnits.All(x => x.IsLoosen);
        public bool ForReport => SalesUnits.First().Project.ForReport;
        public bool InWork => SalesUnits.First().Project.InWork;

        //<infgDp:DateTimeField Name = "TenderDate" Label="Тендер" Width="Auto" />
        //<infgDp:TextField Name = "Builder" Label="Подрядчик" Width="Auto" Converter="{StaticResource LookupToStringConverter}"/>
        //<infgDp:TextField Name = "ProjectMaker" Label="Проектировщик" Width="Auto" Converter="{StaticResource LookupToStringConverter}"/>
        //<infgDp:TextField Name = "Sypplier" Label="Поставщик" Width="Auto" Converter="{StaticResource LookupToStringConverter}"/>


        public ProjectItem(IEnumerable<SalesUnit> salesUnits)
        {
            SalesUnits = new ObservableCollection<SalesUnit>(salesUnits);
            RefreshGroups();

            SalesUnits.CollectionChanged += (sender, args) =>
            {
                RefreshGroups();
                if(SalesUnits.Any())
                    OnPropertyChanged(string.Empty);
            };
        }

        private void RefreshGroups()
        {
            ProjectUnitsGroups.Clear();
            var salesUnitsGroups = SalesUnits.GroupBy(x => new
            {
                x.Product.Id,
                x.Cost,
                x.Facility
            }).OrderByDescending(x => x.Key.Cost);
            ProjectUnitsGroups.AddRange(salesUnitsGroups.Select(x => new ProjectUnitsGroup(x)));
        }
    }
}
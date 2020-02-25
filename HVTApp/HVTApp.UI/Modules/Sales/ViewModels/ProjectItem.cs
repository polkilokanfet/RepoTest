using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProjectItem : BindableBase
    {
        private Project _project;
        private Company _builder;
        private Company _projectMaker;
        private Company _supplier;
        private DateTime? _tenderDate;

        public SalesUnitsCollection SalesUnits { get; }

        public IEnumerable<ProjectUnitsGroup> ProjectUnitsGroups
        {
            get
            {
                return SalesUnits
                    .GroupBy(x => new
                    {
                        ProductId = x.Product.Id,
                        x.Cost,
                        FacilityId = x.Facility.Id,
                        x.OrderInTakeDate,
                        x.RealizationDateCalculated
                    })
                    .OrderByDescending(x => x.Key.Cost)
                    .Select(x => new ProjectUnitsGroup(x));
            }
        }

        public Project Project
        {
            get { return _project; }
            set
            {
                _project = value;
                OnPropertyChanged(nameof(this.InWork));
                OnPropertyChanged(nameof(this.ForReport));
                OnPropertyChanged();
            }
        }

        public IEnumerable<string> Facilities => SalesUnits.Select(x => x.Facility.ToString()).Distinct();
        public double Sum => SalesUnits.Sum(x => x.Cost);

        public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
        public int OrderInTakeYear => OrderInTakeDate.Year;
        public string OrderInTakeMonth => OrderInTakeDate.MonthName();

        public bool IsDone => SalesUnits.All(x => x.IsDone);
        public bool IsLoosen => SalesUnits.All(x => x.IsLoosen);
        public bool ForReport => Project != null && Project.ForReport;
        public bool InWork => Project != null && Project.InWork;

        /// <summary>
        /// Дата ближайшего тендера на поставку
        /// </summary>
        public DateTime? TenderDate
        {
            get { return _tenderDate; }
            set
            {
                _tenderDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Подрядчик
        /// </summary>
        public Company Builder
        {
            get { return _builder; }
            set
            {
                if (Equals(_builder?.Id, value?.Id)) return;
                _builder = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Проектировщик
        /// </summary>
        public Company ProjectMaker
        {
            get { return _projectMaker; }
            set
            {
                if (Equals(_projectMaker?.Id, value?.Id)) return;
                _projectMaker = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Поставщик
        /// </summary>
        public Company Supplier
        {
            get { return _supplier; }
            set
            {
                if (Equals(_supplier?.Id, value?.Id)) return;
                _supplier = value;
                OnPropertyChanged();
            }
        }

        public ProjectItem(IEnumerable<SalesUnit> salesUnits)
        {
            SalesUnits = new SalesUnitsCollection(salesUnits);
            Project = SalesUnits.First().Project;

            SalesUnits.CollectionChanged += () =>
            {
                if (!SalesUnits.Any()) return;

                OnPropertyChanged(nameof(this.Facilities));
                OnPropertyChanged(nameof(this.Sum));
                OnPropertyChanged(nameof(this.OrderInTakeDate));
                OnPropertyChanged(nameof(this.OrderInTakeYear));
                OnPropertyChanged(nameof(this.OrderInTakeMonth));
                OnPropertyChanged(nameof(this.IsLoosen));
                OnPropertyChanged(nameof(this.IsDone));
                OnPropertyChanged(nameof(this.InWork));
                OnPropertyChanged(nameof(this.ForReport));

                OnPropertyChanged(nameof(this.ProjectUnitsGroups));
            };
        }

        public void RefreshTenderInformation(IEnumerable<Tender> tenders)
        {
            Builder = tenders.GetWinner(TenderTypeEnum.ToWork);
            ProjectMaker = tenders.GetWinner(TenderTypeEnum.ToProject);
            Supplier = tenders.GetWinner(TenderTypeEnum.ToSupply);

            var supplyTenders = tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).ToList();
            TenderDate = !supplyTenders.Any() ? null : supplyTenders.OrderBy(x => x.DateClose).Last()?.DateClose;
        }

        /// <summary>
        /// Юнит подходит в этот айтем
        /// </summary>
        /// <param name="salesUnit">Юнит</param>
        /// <returns></returns>
        public bool Fits(SalesUnit salesUnit)
        {
            var units = SalesUnits.Where(x => x.Id != salesUnit.Id).Concat(new[] { salesUnit });
            return units.GroupBy(x => x, new SalesUnitsComparer()).Count() == 1;
        }

    }
}
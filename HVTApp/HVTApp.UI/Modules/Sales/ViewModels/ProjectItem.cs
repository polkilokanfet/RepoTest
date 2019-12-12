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
        public readonly ObservableCollection<Tender> Tenders;
        readonly List<SalesUnit> _salesUnits;
        public ObservableCollection<ProjectUnitsGroup> ProjectUnitsGroups { get; } = new ObservableCollection<ProjectUnitsGroup>();

        public Project Project { get; private set; }
        public IEnumerable<string> Facilities => _salesUnits.Select(x => x.Facility.ToString()).Distinct();
        public double Sum => _salesUnits.Sum(x => x.Cost);
        public DateTime OrderInTakeDate => _salesUnits.First().OrderInTakeDate;
        public DateTime RealizationDate => _salesUnits.First().RealizationDateCalculated;
        public ProjectType ProjectType => _salesUnits.First().Project.ProjectType;
        public bool IsDone => _salesUnits.All(x => x.IsDone);
        public bool IsLoosen => _salesUnits.All(x => x.IsLoosen);
        public bool ForReport => Project.ForReport;
        public bool InWork => Project.InWork;
        public int OrderInTakeYear => OrderInTakeDate.Year;
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        public DateTime? TenderDate
        {
            get
            {
                if (!Tenders.Any()) return null;
                var supply = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).ToList();
                return !supply.Any() ? null : supply.OrderBy(x => x.DateClose).Last()?.DateClose;
            }
        }

        public Company Builder
        {
            get
            {
                if (Tenders.Any())
                {
                    var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork)).OrderBy(x => x.DateClose);
                    return tenders.LastOrDefault()?.Winner;
                }
                return null;
            }
        }

        public Company ProjectMaker
        {
            get
            {
                if (Tenders.Any())
                {
                    var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToProject)).OrderBy(x => x.DateClose);
                    return tenders.LastOrDefault()?.Winner;
                }
                return null;
            }
        }

        public Company Sypplier
        {
            get
            {
                if (Tenders.Any())
                {
                    var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).OrderBy(x => x.DateClose);
                    return tenders.LastOrDefault()?.Winner;
                }
                return null;
            }
        }

        public ProjectItem(IEnumerable<SalesUnit> salesUnits, IEnumerable<Tender> tenders, IEventAggregator eventAggregator)
        {
            _salesUnits = new List<SalesUnit>(salesUnits);
            Tenders = new ObservableCollection<Tender>(tenders);
            Project = _salesUnits.First().Project;
            RefreshGroups();

            Tenders.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(this.TenderDate));
                OnPropertyChanged(nameof(this.Builder));
                OnPropertyChanged(nameof(this.ProjectMaker));
                OnPropertyChanged(nameof(this.Sypplier));
            };

            eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(project =>
            {
                if (Project.Id != project.Id) return;
                Project = project;
                OnPropertyChanged(string.Empty);
            });

            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
            {
                if (Project.Id == tender.Project.Id)
                    Tenders.ReAddById(tender);
            });

        }

        /// <summary>
        /// Юнит подходит в этот айтем
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <returns></returns>
        public bool Fits(SalesUnit salesUnit)
        {
            var units = _salesUnits.Where(x => x.Id != salesUnit.Id).Concat(new[] { salesUnit });
            return units.GroupBy(x => x, new SalesUnitsComparer()).Count() == 1;
        }

        public bool ContainsById(SalesUnit salesUnit)
        {
            return _salesUnits.ContainsById(salesUnit);
        }

        public void AddSalesUnit(SalesUnit salesUnit)
        {
            //заменяемый юнит
            var oldSalesUnit = _salesUnits.SingleOrDefault(x => x.Id == salesUnit.Id);

            _salesUnits.Add(salesUnit);

            //удаляем замененный юнит
            if (oldSalesUnit != null)
                _salesUnits.Remove(oldSalesUnit);

            //обновляем айтем
            RefreshItem();
        }

        public ProjectItemState RemoveSalesUnit(SalesUnit salesUnit)
        {
            _salesUnits.RemoveById(salesUnit);

            if (_salesUnits.Any())
            {
                RefreshItem();
                return ProjectItemState.HasAnySalesUnit;
            }

            return ProjectItemState.HasNoSalesUnit;
        }

        private void RefreshItem()
        {
            RefreshGroups();
            OnPropertyChanged(string.Empty);
        }

        private void RefreshGroups()
        {
            ProjectUnitsGroups.Clear();
            var salesUnitsGroups = _salesUnits.GroupBy(x => new
            {
                ProductId = x.Product.Id,
                x.Cost,
                FacilityId = x.Facility.Id
            }).OrderByDescending(x => x.Key.Cost);
            ProjectUnitsGroups.AddRange(salesUnitsGroups.Select(x => new ProjectUnitsGroup(x)));
        }
    }

    public enum ProjectItemState
    {
        HasAnySalesUnit,
        HasNoSalesUnit
    }
}
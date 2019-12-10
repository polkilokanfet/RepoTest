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
        private bool _reAddInProcess = false;

        public readonly ObservableCollection<Tender> Tenders;
        public readonly ObservableCollection<SalesUnit> SalesUnits;
        public ObservableCollection<ProjectUnitsGroup> ProjectUnitsGroups { get; } = new ObservableCollection<ProjectUnitsGroup>();

        public Project Project { get; private set; }
        public IEnumerable<string> Facilities => SalesUnits.Select(x => x.Facility.ToString()).Distinct();
        public double Sum => SalesUnits.Sum(x => x.Cost);
        public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
        public DateTime RealizationDate => SalesUnits.First().RealizationDateCalculated;
        public ProjectType ProjectType => SalesUnits.First().Project.ProjectType;
        public bool IsDone => SalesUnits.All(x => x.IsDone);
        public bool IsLoosen => SalesUnits.All(x => x.IsLoosen);
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

        /// <summary>
        /// Добавлен уже существующий юнит (не новый) в этот айтем
        /// </summary>
        public event Action<ProjectItem, SalesUnit> AddedOldSalesUnit;

        public event Action<ProjectItem> RemovedLastSalesUnit;

        public event Action<SalesUnit> RemovedSalesUnitToAddToAnotherProjectItem;

        public ProjectItem(IEnumerable<SalesUnit> salesUnits, IEnumerable<Tender> tenders, IEventAggregator eventAggregator)
        {
            SalesUnits = new ObservableCollection<SalesUnit>(salesUnits);
            SalesUnits.CollectionChanged += (sender, args) =>
            {
                if(!SalesUnits.Any() && !_reAddInProcess)
                    RemovedLastSalesUnit?.Invoke(this);
            };

            Tenders = new ObservableCollection<Tender>(tenders);
            Project = SalesUnits.First().Project;
            RefreshGroups();

            SalesUnits.CollectionChanged += (sender, args) =>
            {
                RefreshGroups();
                if(SalesUnits.Any())
                    OnPropertyChanged(string.Empty);
            };

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

            eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                SalesUnits.RemoveIfContainsById(salesUnit);
            });

            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                if (!SalesUnits.ContainsById(salesUnit)) return;

                //если юнит подходит этой группе
                if (SalesUnits.Count == 1 || new SalesUnitsComparer().Equals(salesUnit, SalesUnits.First()))
                {                    
                    if (SalesUnits.ContainsById(salesUnit))
                    {
                        _reAddInProcess = true;

                        SalesUnits.RemoveById(salesUnit);
                        SalesUnits.Add(salesUnit);
                        AddedOldSalesUnit?.Invoke(this, salesUnit);

                        _reAddInProcess = false;
                    }
                    else
                    {
                        SalesUnits.Add(salesUnit);
                    }

                    return;
                }

                //если юнит не подходит этой группе
                SalesUnits.RemoveById(salesUnit);
                RemovedSalesUnitToAddToAnotherProjectItem?.Invoke(salesUnit);
            });

            eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(tender =>
            {
                Tenders.RemoveIfContainsById(tender);
            });

            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
            {
                if (Project.Id == tender.Project.Id)
                    Tenders.ReAddById(tender);
            });

        }

        private void RefreshGroups()
        {
            ProjectUnitsGroups.Clear();
            var salesUnitsGroups = SalesUnits.GroupBy(x => new
            {
                ProductId = x.Product.Id,
                x.Cost,
                FacilityId = x.Facility.Id
            }).OrderByDescending(x => x.Key.Cost);
            ProjectUnitsGroups.AddRange(salesUnitsGroups.Select(x => new ProjectUnitsGroup(x)));
        }
    }
}
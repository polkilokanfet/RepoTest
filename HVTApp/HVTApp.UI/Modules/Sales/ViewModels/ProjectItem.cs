using System;
using System.Collections;
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
    public class SalesUnitsCollection : IList<SalesUnit>
    {
        private readonly List<SalesUnit> _salesUnits;

        public SalesUnitsCollection(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = new List<SalesUnit>(salesUnits);
        }

        public IEnumerator<SalesUnit> GetEnumerator()
        {
            return _salesUnits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(SalesUnit salesUnit)
        {
            if (_salesUnits.Contains(salesUnit))
                return;

            //заменяемый юнит
            var oldSalesUnit = _salesUnits.SingleOrDefault(x => x.Id == salesUnit.Id);

            _salesUnits.Add(salesUnit);

            //удаляем замененный юнит
            if (oldSalesUnit != null)
                _salesUnits.Remove(oldSalesUnit);

            CollectionChanged?.Invoke();
        }

        public void Clear()
        {
            _salesUnits.Clear();
        }

        public bool Contains(SalesUnit item)
        {
            return _salesUnits.Contains(item);
        }

        public void CopyTo(SalesUnit[] array, int arrayIndex)
        {
            _salesUnits.CopyTo(array, arrayIndex);
        }

        public bool Remove(SalesUnit salesUnit)
        {
            var result = _salesUnits.Remove(salesUnit);
            CollectionChanged?.Invoke();
            return result;
        }

        public int Count => _salesUnits.Count;
        public bool IsReadOnly => false;
        public int IndexOf(SalesUnit salesUnit)
        {
            return _salesUnits.IndexOf(salesUnit);
        }

        public void Insert(int index, SalesUnit salesUnit)
        {
            _salesUnits.Insert(index, salesUnit);
        }

        public void RemoveAt(int index)
        {
            _salesUnits.RemoveAt(index);
        }

        public SalesUnit this[int index]
        {
            get { return _salesUnits[index]; }
            set { _salesUnits[index] = value; }
        }

        public event Action CollectionChanged;
    }

    public class ProjectItem : BindableBase
    {
        public readonly ObservableCollection<Tender> Tenders;
        public readonly SalesUnitsCollection SalesUnits;
        public ObservableCollection<ProjectUnitsGroup> ProjectUnitsGroups { get; } = new ObservableCollection<ProjectUnitsGroup>();

        public Project Project { get; private set; }
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
            get
            {
                if (!Tenders.Any()) return null;
                var supply = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).ToList();
                return !supply.Any() ? null : supply.OrderBy(x => x.DateClose).Last()?.DateClose;
            }
        }
        /// <summary>
        /// Подрядчик
        /// </summary>
        public Company Builder => Tenders.GetWinner(TenderTypeEnum.ToWork);
        /// <summary>
        /// Проектировщик
        /// </summary>
        public Company ProjectMaker => Tenders.GetWinner(TenderTypeEnum.ToProject);
        /// <summary>
        /// Поставщик
        /// </summary>
        public Company Supplier => Tenders.GetWinner(TenderTypeEnum.ToSupply);

        public ProjectItem(IEnumerable<SalesUnit> salesUnits, IEnumerable<Tender> tenders, IEventAggregator eventAggregator)
        {
            SalesUnits = new SalesUnitsCollection(salesUnits);
            Tenders = new ObservableCollection<Tender>(tenders);
            Project = SalesUnits.First().Project;
            RefreshGroups();

            //реакция на изменение проекта
            eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(project =>
            {
                if (!SalesUnits.Any()) return;

                if (Project.Id != project.Id) return;
                Project = project;
                OnPropertyChanged(string.Empty);
            });

            //реакция на изменение тендера
            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
            {
                if (!SalesUnits.Any()) return;

                if (Project.Id == tender.Project.Id)
                    Tenders.ReAddById(tender);
            });

            //реакция на изменение коллекции тендеров
            Tenders.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(this.TenderDate));
                OnPropertyChanged(nameof(this.Builder));
                OnPropertyChanged(nameof(this.ProjectMaker));
                OnPropertyChanged(nameof(this.Supplier));
            };

            SalesUnits.CollectionChanged += () =>
            {
                if (!SalesUnits.Any()) return;

                OnPropertyChanged(nameof(this.Sum));
                OnPropertyChanged(nameof(this.OrderInTakeDate));
                OnPropertyChanged(nameof(this.OrderInTakeYear));
                OnPropertyChanged(nameof(this.OrderInTakeMonth));
                OnPropertyChanged(nameof(this.IsLoosen));
                OnPropertyChanged(nameof(this.IsDone));
                OnPropertyChanged(nameof(this.InWork));
                OnPropertyChanged(nameof(this.ForReport));

                RefreshGroups();
            };
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

        public bool ContainsById(SalesUnit salesUnit)
        {
            return SalesUnits.ContainsById(salesUnit);
        }

        private void RefreshGroups()
        {
            ProjectUnitsGroups.Clear();
            var salesUnitsGroups = SalesUnits.GroupBy(x => new
            {
                ProductId = x.Product.Id,
                x.Cost,
                FacilityId = x.Facility.Id,
                x.OrderInTakeDate,
                x.RealizationDateCalculated
            }).OrderByDescending(x => x.Key.Cost);
            ProjectUnitsGroups.AddRange(salesUnitsGroups.Select(x => new ProjectUnitsGroup(x)));
        }
    }
}
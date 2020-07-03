using System;
using System.Collections.Generic;
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

        public static List<Tender> AllTenders { get; } = new List<Tender>();
        private IEnumerable<Tender> Tenders => AllTenders.Where(x => x.Project.Id == Project.Id);

        public SalesUnitsCollection SalesUnits { get; }

        public IEnumerable<ProjectUnitsGroup> ProjectUnitsGroups
        {
            get
            {
                return SalesUnits
                    .GroupBy(x => new
                    {
                        ProductId = x.Product.Id,
                        Cost = x.Cost,
                        FacilityId = x.Facility.Id,
                        OrderInTakeDate = x.OrderInTakeDate,
                        RealizationDateCalculated = x.RealizationDateCalculated
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

        #region OrderInTakeRegion

        public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
        public int OrderInTakeYear => OrderInTakeDate.Year;
        public string OrderInTakeMonth => OrderInTakeDate.MonthName();

        #endregion

        #region CheckRegion

        public bool IsDone => SalesUnits.All(x => x.IsDone);
        public bool IsLoosen => SalesUnits.All(x => x.IsLoosen);
        public bool ForReport => Project != null && Project.ForReport;
        public bool InWork => Project != null && Project.InWork;

        #endregion

        #region TenderInfoRegion

        /// <summary>
        /// Дата ближайшего тендера на поставку
        /// </summary>
        public DateTime? TenderDate
        {
            get
            {
                var supplyTenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).ToList();
                return !supplyTenders.Any() ? null : supplyTenders.OrderBy(x => x.DateClose).Last()?.DateClose;
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

        private void RefreshTenderInformation()
        {
            OnPropertyChanged(nameof(TenderDate));
            OnPropertyChanged(nameof(Builder));
            OnPropertyChanged(nameof(ProjectMaker));
            OnPropertyChanged(nameof(Supplier));
        }

        #endregion

        /// <summary>
        /// В группе не осталось юнитов
        /// </summary>
        public event Action<ProjectItem> LastSalesUnitRemoveEvent; 

        public ProjectItem(IEnumerable<SalesUnit> salesUnits, IEventAggregator eventAggregator)
        {
            SalesUnits = new SalesUnitsCollection(salesUnits);
            Project = SalesUnits.First().Project;

            //если коллекция изменилась, обновляем все отображаемые поля
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

            SalesUnits.CollectionIsEmptyEvent += () =>
            {
                LastSalesUnitRemoveEvent?.Invoke(this);
            };

            //реакция на изменение проекта
            eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(project =>
            {
                if (Project.Id == project.Id)
                    Project = project;
            });

            //реакция на удаление тендера
            eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(tender =>
            {
                AllTenders.RemoveIfContainsById(tender);
                RefreshTenderInformation();
            });

            //реакция на сохранение тендера
            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
            {
                if (tender.Project.Id == Project.Id)
                {
                    AllTenders.ReAddById(tender);
                    RefreshTenderInformation();
                }
            });

            //реакция на удаления юнита
            eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                if (SalesUnits.ContainsById(salesUnit))
                {
                    SalesUnits.Remove(salesUnit);
                }
            });
        }

        public void Check(SalesUnit salesUnit)
        {
            if (!SalesUnits.Any())
                return;

            //Юнит подходит в этот айтем
            var fits =  SalesUnits
                //.Where(x => x.Id != salesUnit.Id)
                .Concat(new[] { salesUnit })
                .GroupBy(x => x, new SalesUnitsComparer())
                .Count() == 1;

            //если юнит подходит этой группе
            if (fits)
            {
                //добавляем/обновляем его
                SalesUnits.Add(salesUnit);
            }
            //если не подходит
            else
            {
                if (SalesUnits.ContainsById(salesUnit))
                    //удаляем его
                    SalesUnits.Remove(salesUnit);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Market.Items
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
                    .Select(x => new ProjectUnitsGroup(x, this));
            }
        }

        public Project Project
        {
            get => _project;
            set
            {
                _project = value;
                OnPropertyChanged(nameof(this.InWork));
                OnPropertyChanged(nameof(this.ForReport));
                OnPropertyChanged();
            }
        }

        public IEnumerable<Facility> Facilities => SalesUnits.Select(x => x.Facility).Distinct().OrderBy(x => x.Name);
        public double Sum => SalesUnits.Sum(x => x.Cost);

        public int? DaysToStartProduction
        {
            get
            {
                if (!SalesUnits.Any()) return null;

                var salesUnit = SalesUnits.First();
                if (salesUnit.IsLoosen || salesUnit.IsWon) return null;

                return (salesUnit.DeliveryDateExpected.AddDays(- salesUnit.ProductionTerm - salesUnit.DeliveryPeriodCalculated) - DateTime.Today).Days;
            }
        }

        #region OrderInTakeRegion

        public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
        public int OrderInTakeYear => OrderInTakeDate.Year;
        public string OrderInTakeMonth => OrderInTakeDate.MonthName();

        #endregion

        #region CheckRegion

        public bool IsDone => SalesUnits.All(salesUnit => salesUnit.IsDone);
        public bool IsLoosen => SalesUnits.All(salesUnit => salesUnit.IsLoosen);
        public bool ForReport => Project != null && Project.ForReport;
        public bool InWork => Project != null && Project.InWork;

        #endregion

        #region TenderInfoRegion

        /// <summary>
        /// ���� ���������� ������� �� ��������
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
        /// ���������
        /// </summary>
        public Company Builder => Tenders.GetWinner(TenderTypeEnum.ToWork);

        /// <summary>
        /// �������������
        /// </summary>
        public Company ProjectMaker => Tenders.GetWinner(TenderTypeEnum.ToProject);

        /// <summary>
        /// ���������
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
        /// � ������ �� �������� ������
        /// </summary>
        public event Action<ProjectItem> LastSalesUnitRemoveEvent;

        public string ProductsInProject => SalesUnits.Select(x => x.Product.Designation).Distinct().OrderBy(x => x).ToStringEnum();

        public IEnumerable<Company> FacilitiesOwners
        {
            get
            {
                var owners = this.Facilities.Select(x => x.OwnerCompany).ToList();
                var result = new List<Company>(owners);
                foreach (var owner in owners)
                {
                    result.AddRange(owner.ParentCompanies());
                }
                return result.Distinct().OrderBy(company => company.ShortName);
            }
        }

        public ProjectItem(IEnumerable<SalesUnit> salesUnits, IEventAggregator eventAggregator)
        {
            SalesUnits = new SalesUnitsCollection(salesUnits);
            Project = SalesUnits.First().Project;

            //���� ��������� ����������, ��������� ��� ������������ ����
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
                OnPropertyChanged(nameof(this.DaysToStartProduction));

                OnPropertyChanged(nameof(this.ProjectUnitsGroups));
            };

            SalesUnits.CollectionIsEmptyEvent += () =>
            {
                LastSalesUnitRemoveEvent?.Invoke(this);
            };

            //������� �� ��������� �������
            eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(project =>
            {
                if (Project.Id == project.Id)
                    Project = project;
            });

            //������� �� �������� �������
            eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(tender =>
            {
                AllTenders.RemoveIfContainsById(tender);
                RefreshTenderInformation();
            });

            //������� �� ���������� �������
            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
            {
                if (tender.Project.Id == Project.Id)
                {
                    AllTenders.ReAddById(tender);
                    RefreshTenderInformation();
                }
            });

            //������� �� �������� �����
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

            //���� �������� � ���� �����
            var fits =  SalesUnits
                //.Where(x => x.Id != salesUnit.Id)
                .Concat(new[] { salesUnit })
                .GroupBy(x => x, new SalesUnitsMarketViewComparer())
                .Count() == 1;

            //���� ���� �������� ���� ������
            if (fits)
            {
                //���������/��������� ���
                SalesUnits.Add(salesUnit);
            }
            //���� �� ��������
            else
            {
                if (SalesUnits.ContainsById(salesUnit))
                    //������� ���
                    SalesUnits.Remove(salesUnit);
            }
        }
    }
}
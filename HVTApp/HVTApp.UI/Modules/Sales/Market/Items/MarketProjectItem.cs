using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Market.Items
{
    public class MarketProjectItem : BindableBase
    {
        private bool _inWork;

        public IEnumerable<SalesUnit> SalesUnits { get; }

        public Project Project
        {
            get => _project;
            set
            {
                _project = value;
                RaisePropertyChanged();
            }
        }

        public string Facilities => SalesUnits.Select(salesUnit => salesUnit.Facility).ToDistinctOrderedString();

        public string FacilitiesOwners
        {
            get
            {
                var owners = SalesUnits
                    .Select(salesUnit => salesUnit.Facility)
                    .Select(facility => facility.OwnerCompany)
                    .ToList();
                var result = new List<Company>(owners);
                foreach (var owner in owners)
                {
                    result.AddRange(owner.ParentCompanies());
                }
                return result.Distinct().OrderBy(company => company.ShortName).ToStringEnum();
            }
        }

        public string ProductsInProject => this.Items
            .Select(salesUnit => salesUnit.Designation.Replace("-УЭТМ-", "-"))
            .ToDistinctOrderedString();

        public double Sum => SalesUnits.Sum(salesUnit => salesUnit.Cost);

        public int? DaysToStartProduction
        {
            get
            {
                if (!SalesUnits.Any()) return null;

                var salesUnit = SalesUnits.First();
                if (salesUnit.IsLoosen || salesUnit.IsWon) return null;

                return (salesUnit.DeliveryDateExpected.AddDays(-salesUnit.ProductionTerm - salesUnit.DeliveryPeriodCalculated) - DateTime.Today).Days;
            }
        }


        #region OrderInTakeRegion

        public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
        public int OrderInTakeYear => OrderInTakeDate.Year;
        public string OrderInTakeMonth => OrderInTakeDate.MonthName();

        #endregion

        #region CheckRegion

        public bool IsDone => SalesUnits.All(salesUnit => salesUnit.IsDone);
        public bool IsWon => SalesUnits.All(salesUnit => salesUnit.IsWon);
        public bool IsLoosen => SalesUnits.All(salesUnit => salesUnit.IsLoosen);

        public bool InWork
        {
            get => _inWork;
            set => SetProperty(ref _inWork, value);
        }

        #endregion

        #region TenderInfoRegion

        /// <summary>
        /// Дата ближайшего тендера на поставку
        /// </summary>
        public DateTime? TenderDate
        {
            get
            {
                var supplyTenders = Project.Tenders
                    .Where(tender => tender.DidNotTakePlace == false)
                    .Where(tender => tender.Types.Select(tenderType => tenderType.Type).Contains(TenderTypeEnum.ToSupply))
                    .ToList();

                return supplyTenders.Any() == false
                    ? null
                    : supplyTenders.OrderBy(tender => tender.DateClose).Last()?.DateClose;
            }
        }

        /// <summary>
        /// Подрядчик
        /// </summary>
        public Company Builder => Project.Tenders.GetWinner(TenderTypeEnum.ToWork);

        /// <summary>
        /// Проектировщик
        /// </summary>
        public Company ProjectMaker => Project.Tenders.GetWinner(TenderTypeEnum.ToProject);

        /// <summary>
        /// Поставщик
        /// </summary>
        public Company Supplier => Project.Tenders.GetWinner(TenderTypeEnum.ToSupply);

        private void RefreshTenderInformation()
        {
            RaisePropertyChanged(nameof(TenderDate));
            RaisePropertyChanged(nameof(Builder));
            RaisePropertyChanged(nameof(ProjectMaker));
            RaisePropertyChanged(nameof(Supplier));
        }

        #endregion


        private readonly ObservableCollection<MarketSalesUnitsItem> _items;
        private Project _project;

        public IEnumerable<MarketSalesUnitsItem> Items => _items;

        public MarketProjectItem(IEnumerable<SalesUnit> salesUnits)
        {
            SalesUnits = salesUnits.ToList();

            Project = SalesUnits.First().Project;

            var marketSalesUnitsItems = SalesUnits
                .GroupBy(salesUnit => salesUnit, new MarketSalesUnitsItem.Comparer())
                .Select(x => new MarketSalesUnitsItem(x, this));
            _items = new ObservableCollection<MarketSalesUnitsItem>(marketSalesUnitsItems);
        }

        public class Comparer : MarketViewBaseComparer
        {
            public override bool OtherEquals(SalesUnit first, SalesUnit second)
            {
                if (!Equals(first.OrderInTakeDate.Year, second.OrderInTakeDate.Year)) return false;
                if (!Equals(first.OrderInTakeDate.Month, second.OrderInTakeDate.Month)) return false;

                return true;
            }
        }
    }
}
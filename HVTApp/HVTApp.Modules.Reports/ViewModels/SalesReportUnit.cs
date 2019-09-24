using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Structures;

namespace HVTApp.Modules.Reports.ViewModels
{
    public class SalesReportUnit : SalesUnit
    {
        private readonly List<CountryUnion> _countryUnions;
        private readonly PriceStructures _priceStructures;
        private readonly List<Tender> _tenders;

        public string OrderNumber => Order?.Number;

        public Company FacilityOwnerHead
        {
            get
            {
                var head = FacilityOwner;
                while (head.ParentCompany != null)
                {
                    head = head.ParentCompany;
                }
                return head;
            }
        }
        public Company FacilityOwner => Facility.OwnerCompany;
        public Company Contragent => Specification?.Contract.Contragent;
        public string ContragentType
        {
            get
            {
                if (Contragent == null)
                    return "нет данных";

                if (Equals(FacilityOwner, Contragent) || FacilityOwner.ParentCompanies().Contains(Contragent))
                    return "конечный заказчик";

                var tender = _tenders.FirstOrDefault(x => Equals(x.Winner, Contragent));
                if (tender != null)
                {
                    var sb = new StringBuilder();
                    tender.Types.ForEach(x => sb.Append(x.Name).Append("; "));
                    return sb.ToString();
                }

                return "посредник";
            }
        }

        public Country Country
        {
            get
            {
                if (Facility.Address != null)
                    return Facility.Address.Locality.Region.District.Country;

                var company = FacilityOwner;
                while (company.ParentCompany != null && company.AddressLegal == null)
                {
                    company = company.ParentCompany;
                }

                return company.AddressLegal?.Locality.Region.District.Country;
            }
        }

        public List<CountryUnion> CountryUnions
        {
            get
            {
                return Country == null 
                    ? new List<CountryUnion>() 
                    : _countryUnions.Where(x => x.Countries.ContainsById(Country)).ToList();
            }
        }

        public District District => Facility.Address?.Locality.Region.District;
        public string Segment
        {
            get
            {
                var actEnums = new List<ActivityFieldEnum> {ActivityFieldEnum.Fuel,
                                                            ActivityFieldEnum.RailWay,
                                                            ActivityFieldEnum.ElectricityDistribution,
                                                            ActivityFieldEnum.ElectricityTransmission,
                                                            ActivityFieldEnum.ElectricityGeneration};
                foreach (var act in actEnums)
                {
                    var af = Facility.OwnerCompany.ActivityFilds.FirstOrDefault(x => Equals(x.ActivityFieldEnum, act));
                    if (af != null) return af.Name;
                }
                
                return string.Empty;
            }
        }
        public ProductType ProductType => Product.ProductType;
        public string Designation => Product.Designation;
        public string Status
        {
            get
            {
                if (StartProductionConditionsDoneDate.HasValue &&
                    StartProductionConditionsDoneDate.Value <= DateTime.Today)
                {
                    return "1 - Условие на запуск производства исполнено";
                }

                if (Specification != null)
                {
                    return Specification.Date <= DateTime.Today 
                        ? "2 - Контракт подписан" 
                        : "3 - Контракт на оформлении";
                }

                if (IsLoosen)
                {
                    return "15 - Проиграно другому производителю";
                }
                
                if (Producer != null && Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
                {
                    return "4 - Большая вероятность реализации";
                }

                return "7 - В проработке";
            }
        }
        public double? Vat => Specification?.Vat;

        public double PriceCalc
        {
            get
            {
                if (this.Price.HasValue) return this.Price.Value;
                return _priceStructures.TotalPriceFixedCostLess;
            }
        }

        public double FixedCost => _priceStructures.TotalFixedCost;

        /// <summary>
        /// Минимально возможная цена (продукты с фиксированной ценой + стоимость доставки)
        /// </summary>
        private double CostMin => CostDelivery.HasValue ? CostDelivery.Value + FixedCost : FixedCost;


        public double? MarginalIncome => Cost - CostMin <= 0 ? default(double?) : (1.0 - Price / (Cost - CostMin)) * 100.0;

        public string Voltage => Product.ProductBlock.Parameters.SingleOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;

        #region Fake

        public double CostResult => this.FakeData?.Cost ?? this.Cost;

        public DateTime OrderInTakeDateResult => this.FakeData?.OrderInTakeDate ?? this.OrderInTakeDate;

        public DateTime RealizationDateResult => this.FakeData?.RealizationDate ?? this.RealizationDateCalculated;

        public PaymentConditionSet PaymentConditionSetResult => this.FakeData?.PaymentConditionSet ?? this.PaymentConditionSet;

        #endregion

        public SalesReportUnit(
            SalesUnit salesUnit, 
            IEnumerable<Tender> tenders, 
            IEnumerable<ProductBlock> blocks,
            IEnumerable<CountryUnion> countryUnions)
        {
            SetProperties(salesUnit);
            _priceStructures = new PriceStructures(this, this.OrderInTakeDate, GlobalAppProperties.Actual.ActualPriceTerm, blocks);
            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();
        }

        private void SetProperties(SalesUnit salesUnit)
        {
            var properties = salesUnit.GetType().GetProperties().Where(x => x.CanWrite);
            foreach (var property in properties)
            {
                var value = property.GetValue(salesUnit);
                this.GetType().GetProperty(property.Name).SetValue(this, value);
            }
        }
    }
}
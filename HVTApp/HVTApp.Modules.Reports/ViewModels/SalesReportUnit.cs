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
        private readonly List<Tender> _tenders;

        public string OrderNumber { get; }
        public Company FacilityOwner { get; }
        public Company FacilityOwnerHead { get; }
        public Company Contragent { get; }
        public string ContragentType { get; }
        public Country Country { get; }
        public List<CountryUnion> CountryUnions { get; }

        public District District { get; }
        public string Segment { get; }

        public ProductType ProductType { get; }
        public string Designation { get; }
        public string Status { get; }

        public double? Vat { get; }

        public double PriceCalc { get; }

        public double FixedCost { get; }

        /// <summary>
        /// Минимально возможная цена (продукты с фиксированной ценой + стоимость доставки)
        /// </summary>
        private double FixedCostAndDelivery { get; }

        public double? MarginalIncome { get; }

        public string Voltage { get; }

        #region Fake

        public double CostResult { get; }

        public DateTime OrderInTakeDateResult { get; }

        public DateTime RealizationDateResult { get; }

        public PaymentConditionSet PaymentConditionSetResult { get; }

        #endregion

        public SalesReportUnit(
            SalesUnit salesUnit, 
            IEnumerable<Tender> tenders, 
            IEnumerable<ProductBlock> blocks,
            IEnumerable<CountryUnion> countryUnions)
        {
            SetProperties(salesUnit);

            var priceStructures = new PriceStructures(this, this.OrderInTakeDate, GlobalAppProperties.Actual.ActualPriceTerm, blocks);
            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();

            OrderNumber = Order?.Number;
            FacilityOwner = Facility.OwnerCompany;
            FacilityOwnerHead = GetFacilityOwnerHead();
            Contragent = Specification?.Contract.Contragent;
            ContragentType = GetContragentType();
            Country = GetCountry();
            CountryUnions = GetCountryUnions();
            District = Facility.Address?.Locality.Region.District;
            Segment = GetSegment();
            ProductType = Product.ProductType;
            Designation = Product.Designation;
            Status = GetStatus();
            Vat = Specification?.Vat;

            Voltage = Product.ProductBlock.Parameters.SingleOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;

            PriceCalc = Price ?? priceStructures.TotalPriceFixedCostLess;
            FixedCost = priceStructures.TotalFixedCost;
            FixedCostAndDelivery = CostDelivery.HasValue ? CostDelivery.Value + FixedCost : FixedCost;
            MarginalIncome = Cost - FixedCostAndDelivery <= 0 ? default(double?) : (1.0 - PriceCalc / (Cost - FixedCostAndDelivery)) * 100.0;

            CostResult = FakeData?.Cost ?? Cost;
            OrderInTakeDateResult = this.FakeData?.OrderInTakeDate ?? this.OrderInTakeDate;
            RealizationDateResult = this.FakeData?.RealizationDate ?? this.RealizationDateCalculated;
            PaymentConditionSetResult = this.FakeData?.PaymentConditionSet ?? this.PaymentConditionSet;
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

        private string GetContragentType()
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

        private Country GetCountry()
        {
            //страна по адресу объекта
            if (Facility.Address != null)
                return Facility.Address.Locality.Region.District.Country;

            //страна по адресу владельца объекта
            var company = FacilityOwner;
            while (company.ParentCompany != null && company.AddressLegal == null)
            {
                company = company.ParentCompany;
            }

            return company.AddressLegal?.Locality.Region.District.Country;
        }

        private List<CountryUnion> GetCountryUnions()
        {
            return Country == null
                ? new List<CountryUnion>()
                : _countryUnions.Where(x => x.Countries.ContainsById(Country)).ToList();
        }

        private string GetSegment()
        {
            //актуальный список сфер деятельности
            var actEnums = new List<ActivityFieldEnum>
            {
                ActivityFieldEnum.ElectricityDistribution,
                ActivityFieldEnum.ElectricityTransmission,
                ActivityFieldEnum.ElectricityGeneration,
                ActivityFieldEnum.Fuel,
                ActivityFieldEnum.RailWay
            };

            //сегмент по владельцам объекта
            var owner = Facility.OwnerCompany;
            do
            {
                var activityField = owner.ActivityFilds.FirstOrDefault(x => actEnums.Contains(x.ActivityFieldEnum));
                if (activityField != null) return activityField.Name;
                owner = owner.ParentCompany;
            } while (owner != null);

            return string.Empty;
        }

        private string GetStatus()
        {
            if (RealizationDateCalculated < DateTime.Today)
            {
                return "0 - Продукт реализован";
            }

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

        private Company GetFacilityOwnerHead()
        {
            var head = FacilityOwner;
            while (head.ParentCompany != null)
            {
                head = head.ParentCompany;
            }
            return head;
        }
    }
}
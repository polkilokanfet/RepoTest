using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesReportUnit : SalesUnit
    {
        public List<SalesUnit> SalesUnits { get; }
        private readonly List<CountryUnion> _countryUnions;
        private readonly List<Tender> _tenders;

        [Designation("Владелец объекта")]
        public Company FacilityOwner { get; }

        [Designation("Владелец объекта (головная компания)")]
        public Company FacilityOwnerHead { get; }

        [Designation("Контрагент")]
        public Company Contragent { get; }

        [Designation("Тип контрагента")]
        public string ContragentType { get; }

        [Designation("Регион")]
        public Region Region { get; }

        [Designation("Округ")]
        public District District { get; }

        [Designation("Страна поставки")]
        public Country Country { get; }

        [Designation("Объединения стран")]
        public List<CountryUnion> CountryUnions { get; }


        [Designation("Сегмент рынка")]
        public string Segment { get; }

        [Designation("Тип продукта")]
        public ProductType ProductType { get; }

        [Designation("Обозначение")]
        public string Designation { get; }

        [Designation("Количество")]
        public int Amount { get; }

        [Designation("Статус")]
        public string Status { get; }

        [Designation("НДС, %")]
        public double? Vat { get; }

        [Designation("Себестоимость (результирующая)"), OrderStatus(984)]
        public double PriceResult { get; }

        [Designation("Стоимость блоков с фиксированной ценой"), OrderStatus(983)]
        public double FixedCost { get; }

        /// <summary>
        /// Минимально возможная цена (продукты с фиксированной ценой + стоимость доставки)
        /// </summary>
        private double FixedCostAndDelivery { get; }

        [Designation("МД, %"), OrderStatus(977)]
        public double? MarginalIncome { get; }

        [Designation("МД, руб."), OrderStatus(977)]
        public double? MarginalIncomeNatural { get; }

        [Designation("Напряжение")]
        public string Voltage { get; }

        [Designation("Спецификация дата")]
        public DateTime? SpecificationDate { get; }


        [Designation("Договор")]
        public string ContractNumber { get; }

        [Designation("Договор дата")]
        public DateTime? ContractDate { get; }

        [Designation("Договор год")]
        public int ContractYear { get; }

        [Designation("Договор месяц")]
        public int ContractMonth { get; }

        [Designation("Тип доставки")]
        public string DeliveryType { get; }

        [Designation("Менеджер")]
        public Employee Manager { get; }

        [Designation("Цена с НДС")]
        public double CostWithVat { get; }

        #region Fake

        [Designation("Цена (результирующая)"), OrderStatus(989)]
        public double CostResult { get; }

        [Designation("Дата ОИТ (результирующая)")]
        public DateTime OrderInTakeDateResult { get; }

        [Designation("Дата реализации (результирующая)")]
        public DateTime RealizationDateResult { get; }

        [Designation("Год реализации (результирующая)")]
        public int RealizationDateResultYear { get; }

        [Designation("Месяц реализации (результирующая)")]
        public int RealizationDateResultMonth { get; }


        [Designation("Условия оплаты (результирующие)")]
        public PaymentConditionSet PaymentConditionSetResult { get; }

        #endregion

        public SalesReportUnit(
            IEnumerable<SalesUnit> salesUnits, 
            IEnumerable<Tender> tenders, 
            IEnumerable<ProductBlock> blocks,
            IEnumerable<CountryUnion> countryUnions)
        {
            SalesUnits = salesUnits.ToList();
            var salesUnit = SalesUnits.First();
            SetProperties(salesUnit);

            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();

            Amount = SalesUnits.Count;
            FacilityOwner = Facility.OwnerCompany;
            FacilityOwnerHead = GetFacilityOwnerHead();
            Contragent = Specification?.Contract.Contragent;
            ContragentType = GetContragentType();
            Region = Facility.GetRegion();
            District = Region?.District;
            Country = District?.Country;
            CountryUnions = GetCountryUnions();
            Segment = GetSegment();
            ProductType = Product.ProductType;
            Designation = Product.Designation;
            Status = GetStatus();
            Vat = Specification?.Vat;
            CostResult = FakeData?.Cost ?? Cost;
            CostWithVat = (1.0 + Vat)/100.0 * CostResult ?? CostResult;

            Voltage = Product.ProductBlock.Parameters.SingleOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;

            var priceStructures = GlobalAppProperties.PriceService.GetPriceStructures(this, this.OrderInTakeDate, GlobalAppProperties.Actual.ActualPriceTerm);
            PriceResult = Price ?? GlobalAppProperties.PriceService.GetPrice(salesUnit) ?? priceStructures.TotalPriceFixedCostLess;
            FixedCost = priceStructures.TotalFixedCost;
            FixedCostAndDelivery = CostDelivery.HasValue ? CostDelivery.Value + FixedCost : FixedCost;

            MarginalIncomeNatural = CostResult - FixedCostAndDelivery;
            MarginalIncome = CostResult - FixedCostAndDelivery == 0 ? default(double?) : (1.0 - PriceResult / (CostResult - FixedCostAndDelivery)) * 100.0;

            if (Specification != null)
            {
                SpecificationDate = Specification.Date;

                ContractNumber = Specification.Contract.Number;
                ContractDate = Specification.Contract.Date;
                ContractYear = ContractDate.Value.Year;
                ContractMonth = ContractDate.Value.Month;
            }

            DeliveryType = CostDelivery.HasValue ? "Доставка" : "Самовывоз";

            Manager = Project.Manager.Employee;

            OrderInTakeDateResult = this.FakeData?.OrderInTakeDate ?? this.OrderInTakeDate;
            RealizationDateResult = this.FakeData?.RealizationDate ?? this.RealizationDateCalculated;
            PaymentConditionSetResult = this.FakeData?.PaymentConditionSet ?? this.PaymentConditionSet;

            RealizationDateResultYear = RealizationDateResult.Year;
            RealizationDateResultMonth = RealizationDateResult.Month;
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
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesReport
{
    public class SalesReportUnit
    {
        public List<SalesUnit> SalesUnits { get; }

        private readonly List<CountryUnion> _countryUnions;
        private readonly List<Tender> _tenders;
        private int? _daysToStartProduction;
        private double? _paymentStartProduction;
        private DateTime? _datePaymentStartProduction;
        private string _paymentTypeStartProduction;
        private int? _daysToEndProduction;
        private double? _paymentEndProduction;
        private DateTime? _datePaymentEndProduction;
        private string _paymentTypeEndProduction;
        private int? _daysToShipping;
        private double? _paymentShipping;
        private DateTime? _datePaymentShipping;
        private string _paymentTypeShipping;
        private int? _daysToDelivery;
        private double? _paymentDelivery;
        private DateTime? _datePaymentDelivery;
        private string _paymentTypeDelivery;

        [Designation("Заказ"), OrderStatus(-1)]
        public string Order { get; }

        [Designation("Позиции"), OrderStatus(-2)]
        public string OrderPositions { get; }

        [Designation("Владелец объекта"), OrderStatus(-3)]
        public string FacilityOwners { get; }

        [Designation("Контрагент"), OrderStatus(-4)]
        public string Contragent { get; }

        [Designation("Тип контрагента"), OrderStatus(-5)]
        public string ContragentType { get; }

        [Designation("Объект"), OrderStatus(-6)]
        public string Facility { get; }

        [Designation("Страна поставки"), OrderStatus(-1000)]
        public Country Country { get; }

        [Designation("РФ/экспорт"), OrderStatus(-7)]
        public string IsExport { get; }

        [Designation("РФ/СНГ"), OrderStatus(-8)]
        public string RfSng { get; }

        [Designation("Федеральный округ"), OrderStatus(-9)]
        public string District { get; }

        [Designation("Сегмент"), OrderStatus(-10)]
        public string Segment { get; }

        [Designation("Категория производства"), OrderStatus(-11)]
        public string Kat { get; } = "ВВА";

        [Designation("Тип продукта"), OrderStatus(-12)]
        public string ProductType { get; }

        [Designation("Тип трансформатора"), OrderStatus(-13)]
        public string TransformerType { get; } = "-";

        [Designation("Суммарная мощность"), OrderStatus(-14)]
        public string Power { get; } = "-";

        [Designation("Обозначение"), OrderStatus(-15)]
        public string Designation { get; }

        [Designation("Категория оборудования"), OrderStatus(-16)]
        public string ProductCategory { get; }

        [Designation("Кол."), OrderStatus(-17)]
        public int Amount { get; }
        
        [Designation("Статус"), OrderStatus(-18)]
        public string Status { get; }

        [Designation("Категория статуса"), OrderStatus(-19)]
        public string StatusCategory { get; private set; }

        [Designation("НДС, %"), OrderStatus(-20)]
        public double Vat { get; }

        [Designation("Цена"), OrderStatus(-21)]
        public double Cost { get; }

        [Designation("Цена с НДС"), OrderStatus(-22)]
        public double CostWithVat => Vat * Cost;

        [Designation("Стоимость"), OrderStatus(-23)]
        public double Sum => Cost * Amount;

        [Designation("Стоимость с НДС"), OrderStatus(-24)]
        public double SumWithVat => Vat * Sum;

        [Designation("Логистика"), OrderStatus(-25)]
        public double CostDelivery { get; }
        
        [Designation("Стоимость блоков с фиксированной ценой"), OrderStatus(-26)]
        public double FixedCost { get; }

        [Designation("КЗ"), OrderStatus(-27)]
        public double Kz { get; } = 0.0;

        [Designation("Выручка"), OrderStatus(-28)]
        public double Proceeds => Sum + CostDelivery + FixedCost;

        [Designation("ПЗ"), OrderStatus(-29)]
        public double Price { get; }

        [Designation("ПЗ на кол."), OrderStatus(-30)]
        public double PriceOnAmount => -1.0 * Price * Amount;

        [Designation("МД, руб."), OrderStatus(-31)]
        public double MarginalIncomeNatural => Proceeds + PriceOnAmount;

        [Designation("МД, %"), OrderStatus(-32)]
        public double MarginalIncome => Math.Abs(Proceeds) < 0.000001 ? 0.0 : 100.0 * MarginalIncomeNatural / Proceeds;


        [Designation("Менеджер"), OrderStatus(-33)]
        public string Manager { get; }


        [Designation("Договор"), OrderStatus(-34)]
        public string ContractNumber { get; }

        [Designation("Спецификация"), OrderStatus(-35)]
        public string SpecificationNumber { get; }

        [Designation("Дата договора"), OrderStatus(-36)]
        public DateTime? ContractDate { get; }

        [Designation("Дата спецификации"), OrderStatus(-37)]
        public DateTime? SpecificationDate { get; }

        [Designation("Месяц спецификации"), OrderStatus(-38)]
        public int? SpecificationMonth => SpecificationDate?.Month;

        [Designation("Квартал спецификации"), OrderStatus(-39)]
        public int? SpecificationQuarter => (SpecificationMonth + 2) / 3;

        [Designation("Год контракта"), OrderStatus(-40)]
        public int ContractYear { get; }

        [Designation("Месяц ОИТ"), OrderStatus(-41)]
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        [Designation("Год ОИТ"), OrderStatus(-42)]
        public int OrderInTakeYear => OrderInTakeDate.Year;

        [Designation("ОИТ"), OrderStatus(-43)]
        public DateTime OrderInTakeDate { get; }

        [Designation("Дата запуска"), OrderStatus(-44)]
        public DateTime StartProductionDate { get; }

        [Designation("Месяц запуска"), OrderStatus(-45)]
        public int StartProductionMonth => StartProductionDate.Month;

        [Designation("Год запуска"), OrderStatus(-46)]
        public int StartProductionYear => StartProductionDate.Year;

        [Designation("Дата отгрузки"), OrderStatus(-47)]
        public DateTime ShipmentDate { get; }

        [Designation("Дата реализации"), OrderStatus(-48)]
        public DateTime RealizationDate { get; }

        [Designation("Месяц реализации"), OrderStatus(-49)]
        public int RealizationDatetMonth => RealizationDate.Month;

        [Designation("Год реализации"), OrderStatus(-50)]
        public int RealizationDateYear => RealizationDate.Year;

        [Designation("Дата реализации требуемая"), OrderStatus(-51)]
        public DateTime RealizationDateRequared { get; }

        [Designation("Дата реализации по контракту"), OrderStatus(-52)]
        public DateTime? RealizationDateContract { get; }

        [Designation("Условия оплаты"), OrderStatus(-59)]
        public PaymentConditionSet PaymentConditionSet { get; }


        [Designation("Срок до (начало производства)"), OrderStatus(-69)]
        public int? DaysToStartProduction
        {
            get { return _daysToStartProduction; }
            set { _daysToStartProduction = value; }
        }

        [Designation("Платеж (начало производства)"), OrderStatus(-70)]
        public double? PaymentStartProduction
        {
            get { return _paymentStartProduction; }
            set { _paymentStartProduction = value; }
        }

        [Designation("Дата (начало производства)"), OrderStatus(-71)]
        public DateTime? DatePaymentStartProduction
        {
            get { return _datePaymentStartProduction; }
            set { _datePaymentStartProduction = value; }
        }

        [Designation("Тип (начало производства)"), OrderStatus(-72)]
        public string PaymentTypeStartProduction
        {
            get { return _paymentTypeStartProduction; }
            set { _paymentTypeStartProduction = value; }
        }


        [Designation("Срок до (окончание производства)"), OrderStatus(-75)]
        public int? DaysToEndProduction
        {
            get { return _daysToEndProduction; }
            set { _daysToEndProduction = value; }
        }

        [Designation("Платеж (окончание производства)"), OrderStatus(-76)]
        public double? PaymentEndProduction
        {
            get { return _paymentEndProduction; }
            set { _paymentEndProduction = value; }
        }

        [Designation("Дата (окончание производства)"), OrderStatus(-77)]
        public DateTime? DatePaymentEndProduction
        {
            get { return _datePaymentEndProduction; }
            set { _datePaymentEndProduction = value; }
        }

        [Designation("Тип (окончание производства)"), OrderStatus(-78)]
        public string PaymentTypeEndProduction
        {
            get { return _paymentTypeEndProduction; }
            set { _paymentTypeEndProduction = value; }
        }


        [Designation("Срок до (отгрузка)"), OrderStatus(-81)]
        public int? DaysToShipping
        {
            get { return _daysToShipping; }
            set { _daysToShipping = value; }
        }

        [Designation("Платежи (отгрузка)"), OrderStatus(-82)]
        public double? PaymentShipping
        {
            get { return _paymentShipping; }
            set { _paymentShipping = value; }
        }

        [Designation("Дата (отгрузка)"), OrderStatus(-83)]
        public DateTime? DatePaymentShipping
        {
            get { return _datePaymentShipping; }
            set { _datePaymentShipping = value; }
        }

        [Designation("Тип (отгрузка)"), OrderStatus(-84)]
        public string PaymentTypeShipping
        {
            get { return _paymentTypeShipping; }
            set { _paymentTypeShipping = value; }
        }


        [Designation("Срок до (поставка)"), OrderStatus(-88)]
        public int? DaysToDelivery
        {
            get { return _daysToDelivery; }
            set { _daysToDelivery = value; }
        }

        [Designation("Платеж (поставка)"), OrderStatus(-89)]
        public double? PaymentDelivery
        {
            get { return _paymentDelivery; }
            set { _paymentDelivery = value; }
        }

        [Designation("Дата (поставка)"), OrderStatus(-90)]
        public DateTime? DatePaymentDelivery
        {
            get { return _datePaymentDelivery; }
            set { _datePaymentDelivery = value; }
        }

        [Designation("Тип (поставка)"), OrderStatus(-91)]
        public string PaymentTypeDelivery
        {
            get { return _paymentTypeDelivery; }
            set { _paymentTypeDelivery = value; }
        }


        [Designation("Тип доставки"), OrderStatus(-137)]
        public string DeliveryType { get; }

        [Designation("Адрес доставки"), OrderStatus(-138)]
        public string DeliveryAddress { get; }

        [Designation("Комплектация"), OrderStatus(-139)]
        public DateTime? PickingDate { get; }

        [Designation("Включенное оборудование (на единицу)"), OrderStatus(-140)]
        public string ProductsIncluded { get; }

        [Designation("Оплаты"), OrderStatus(-141)]
        public string PaymentsActual { get; }


        [Designation("Информация из ТСЕ"), OrderStatus(-150)]
        public string TceInfo { get; }

        [Designation("Материал изолятора"), OrderStatus(-160)]
        public string IsolationMaterial { get; }

        [Designation("ДПУ"), OrderStatus(-161)]
        public string IsolationDpu { get; }

        [Designation("Цвет изолятора"), OrderStatus(-162)]
        public string IsolationColor { get; }



        [Designation("TenderStatus"), OrderStatus(-200)]
        public string TenderStatus { get; }

        [Designation("Производитель"), OrderStatus(-201)]
        public string Producer { get; }

        #region Fake

        [OrderStatus(-500)]
        public bool CostIsFake { get; }
        [OrderStatus(-500)]
        public bool RealizationDateIsFake { get; }
        [OrderStatus(-500)]
        public bool OrderInTakeDateIsFake { get; }
        [OrderStatus(-500)]
        public bool PaymentConditionSetIsFake { get; }

        #endregion

        public SalesReportUnit(
            IEnumerable<SalesUnit> salesUnits, 
            IEnumerable<Tender> tenders, 
            IEnumerable<CountryUnion> countryUnions, 
            PriceCalculationItem priceCalculationItem)
        {
            SalesUnits = salesUnits.ToList();
            var salesUnit = SalesUnits.First();
            //SetProperties(salesUnit);

            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();

            Order = salesUnit.Order?.ToString();
            OrderPositions = SalesUnits.Select(x => x.OrderPosition).GetOrderPositions();

            var owners = new List<Company> {salesUnit.Facility.OwnerCompany};
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ToStringEnum();
            var contragent = salesUnit.Specification?.Contract.Contragent ?? GetTenderWinner() ?? salesUnit.Facility.OwnerCompany;
            Contragent = contragent.ToString();
            ContragentType = GetContragentType(contragent);
            Facility = salesUnit.Facility.ToString();
            var region = salesUnit.Facility.GetRegion();
            Country = region?.District.Country;
            if (Country != null)
            {
                IsExport = Country.Name == "Россия" ? "РФ" : Country.Name;
                if (Country.Name == "Россия")
                {
                    RfSng = "РФ";
                }
                else if (GetCountryUnions().Any(x => x.Name == "СНГ"))
                {
                    RfSng = "СНГ";
                }
                else
                {
                    RfSng = "ДЗ";
                }
            }
            District = region?.District.Name;
            Segment = GetSegment();
            ProductType = salesUnit.Product.ProductType.Name;
            Designation = salesUnit.Product.Designation;
            ProductCategory = GetProductCategory(salesUnit.Product);
            Amount = SalesUnits.Count;
            Status = GetStatus();
            Vat = salesUnit.Vat / 100.0 + 1.0;
            Cost = salesUnit.FakeData?.Cost ?? salesUnit.Cost;
            var costDelivery = SalesUnits.Select(x => x.CostDelivery).Where(x => x.HasValue).Sum(x => x.Value);
            CostDelivery = -1.0 * costDelivery;

            var price = GlobalAppProperties.PriceService.GetPrice(salesUnit, salesUnit.OrderInTakeDate);
            Price = price.SumPriceTotal;
            FixedCost = -1.0 * price.SumFixedTotal * Amount;
            //FixedCostAndDelivery = CostDelivery.HasValue ? CostDelivery.Value + FixedCost : FixedCost;

            var manager = salesUnit.Project.Manager.Employee;
            Manager = $"{manager.Person.Surname}";

            if (salesUnit.Specification != null)
            {
                var specification = salesUnit.Specification;
                SpecificationNumber = specification.Number;
                SpecificationDate = specification.Date;

                ContractNumber = specification.Contract.Number;
                ContractDate = specification.Contract.Date;
                ContractYear = ContractDate.Value.Year;
            }

            RealizationDateContract = salesUnit.EndProductionDateByContractCalculated;

            OrderInTakeDate = salesUnit.FakeData?.OrderInTakeDate ?? salesUnit.OrderInTakeDate;
            StartProductionDate = salesUnit.StartProductionDateCalculated;
            ShipmentDate = salesUnit.ShipmentDateCalculated;
            RealizationDate = salesUnit.FakeData?.RealizationDate ?? salesUnit.RealizationDateCalculated;
            RealizationDateRequared = salesUnit.DeliveryDateExpected;

            PaymentConditionSet = salesUnit.FakeData?.PaymentConditionSet ?? salesUnit.PaymentConditionSet;
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.ProductionStart, ref _daysToStartProduction, ref _paymentStartProduction, ref _datePaymentStartProduction, ref _paymentTypeStartProduction);
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.ProductionEnd, ref _daysToEndProduction, ref _paymentEndProduction, ref _datePaymentEndProduction, ref _paymentTypeEndProduction);
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.Shipment, ref _daysToShipping, ref _paymentShipping, ref _datePaymentShipping, ref _paymentTypeShipping);
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.Delivery, ref _daysToDelivery, ref _paymentDelivery, ref _datePaymentDelivery, ref _paymentTypeDelivery);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             

            DeliveryType = -1 * CostDelivery > 0  ? "Доставка" : "Самовывоз";

            DeliveryAddress = salesUnit.AddressDelivery?.ToString() ?? salesUnit.Facility.Address?.ToString() ?? $"{Country}, {region}, {Facility}";

            PickingDate = salesUnit.PickingDate;

            ProductsIncluded = SalesUnits
                .SelectMany(x => x.ProductsIncluded)
                .Distinct()
                .OrderBy(x => x.Product.Designation)
                .ToStringEnum();

            PaymentsActual = salesUnits.SelectMany(x => x.PaymentsActual).ToStringEnum();

            TceInfo = priceCalculationItem?.ToString();

            IsolationMaterial = salesUnit.Product.ProductBlock.Parameters
                .FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.IsolationMaterialGroup))?.Value;
            IsolationDpu = salesUnit.Product.ProductBlock.Parameters
                .FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.IsolationDpuGroup))?.Value;
            IsolationColor = salesUnit.Product.ProductBlock.Parameters
                .FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.IsolationColorGroup))?.Value;

            if (salesUnit.FakeData != null)
            {
                CostIsFake = salesUnit.FakeData.Cost.HasValue;
                RealizationDateIsFake = salesUnit.FakeData.RealizationDate.HasValue;
                OrderInTakeDateIsFake = salesUnit.FakeData.OrderInTakeDate.HasValue;
                PaymentConditionSetIsFake = salesUnit.FakeData.PaymentConditionSet != null;
            }

            if (salesUnit.IsLoosen)
            {
                TenderStatus = string.IsNullOrEmpty(Order) ? "Тендер проигран" : "Заказ аннулирован";
            }
            else if (OrderInTakeDate < DateTime.Today) TenderStatus = $"Факт ОИТ {OrderInTakeDate.Year}";
            else if (OrderInTakeDate.Year > DateTime.Today.Year) TenderStatus = $"Закупка или тендер перенесены на {OrderInTakeDate.Year} год";

            Producer = salesUnit.Producer?.ToString() ?? string.Empty;
        }

        private void SetPaymentsConditions(
            SalesUnit salesUnit,
            PaymentConditionPointEnum point, 
            ref int? daysTo, 
            ref double? payment,
            ref DateTime? date,
            ref string paymentType)
        {
            var conditions = salesUnit.PaymentConditionSet.PaymentConditions
                .Where(x => x.PaymentConditionPoint.PaymentConditionPointEnum == point)
                .OrderBy(x => x.DaysToPoint)
                .ToList();

            if (conditions.Any())
            {
                daysTo = conditions.First().DaysToPoint;
                payment = SumWithVat * conditions.Sum(x => x.Part);
                date = salesUnit.GetPaymentDate(conditions.First());

                var realization = salesUnit.RealizationDateCalculated;
                if (date <= realization
                    && date.Value.Year == realization.Year
                    && date.Value.Month == realization.Month)
                {
                    paymentType = "ТП";
                }
                else
                {
                    paymentType = date < realization ? "АВ" : "ДЗ";
                }
            }            
        }

        private string GetProductCategory(Product product)
        {
            var designation = product.Designation.Replace("УЭТМ-", string.Empty);

            var categories = new List<string>
            {
                "ВГБ-35",

                "ВЭБ-110",
                "ВЭБ-220",

                "ВГТ-35",
                "ВГТ-110",
                "ВГТЗ-110",
                "ВГТ-220",
                "ВГТ-1А1-220",
                "ВГТ-330",
                "ВГТ-500",
                "ВГТ-750",

                "ЗНГ-35",
                "ЗНГ-110",
                "ЗНГ-220",
                "ЗНГ-330",

                "ТРГ-35",
                "ТРГ-110",
                "ТРГ-220",
                "ТРГ-330",
                "ТРГ-500",

                "ЗРО-110",
                "ЗРО-220",

                "РПД-110",
                "РПД-1п-110",
                "РПД-1к-110",
                "РПД-2-110",
                "РПД-220",
                "РПД-1п-220",
                "РПД-1к-220",
                "РПД-2-220",

                "РПДО-110",
                "РПДО-1п-110",
                "РПДО-1к-110",
                "РПДО-2-110",
                "РПДО-220",
                "РПДО-1п-220",
                "РПДО-1к-220",
                "РПДО-2-220",

                "БВГ-35",
                "БВГ-110",
                "БВГ-220",

                "РУЭН-110",
                "РУЭН-220",

                "КРУЭ-110",
                "КРУЭ-220"
            };

            foreach (var category in categories)
            {
                if (designation.StartsWith(category))
                    return category;
            }

            if (designation.StartsWith("ВАБ") || designation.StartsWith("ВАТ"))
                return "БВПТ";

            return "другое";
        }

        //private void SetProperties(SalesUnit salesUnit)
        //{
        //    var properties = salesUnit.GetType().GetProperties().Where(x => x.CanWrite);
        //    foreach (var property in properties)
        //    {
        //        var value = property.GetValue(salesUnit);
        //        this.GetType().GetProperty(property.Name).SetValue(this, value);
        //    }
        //}

        private string GetContragentType(Company contragent)
        {
            if (Contragent == null)
                return "Нет данных";

            var salesUnit = SalesUnits.First();
            if (Equals(salesUnit.Facility.OwnerCompany, contragent) ||
                salesUnit.Facility.OwnerCompany.ParentCompanies().Contains(contragent))
                return "Конечный заказчик";

            if (_tenders.FirstOrDefault(x => Equals(x.Winner, contragent)) != null) return "Подрядчик";

            return "Посредник";
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
            var actualActivities = new List<ActivityFieldEnum>
            {
                ActivityFieldEnum.ElectricityDistribution,
                ActivityFieldEnum.ElectricityTransmission,
                ActivityFieldEnum.ElectricityGeneration,
                ActivityFieldEnum.Fuel,
                ActivityFieldEnum.RailWay,
                ActivityFieldEnum.IndustrialEnterprise
            };

            //сегмент по владельцам объекта
            var owner = SalesUnits.First().Facility.OwnerCompany;
            do
            {
                var activityField = owner.ActivityFilds.FirstOrDefault(x => actualActivities.Contains(x.ActivityFieldEnum));
                if (activityField != null) return activityField.Name;
                owner = owner.ParentCompany;
            } while (owner != null);

            return "Промышленное предприятие";
        }

        private string GetStatus()
        {
            var salesUnit = SalesUnits.First();

            if (salesUnit.IsLoosen)
            {
                StatusCategory = "14";
                return "14";
                //return "14 - Проиграно другому производителю";
            }

            if (salesUnit.RealizationDateCalculated < DateTime.Today)
            {
                StatusCategory = "0";
                return "0";
                //return "0 - Продукт реализован";
            }

            if (salesUnit.StartProductionConditionsDoneDate.HasValue &&
                salesUnit.StartProductionConditionsDoneDate.Value <= DateTime.Today)
            {
                StatusCategory = "1-3";
                return "1";
                //return "1 - Условие на запуск производства исполнено";
            }

            if (salesUnit.Specification != null)
            {
                StatusCategory = "1-3";
                return salesUnit.Specification.Date <= DateTime.Today
                    ? "2"
                    : "3";
                //return salesUnit.Specification.Date <= DateTime.Today
                //    ? "2 - Контракт подписан"
                //    : "3 - Контракт на оформлении";
            }

            if (salesUnit.Producer != null && salesUnit.Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
            {
                StatusCategory = "4-7";
                return "4";
                //return "4 - Большая вероятность реализации";
            }

            StatusCategory = "4-7";
            return "7";
            //return "7 - В проработке";
        }

        //private Company GetFacilityOwnerHead()
        //{
        //    var head = FacilityOwners;
        //    while (head.ParentCompany != null)
        //    {
        //        head = head.ParentCompany;
        //    }
        //    return head;
        //}

        private Company GetTenderWinner()
        {
            var tenders = _tenders.Where(x => Equals(SalesUnits.First().Project.Id, x.Project.Id)).ToList();
            if (!tenders.Any()) return null;

            //поставщик
            var supplier = tenders
                .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply))
                .OrderBy(x => x.DateClose)
                .LastOrDefault()?.Winner;
            if (supplier != null) return supplier;

            //подрядчик
            var worker = tenders
                .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork))
                .OrderBy(x => x.DateClose)
                .LastOrDefault()?.Winner;

            return worker;
        }
    }
}
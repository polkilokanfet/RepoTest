using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.CommonInfo
{
    public class CommonInfoUnit
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesUnit SalesUnit { get; }

        public string OrderPosition => SalesUnit.OrderPosition;

        [Designation("Информация из ТСЕ"), OrderStatus(-150)]
        public string TceInfo => SalesUnit.ActualPriceCalculationItem(_unitOfWork)?.ToString() ?? "no information";

        [Designation("Включенное оборудование (на единицу)"), OrderStatus(-140)]
        public string ProductsIncluded => SalesUnit.ProductsIncluded.OrderBy(productIncluded => productIncluded.Product.Designation).ToStringEnum();

        public CommonInfoUnit(SalesUnit salesUnit, IUnitOfWork unitOfWork)
        {
            SalesUnit = salesUnit;
            _unitOfWork = unitOfWork;
        }
    }
    public class CommonInfoUnitGroup
    {
        public List<CommonInfoUnit> Units { get; }

        private readonly List<Tender> _tenders;

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

        [Designation("Федеральный округ"), OrderStatus(-9)]
        public string District { get; }

        [Designation("Сегмент"), OrderStatus(-10)]
        public string Segment { get; }

        [Designation("Тип продукта"), OrderStatus(-12)]
        public string ProductType { get; }

        [Designation("Обозначение"), OrderStatus(-15)]
        public string Designation { get; }

        [Designation("Кол."), OrderStatus(-17)]
        public int Amount { get; }

        [Designation("НДС, %"), OrderStatus(-20)]
        public double Vat { get; }

        [Designation("Цена"), OrderStatus(-21)]
        public double Cost { get; }

        [Designation("Стоимость"), OrderStatus(-23)]
        public double Sum => Cost * Amount;

        [Designation("Стоимость с НДС"), OrderStatus(-24)]
        public double SumWithVat => Vat * Sum;

        [Designation("Логистика"), OrderStatus(-25)]
        public double CostDelivery { get; }

        [Designation("Стоимость блоков с фиксированной ценой"), OrderStatus(-26)]
        public double FixedCost { get; }

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

        [Designation("ОИТ"), OrderStatus(-43)]
        public DateTime OrderInTakeDate { get; }

        [Designation("Условия оплаты"), OrderStatus(-59)]
        public PaymentConditionSet PaymentConditionSet { get; }

        [Designation("Тип доставки"), OrderStatus(-137)]
        public string DeliveryType { get; }

        [Designation("Адрес доставки"), OrderStatus(-138)]
        public string DeliveryAddress { get; }

        [Designation("Срок производства"), OrderStatus(-245)]
        public int ProductionTerm { get; }

        public CommonInfoUnitGroup(IEnumerable<SalesUnit> salesUnits1, IEnumerable<Tender> tenders, IUnitOfWork unitOfWork)
        {
            var salesUnits = salesUnits1.ToList();
            Units = salesUnits.Select(unit => new CommonInfoUnit(unit, unitOfWork)).ToList();
            var salesUnit = salesUnits.First();

            _tenders = tenders.ToList();

            Order = salesUnit.Order?.ToString();
            OrderPositions = salesUnits.Select(unit => unit.OrderPosition).GetOrderPositions();

            ProductionTerm = salesUnit.ProductionTerm;
            var owners = new List<Company> {salesUnit.Facility.OwnerCompany};
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ToStringEnum();
            var contragent = salesUnit.Specification?.Contract.Contragent ?? GetTenderWinner() ?? salesUnit.Facility.OwnerCompany;
            Contragent = contragent.ToString();
            ContragentType = GetContragentType(contragent);
            Facility = salesUnit.Facility.ToString();
            var region = salesUnit.Facility.GetRegion();
            Country = region?.District.Country;
            District = region?.District.Name;
            Segment = SegmentConverter(GetSegment());
            ProductType = salesUnit.Product.ProductType.Name;
            Designation = salesUnit.Product.Designation;
            Amount = salesUnits.Count;
            Vat = salesUnit.Vat / 100.0 + 1.0;
            Cost = salesUnit.Cost;
            var costDelivery = salesUnits.Select(unit => unit.CostDelivery).Where(x => x.HasValue).Sum(x => x.Value);
            CostDelivery = -1.0 * costDelivery;

            var price = GlobalAppProperties.PriceService.GetPrice(salesUnit, salesUnit.OrderInTakeDate, true);
            FixedCost = -1.0 * price.SumFixedTotal * Amount;

            Manager = $"{salesUnit.Project.Manager.Employee.Person}";

            if (salesUnit.Specification != null)
            {
                var specification = salesUnit.Specification;
                SpecificationNumber = specification.Number;
                SpecificationDate = specification.Date;

                ContractNumber = specification.Contract.Number;
                ContractDate = specification.Contract.Date;
            }


            OrderInTakeDate = salesUnit.OrderInTakeDate;

            PaymentConditionSet = salesUnit.PaymentConditionSet;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             

            DeliveryType = Math.Abs(CostDelivery) > 0  ? "Доставка" : "Самовывоз";

            DeliveryAddress = salesUnit.GetDeliveryAddressString();
        }

        private string GetContragentType(Company contragent)
        {
            if (Contragent == null)
                return "Нет данных";

            var salesUnit = Units.First().SalesUnit;
            if (Equals(salesUnit.Facility.OwnerCompany, contragent) ||
                salesUnit.Facility.OwnerCompany.ParentCompanies().Contains(contragent))
                return "Конечный заказчик";

            if (_tenders.FirstOrDefault(x => Equals(x.Winner, contragent)) != null) return "Подрядчик";

            return "Посредник";
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
            var owner = Units.First().SalesUnit.Facility.OwnerCompany;
            do
            {
                var activityField = owner.ActivityFilds.FirstOrDefault(x => actualActivities.Contains(x.ActivityFieldEnum));
                if (activityField != null)
                    return activityField.Name;
                owner = owner.ParentCompany;
            } while (owner != null);

            return "Промышленное предприятие";
        }

        private string SegmentConverter(string segment)
        {
            switch (segment)
            {
                case "Генерация электроэнергии": return "Генерация";
                case "Железная дорога": return "РЖД";
                case "Передача электроэнергии": return "Сети";
                case "Промышленное предприятие": return "Промышленность";
                case "Распределение электроэнергии": return "Распределение";
                case "Топливно-энергетический сектор": return "Нефтегаз.";
            }

            return segment;
        }


        private Company GetTenderWinner()
        {
            var tenders = _tenders.Where(tender => Equals(Units.First().SalesUnit.Project.Id, tender.Project.Id)).ToList();
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
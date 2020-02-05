using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesReportUnit
    {
        public List<SalesUnit> SalesUnits { get; }
        private readonly List<CountryUnion> _countryUnions;
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

        [Designation("РФ/экспорт"), OrderStatus(-7)]
        public string IsExport { get; }

        [Designation("РФ/СНГ"), OrderStatus(-8)]
        public string RfSng { get; }

        [Designation("Регион"), OrderStatus(-9)]
        public string Region { get; }

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
        public double Vat { get; } = 1.2;

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
        public double PriceOnAmount => Price * Amount;

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
        public int? SpecificationQuarter => SpecificationMonth / 3;

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

        [Designation("Тип доставки"), OrderStatus(-137)]
        public string DeliveryType { get; }

        [Designation("Адрес доставки"), OrderStatus(-138)]
        public string DeliveryAddress { get; }

        //[Designation("Напряжение")]
        //public string Voltage { get; }

        #region Fake

        #endregion

        public SalesReportUnit(
            IEnumerable<SalesUnit> salesUnits, 
            IEnumerable<Tender> tenders, 
            IEnumerable<CountryUnion> countryUnions)
        {
            SalesUnits = salesUnits.ToList();
            var salesUnit = SalesUnits.First();
            //SetProperties(salesUnit);

            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();

            Order = salesUnit.Order?.ToString();
            OrderPositions = SalesUnits.Select(x => x.OrderPosition).ConvertToString();

            var owners = new List<Company> {salesUnit.Facility.OwnerCompany};
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ConvertToString();
            var contragent = salesUnit.Specification?.Contract.Contragent ?? GetTenderWinner() ?? salesUnit.Facility.OwnerCompany;
            Contragent = contragent.ToString();
            ContragentType = GetContragentType(contragent);
            Facility = salesUnit.Facility.ToString();
            Country = salesUnit.Facility.GetRegion()?.District.Country;
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
            }
            Region = salesUnit.Facility.GetRegion()?.Name;
            Segment = GetSegment();
            ProductType = salesUnit.Product.ProductType.Name;
            Designation = salesUnit.Product.Designation;
            ProductCategory = string.Empty;
            Amount = SalesUnits.Count;
            Status = GetStatus();
            if(salesUnit.Specification != null)
                Vat = salesUnit.Specification.Vat + 1.0;
            Cost = salesUnit.FakeData?.Cost ?? salesUnit.Cost;
            CostDelivery = -1.0 * salesUnit.CostDelivery ?? 0.0;

            var priceStructures = GlobalAppProperties.PriceService.GetPriceStructures(salesUnit, salesUnit.OrderInTakeDate, GlobalAppProperties.Actual.ActualPriceTerm);
            Price = salesUnit.Price ?? GlobalAppProperties.PriceService.GetPrice(salesUnit) ?? priceStructures.TotalPriceFixedCostLess;
            Price = -1.0 * Price;
            FixedCost = -1.0 * priceStructures.TotalFixedCost;
            //FixedCostAndDelivery = CostDelivery.HasValue ? CostDelivery.Value + FixedCost : FixedCost;

            var manager = salesUnit.Project.Manager.Employee;
            Manager = $"{manager.Person.Surname} {manager.Person.Name} {manager.Person.Patronymic}";

            if (salesUnit.Specification != null)
            {
                var specification = salesUnit.Specification;
                SpecificationNumber = specification.Number;
                SpecificationDate = specification.Date;

                ContractNumber = specification.Contract.Number;
                ContractDate = specification.Contract.Date;
                ContractYear = ContractDate.Value.Year;

                RealizationDateContract = salesUnit.StartProductionDate?.AddDays(salesUnit.ProductionTerm);
            }

            OrderInTakeDate = salesUnit.FakeData?.OrderInTakeDate ?? salesUnit.OrderInTakeDate;
            StartProductionDate = salesUnit.StartProductionDateCalculated;
            ShipmentDate = salesUnit.ShipmentDateCalculated;
            RealizationDate = salesUnit.FakeData?.RealizationDate ?? salesUnit.RealizationDateCalculated;
            RealizationDateRequared = salesUnit.DeliveryDateExpected;

            PaymentConditionSet = salesUnit.FakeData?.PaymentConditionSet ?? salesUnit.PaymentConditionSet;

            DeliveryType = CostDelivery > 0  ? "Доставка" : "Самовывоз";

            DeliveryAddress = salesUnit.AddressDelivery?.ToString() ?? salesUnit.Facility.Address?.ToString() ?? string.Empty;

            //Voltage = Product.ProductBlock.Parameters.SingleOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;
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
            var actEnums = new List<ActivityFieldEnum>
            {
                ActivityFieldEnum.ElectricityDistribution,
                ActivityFieldEnum.ElectricityTransmission,
                ActivityFieldEnum.ElectricityGeneration,
                ActivityFieldEnum.Fuel,
                ActivityFieldEnum.RailWay
            };

            //сегмент по владельцам объекта
            var owner = SalesUnits.First().Facility.OwnerCompany;
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
            var salesUnit = SalesUnits.First();
            if (salesUnit.RealizationDateCalculated < DateTime.Today)
            {
                StatusCategory = "1-3";
                return "0 - Продукт реализован";
            }

            if (salesUnit.StartProductionConditionsDoneDate.HasValue &&
                salesUnit.StartProductionConditionsDoneDate.Value <= DateTime.Today)
            {
                StatusCategory = "1-3";
                return "1 - Условие на запуск производства исполнено";
            }

            if (salesUnit.Specification != null)
            {
                StatusCategory = "1-3";
                return salesUnit.Specification.Date <= DateTime.Today
                    ? "2 - Контракт подписан"
                    : "3 - Контракт на оформлении";
            }

            if (salesUnit.IsLoosen)
            {
                StatusCategory = "15";
                return "15 - Проиграно другому производителю";
            }

            if (salesUnit.Producer != null && salesUnit.Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
            {
                StatusCategory = "4-7";
                return "4 - Большая вероятность реализации";
            }

            StatusCategory = "4-7";
            return "7 - В проработке";
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
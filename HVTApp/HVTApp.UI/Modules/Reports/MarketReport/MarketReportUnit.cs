using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.MarketReport
{
    public class MarketReportUnit
    {
        public List<SalesUnit> SalesUnits { get; }

        [Designation("Проект"), OrderStatus(-3)]
        public string ProjectName { get; }

        [Designation("Владелец объекта"), OrderStatus(-3)]
        public string FacilityOwners { get; }

        [Designation("Заказ"), OrderStatus(-1)]
        public string Order { get; }

        [Designation("Контрагент"), OrderStatus(-4)]
        public string Contragent { get; }

        [Designation("Объект"), OrderStatus(-6)]
        public string Facility { get; }

        [Designation("Регион"), OrderStatus(-8)]
        public string Region { get; }

        [Designation("Федеральный округ"), OrderStatus(-9)]
        public string District { get; }

        [Designation("Сегмент"), OrderStatus(-10)]
        public string Segment { get; }

        [Designation("Тип продукта"), OrderStatus(-12)]
        public string ProductType { get; }

        [Designation("Обозначение"), OrderStatus(-15)]
        public string Designation { get; }

        [Designation("Категория оборудования"), OrderStatus(-16)]
        public string ProductCategory { get; }

        [Designation("Кол."), OrderStatus(-17)]
        public int Amount { get; }
        
        [Designation("Статус"), OrderStatus(-18)]
        public string Status { get; }

        [Designation("Цена"), OrderStatus(-21)]
        public double Cost { get; }

        [Designation("Стоимость"), OrderStatus(-23)]
        public double Sum => Cost * Amount;


        [Designation("Менеджер"), OrderStatus(-33)]
        public string Manager { get; }


        [Designation("Производитель"), OrderStatus(-33)]
        public string Producer { get; }

        public string Builder { get; }
        public string ProjectMaker { get; }
        public string Supplier { get; }


        [Designation("Месяц ОИТ"), OrderStatus(-41)]
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        [Designation("Год ОИТ"), OrderStatus(-42)]
        public int OrderInTakeYear => OrderInTakeDate.Year;

        [Designation("ОИТ"), OrderStatus(-43)]
        public DateTime OrderInTakeDate { get; }

        [Designation("Дата отгрузки"), OrderStatus(-47)]
        public DateTime ShipmentDate { get; }

        [Designation("Дата доставки"), OrderStatus(-47)]
        public DateTime DeliveryDate { get; }

        [Designation("Дата реализации"), OrderStatus(-48)]
        public DateTime RealizationDate { get; }

        [Designation("Дата реализации требуемая"), OrderStatus(-51)]
        public DateTime RealizationDateRequared { get; }

        [Designation("Дата реализации по контракту"), OrderStatus(-52)]
        public DateTime? RealizationDateContract { get; }

        public string Voltage { get; }


        public MarketReportUnit(IEnumerable<SalesUnit> salesUnits, IEnumerable<Tender> tenders)
        {
            SalesUnits = salesUnits.ToList();
            var salesUnit = SalesUnits.First();

            var tenders1 = tenders.ToList();

            ProjectName = salesUnit.Project.Name;

            Order = salesUnit.Order?.ToString();

            Producer = salesUnit.Producer?.ToString();

            Voltage = salesUnit.Product.GetVoltageParameter()?.Value;

            Builder = tenders1.GetWinner(TenderTypeEnum.ToWork)?.ToString();
            ProjectMaker = tenders1.GetWinner(TenderTypeEnum.ToProject)?.ToString();
            Supplier = tenders1.GetWinner(TenderTypeEnum.ToSupply)?.ToString();

            var owners = new List<Company> {salesUnit.Facility.OwnerCompany};
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ToStringEnum();
            var contragent = salesUnit.Specification?.Contract.Contragent ?? salesUnit.Facility.OwnerCompany;
            Contragent = contragent?.ToString();
            Facility = salesUnit.Facility.ToString();
            var region = salesUnit.Facility.GetRegion();
            Region = region?.Name;
            District = region?.District.Name;
            Segment = GetSegment();
            ProductType = salesUnit.Product.ProductType.Name;
            Designation = salesUnit.Product.Designation;
            ProductCategory = GetProductCategory(salesUnit.Product);
            Amount = SalesUnits.Count;
            Status = GetStatus(salesUnit);
            Cost = salesUnit.Cost;

            var manager = salesUnit.Project.Manager.Employee;
            Manager = $"{manager.Person.Surname} {manager.Person.Name} {manager.Person.Patronymic}";

            RealizationDateContract = salesUnit.EndProductionDateByContractCalculated;

            OrderInTakeDate = salesUnit.OrderInTakeDate;
            ShipmentDate = salesUnit.ShipmentDateCalculated;
            DeliveryDate = salesUnit.DeliveryDateCalculated;
            RealizationDate = salesUnit.RealizationDateCalculated;
            RealizationDateRequared = salesUnit.DeliveryDateExpected;
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

        private string GetStatus(SalesUnit salesUnit)
        {
            if (salesUnit.IsLoosen)
                return "Проиграно";

            if (salesUnit.IsWon)
                return "Выиграно";

            return "";
        }

        //private Company GetTenderWinner()
        //{
        //    var tenders = _tenders.Where(x => Equals(SalesUnits.First().Project.Id, x.Project.Id)).ToList();
        //    if (!tenders.Any()) return null;

        //    //поставщик
        //    var supplier = tenders
        //        .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply))
        //        .OrderBy(x => x.DateClose)
        //        .LastOrDefault()?.Winner;
        //    if (supplier != null) return supplier;

        //    //подрядчик
        //    var worker = tenders
        //        .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork))
        //        .OrderBy(x => x.DateClose)
        //        .LastOrDefault()?.Winner;

        //    return worker;
        //}
    }
}
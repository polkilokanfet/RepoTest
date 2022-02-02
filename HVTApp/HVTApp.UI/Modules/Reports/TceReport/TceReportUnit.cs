using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.TceReport
{
    public class TceReportUnit
    {
        private static Random _random = new Random();
        public static List<Offer> Offers { get; } = new List<Offer>();

        #region MyRegion

        private readonly List<Tender> _tenders;

        private DateTime OrderInTakeDate { get; }
        private List<SalesUnit> SalesUnits { get; }

        [Designation("Запрос"), OrderStatus(-10)]
        public string TceNumber { get; }

        [Designation("Дата запроса"), OrderStatus(-20)]
        public DateTime TaskOpenDate { get; }

        [Designation("Менеджер"), OrderStatus(-30)]
        public string Manager { get; }

        [Designation("Back-manager"), OrderStatus(-300)]
        public string BackManager { get; }

        [Designation("Владелец объекта"), OrderStatus(-40)]
        public string FacilityOwners { get; }

        [Designation("Контрагент"), OrderStatus(-50)]
        public string Contragent { get; }

        [Designation("Объект"), OrderStatus(-60)]
        public string Facility { get; }

        [Designation("Продукт"), OrderStatus(-70)]
        public string Product { get; }

        [Designation("Статус запроса"), OrderStatus(-80)]
        public string CommonStatus { get; }

        [Designation("Год проекта"), OrderStatus(-90)]
        public int YearOfProject => this.OrderInTakeDate.Year;

        [Designation("Производитель"), OrderStatus(-100)]
        public string Producer { get; }

        [Designation("Причина проигрыша"), OrderStatus(-110)]
        public string LossReason { get; }

        [Designation("Производитель - победитель"), OrderStatus(-120)]
        public string ProducerWinner { get; }

        [Designation("Цена"), OrderStatus(-130)]
        public double Cost { get; }

        [Designation("Цена победителя"), OrderStatus(-140)]
        public double? CostOfWinner { get; }

        [Designation("Комментарий по тендеру"), OrderStatus(-150)]
        public string CommentTender { get; }

        [Designation("Дата реализации"), OrderStatus(-160)]
        public DateTime RealizationDate { get; }

        [Designation("Позиции"), OrderStatus(-170)]
        public string OrderPositions { get; }

        [Designation("Кол."), OrderStatus(-180)]
        public int Amount { get; }

        [Designation("Статус ОИТ"), OrderStatus(-190)]
        public string OrderInTakeStatus { get; }

        [Designation("Цена ОИТ"), OrderStatus(-200)]
        public double CostOrderInTake => CostOfWinner ?? Cost;

        [Designation("Валюта"), OrderStatus(-210)]
        public string Currency => "RUB";

        [Designation("Дата 1 ТКП"), OrderStatus(-220)]
        public DateTime? FirstOfferDate { get; }

        [Designation("От начала проработки до ТКП (дней)"), OrderStatus(-230)]
        public int? Term { get; }

        [Designation("Дата старта проработки"), OrderStatus(-240)]
        public DateTime? TaskStartDate { get; }

        [Designation("Дата финиша проработки"), OrderStatus(-250)]
        public DateTime? TaskFinishDate { get; }
        
        [Designation("Заказ"), OrderStatus(-260)]
        public string Order { get; }

        #endregion

        public TceReportUnit(TechnicalRequrementsTask technicalRequrementsTask, TechnicalRequrements technicalRequrements, IEnumerable<Tender> tenders)
        {
            SalesUnits = technicalRequrements.SalesUnits.ToList();
            var salesUnit = SalesUnits.First();

            _tenders = tenders.ToList();

            OrderInTakeDate = salesUnit.OrderInTakeDate;

            if (technicalRequrementsTask.PriceCalculations.Any())
            {
                TceNumber = GetTceNumber(technicalRequrementsTask);
            }

            TaskOpenDate = technicalRequrementsTask.Start?.Date ?? DateTime.Today.AddYears(10);

            TaskStartDate = technicalRequrementsTask.Start;
            TaskFinishDate = technicalRequrementsTask.Finish;

            Order = salesUnit.Order?.ToString();

            OrderPositions = SalesUnits.Select(unit => unit.OrderPosition).GetOrderPositions();

            var owners = new List<Company> { salesUnit.Facility.OwnerCompany };
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ToStringEnum();

            var contragent = salesUnit.Specification?.Contract.Contragent ?? GetTenderWinner() ?? salesUnit.Facility.OwnerCompany;
            Contragent = contragent.ToString();

            Facility = salesUnit.Facility.ToString();

            Product = $"{salesUnit.Product.ProductType} {salesUnit.Product.Designation}";

            Amount = SalesUnits.Count;

            this.OrderInTakeStatus = GetOrderInTakeStatus();

            if (TaskStartDate.HasValue)
            {
                var dates = Offers
                    .Where(offer => offer.Project.Id == salesUnit.Project.Id)
                    .Select(offer => offer.Date)
                    .Where(x => x >= this.TaskStartDate.Value)
                    .ToList();

                if (dates.Any())
                {
                    FirstOfferDate = dates.Min();
                }

                if (FirstOfferDate.HasValue)
                {
                    Term = (FirstOfferDate.Value - TaskStartDate.Value).Days;
                }
            }


            if (salesUnit.IsLoosen)
            {
                CostOfWinner = salesUnit.Cost;
                Cost = Math.Round(CostOfWinner.Value * (1.0 + _random.Next(5, 20) / 100.0), MidpointRounding.ToEven);

                if (salesUnit.ProductionTerm < GlobalAppProperties.Actual.StandartTermFromStartToEndProduction)
                {
                    LossReason = "Сроки производства";
                }
            }
            else
            {
                Cost = salesUnit.Cost;
            }

            Manager = salesUnit.Project.Manager?.Employee?.Person.ToString();

            BackManager = technicalRequrementsTask.BackManager?.Employee?.Person.ToString();

            RealizationDate = salesUnit.RealizationDateCalculated;


            if (salesUnit.IsLoosen)
            {
                CommonStatus = string.IsNullOrEmpty(Order) ? "Конкурс проигран" : "Заказ аннулирован";
            }
            else if (OrderInTakeDate < DateTime.Today) CommonStatus = $"Факт ОИТ {OrderInTakeDate.Year}";
            else if (OrderInTakeDate.Year > DateTime.Today.Year) CommonStatus = $"Закупка или тендер перенесены на {OrderInTakeDate.Year} год";

            Producer = salesUnit.Producer?.ToString() ?? string.Empty;
            ProducerWinner = Producer;
        }

        private string GetTceNumber(TechnicalRequrementsTask requrementsTask)
        {
            try
            {
                return requrementsTask.PriceCalculations
                    .SelectMany(calculation => calculation.PriceCalculationItems.SelectMany(item => item.StructureCosts))
                    .Select(structureCost => structureCost.Number)
                    .Where(x => string.IsNullOrEmpty(x) == false)
                    .Select(x => x.Split(' ').First())
                    .Distinct()
                    .OrderBy(x => x)
                    .ToStringEnum();
            }
            catch (Exception)
            {
                return string.Empty;
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

        private string GetOrderInTakeStatus()
        {
            var salesUnit = SalesUnits.First();

            if (salesUnit.IsLoosen)
            {
                return "14 - Проиграно другому производителю";
            }

            if (salesUnit.RealizationDateCalculated < DateTime.Today)
            {
                return "0 - Продукт реализован";
            }

            if (salesUnit.StartProductionConditionsDoneDate.HasValue &&
                salesUnit.StartProductionConditionsDoneDate.Value <= DateTime.Today)
            {
                return "1 - Условие на запуск производства исполнено";
            }

            if (salesUnit.Specification != null)
            {
                return salesUnit.Specification.Date <= DateTime.Today
                    ? "2 - Контракт подписан"
                    : "3 - Контракт на оформлении";
            }

            if (salesUnit.Producer != null && salesUnit.Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
            {
                return "4 - Большая вероятность реализации";
            }

            return "7 - В проработке";
        }

        private Company GetTenderWinner()
        {
            var tenders = _tenders.Where(tender => Equals(SalesUnits.First().Project.Id, tender.Project.Id)).ToList();
            if (!tenders.Any()) return null;

            //поставщик
            var supplier = tenders
                .Where(tender => tender.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply))
                .OrderBy(tender => tender.DateClose)
                .LastOrDefault()?.Winner;
            if (supplier != null) return supplier;

            //подрядчик
            var worker = tenders
                .Where(tender => tender.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork))
                .OrderBy(tender => tender.DateClose)
                .LastOrDefault()?.Winner;

            return worker;
        }
    }
}
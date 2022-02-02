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

        [Designation("������"), OrderStatus(-10)]
        public string TceNumber { get; }

        [Designation("���� �������"), OrderStatus(-20)]
        public DateTime TaskOpenDate { get; }

        [Designation("��������"), OrderStatus(-30)]
        public string Manager { get; }

        [Designation("Back-manager"), OrderStatus(-300)]
        public string BackManager { get; }

        [Designation("�������� �������"), OrderStatus(-40)]
        public string FacilityOwners { get; }

        [Designation("����������"), OrderStatus(-50)]
        public string Contragent { get; }

        [Designation("������"), OrderStatus(-60)]
        public string Facility { get; }

        [Designation("�������"), OrderStatus(-70)]
        public string Product { get; }

        [Designation("������ �������"), OrderStatus(-80)]
        public string CommonStatus { get; }

        [Designation("��� �������"), OrderStatus(-90)]
        public int YearOfProject => this.OrderInTakeDate.Year;

        [Designation("�������������"), OrderStatus(-100)]
        public string Producer { get; }

        [Designation("������� ���������"), OrderStatus(-110)]
        public string LossReason { get; }

        [Designation("������������� - ����������"), OrderStatus(-120)]
        public string ProducerWinner { get; }

        [Designation("����"), OrderStatus(-130)]
        public double Cost { get; }

        [Designation("���� ����������"), OrderStatus(-140)]
        public double? CostOfWinner { get; }

        [Designation("����������� �� �������"), OrderStatus(-150)]
        public string CommentTender { get; }

        [Designation("���� ����������"), OrderStatus(-160)]
        public DateTime RealizationDate { get; }

        [Designation("�������"), OrderStatus(-170)]
        public string OrderPositions { get; }

        [Designation("���."), OrderStatus(-180)]
        public int Amount { get; }

        [Designation("������ ���"), OrderStatus(-190)]
        public string OrderInTakeStatus { get; }

        [Designation("���� ���"), OrderStatus(-200)]
        public double CostOrderInTake => CostOfWinner ?? Cost;

        [Designation("������"), OrderStatus(-210)]
        public string Currency => "RUB";

        [Designation("���� 1 ���"), OrderStatus(-220)]
        public DateTime? FirstOfferDate { get; }

        [Designation("�� ������ ���������� �� ��� (����)"), OrderStatus(-230)]
        public int? Term { get; }

        [Designation("���� ������ ����������"), OrderStatus(-240)]
        public DateTime? TaskStartDate { get; }

        [Designation("���� ������ ����������"), OrderStatus(-250)]
        public DateTime? TaskFinishDate { get; }
        
        [Designation("�����"), OrderStatus(-260)]
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
                    LossReason = "����� ������������";
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
                CommonStatus = string.IsNullOrEmpty(Order) ? "������� ��������" : "����� �����������";
            }
            else if (OrderInTakeDate < DateTime.Today) CommonStatus = $"���� ��� {OrderInTakeDate.Year}";
            else if (OrderInTakeDate.Year > DateTime.Today.Year) CommonStatus = $"������� ��� ������ ���������� �� {OrderInTakeDate.Year} ���";

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
            var designation = product.Designation.Replace("����-", string.Empty);

            var categories = new List<string>
            {
                "���-35",

                "���-110",
                "���-220",

                "���-35",
                "���-110",
                "����-110",
                "���-220",
                "���-1�1-220",
                "���-330",
                "���-500",
                "���-750",

                "���-35",
                "���-110",
                "���-220",
                "���-330",

                "���-35",
                "���-110",
                "���-220",
                "���-330",
                "���-500",

                "���-110",
                "���-220",

                "���-110",
                "���-1�-110",
                "���-1�-110",
                "���-2-110",
                "���-220",
                "���-1�-220",
                "���-1�-220",
                "���-2-220",

                "����-110",
                "����-1�-110",
                "����-1�-110",
                "����-2-110",
                "����-220",
                "����-1�-220",
                "����-1�-220",
                "����-2-220",

                "���-35",
                "���-110",
                "���-220",

                "����-110",
                "����-220",

                "����-110",
                "����-220"
            };

            foreach (var category in categories)
            {
                if (designation.StartsWith(category))
                    return category;
            }

            if (designation.StartsWith("���") || designation.StartsWith("���"))
                return "����";

            return "������";
        }

        private string GetOrderInTakeStatus()
        {
            var salesUnit = SalesUnits.First();

            if (salesUnit.IsLoosen)
            {
                return "14 - ��������� ������� �������������";
            }

            if (salesUnit.RealizationDateCalculated < DateTime.Today)
            {
                return "0 - ������� ����������";
            }

            if (salesUnit.StartProductionConditionsDoneDate.HasValue &&
                salesUnit.StartProductionConditionsDoneDate.Value <= DateTime.Today)
            {
                return "1 - ������� �� ������ ������������ ���������";
            }

            if (salesUnit.Specification != null)
            {
                return salesUnit.Specification.Date <= DateTime.Today
                    ? "2 - �������� ��������"
                    : "3 - �������� �� ����������";
            }

            if (salesUnit.Producer != null && salesUnit.Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
            {
                return "4 - ������� ����������� ����������";
            }

            return "7 - � ����������";
        }

        private Company GetTenderWinner()
        {
            var tenders = _tenders.Where(tender => Equals(SalesUnits.First().Project.Id, tender.Project.Id)).ToList();
            if (!tenders.Any()) return null;

            //���������
            var supplier = tenders
                .Where(tender => tender.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply))
                .OrderBy(tender => tender.DateClose)
                .LastOrDefault()?.Winner;
            if (supplier != null) return supplier;

            //���������
            var worker = tenders
                .Where(tender => tender.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork))
                .OrderBy(tender => tender.DateClose)
                .LastOrDefault()?.Winner;

            return worker;
        }
    }
}
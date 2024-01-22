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

        [Designation("������"), OrderStatus(-3)]
        public string ProjectName { get; }

        [Designation("�������� �������"), OrderStatus(-3)]
        public string FacilityOwners { get; }

        [Designation("�����"), OrderStatus(-1)]
        public string Order { get; }

        [Designation("����������"), OrderStatus(-4)]
        public string Contragent { get; }

        [Designation("������"), OrderStatus(-6)]
        public string Facility { get; }

        [Designation("������"), OrderStatus(-8)]
        public string Region { get; }

        [Designation("����������� �����"), OrderStatus(-9)]
        public string District { get; }

        [Designation("�������"), OrderStatus(-10)]
        public string Segment { get; }

        [Designation("��� ��������"), OrderStatus(-12)]
        public string ProductType { get; }

        [Designation("�����������"), OrderStatus(-15)]
        public string Designation { get; }

        [Designation("��������� ������������"), OrderStatus(-16)]
        public string ProductCategory { get; }

        [Designation("���."), OrderStatus(-17)]
        public int Amount { get; }
        
        [Designation("������"), OrderStatus(-18)]
        public string Status { get; }

        [Designation("����"), OrderStatus(-21)]
        public double Cost { get; }

        [Designation("���������"), OrderStatus(-23)]
        public double Sum => Cost * Amount;


        [Designation("��������"), OrderStatus(-33)]
        public string Manager { get; }


        [Designation("�������������"), OrderStatus(-33)]
        public string Producer { get; }

        public string Builder { get; }
        public string ProjectMaker { get; }
        public string Supplier { get; }


        [Designation("����� ���"), OrderStatus(-41)]
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        [Designation("��� ���"), OrderStatus(-42)]
        public int OrderInTakeYear => OrderInTakeDate.Year;

        [Designation("���"), OrderStatus(-43)]
        public DateTime OrderInTakeDate { get; }

        [Designation("���� ��������"), OrderStatus(-47)]
        public DateTime ShipmentDate { get; }

        [Designation("���� ��������"), OrderStatus(-47)]
        public DateTime DeliveryDate { get; }

        [Designation("���� ����������"), OrderStatus(-48)]
        public DateTime RealizationDate { get; }

        [Designation("���� ���������� ���������"), OrderStatus(-51)]
        public DateTime RealizationDateRequared { get; }

        [Designation("���� ���������� �� ���������"), OrderStatus(-52)]
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

        private string GetSegment()
        {
            //���������� ������ ���� ������������
            var actualActivities = new List<ActivityFieldEnum>
            {
                ActivityFieldEnum.ElectricityDistribution,
                ActivityFieldEnum.ElectricityTransmission,
                ActivityFieldEnum.ElectricityGeneration,
                ActivityFieldEnum.Fuel,
                ActivityFieldEnum.RailWay,
                ActivityFieldEnum.IndustrialEnterprise
            };

            //������� �� ���������� �������
            var owner = SalesUnits.First().Facility.OwnerCompany;
            do
            {
                var activityField = owner.ActivityFilds.FirstOrDefault(x => actualActivities.Contains(x.ActivityFieldEnum));
                if (activityField != null) return activityField.Name;
                owner = owner.ParentCompany;
            } while (owner != null);

            return "������������ �����������";
        }

        private string GetStatus(SalesUnit salesUnit)
        {
            if (salesUnit.IsLoosen)
                return "���������";

            if (salesUnit.IsWon)
                return "��������";

            return "";
        }

        //private Company GetTenderWinner()
        //{
        //    var tenders = _tenders.Where(x => Equals(SalesUnits.First().Project.Id, x.Project.Id)).ToList();
        //    if (!tenders.Any()) return null;

        //    //���������
        //    var supplier = tenders
        //        .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply))
        //        .OrderBy(x => x.DateClose)
        //        .LastOrDefault()?.Winner;
        //    if (supplier != null) return supplier;

        //    //���������
        //    var worker = tenders
        //        .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork))
        //        .OrderBy(x => x.DateClose)
        //        .LastOrDefault()?.Winner;

        //    return worker;
        //}
    }
}
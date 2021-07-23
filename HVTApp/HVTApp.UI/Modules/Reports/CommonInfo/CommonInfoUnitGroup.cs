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

        [Designation("���������� �� ���"), OrderStatus(-150)]
        public string TceInfo => SalesUnit.ActualPriceCalculationItem(_unitOfWork)?.ToString() ?? "no information";

        [Designation("���������� ������������ (�� �������)"), OrderStatus(-140)]
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

        [Designation("�����"), OrderStatus(-1)]
        public string Order { get; }

        [Designation("�������"), OrderStatus(-2)]
        public string OrderPositions { get; }

        [Designation("�������� �������"), OrderStatus(-3)]
        public string FacilityOwners { get; }

        [Designation("����������"), OrderStatus(-4)]
        public string Contragent { get; }

        [Designation("��� �����������"), OrderStatus(-5)]
        public string ContragentType { get; }

        [Designation("������"), OrderStatus(-6)]
        public string Facility { get; }

        [Designation("������ ��������"), OrderStatus(-1000)]
        public Country Country { get; }

        [Designation("����������� �����"), OrderStatus(-9)]
        public string District { get; }

        [Designation("�������"), OrderStatus(-10)]
        public string Segment { get; }

        [Designation("��� ��������"), OrderStatus(-12)]
        public string ProductType { get; }

        [Designation("�����������"), OrderStatus(-15)]
        public string Designation { get; }

        [Designation("���."), OrderStatus(-17)]
        public int Amount { get; }

        [Designation("���, %"), OrderStatus(-20)]
        public double Vat { get; }

        [Designation("����"), OrderStatus(-21)]
        public double Cost { get; }

        [Designation("���������"), OrderStatus(-23)]
        public double Sum => Cost * Amount;

        [Designation("��������� � ���"), OrderStatus(-24)]
        public double SumWithVat => Vat * Sum;

        [Designation("���������"), OrderStatus(-25)]
        public double CostDelivery { get; }

        [Designation("��������� ������ � ������������� �����"), OrderStatus(-26)]
        public double FixedCost { get; }

        [Designation("��������"), OrderStatus(-33)]
        public string Manager { get; }

        [Designation("�������"), OrderStatus(-34)]
        public string ContractNumber { get; }

        [Designation("������������"), OrderStatus(-35)]
        public string SpecificationNumber { get; }

        [Designation("���� ��������"), OrderStatus(-36)]
        public DateTime? ContractDate { get; }

        [Designation("���� ������������"), OrderStatus(-37)]
        public DateTime? SpecificationDate { get; }

        [Designation("���"), OrderStatus(-43)]
        public DateTime OrderInTakeDate { get; }

        [Designation("������� ������"), OrderStatus(-59)]
        public PaymentConditionSet PaymentConditionSet { get; }

        [Designation("��� ��������"), OrderStatus(-137)]
        public string DeliveryType { get; }

        [Designation("����� ��������"), OrderStatus(-138)]
        public string DeliveryAddress { get; }

        [Designation("���� ������������"), OrderStatus(-245)]
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
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             

            DeliveryType = Math.Abs(CostDelivery) > 0  ? "��������" : "���������";

            DeliveryAddress = salesUnit.GetDeliveryAddressString();
        }

        private string GetContragentType(Company contragent)
        {
            if (Contragent == null)
                return "��� ������";

            var salesUnit = Units.First().SalesUnit;
            if (Equals(salesUnit.Facility.OwnerCompany, contragent) ||
                salesUnit.Facility.OwnerCompany.ParentCompanies().Contains(contragent))
                return "�������� ��������";

            if (_tenders.FirstOrDefault(x => Equals(x.Winner, contragent)) != null) return "���������";

            return "���������";
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
            var owner = Units.First().SalesUnit.Facility.OwnerCompany;
            do
            {
                var activityField = owner.ActivityFilds.FirstOrDefault(x => actualActivities.Contains(x.ActivityFieldEnum));
                if (activityField != null)
                    return activityField.Name;
                owner = owner.ParentCompany;
            } while (owner != null);

            return "������������ �����������";
        }

        private string SegmentConverter(string segment)
        {
            switch (segment)
            {
                case "��������� ��������������": return "���������";
                case "�������� ������": return "���";
                case "�������� ��������������": return "����";
                case "������������ �����������": return "��������������";
                case "������������� ��������������": return "�������������";
                case "��������-�������������� ������": return "��������.";
            }

            return segment;
        }


        private Company GetTenderWinner()
        {
            var tenders = _tenders.Where(tender => Equals(Units.First().SalesUnit.Project.Id, tender.Project.Id)).ToList();
            if (!tenders.Any()) return null;

            //���������
            var supplier = tenders
                .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply))
                .OrderBy(x => x.DateClose)
                .LastOrDefault()?.Winner;
            if (supplier != null) return supplier;

            //���������
            var worker = tenders
                .Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork))
                .OrderBy(x => x.DateClose)
                .LastOrDefault()?.Winner;

            return worker;
        }
    }
}
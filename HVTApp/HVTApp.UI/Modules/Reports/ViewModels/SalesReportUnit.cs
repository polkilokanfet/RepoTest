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

        [Designation("��/�������"), OrderStatus(-7)]
        public string IsExport { get; }

        [Designation("��/���"), OrderStatus(-8)]
        public string RfSng { get; }

        [Designation("������"), OrderStatus(-9)]
        public string Region { get; }

        [Designation("�������"), OrderStatus(-10)]
        public string Segment { get; }

        [Designation("��������� ������������"), OrderStatus(-11)]
        public string Kat { get; } = "���";

        [Designation("��� ��������"), OrderStatus(-12)]
        public string ProductType { get; }

        [Designation("��� ��������������"), OrderStatus(-13)]
        public string TransformerType { get; } = "-";

        [Designation("��������� ��������"), OrderStatus(-14)]
        public string Power { get; } = "-";

        [Designation("�����������"), OrderStatus(-15)]
        public string Designation { get; }

        [Designation("��������� ������������"), OrderStatus(-16)]
        public string ProductCategory { get; }

        [Designation("���."), OrderStatus(-17)]
        public int Amount { get; }
        
        [Designation("������"), OrderStatus(-18)]
        public string Status { get; }

        [Designation("��������� �������"), OrderStatus(-19)]
        public string StatusCategory { get; private set; }

        [Designation("���, %"), OrderStatus(-20)]
        public double Vat { get; } = 1.2;

        [Designation("����"), OrderStatus(-21)]
        public double Cost { get; }

        [Designation("���� � ���"), OrderStatus(-22)]
        public double CostWithVat => Vat * Cost;

        [Designation("���������"), OrderStatus(-23)]
        public double Sum => Cost * Amount;

        [Designation("��������� � ���"), OrderStatus(-24)]
        public double SumWithVat => Vat * Sum;

        [Designation("���������"), OrderStatus(-25)]
        public double CostDelivery { get; }
        
        [Designation("��������� ������ � ������������� �����"), OrderStatus(-26)]
        public double FixedCost { get; }

        [Designation("��"), OrderStatus(-27)]
        public double Kz { get; } = 0.0;

        [Designation("�������"), OrderStatus(-28)]
        public double Proceeds => Sum + CostDelivery + FixedCost;

        [Designation("��"), OrderStatus(-29)]
        public double Price { get; }

        [Designation("�� �� ���."), OrderStatus(-30)]
        public double PriceOnAmount => Price * Amount;

        [Designation("��, ���."), OrderStatus(-31)]
        public double MarginalIncomeNatural => Proceeds + PriceOnAmount;

        [Designation("��, %"), OrderStatus(-32)]
        public double MarginalIncome => Math.Abs(Proceeds) < 0.000001 ? 0.0 : 100.0 * MarginalIncomeNatural / Proceeds;


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

        [Designation("����� ������������"), OrderStatus(-38)]
        public int? SpecificationMonth => SpecificationDate?.Month;

        [Designation("������� ������������"), OrderStatus(-39)]
        public int? SpecificationQuarter => SpecificationMonth / 3;

        [Designation("��� ���������"), OrderStatus(-40)]
        public int ContractYear { get; }

        [Designation("����� ���"), OrderStatus(-41)]
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        [Designation("��� ���"), OrderStatus(-42)]
        public int OrderInTakeYear => OrderInTakeDate.Year;

        [Designation("���"), OrderStatus(-43)]
        public DateTime OrderInTakeDate { get; }

        [Designation("���� �������"), OrderStatus(-44)]
        public DateTime StartProductionDate { get; }

        [Designation("����� �������"), OrderStatus(-45)]
        public int StartProductionMonth => StartProductionDate.Month;

        [Designation("��� �������"), OrderStatus(-46)]
        public int StartProductionYear => StartProductionDate.Year;

        [Designation("���� ��������"), OrderStatus(-47)]
        public DateTime ShipmentDate { get; }

        [Designation("���� ����������"), OrderStatus(-48)]
        public DateTime RealizationDate { get; }

        [Designation("����� ����������"), OrderStatus(-49)]
        public int RealizationDatetMonth => RealizationDate.Month;

        [Designation("��� ����������"), OrderStatus(-50)]
        public int RealizationDateYear => RealizationDate.Year;

        [Designation("���� ���������� ���������"), OrderStatus(-51)]
        public DateTime RealizationDateRequared { get; }

        [Designation("���� ���������� �� ���������"), OrderStatus(-52)]
        public DateTime? RealizationDateContract { get; }

        [Designation("������� ������"), OrderStatus(-59)]
        public PaymentConditionSet PaymentConditionSet { get; }

        [Designation("��� ��������"), OrderStatus(-137)]
        public string DeliveryType { get; }

        [Designation("����� ��������"), OrderStatus(-138)]
        public string DeliveryAddress { get; }

        //[Designation("����������")]
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
                IsExport = Country.Name == "������" ? "��" : Country.Name;
                if (Country.Name == "������")
                {
                    RfSng = "��";
                }
                else if (GetCountryUnions().Any(x => x.Name == "���"))
                {
                    RfSng = "���";
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

            DeliveryType = CostDelivery > 0  ? "��������" : "���������";

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
                return "��� ������";

            var salesUnit = SalesUnits.First();
            if (Equals(salesUnit.Facility.OwnerCompany, contragent) ||
                salesUnit.Facility.OwnerCompany.ParentCompanies().Contains(contragent))
                return "�������� ��������";

            if (_tenders.FirstOrDefault(x => Equals(x.Winner, contragent)) != null) return "���������";

            return "���������";
        }

        private List<CountryUnion> GetCountryUnions()
        {
            return Country == null
                ? new List<CountryUnion>()
                : _countryUnions.Where(x => x.Countries.ContainsById(Country)).ToList();
        }

        private string GetSegment()
        {
            //���������� ������ ���� ������������
            var actEnums = new List<ActivityFieldEnum>
            {
                ActivityFieldEnum.ElectricityDistribution,
                ActivityFieldEnum.ElectricityTransmission,
                ActivityFieldEnum.ElectricityGeneration,
                ActivityFieldEnum.Fuel,
                ActivityFieldEnum.RailWay
            };

            //������� �� ���������� �������
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
                return "0 - ������� ����������";
            }

            if (salesUnit.StartProductionConditionsDoneDate.HasValue &&
                salesUnit.StartProductionConditionsDoneDate.Value <= DateTime.Today)
            {
                StatusCategory = "1-3";
                return "1 - ������� �� ������ ������������ ���������";
            }

            if (salesUnit.Specification != null)
            {
                StatusCategory = "1-3";
                return salesUnit.Specification.Date <= DateTime.Today
                    ? "2 - �������� ��������"
                    : "3 - �������� �� ����������";
            }

            if (salesUnit.IsLoosen)
            {
                StatusCategory = "15";
                return "15 - ��������� ������� �������������";
            }

            if (salesUnit.Producer != null && salesUnit.Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
            {
                StatusCategory = "4-7";
                return "4 - ������� ����������� ����������";
            }

            StatusCategory = "4-7";
            return "7 - � ����������";
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
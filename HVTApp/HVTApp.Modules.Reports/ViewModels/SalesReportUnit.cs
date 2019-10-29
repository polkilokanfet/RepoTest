using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Structures;

namespace HVTApp.Modules.Reports.ViewModels
{
    public class SalesReportUnit : SalesUnit
    {
        private readonly List<CountryUnion> _countryUnions;
        private readonly List<Tender> _tenders;

        [Designation("�������� �������")]
        public Company FacilityOwner { get; }

        [Designation("�������� ������� (�������� ��������)")]
        public Company FacilityOwnerHead { get; }

        [Designation("����������")]
        public Company Contragent { get; }

        [Designation("��� �����������")]
        public string ContragentType { get; }

        [Designation("������ ��������")]
        public Country Country { get; }

        [Designation("����������� �����")]
        public List<CountryUnion> CountryUnions { get; }

        [Designation("�����")]
        public District District { get; }

        [Designation("������� �����")]
        public string Segment { get; }

        [Designation("��� ��������")]
        public ProductType ProductType { get; }

        [Designation("�����������")]
        public string Designation { get; }

        [Designation("������")]
        public string Status { get; }

        [Designation("���, %")]
        public double? Vat { get; }

        [Designation("������������� (��������������)"), OrderStatus(984)]
        public double PriceResult { get; }

        [Designation("��������� ������ � ������������� �����"), OrderStatus(983)]
        public double FixedCost { get; }

        /// <summary>
        /// ���������� ��������� ���� (�������� � ������������� ����� + ��������� ��������)
        /// </summary>
        private double FixedCostAndDelivery { get; }

        [Designation("��, %"), OrderStatus(977)]
        public double? MarginalIncome { get; }

        [Designation("��, ���."), OrderStatus(977)]
        public double? MarginalIncomeNatural { get; }

        [Designation("����������")]
        public string Voltage { get; }

        [Designation("������������ ����")]
        public DateTime? SpecificationDate { get; }


        [Designation("�������")]
        public string ContractNumber { get; }

        [Designation("������� ����")]
        public DateTime? ContractDate { get; }

        [Designation("������� ���")]
        public int ContractYear { get; }

        [Designation("������� �����")]
        public int ContractMonth { get; }

        [Designation("��� ��������")]
        public string DeliveryType { get; }

        [Designation("��������")]
        public Employee Manager { get; }

        [Designation("���� � ���")]
        public double CostWithVat { get; }

        #region Fake

        [Designation("���� (��������������)"), OrderStatus(989)]
        public double CostResult { get; }

        [Designation("���� ��� (��������������)")]
        public DateTime OrderInTakeDateResult { get; }

        [Designation("���� ���������� (��������������)")]
        public DateTime RealizationDateResult { get; }

        [Designation("��� ���������� (��������������)")]
        public int RealizationDateResultYear { get; }

        [Designation("����� ���������� (��������������)")]
        public int RealizationDateResultMonth { get; }


        [Designation("������� ������ (��������������)")]
        public PaymentConditionSet PaymentConditionSetResult { get; }

        #endregion

        public SalesReportUnit(
            SalesUnit salesUnit, 
            IEnumerable<Tender> tenders, 
            IEnumerable<ProductBlock> blocks,
            IEnumerable<CountryUnion> countryUnions)
        {
            SetProperties(salesUnit);

            var priceStructures = new PriceStructures(this, this.OrderInTakeDate, GlobalAppProperties.Actual.ActualPriceTerm, blocks);
            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();

            FacilityOwner = Facility.OwnerCompany;
            FacilityOwnerHead = GetFacilityOwnerHead();
            Contragent = Specification?.Contract.Contragent;
            ContragentType = GetContragentType();
            Country = GetCountry();
            CountryUnions = GetCountryUnions();
            District = Facility.Address?.Locality.Region.District;
            Segment = GetSegment();
            ProductType = Product.ProductType;
            Designation = Product.Designation;
            Status = GetStatus();
            Vat = Specification?.Vat;
            CostWithVat = (1.0 + Vat) * Cost ?? Cost;


            Voltage = Product.ProductBlock.Parameters.SingleOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;

            PriceResult = Price ?? priceStructures.TotalPriceFixedCostLess;
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

            DeliveryType = CostDelivery.HasValue ? "��������" : "���������";

            Manager = Project.Manager.Employee;

            RealizationDateResultYear = RealizationDateResult.Year;
            RealizationDateResultMonth = RealizationDateResult.Month;

            CostResult = FakeData?.Cost ?? Cost;
            OrderInTakeDateResult = this.FakeData?.OrderInTakeDate ?? this.OrderInTakeDate;
            RealizationDateResult = this.FakeData?.RealizationDate ?? this.RealizationDateCalculated;
            PaymentConditionSetResult = this.FakeData?.PaymentConditionSet ?? this.PaymentConditionSet;
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
                return "��� ������";

            if (Equals(FacilityOwner, Contragent) || FacilityOwner.ParentCompanies().Contains(Contragent))
                return "�������� ��������";

            var tender = _tenders.FirstOrDefault(x => Equals(x.Winner, Contragent));
            if (tender != null)
            {
                var sb = new StringBuilder();
                tender.Types.ForEach(x => sb.Append(x.Name).Append("; "));
                return sb.ToString();
            }

            return "���������";
        }

        private Country GetCountry()
        {
            //������ �� ������ �������
            if (Facility.Address != null)
                return Facility.Address.Locality.Region.District.Country;

            //������ �� ������ ��������� �������
            var company = FacilityOwner;
            while (company.ParentCompany != null && company.AddressLegal == null)
            {
                company = company.ParentCompany;
            }

            return company.AddressLegal?.Locality.Region.District.Country;
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
                return "0 - ������� ����������";
            }

            if (StartProductionConditionsDoneDate.HasValue &&
                StartProductionConditionsDoneDate.Value <= DateTime.Today)
            {
                return "1 - ������� �� ������ ������������ ���������";
            }

            if (Specification != null)
            {
                return Specification.Date <= DateTime.Today
                    ? "2 - �������� ��������"
                    : "3 - �������� �� ����������";
            }

            if (IsLoosen)
            {
                return "15 - ��������� ������� �������������";
            }

            if (Producer != null && Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
            {
                return "4 - ������� ����������� ����������";
            }

            return "7 - � ����������";
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
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesReportUnit : SalesUnit
    {
        public List<SalesUnit> SalesUnits { get; }
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

        [Designation("������")]
        public Region Region { get; }

        [Designation("�����")]
        public District District { get; }

        [Designation("������ ��������")]
        public Country Country { get; }

        [Designation("����������� �����")]
        public List<CountryUnion> CountryUnions { get; }


        [Designation("������� �����")]
        public string Segment { get; }

        [Designation("��� ��������")]
        public ProductType ProductType { get; }

        [Designation("�����������")]
        public string Designation { get; }

        [Designation("����������")]
        public int Amount { get; }

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
        public string Manager { get; }

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
            IEnumerable<SalesUnit> salesUnits, 
            IEnumerable<Tender> tenders, 
            IEnumerable<ProductBlock> blocks,
            IEnumerable<CountryUnion> countryUnions)
        {
            SalesUnits = salesUnits.ToList();
            var salesUnit = SalesUnits.First();
            SetProperties(salesUnit);

            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();

            Amount = SalesUnits.Count;
            FacilityOwner = Facility.OwnerCompany;
            FacilityOwnerHead = GetFacilityOwnerHead();

            Contragent = GetContragent();
            ContragentType = GetContragentType(Contragent);

            Region = Facility.GetRegion();
            District = Region?.District;
            Country = District?.Country;
            CountryUnions = GetCountryUnions();
            Segment = GetSegment();
            ProductType = Product.ProductType;
            Designation = Product.Designation;
            Status = GetStatus();
            Vat = Specification?.Vat;
            CostResult = FakeData?.Cost ?? Cost;
            CostWithVat = (1.0 + Vat)/100.0 * CostResult ?? CostResult;

            Voltage = Product.ProductBlock.Parameters.SingleOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;

            var priceStructures = GlobalAppProperties.PriceService.GetPriceStructures(this, this.OrderInTakeDate, GlobalAppProperties.Actual.ActualPriceTerm);
            PriceResult = Price ?? GlobalAppProperties.PriceService.GetPrice(salesUnit) ?? priceStructures.TotalPriceFixedCostLess;
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

            DeliveryType = CostDelivery.HasValue && CostDelivery.Value > 0  ? "��������" : "���������";

            var manager = Project.Manager.Employee;
            Manager = $"{manager.Person.Surname} {manager.Person.Name} {manager.Person.Patronymic}";

            OrderInTakeDateResult = this.FakeData?.OrderInTakeDate ?? this.OrderInTakeDate;
            RealizationDateResult = this.FakeData?.RealizationDate ?? this.RealizationDateCalculated;
            PaymentConditionSetResult = this.FakeData?.PaymentConditionSet ?? this.PaymentConditionSet;

            RealizationDateResultYear = RealizationDateResult.Year;
            RealizationDateResultMonth = RealizationDateResult.Month;
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

        private string GetContragentType(Company contragent)
        {
            if (Contragent == null)
                return "��� ������";

            if (Equals(FacilityOwner, Contragent) || FacilityOwner.ParentCompanies().Contains(Contragent))
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

        private Company GetContragent()
        {
            return Specification?.Contract.Contragent ?? GetTenderWinner() ?? Facility.OwnerCompany;
        }
    }
}
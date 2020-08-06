using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesReport
{
    public class SalesReportUnit
    {
        public List<SalesUnit> SalesUnits { get; }

        private readonly List<CountryUnion> _countryUnions;
        private readonly List<Tender> _tenders;
        private int? _daysToStartProduction;
        private double? _paymentStartProduction;
        private DateTime? _datePaymentStartProduction;
        private string _paymentTypeStartProduction;
        private int? _daysToEndProduction;
        private double? _paymentEndProduction;
        private DateTime? _datePaymentEndProduction;
        private string _paymentTypeEndProduction;
        private int? _daysToShipping;
        private double? _paymentShipping;
        private DateTime? _datePaymentShipping;
        private string _paymentTypeShipping;
        private int? _daysToDelivery;
        private double? _paymentDelivery;
        private DateTime? _datePaymentDelivery;
        private string _paymentTypeDelivery;

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

        [Designation("����������� �����"), OrderStatus(-9)]
        public string District { get; }

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
        public double Vat { get; }

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
        public double PriceOnAmount => -1.0 * Price * Amount;

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
        public int? SpecificationQuarter => (SpecificationMonth + 2) / 3;

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


        [Designation("���� �� (������ ������������)"), OrderStatus(-69)]
        public int? DaysToStartProduction
        {
            get { return _daysToStartProduction; }
            set { _daysToStartProduction = value; }
        }

        [Designation("������ (������ ������������)"), OrderStatus(-70)]
        public double? PaymentStartProduction
        {
            get { return _paymentStartProduction; }
            set { _paymentStartProduction = value; }
        }

        [Designation("���� (������ ������������)"), OrderStatus(-71)]
        public DateTime? DatePaymentStartProduction
        {
            get { return _datePaymentStartProduction; }
            set { _datePaymentStartProduction = value; }
        }

        [Designation("��� (������ ������������)"), OrderStatus(-72)]
        public string PaymentTypeStartProduction
        {
            get { return _paymentTypeStartProduction; }
            set { _paymentTypeStartProduction = value; }
        }


        [Designation("���� �� (��������� ������������)"), OrderStatus(-75)]
        public int? DaysToEndProduction
        {
            get { return _daysToEndProduction; }
            set { _daysToEndProduction = value; }
        }

        [Designation("������ (��������� ������������)"), OrderStatus(-76)]
        public double? PaymentEndProduction
        {
            get { return _paymentEndProduction; }
            set { _paymentEndProduction = value; }
        }

        [Designation("���� (��������� ������������)"), OrderStatus(-77)]
        public DateTime? DatePaymentEndProduction
        {
            get { return _datePaymentEndProduction; }
            set { _datePaymentEndProduction = value; }
        }

        [Designation("��� (��������� ������������)"), OrderStatus(-78)]
        public string PaymentTypeEndProduction
        {
            get { return _paymentTypeEndProduction; }
            set { _paymentTypeEndProduction = value; }
        }


        [Designation("���� �� (��������)"), OrderStatus(-81)]
        public int? DaysToShipping
        {
            get { return _daysToShipping; }
            set { _daysToShipping = value; }
        }

        [Designation("������� (��������)"), OrderStatus(-82)]
        public double? PaymentShipping
        {
            get { return _paymentShipping; }
            set { _paymentShipping = value; }
        }

        [Designation("���� (��������)"), OrderStatus(-83)]
        public DateTime? DatePaymentShipping
        {
            get { return _datePaymentShipping; }
            set { _datePaymentShipping = value; }
        }

        [Designation("��� (��������)"), OrderStatus(-84)]
        public string PaymentTypeShipping
        {
            get { return _paymentTypeShipping; }
            set { _paymentTypeShipping = value; }
        }


        [Designation("���� �� (��������)"), OrderStatus(-88)]
        public int? DaysToDelivery
        {
            get { return _daysToDelivery; }
            set { _daysToDelivery = value; }
        }

        [Designation("������ (��������)"), OrderStatus(-89)]
        public double? PaymentDelivery
        {
            get { return _paymentDelivery; }
            set { _paymentDelivery = value; }
        }

        [Designation("���� (��������)"), OrderStatus(-90)]
        public DateTime? DatePaymentDelivery
        {
            get { return _datePaymentDelivery; }
            set { _datePaymentDelivery = value; }
        }

        [Designation("��� (��������)"), OrderStatus(-91)]
        public string PaymentTypeDelivery
        {
            get { return _paymentTypeDelivery; }
            set { _paymentTypeDelivery = value; }
        }


        [Designation("��� ��������"), OrderStatus(-137)]
        public string DeliveryType { get; }

        [Designation("����� ��������"), OrderStatus(-138)]
        public string DeliveryAddress { get; }

        [Designation("������������"), OrderStatus(-139)]
        public DateTime? PickingDate { get; }

        [Designation("���������� ������������ (�� �������)"), OrderStatus(-140)]
        public string ProductsIncluded { get; }

        [Designation("������"), OrderStatus(-141)]
        public string PaymentsActual { get; }


        [Designation("���������� �� ���"), OrderStatus(-150)]
        public string TceInfo { get; }

        [Designation("�������� ���������"), OrderStatus(-160)]
        public string IsolationMaterial { get; }

        [Designation("���"), OrderStatus(-161)]
        public string IsolationDpu { get; }

        [Designation("���� ���������"), OrderStatus(-162)]
        public string IsolationColor { get; }



        [Designation("TenderStatus"), OrderStatus(-200)]
        public string TenderStatus { get; }

        [Designation("�������������"), OrderStatus(-201)]
        public string Producer { get; }

        #region Fake

        [OrderStatus(-500)]
        public bool CostIsFake { get; }
        [OrderStatus(-500)]
        public bool RealizationDateIsFake { get; }
        [OrderStatus(-500)]
        public bool OrderInTakeDateIsFake { get; }
        [OrderStatus(-500)]
        public bool PaymentConditionSetIsFake { get; }

        #endregion

        public SalesReportUnit(
            IEnumerable<SalesUnit> salesUnits, 
            IEnumerable<Tender> tenders, 
            IEnumerable<CountryUnion> countryUnions, 
            PriceCalculationItem priceCalculationItem)
        {
            SalesUnits = salesUnits.ToList();
            var salesUnit = SalesUnits.First();
            //SetProperties(salesUnit);

            _tenders = tenders.ToList();
            _countryUnions = countryUnions.ToList();

            Order = salesUnit.Order?.ToString();
            OrderPositions = SalesUnits.Select(x => x.OrderPosition).GetOrderPositions();

            var owners = new List<Company> {salesUnit.Facility.OwnerCompany};
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ToStringEnum();
            var contragent = salesUnit.Specification?.Contract.Contragent ?? GetTenderWinner() ?? salesUnit.Facility.OwnerCompany;
            Contragent = contragent.ToString();
            ContragentType = GetContragentType(contragent);
            Facility = salesUnit.Facility.ToString();
            var region = salesUnit.Facility.GetRegion();
            Country = region?.District.Country;
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
                else
                {
                    RfSng = "��";
                }
            }
            District = region?.District.Name;
            Segment = GetSegment();
            ProductType = salesUnit.Product.ProductType.Name;
            Designation = salesUnit.Product.Designation;
            ProductCategory = GetProductCategory(salesUnit.Product);
            Amount = SalesUnits.Count;
            Status = GetStatus();
            Vat = salesUnit.Vat / 100.0 + 1.0;
            Cost = salesUnit.FakeData?.Cost ?? salesUnit.Cost;
            var costDelivery = SalesUnits.Select(x => x.CostDelivery).Where(x => x.HasValue).Sum(x => x.Value);
            CostDelivery = -1.0 * costDelivery;

            var price = GlobalAppProperties.PriceService.GetPrice(salesUnit, salesUnit.OrderInTakeDate);
            Price = price.SumPriceTotal;
            FixedCost = -1.0 * price.SumFixedTotal * Amount;
            //FixedCostAndDelivery = CostDelivery.HasValue ? CostDelivery.Value + FixedCost : FixedCost;

            var manager = salesUnit.Project.Manager.Employee;
            Manager = $"{manager.Person.Surname}";

            if (salesUnit.Specification != null)
            {
                var specification = salesUnit.Specification;
                SpecificationNumber = specification.Number;
                SpecificationDate = specification.Date;

                ContractNumber = specification.Contract.Number;
                ContractDate = specification.Contract.Date;
                ContractYear = ContractDate.Value.Year;
            }

            RealizationDateContract = salesUnit.EndProductionDateByContractCalculated;

            OrderInTakeDate = salesUnit.FakeData?.OrderInTakeDate ?? salesUnit.OrderInTakeDate;
            StartProductionDate = salesUnit.StartProductionDateCalculated;
            ShipmentDate = salesUnit.ShipmentDateCalculated;
            RealizationDate = salesUnit.FakeData?.RealizationDate ?? salesUnit.RealizationDateCalculated;
            RealizationDateRequared = salesUnit.DeliveryDateExpected;

            PaymentConditionSet = salesUnit.FakeData?.PaymentConditionSet ?? salesUnit.PaymentConditionSet;
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.ProductionStart, ref _daysToStartProduction, ref _paymentStartProduction, ref _datePaymentStartProduction, ref _paymentTypeStartProduction);
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.ProductionEnd, ref _daysToEndProduction, ref _paymentEndProduction, ref _datePaymentEndProduction, ref _paymentTypeEndProduction);
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.Shipment, ref _daysToShipping, ref _paymentShipping, ref _datePaymentShipping, ref _paymentTypeShipping);
            SetPaymentsConditions(salesUnit, PaymentConditionPointEnum.Delivery, ref _daysToDelivery, ref _paymentDelivery, ref _datePaymentDelivery, ref _paymentTypeDelivery);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             

            DeliveryType = -1 * CostDelivery > 0  ? "��������" : "���������";

            DeliveryAddress = salesUnit.AddressDelivery?.ToString() ?? salesUnit.Facility.Address?.ToString() ?? $"{Country}, {region}, {Facility}";

            PickingDate = salesUnit.PickingDate;

            ProductsIncluded = SalesUnits
                .SelectMany(x => x.ProductsIncluded)
                .Distinct()
                .OrderBy(x => x.Product.Designation)
                .ToStringEnum();

            PaymentsActual = salesUnits.SelectMany(x => x.PaymentsActual).ToStringEnum();

            TceInfo = priceCalculationItem?.ToString();

            IsolationMaterial = salesUnit.Product.ProductBlock.Parameters
                .FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.IsolationMaterialGroup))?.Value;
            IsolationDpu = salesUnit.Product.ProductBlock.Parameters
                .FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.IsolationDpuGroup))?.Value;
            IsolationColor = salesUnit.Product.ProductBlock.Parameters
                .FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.IsolationColorGroup))?.Value;

            if (salesUnit.FakeData != null)
            {
                CostIsFake = salesUnit.FakeData.Cost.HasValue;
                RealizationDateIsFake = salesUnit.FakeData.RealizationDate.HasValue;
                OrderInTakeDateIsFake = salesUnit.FakeData.OrderInTakeDate.HasValue;
                PaymentConditionSetIsFake = salesUnit.FakeData.PaymentConditionSet != null;
            }

            if (salesUnit.IsLoosen)
            {
                TenderStatus = string.IsNullOrEmpty(Order) ? "������ ��������" : "����� �����������";
            }
            else if (OrderInTakeDate < DateTime.Today) TenderStatus = $"���� ��� {OrderInTakeDate.Year}";
            else if (OrderInTakeDate.Year > DateTime.Today.Year) TenderStatus = $"������� ��� ������ ���������� �� {OrderInTakeDate.Year} ���";

            Producer = salesUnit.Producer?.ToString() ?? string.Empty;
        }

        private void SetPaymentsConditions(
            SalesUnit salesUnit,
            PaymentConditionPointEnum point, 
            ref int? daysTo, 
            ref double? payment,
            ref DateTime? date,
            ref string paymentType)
        {
            var conditions = salesUnit.PaymentConditionSet.PaymentConditions
                .Where(x => x.PaymentConditionPoint.PaymentConditionPointEnum == point)
                .OrderBy(x => x.DaysToPoint)
                .ToList();

            if (conditions.Any())
            {
                daysTo = conditions.First().DaysToPoint;
                payment = SumWithVat * conditions.Sum(x => x.Part);
                date = salesUnit.GetPaymentDate(conditions.First());

                var realization = salesUnit.RealizationDateCalculated;
                if (date <= realization
                    && date.Value.Year == realization.Year
                    && date.Value.Month == realization.Month)
                {
                    paymentType = "��";
                }
                else
                {
                    paymentType = date < realization ? "��" : "��";
                }
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

        private string GetStatus()
        {
            var salesUnit = SalesUnits.First();

            if (salesUnit.IsLoosen)
            {
                StatusCategory = "14";
                return "14";
                //return "14 - ��������� ������� �������������";
            }

            if (salesUnit.RealizationDateCalculated < DateTime.Today)
            {
                StatusCategory = "0";
                return "0";
                //return "0 - ������� ����������";
            }

            if (salesUnit.StartProductionConditionsDoneDate.HasValue &&
                salesUnit.StartProductionConditionsDoneDate.Value <= DateTime.Today)
            {
                StatusCategory = "1-3";
                return "1";
                //return "1 - ������� �� ������ ������������ ���������";
            }

            if (salesUnit.Specification != null)
            {
                StatusCategory = "1-3";
                return salesUnit.Specification.Date <= DateTime.Today
                    ? "2"
                    : "3";
                //return salesUnit.Specification.Date <= DateTime.Today
                //    ? "2 - �������� ��������"
                //    : "3 - �������� �� ����������";
            }

            if (salesUnit.Producer != null && salesUnit.Producer.Id == GlobalAppProperties.Actual.OurCompany.Id)
            {
                StatusCategory = "4-7";
                return "4";
                //return "4 - ������� ����������� ����������";
            }

            StatusCategory = "4-7";
            return "7";
            //return "7 - � ����������";
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
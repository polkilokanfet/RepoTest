using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ������")]
    public partial class SalesUnit : BaseEntity, IUnit, ICloneable
    {
        #region Model

        [Designation("������"), OrderStatus(1000), Required]
        public virtual Facility Facility { get; set; }

        [NotForDetailsView, NotForListView]
        public virtual Guid FacilityId { get; set; }

        [Designation("�������"), Required, OrderStatus(995)]
        public virtual Product Product { get; set; }

        [NotForDetailsView, NotForListView]
        public virtual Guid ProductId { get; set; }

        [Designation("���������"), Required, OrderStatus(990)]
        public double Cost { get; set; }

        /// <summary>
        /// ��������� ��� ������������ ������� (� �������)
        /// </summary>
        [Designation("��������� ��� ������������ �������"), OrderStatus(990)]
        public double? CostWithReserve { get; set; }

        [Designation("�������������"), OrderStatus(985)]
        public double? Price { get; set; }

        [Designation("�����-����"), OrderStatus(980)]
        public double? LaborHours { get; set; }

        [Designation("���������� ��������")]
        public virtual List<ProductIncluded> ProductsIncluded { get; set; } = new List<ProductIncluded>();

        [Designation("������� ������"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [NotForDetailsView, NotForListView]
        public virtual Guid PaymentConditionSetId { get; set; }

        [Designation("���� ������������"), Required]
        public int ProductionTerm { get; set; } = GlobalAppProperties.Actual.StandartTermFromStartToEndProduction;

        [Designation("�����������"), MaxLength(150)]
        public string Comment { get; set; }

        #region ������

        [Designation("������"), OrderStatus(1005)]
        public virtual Project Project { get; set; }

        [NotForDetailsView, NotForListView]
        public virtual Guid ProjectId { get; set; }

        /// <summary>
        /// ��������� ���� ��������
        /// </summary>
        [Designation("��������� ���� ��������"), Required]
        public virtual DateTime DeliveryDateExpected { get; set; } =
            DateTime.Today.AddDays(GlobalAppProperties.Actual.StandartTermFromStartToEndProduction + 120).SkipWeekend();

        [Designation("�������������")]
        public virtual Company Producer { get; set; }

        [Designation("������� ���������")]
        public virtual List<LosingReason> LosingReasons { get; set; } = new List<LosingReason>();

        [Designation("���� ����������")]
        public virtual DateTime? RealizationDate { get; set; }

        #endregion

        #region ���������� � ������������

        [Designation("�����")]
        public virtual Order Order { get; set; }

        [Designation("�������"), MaxLength(4)]
        public string OrderPosition { get; set; }

        [Designation("�����"), MaxLength(10)]
        public string SerialNumber { get; set; }

        [Designation("���� ������")]
        public int? AssembleTerm { get; set; }

        [Designation("������ ��������� � ������������")]
        public DateTime? SignalToStartProduction { get; set; }

        [Designation("���� ���������� � ������������")]
        public DateTime? SignalToStartProductionDone { get; set; }

        [Designation("���� ������ ������������")]
        public DateTime? StartProductionDate { get; set; }

        [Designation("���� ������������")]
        public DateTime? PickingDate { get; set; }

        [Designation("�������� ���� ��������� ������������")]
        public DateTime? EndProductionPlanDate { get; set; }

        [Designation("���� ��������� ������������")]
        public DateTime? EndProductionDate { get; set; }

        #endregion

        #region ������������ ����������

        [Designation("������������")]
        public virtual Specification Specification { get; set; }

        [Designation("����������� �������")]
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

        [Designation("����������� �������")]
        public virtual List<PaymentPlanned> PaymentsPlanned { get; set; } = new List<PaymentPlanned>();

        [Designation("�������� �������")]
        public virtual Penalty Penalty { get; set; }

        #endregion

        #region ����������� ����������


        [Designation("��������� ��������"), OrderStatus(980)]
        public double? CostDelivery { get; set; }

        [Designation("��������� �������� �������� � �������� ���������")]
        public bool CostDeliveryIncluded { get; set; } = true;

        [Designation("���� ��������")]
        public int? ExpectedDeliveryPeriod { get; set; }

        [Designation("���� �������� ���������"), NotMapped]
        public int? ExpectedDeliveryPeriodCalculated => GlobalAppProperties.ShippingService.DeliveryTerm(this);

        [Designation("����� ��������")]
        public virtual Address AddressDelivery { get; set; }

        [Designation("���� ��������")]
        public virtual DateTime? ShipmentDate { get; set; }

        [Designation("���� �������� (��������)")]
        public virtual DateTime? ShipmentPlanDate { get; set; }

        [Designation("���� ��������")]
        public virtual DateTime? DeliveryDate { get; set; }

        #endregion

        [Designation("������")]
        public bool IsRemoved { get; set; } = false;

        [NotForWrapper, NotForDetailsView]
        public virtual List<PriceCalculationItem> PriceCalculationItems { get; set; } = new List<PriceCalculationItem>();


        [NotMapped, Designation("���������� �� �������������� ���������")]
        public bool AllowEditCost => Specification == null;

        [NotMapped, Designation("���������� �� �������������� �������")]
        public bool AllowEditProduct => SignalToStartProduction == null;

        [NotMapped, Designation("���������")]
        public bool IsLoosen => Producer != null && Producer.Id != GlobalAppProperties.Actual.OurCompany.Id;

        [NotMapped, Designation("��������")]
        public bool IsWon => Producer != null && Producer.Id == GlobalAppProperties.Actual.OurCompany.Id && OrderInTakeDate <= DateTime.Today;

        [NotMapped, Designation("���������")]
        public bool IsDone
        {
            get
            {
                if (this.Product.ProductBlock.IsService)
                    return RealizationDateCalculated < DateTime.Today;

                return RealizationDateCalculated < DateTime.Today && ShipmentDateCalculated < DateTime.Today;
            }
        }

        [NotMapped, Designation("Id ����������� ������� �����������")]
        public Guid ActualPriceCalculationItemId =>
            GlobalAppProperties.PriceService?.GetPriceCalculationItem(this)?.Id ?? default;




        /// <summary>
        /// ������ ������ �� ������
        /// </summary>
        [Designation("���� ������� ������� �� ������")]
        public DateTime? FirstPaymentDate { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Designation("��������")]
        public double PaidSum { get; set; }




        public override string ToString()
        {
            if (Order != null)
                return $"{Product} ��� {Facility} (�/� {Order.Number}, ���.{OrderPosition})";

            return $"{Product} ��� {Facility}";
        }


        #endregion

        #region Func

        /// <summary>
        /// ����� ����
        /// </summary>
        [Designation("����� ����")]
        public bool OrderIsTaken => !IsLoosen && StartProductionDateCalculated < DateTime.Today;

        /// <summary>
        /// ����� ����������
        /// </summary>
        [Designation("����� ����������")]
        public bool OrderIsRealized => RealizationDateCalculated < DateTime.Today;


        /// <summary>
        /// ��������� ��������� �������� �����
        /// </summary>
        [Designation("��������� ��������� ��������")]
        public bool AllowTotalRemove
        {
            get
            {
                if (Order != null) return false;
                return true;
            }
        }

        [Designation("����� �������� (���������)")]
        public virtual Address AddressDeliveryCalculated => this.AddressDelivery ?? this.Facility.Address;


        /// <summary>
        /// ��� ������� (����������� + ��������).
        /// </summary>
        //public IEnumerable<IPayment> Payments => PaymentsActual.Cast<IPayment>().Union(PaymentsPlannedByConditions);

        #region �����
        [Designation("��������?"), NotMapped]
        public bool IsPaid => Math.Abs(SumNotPaid) < 0.00001;

        /// <summary>
        /// ������������ ����� ��� ���
        /// </summary>
        [Designation("���������� ��� ���"), NotMapped]
        public double SumNotPaid => Cost - PaidSum;

        /// <summary>
        /// ���
        /// </summary>
        [Designation("���"), NotMapped]
        public double Vat => Specification?.Vat ?? GlobalAppProperties.Actual.Vat;

        /// <summary>
        /// ������������ ����� � ���
        /// </summary>
        [Designation("���������� � ���"), NotMapped]
        public double SumNotPaidWithVat => SumNotPaid * (100.0 + Vat) / 100;


        /// <summary>
        /// ������, ����������� ��� ������ ������������
        /// </summary>
        [Designation("����� ������ ������������"), NotMapped]
        public double SumToStartProduction => PaymentConditionSet.PaymentConditions
            .Where(condition =>
                condition.PaymentConditionPoint.PaymentConditionPointEnum == PaymentConditionPointEnum.ProductionStart &&
                condition.DaysToPoint <= 0)
            .Sum(condition => Cost * condition.Part);

        /// <summary>
        /// �����, ����������� ��� ��������
        /// </summary>
        [Designation("����� ��������"), NotMapped]
        public double SumToShipping => PaymentConditionSet.PaymentConditions
            .Where(condition => 
                condition.PaymentConditionPoint.PaymentConditionPointEnum == PaymentConditionPointEnum.ProductionStart || 
                condition.PaymentConditionPoint.PaymentConditionPointEnum == PaymentConditionPointEnum.ProductionEnd || 
                condition.PaymentConditionPoint.PaymentConditionPointEnum == PaymentConditionPointEnum.Shipment && 
                condition.DaysToPoint <= 0).
            Sum(condition => Cost * condition.Part);

        #endregion

        #region ����

        /// <summary>
        /// ���� ���, ���������� ����� (�� ������)
        /// </summary>
        [NotMapped]
        public DateTime? OrderInTakeDateInjected { get; set; }

        [Designation("���"), OrderStatus(990), NotMapped]
        public DateTime OrderInTakeDate
        {
            get
            {
                if (OrderInTakeDateInjected.HasValue)
                    return OrderInTakeDateInjected.Value.Date;

                //�� ���� ���������� ������������
                if (Specification?.SignDate != null)
                {
                    return Specification.SignDate.Value.Date;
                }

                //���� ��� ������ ������������ �� ��������� �����
                if (Specification != null)
                {
                    if (Cost > 0 && Math.Abs(SumToStartProduction) < 0.001)
                        return Specification.Date;
                }

                //������ ������ �� ������
                if (this.FirstPaymentDate.HasValue) return this.FirstPaymentDate.Value;
                
                //���� ������ ������������
                return StartProductionDateCalculated.Date;
            }
        }

        [Designation("��� ���"), OrderStatus(985), NotMapped]
        public int OrderInTakeYear => OrderInTakeDate.Year;

        [Designation("����� ���"), OrderStatus(980), NotMapped]
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        /// <summary>
        /// ���� ���������� �����
        /// </summary>
        /// <param name="sumToAchive"></param>
        /// <returns></returns>
        private DateTime? AchiveSumDate(double sumToAchive)
        {
            var accuracy = 0.001;
            double sum = 0;
            foreach (var payment in PaymentsActual.OrderBy(paymentActual => paymentActual.Date))
            {
                sum += payment.Sum;
                if (sumToAchive - sum <= accuracy) return payment.Date.Date;
            }

            var dic = PaymentConditionsDictionary;
            foreach (var payment in PaymentsPlannedActual.OrderBy(paymentPlanned => paymentPlanned.Date))
            {
                //���� ��������� �����-�� ������� ����� ������������ - �����
                if (!Equals(dic.First(x => x.Value < 1).Key, payment.Condition))
                {
                    return null;
                }

                sum += payment.Part * payment.Condition.Part * Cost;
                if (sumToAchive - sum <= accuracy) return payment.Date.Date;

                dic[payment.Condition] += payment.Part;
            }
            return null;
        }

        /// <summary>
        /// ���� ���������� ������� ��� ������� ������������
        /// </summary>
        [Designation("���� ���������� ������� ��� ������ ������������"), OrderStatus(870), NotMapped]
        public DateTime? StartProductionConditionsDoneDate
        {
            get
            {
                //���� ��� ������ ������������ �� ��������� �����
                if (Cost > 0 && Math.Abs(SumToStartProduction) < 0.001)
                {
                    if (Specification != null)
                        return Specification.Date.Date;
                }

                //���� ��������� ������
                return AchiveSumDate(SumToStartProduction);
            }
        }

        /// <summary>
        /// ���� ���������� ������� ��� ������������� ��������
        /// </summary>
        [Designation("���� ���������� ������� ��� ��������"), OrderStatus(865), NotMapped]
        public DateTime? ShippingConditionsDoneDate => AchiveSumDate(SumToShipping);

        /// <summary>
        /// ���������� ���� ������ ������������ (��� ������)
        /// </summary>
        [NotMapped]
        public DateTime? StartProductionDateInjected { get; set; }

        /// <summary>
        /// ��������� ���� ������ ������������.
        /// </summary>
        [Designation("������ ������������ (����.)"), OrderStatus(860), NotMapped]
        public DateTime StartProductionDateCalculated
        {
            get
            {
                if (StartProductionDateInjected.HasValue) return StartProductionDateInjected.Value.Date;

                if (StartProductionDate.HasValue) return StartProductionDate.Value.Date;

                //�� ���������� �������, ����������� ��� ������� ������������
                var startProductionConditionsDoneDate = StartProductionConditionsDoneDate;
                if (startProductionConditionsDoneDate.HasValue) return startProductionConditionsDoneDate.Value.Date;

                ////�� ���� �������� �/�
                //if (this.Order != null) return this.Order.DateOpen;

                //�� ������� ���������
                if (SignalToStartProduction.HasValue) return SignalToStartProduction.Value.Date;

                //�� ���� ������� �������
                if (FirstPaymentDate.HasValue) return FirstPaymentDate.Value;

                //�� ���� �������� ������������ �� ������
                if (DeliveryDate.HasValue) return DeliveryDate.Value.AddDays(-ProductionTerm).AddDays(-DeliveryPeriodCalculated).SkipPastAndWeekend().Date;

                //�� ���� ����������
                if (RealizationDate.HasValue) return RealizationDate.Value.AddDays(-ProductionTerm).SkipPastAndWeekend().Date;

                //���� ���������
                if (IsLoosen) return DeliveryDateExpected.AddDays(-ProductionTerm).Date;

                //�� ����������� ���� �������� �� ������
                return DeliveryDateExpected.AddDays(-ProductionTerm).AddDays(-DeliveryPeriodCalculated).SkipPastAndWeekend().Date;
            }
        }

        /// <summary>
        /// ��������� ���� ��������� ������������.
        /// </summary>
        [Designation("��������� ������������ (����.)"), OrderStatus(855), NotMapped]
        public DateTime EndProductionDateCalculated
        {
            get
            {
                //�� ���� ������������
                if (EndProductionDate.HasValue) return EndProductionDate.Value.Date;

                //���� ���������
                if (IsLoosen)
                    return DeliveryDateExpected.AddDays(-DeliveryPeriodCalculated).Date;

                //�� ���� ������������
                if (PickingDate.HasValue)
                {
                    var assembleTerm = this.AssembleTerm ?? GlobalAppProperties.Actual.StandartTermFromPickToEndProduction;
                    return PickingDate.Value.AddDays(assembleTerm).SkipPastAndWeekend().Date;
                }

                //�� ���� ���������� � ������������ (����)
                if (EndProductionPlanDate.HasValue) return EndProductionPlanDate.Value.Date.SkipPast();

                //�� ����� ������������
                return StartProductionDateCalculated.AddDays(ProductionTerm).SkipPastAndWeekend().Date;
            }
        }

        /// <summary>
        /// ��������� ���� ��������� ������������.
        /// </summary>
        [Designation("��������� ������������ �� ��������"), OrderStatus(854), NotMapped]
        public DateTime EndProductionDateByContractCalculated => StartProductionDateCalculated.AddDays(ProductionTerm).Date;

        /// <summary>
        /// ��������� ���� ����������.
        /// </summary>
        [Designation("��������� ���� ����������"), OrderStatus(850), NotMapped]
        public DateTime RealizationDateCalculated => RealizationDate?.Date ?? DeliveryDateCalculated.Date;

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        [Designation("��������� ���� ��������"), OrderStatus(845), NotMapped]
        public DateTime ShipmentDateCalculated
        {
            get
            {
                //�� �������� ���� ��������
                if (ShipmentDate.HasValue)
                    return ShipmentDate.Value.Date;

                //���� ���������
                if (IsLoosen)
                    return EndProductionDateCalculated.Date;

                //�� �������� ���� ��������
                if (ShipmentPlanDate.HasValue)
                {
                    if (ShippingConditionsDoneDate.HasValue)
                    {
                        if (ShipmentPlanDate.Value >= ShippingConditionsDoneDate &&
                            ShipmentPlanDate.Value >= EndProductionDateCalculated)
                            return ShipmentPlanDate.Value.Date;
                    }
                    else
                    {
                        if (ShipmentPlanDate.Value >= EndProductionDateCalculated)
                            return ShipmentPlanDate.Value.Date;
                    }
                }

                //�� ���� ���������� ������� ��� ��������
                if (ShippingConditionsDoneDate.HasValue &&
                    ShippingConditionsDoneDate >= EndProductionDateCalculated)
                    return ShippingConditionsDoneDate.Value.SkipPastAndWeekend().Date;

                //�� ���� ��������� ������������
                return EndProductionDateCalculated.SkipPastAndWeekend().Date;
            }
        }

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        [Designation("��������� ���� ��������"), OrderStatus(840), NotMapped]
        public DateTime DeliveryDateCalculated
        {
            get
            {
                if (DeliveryDate.HasValue) return DeliveryDate.Value.Date;
                return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).SkipWeekend().Date;
            }
        }

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        [Designation("��������� ���� ��������"), NotMapped]
        public double DeliveryPeriodCalculated
        {
            get
            {
                //�� ���������� ����� ��������
                if (ExpectedDeliveryPeriod.HasValue) return ExpectedDeliveryPeriod.Value;

                //�� ����������
                if (ExpectedDeliveryPeriodCalculated.HasValue)
                    return ExpectedDeliveryPeriodCalculated.Value;

                return 3;
            }
        }


        #endregion

        #region �������

        /// <summary>
        /// ���� �� �������.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DateTime GetPaymentDate(PaymentCondition condition)
        {
            switch (condition.PaymentConditionPoint.PaymentConditionPointEnum)
            {
                case PaymentConditionPointEnum.ProductionStart:
                    return StartProductionDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPointEnum.ProductionEnd:
                    return EndProductionDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPointEnum.Shipment:
                    return ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPointEnum.Delivery:
                    return DeliveryDateCalculated.AddDays(condition.DaysToPoint);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// ��������� ������� � ������� ��� ����������
        /// </summary>
        private Dictionary<PaymentCondition, double> PaymentConditionsDictionary => GetConditionsDictionary(PaidSum);

        private Dictionary<PaymentCondition, double> GetConditionsDictionary(double sum)
        {
            var result = new Dictionary<PaymentCondition, double>();

            //���� ��������� ������� ��� ������ ������� - �������
            if (Cost < 0.00001) return result;

            //���������� �������
            var paidRest = sum;

            //����� ��� ������� � ������������� ��
            List<PaymentCondition> conditions = PaymentConditionSet.PaymentConditions.OrderBy(paymentCondition => paymentCondition).ToList();

            foreach (var condition in conditions)
            {
                double conditionSum = condition.Part * Cost;
                double paidConditionPart = 0;
                //���� ������� ������� �� ��������
                if (paidRest >= conditionSum)
                {
                    paidConditionPart = 1;
                    paidRest -= conditionSum;
                }
                else
                {
                    paidConditionPart = paidRest / conditionSum;
                    paidRest = 0;
                }
                result.Add(condition, paidConditionPart);
            }
            return result;
        }

        /// <summary>
        /// ���������� �������� �������. 
        /// !!!�����!!! ��������� ����� ��������!
        /// </summary>
        [NotMapped]
        public List<PaymentPlanned> PaymentsPlannedActual
        {
            get
            {
                var result = new List<PaymentPlanned>();

                //���� ������ �� ��������, �� � �� �������� ������� �� ����� �������� ��������
                if (this.Project != null && this.Project.ForReport == false)
                {
                    return result;
                }

                //������� ������� � ����������� �� ����������
                var dictionary = PaymentConditionsDictionary;

                foreach (var payment in PaymentsPlanned)
                {
                    //���� ��������� ������� ���������� � ��� �� ���������
                    if (payment.Condition != null &&
                        dictionary.ContainsKey(payment.Condition) &&
                        dictionary[payment.Condition] < 1)
                    {
                        double part = payment.Part;
                        //���� ������� �������
                        if (part + dictionary[payment.Condition] <= 1)
                        {
                            dictionary[payment.Condition] += part;
                        }
                        else
                        {
                            part = 1 - dictionary[payment.Condition];
                            dictionary[payment.Condition] = 1;
                        }

                        //��������� ���������� �������� ������
                        result.Add(new PaymentPlanned
                        {
                            Date = payment.Date.SkipPast(),
                            Part = part,

                            Id = payment.Id,
                            Condition = payment.Condition,
                            Comment = payment.Comment,
                            DateCalculated = payment.DateCalculated
                        });
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// ��������� �������� ������� (��� �����������).
        /// </summary>
        [Designation("��������� �������� �������"), NotMapped]
        public List<PaymentPlanned> PaymentsPlannedGenerated
        {
            get
            {
                var dictionary = PaymentConditionsDictionary;
                foreach (var paymentPlanned in PaymentsPlannedActual)
                {
                    dictionary[paymentPlanned.Condition] += paymentPlanned.Part;
                }

                var result = new List<PaymentPlanned>();
                foreach (var conditions in dictionary)
                {
                    if (conditions.Value >= 1)
                        continue;

                    result.Add(new PaymentPlanned
                    {
                        Condition = conditions.Key,
                        Date = GetPaymentDate(conditions.Key).SkipPast(),
                        Part = 1 - conditions.Value,
                        //Comment = "��������������� ������"
                    });
                }
                return result;
            }
        }


        /// <summary>
        /// ��������� �������� ������� + �����������.
        /// </summary>
        [Designation("��������� �������� ������� + �����������"), NotMapped]
        public List<PaymentPlanned> PaymentsPlannedCalculated => PaymentsPlannedActual.Union(PaymentsPlannedGenerated).ToList();

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #endregion

        public int? GetOrderPosition()
        {
            if (int.TryParse(this.OrderPosition, out var result))
                return result;
            return null;
        }

    }
}
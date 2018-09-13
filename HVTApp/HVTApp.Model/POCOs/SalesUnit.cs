using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ������")]
    [DesignationPlural("������� ������")]
    public partial class SalesUnit : BaseEntity, IUnitPoco, ICloneable
    {
        #region Model

        [Designation("���������"), Required, OrderStatus(45)]
        public double Cost { get; set; }


        [Designation("�������"), Required, OrderStatus(50)]
        public virtual Product Product { get; set; }

        [Designation("���������� ��������"), Required]
        public virtual List<ProductIncluded> ProductsIncluded { get; set; } = new List<ProductIncluded>();

        [Designation("������"), OrderStatus(51), Required]
        public virtual Facility Facility { get; set; }

        [Designation("������� ������"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("���� ������������")]
        public int? ProductionTerm { get; set; }


        #region ������
        [Designation("������"), OrderStatus(52), Required]
        public virtual Project Project { get; set; }

        [Designation("��������� ���� ��������"), Required]
        public virtual DateTime DeliveryDateExpected { get; set; } = DateTime.Today.AddDays(CommonOptions.ProductionTerm + 120).SkipWeekend(); //��������� ���� ��������

        [Designation("�������������")]
        public virtual Company Producer { get; set; }

        [Designation("���� ����������")]
        public virtual DateTime? RealizationDate { get; set; }

        #endregion

        #region ���������� � ������������
        [Designation("�����")]
        public virtual Order Order { get; set; }

        [Designation("�������")]
        public string OrderPosition { get; set; }

        [Designation("�����")]
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

        [Designation("����������� �������"), NotUpdate(Role.SalesManager | Role.Director)]
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

        [Designation("����������� �������")]
        public virtual List<PaymentPlanned> PaymentsPlanned { get; set; } = new List<PaymentPlanned>();

        #endregion

        #region ����������� ����������
        [Designation("���� ��������")]
        public int? ExpectedDeliveryPeriod { get; set; }

        //������� �� ������� (�� ��������������� �������)
        [Designation("���� �������� ���������"), NotMapped]
        public int? ExpectedDeliveryPeriodCalculated { get; set; }

        [Designation("����� ��������")]
        public virtual Address Address { get; set; }

        [Designation("���� ��������")]
        public virtual DateTime? ShipmentDate { get; set; }

        [Designation("���� �������� ��������")]
        public virtual DateTime? ShipmentPlanDate { get; set; }

        [Designation("���� ��������")]
        public virtual DateTime? DeliveryDate { get; set; }

        #endregion

        [NotMapped, Designation("���������� �� �������������� ���������")]
        public bool AllowEditCost => Specification == null;

        [NotMapped, Designation("���������� �� �������������� �������")]
        public bool AllowEditProduct => SignalToStartProduction == null;

        [NotMapped, Designation("���������")]
        public bool IsLoosen => Producer != null && Producer.Id != CommonOptions.OurCompanyId;

        public override string ToString()
        {
            return $"{Product} ��� {Facility}";
        }


        #endregion

        #region Func

        /// <summary>
        /// ��� ������� (����������� + ��������).
        /// </summary>
        //public IEnumerable<IPayment> Payments => PaymentsActual.Cast<IPayment>().Union(PaymentsPlannedByConditions);

        #region �����
        [Designation("��������?"), NotMapped]
        public bool IsPaid => Math.Abs(SumNotPaid) < 0.0000001;

        /// <summary>
        /// ���������� �����
        /// </summary>
        [Designation("��������"), NotMapped]
        public double SumPaid => PaymentsActual.Sum(x => x.Sum);

        /// <summary>
        /// ������������ �����
        /// </summary>
        [Designation("����������"), NotMapped]
        public double SumNotPaid => Cost - SumPaid;


        /// <summary>
        /// ������, ����������� ��� ������ ������������
        /// </summary>
        [Designation("����� ������ ������������"), NotMapped]
        public double SumToStartProduction => PaymentConditionSet.PaymentConditions.Where(x =>
                                              x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart &&
                                              x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// �����, ����������� ��� ��������
        /// </summary>
        [Designation("����� ��������"), NotMapped]
        public double SumToShipping => PaymentConditionSet.PaymentConditions.Where(x => (
                                        x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).
                                       Sum(condition => Cost * condition.Part);


        #endregion

        #region ����

        [Designation("���"), NotMapped]
        public DateTime OrderInTakeDate => StartProductionDate ?? StartProductionDateCalculated;

        [Designation("���"), NotMapped]
        public int OrderInTakeYear => OrderInTakeDate.Year;

        [Designation("�����"), NotMapped]
        public int OrderInTakeMonth => OrderInTakeDate.Month;

        /// <summary>
        /// ���� ���������� �����
        /// </summary>
        /// <param name="sumToAchive"></param>
        /// <returns></returns>
        private DateTime? AchiveSumDate(double sumToAchive)
        {
            double sum = 0;
            foreach (var payment in PaymentsActual.OrderBy(x => x.Date))
            {
                sum += payment.Sum;
                if (sumToAchive <= sum) return payment.Date;
            }

            var dic = PaymentConditionsDictionary;
            foreach (var payment in PaymentsPlannedActual.OrderBy(x => x.Date))
            {
                //���� ��������� �����-�� ������� ����� ������������ - �����
                if (!Equals(dic.First(x => x.Value < 1).Key, payment.Condition))
                {
                    return null;
                }

                sum += payment.Part * payment.Condition.Part * Cost;
                if (sumToAchive <= sum) return payment.Date;

                dic[payment.Condition] += payment.Part;
            }
            return null;
        }

        /// <summary>
        /// ���� ���������� ������� ��� ������� ������������
        /// </summary>
        [Designation("���� ���������� ������� ��� ������ ������������"), NotMapped]
        public DateTime? StartProductionConditionsDoneDate => AchiveSumDate(SumToStartProduction);

        /// <summary>
        /// ���� ���������� ������� ��� ������������� ��������
        /// </summary>
        [Designation("���� ���������� ������� ��� ��������"), NotMapped]
        public DateTime? ShippingConditionsDoneDate => AchiveSumDate(SumToShipping);

        /// <summary>
        /// ��������� ���� ������ ������������.
        /// </summary>
        [Designation("������ ������������ (����.)"), NotMapped]
        public DateTime StartProductionDateCalculated
        {
            get
            {
                if (StartProductionDate.HasValue) return StartProductionDate.Value;

                //�� ������� ���������
                if (SignalToStartProduction.HasValue) return SignalToStartProduction.Value;

                //�� ���������� �������, ����������� ��� ������� ������������
                if (StartProductionConditionsDoneDate.HasValue) return StartProductionConditionsDoneDate.Value;

                //�� ���� ������� �������
                if (PaymentsActual.Any()) return PaymentsActual.Select(x => x.Date).Min();

                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;

                //�� ���� �������� ������������ �� ������
                if (DeliveryDate.HasValue) return DeliveryDate.Value.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).SkipPastAndWeekend();

                //�� ����������� ���� ���������� �������
                return DeliveryDateExpected.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).SkipPastAndWeekend();
            }
        }

        /// <summary>
        /// ��������� ���� ��������� ������������.
        /// </summary>
        [Designation("��������� ������������ (����.)"), NotMapped]
        public DateTime EndProductionDateCalculated
        {
            get
            {
                //�� ���� ������������
                if (EndProductionDate.HasValue) return EndProductionDate.Value;

                //�� ���� ������������
                if (PickingDate.HasValue)
                {
                    var assembleTerm = this.AssembleTerm ?? CommonOptions.AssembleTerm;
                    return PickingDate.Value.AddDays(assembleTerm).SkipPastAndWeekend();
                }

                //�� ���� ���������� � ������������ (����)
                if (EndProductionPlanDate.HasValue) return EndProductionPlanDate.Value;

                //�� ����� ������������
                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;
                return StartProductionDateCalculated.AddDays(productionTerm).SkipPastAndWeekend();
            }
        }

        /// <summary>
        /// ��������� ���� ����������.
        /// </summary>
        [Designation("��������� ���� ����������"), NotMapped]
        public DateTime RealizationDateCalculated => RealizationDate ?? DeliveryDateCalculated;

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        [Designation("��������� ���� ��������"), NotMapped]
        public DateTime ShipmentDateCalculated
        {
            get
            {
                //�� �������� ���� ��������
                if (ShipmentDate.HasValue) return ShipmentDate.Value;

                //�� �������� ���� ��������
                if (ShipmentPlanDate.HasValue)
                {
                    if (ShippingConditionsDoneDate.HasValue)
                    {
                        if (ShipmentPlanDate.Value >= ShippingConditionsDoneDate &&
                            ShipmentPlanDate.Value >= EndProductionDateCalculated)
                            return ShipmentPlanDate.Value;
                    }
                    else
                    {
                        if (ShipmentPlanDate.Value >= EndProductionDateCalculated)
                            return ShipmentPlanDate.Value;
                    }

                }

                //�� ���� ���������� ������� ��� ��������
                if (ShippingConditionsDoneDate.HasValue &&
                    ShippingConditionsDoneDate >= EndProductionDateCalculated)
                    return ShippingConditionsDoneDate.Value.SkipPastAndWeekend();

                //�� ���� ��������� ������������
                return EndProductionDateCalculated.SkipPastAndWeekend();
            }
        }

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        [Designation("��������� ���� ��������"), NotMapped]
        public DateTime DeliveryDateCalculated
        {
            get
            {
                if (DeliveryDate.HasValue) return DeliveryDate.Value;
                return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).SkipPastAndWeekend();
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
        private DateTime GetPaymentDate(PaymentCondition condition)
        {
            switch (condition.PaymentConditionPoint)
            {
                case PaymentConditionPoint.ProductionStart:
                    return StartProductionDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPoint.ProductionEnd:
                    return EndProductionDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPoint.Shipment:
                    return ShipmentDateCalculated.AddDays(condition.DaysToPoint);
                case PaymentConditionPoint.Delivery:
                    return DeliveryDateCalculated.AddDays(condition.DaysToPoint);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// ��������� ������� � ������� ��� ����������
        /// </summary>
        private Dictionary<PaymentCondition, double> PaymentConditionsDictionary => GetConditionsDictionary(SumPaid);

        private Dictionary<PaymentCondition, double> GetConditionsDictionary(double sum)
        {
            var result = new Dictionary<PaymentCondition, double>();

            //���� ��������� ������� ��� ������ ������� - �������
            if (Cost < 0.00001) return result;

            //���������� �������
            var paidRest = sum;

            //����� ��� ������� � ������������� ��
            var conditions = PaymentConditionSet.PaymentConditions.OrderBy(x => x).ToList();

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

                //������� ������� � ����������� �� ����������
                var dictionary = PaymentConditionsDictionary;

                foreach (var payment in PaymentsPlanned)
                {
                    //���� ��������� ������� ���������� � ��� �� ���������
                    if (dictionary.ContainsKey(payment.Condition) && dictionary[payment.Condition] < 1)
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
                            Date = payment.Date < DateTime.Today ? DateTime.Today : payment.Date,
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
                        Date = GetPaymentDate(conditions.Key),
                        Part = 1 - conditions.Value,
                        //Comment = "��������������� ������"
                    });
                }
                return result;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion


        #endregion
    }
}
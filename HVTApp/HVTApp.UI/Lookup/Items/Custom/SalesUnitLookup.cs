using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookup : IUnitLookup
    {
        private List<PaymentConditionPart> _paymentConditionParts;

        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            ProductsIncluded = Entity.ProductsIncluded.Select(x => new ProductIncludedLookup(x)).ToList();
            foreach (var productIncluded in ProductsIncluded)
                await productIncluded.LoadOther(unitOfWork);

            PaymentsActual = Entity.PaymentsActual.Select(x => new PaymentActualLookup(x)).ToList();
            foreach (var paymentActualLookup in PaymentsActual)
                await paymentActualLookup.LoadOther(unitOfWork);

            PaymentsPlanned = Entity.PaymentsPlanned.Select(x => new PaymentPlannedLookup(x)).ToList();
            foreach (var paymentPlannedLookup in PaymentsPlanned)
                await paymentPlannedLookup.LoadOther(unitOfWork);
            //����������� � ����������� �������� �����
            PaymentsPlanned.ForEach(x => x.Sum = Cost * x.Part * x.Condition.Part);

            await PaymentConditionSet.LoadOther(unitOfWork);
        }

        [Designation("����������� �������")]
        public List<PaymentActualLookup> PaymentsActual { get; private set; }

        [Designation("����������� �������")]
        public List<PaymentPlannedLookup> PaymentsPlanned { get; private set; }

        [Designation("���������� � ��������� ��������")]
        public List<ProductIncludedLookup> ProductsIncluded { get; private set; }

        /// <summary>
        /// ��� ������� (����������� + ��������).
        /// </summary>
        public IEnumerable<IPayment> Payments => PaymentsActual.Cast<IPayment>().Union(PaymentsPlannedByConditions);

        #region �����

        /// <summary>
        /// ���������� �����
        /// </summary>
        [Designation("��������")]
        public double SumPaid => PaymentsActual.Sum(x => x.Sum);

        /// <summary>
        /// ������������ �����
        /// </summary>
        [Designation("����������")]
        public double SumNotPaid => Cost - SumPaid;


        /// <summary>
        /// ������, ����������� ��� ������ ������������
        /// </summary>
        [Designation("����� ������ ������������")]
        public double SumToStartProduction => PaymentConditionSet.PaymentConditions.Where(x =>
                                              x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart &&
                                              x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// �����, ����������� ��� ��������
        /// </summary>
        [Designation("����� ��������")]
        public double SumToShipping => PaymentConditionSet.PaymentConditions.Where(x => (
                                        x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd) ||
                                       (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).
                                       Sum(condition => Cost * condition.Part);


        #endregion

        #region ����

        [Designation("���")]
        public DateTime OrderInTakeDate => StartProductionDate ?? StartProductionDateCalculated;
        [Designation("���")]
        public int OrderInTakeYear => OrderInTakeDate.Year;
        [Designation("�����")]
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
            return null;
        }

        /// <summary>
        /// ���� ���������� ������� ��� ������� ������������
        /// </summary>
        public DateTime? StartProductionConditionsDoneDate => AchiveSumDate(SumToStartProduction);

        /// <summary>
        /// ���� ���������� ������� ��� ������������� ��������
        /// </summary>
        public DateTime? ShippingConditionsDoneDate => AchiveSumDate(SumToShipping);

        /// <summary>
        /// ��������� ���� ������ ������������.
        /// </summary>
        [Designation("������ ������������ (����.)")]
        public DateTime StartProductionDateCalculated
        {
            get
            {
                if (StartProductionDate.HasValue) return StartProductionDate.Value;

                //�� ���������� �������, ����������� ��� ������� ������������
                if (StartProductionConditionsDoneDate.HasValue) return StartProductionConditionsDoneDate.Value;

                //�� ���� ������� �������
                if (PaymentsActual.Any()) return PaymentsActual.OrderBy(x => x.Date).First().Date;

                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;

                //�� ���� �������� ������������ �� ������
                if (DeliveryDate.HasValue) return DeliveryDate.Value.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();

                //�� ����������� ���� ���������� �������
                return DeliveryDateExpected.AddDays(-productionTerm).AddDays(-DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// ��������� ���� ��������� ������������.
        /// </summary>
        [Designation("��������� ������������ (����.)")]
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
                    return PickingDate.Value.AddDays(assembleTerm).GetTodayIfDateFromPastAndSkipWeekend();
                }

                //�� ����� ������������
                var productionTerm = this.ProductionTerm ?? CommonOptions.ProductionTerm;
                return StartProductionDateCalculated.AddDays(productionTerm).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// ��������� ���� ����������.
        /// </summary>
        public DateTime RealizationDateCalculated => RealizationDate ?? DeliveryDateCalculated;

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
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
                    return ShippingConditionsDoneDate.Value.GetTodayIfDateFromPastAndSkipWeekend();

                //�� ���� ��������� ������������
                return EndProductionDateCalculated.GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        public DateTime DeliveryDateCalculated
        {
            get
            {
                if (DeliveryDate.HasValue) return DeliveryDate.Value;
                return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        public double DeliveryPeriodCalculated
        {
            get
            {
                //�� ���������� ����� ��������
                if (ExpectedDeliveryPeriod.HasValue) return ExpectedDeliveryPeriod.Value;

                //�� ������������ ����� �������� �� ������ ���������
                if (Address?.Locality.StandartDeliveryPeriod != null) return Address.Locality.StandartDeliveryPeriod.Value;

                //�� ������������ ����� �������� �� ������ �������
                if (Facility.Address?.Locality.StandartDeliveryPeriod != null) return Facility.Address.Locality.StandartDeliveryPeriod.Value;

                ////�� ������������ ����� �������� �� ������� �������
                //if (Address.Locality.Region.Capital.StandartDeliveryPeriod.HasValue) return Address.Locality.Region.Capital.StandartDeliveryPeriod.Value;

                ////�� ������������ ����� �������� �� ������� ������������ ������
                //if (Address.Locality.Region.District.Capital?.StandartDeliveryPeriod != null) return Address.Locality.Region.District.Capital.StandartDeliveryPeriod.Value;

                ////�� ������������ ����� �������� �� ������� ������
                //if (Address.Locality.Region.District.Country.Capital?.StandartDeliveryPeriod != null) return Address.Locality.Region.District.Country.Capital.StandartDeliveryPeriod.Value;

                return 7;
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
        /// ������������� ��������� �������
        /// </summary>
        [Designation("������������� ��������� �������")]
        private IEnumerable<PaymentCondition> PaymentConditionsToDone => GeneratePaymentConditionsToDone();

        private IEnumerable<PaymentCondition> GeneratePaymentConditionsToDone()
        {
            var result = new List<PaymentCondition>();

            if (_paymentConditionParts == null)
            {
                _paymentConditionParts = new List<PaymentConditionPart>();
            }
            _paymentConditionParts.Clear();

            //���� ��������� ������� ��� ������ ������� - �������
            if (Cost < 0.00001 || SumNotPaid < 0.00001) return result;

            //����� ��� ������� � ������������� ��
            var conditions = PaymentConditionSet.PaymentConditions.Select(x => x.Entity).OrderByDescending(x => x).ToList();

            //������������ �����
            var rest = SumNotPaid / Cost;

            //���������� � ��������� ������������� �������
            foreach (var condition in conditions)
            {
                rest -= condition.Part;
                //���� �������� ������������ - ������� ��������
                if (rest >= 0)
                {
                    result.Add(condition);
                    continue;
                }

                var newCondition = new PaymentCondition
                {
                    DaysToPoint = condition.DaysToPoint,
                    PaymentConditionPoint = condition.PaymentConditionPoint,
                    Part = condition.Part + rest
                };
                if (newCondition.Part > 0)
                {
                    result.Add(newCondition);
                    _paymentConditionParts.Add(new PaymentConditionPart(condition, newCondition));
                }
            }
            //���������� ������������� �������
            return result.OrderBy(x => x);
        }

        /// <summary>
        /// ���������� �������� �������
        /// </summary>
        public IEnumerable<PaymentPlanned> PaymentsPlannedActual
        {
            get
            {
                var remove = new List<PaymentPlanned>();
                foreach (var payment in Entity.PaymentsPlanned)
                {
                    if (PaymentConditionsToDone.Contains(payment.Condition))
                    {
                        yield return payment;
                        continue;
                    }
                    if (PaymentConditionParts.Select(x => x.Origin).Contains(payment.Condition))
                    {
                        var payment1 = payment;
                        var pcp = PaymentConditionParts.Single(x => Equals(x.Origin, payment1.Condition));
                        payment.Part = payment.Part * pcp.Part.Part;
                        yield return payment;
                        continue;
                    }
                    remove.Add(payment);
                }
                remove.ForEach(x => Entity.PaymentsPlanned.Remove(x));
            }
        }

        public IEnumerable<PaymentPlannedLookup> PaymentsPlannedActualLookups
            => PaymentsPlanned.Where(x => PaymentsPlannedActual.Contains(x.Entity));

        /// <summary>
        /// �������� ������������� �������
        /// </summary>
        private List<PaymentConditionPart> PaymentConditionParts
        {
            get
            {
                if (_paymentConditionParts == null)
                {
                    _paymentConditionParts = new List<PaymentConditionPart>();
                    GeneratePaymentConditionsToDone();
                }
                return _paymentConditionParts;
                
            }
        }

        /// <summary>
        /// ������: ��������� ������� + ��������� ��� �� ���������
        /// </summary>
        class PaymentConditionPart
        {
            public PaymentCondition Origin { get; }
            public PaymentCondition Part { get; }

            public PaymentConditionPart(PaymentCondition origin, PaymentCondition part)
            {
                Origin = origin;
                Part = part;
            }
        }

        /// <summary>
        /// �������� ������� �� �������� (���������).
        /// </summary>
        [Designation("��������� ���������� �������")]
        public IEnumerable<PaymentPlannedLookup> PaymentsPlannedByConditions
        {
            get
            {
                var paymentsPlanned = PaymentConditionsToDone.Select(x => new PaymentPlanned
                {
                    Date = GetPaymentDate(x),
                    Condition = x,
                    Part = x.Part
                });
                return paymentsPlanned.Select(x => new PaymentPlannedLookup(x) {Sum = Cost * x.Part});
            }
        }

        /// <summary>
        /// �������� ������� (����������� + ���������������)
        /// </summary>
        public IEnumerable<PaymentPlannedLookup> PaymentPlannedWithSaved
        {
            get
            {
                //���������� �������� ������� �� �������� ���������
                //��������� �� ��� ������� � ���������, ������������� � �����������
                var conditions = PaymentsPlannedActual.Select(x => x.Condition.Id);
                var generated = PaymentsPlannedByConditions.Where(x => !conditions.Contains(x.Condition.Id));

                //���������� ������������� ����������� �������������������
                return PaymentsPlannedActualLookups.Union(generated).OrderBy(x => x.Date);
            }
            
        }

        #endregion

    }
}
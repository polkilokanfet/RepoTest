using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookup : IUnitLookup
    {
        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            //ProductDependents = Entity.ProductsIncluded.Select(x => new ProductDependentLookup(x)).ToList();
            //foreach (var productDependent in ProductDependents)
            //    await productDependent.LoadOther(unitOfWork);

            PaymentsActual = Entity.PaymentsActual.Select(x => new PaymentActualLookup(x)).ToList();
            foreach (var paymentActualLookup in PaymentsActual)
                await paymentActualLookup.LoadOther(unitOfWork);

            PaymentsPlanned = Entity.PaymentsPlanned.Select(x => new PaymentPlannedLookup(x)).ToList();
            foreach (var paymentPlannedLookup in PaymentsPlanned)
                await paymentPlannedLookup.LoadOther(unitOfWork);

            await PaymentConditionSet.LoadOther(unitOfWork);
        }

        //public List<> ProductDependents { get; set; }
        public List<PaymentActualLookup> PaymentsActual { get; set; }
        public List<PaymentPlannedLookup> PaymentsPlanned { get; set; }

        /// <summary>
        /// ��� ������� (����������� + ��������).
        /// </summary>
        public IEnumerable<IPayment> Payments => PaymentsActual.Select(x => (IPayment)x.Entity).Union(PaymentsPlannedByConditions);

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
        [Designation("��� ������ ������������")]
        public double SumToStartProduction => PaymentConditionSet.PaymentConditions.Where(x =>
                                              x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart &&
                                              x.DaysToPoint <= 0).Sum(condition => Cost * condition.Part);

        /// <summary>
        /// �����, ����������� ��� ��������
        /// </summary>
        [Designation("��� ��������")]
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
        /// �� ����������� ��������� �������
        /// </summary>
        [Designation("�� ����������� ��������� �������")]
        private IEnumerable<PaymentCondition> PaymentConditionsToDone
        {
            get
            {
                //����� ��� ������� � ������������� ��
                var conditions = PaymentConditionSet.PaymentConditions.Select(x => x.Entity).OrderByDescending(x => x).ToList();

                var result = new List<PaymentCondition>();

                //���� ��������� ������� - �������
                if (Math.Abs(Cost) < 0.0001) return result;

                //������������ �����
                var rest = SumNotPaid / Cost;

                foreach (var condition in conditions)
                {
                    rest -= condition.Part;
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
                    if (newCondition.Part > 0) result.Add(newCondition);
                }
                return result.OrderBy(x => x);
            }
        }

        /// <summary>
        /// �������� ������� �� ��������.
        /// </summary>
        public IEnumerable<PaymentPlanned> PaymentsPlannedByConditions =>
            PaymentConditionsToDone.Select(x => new PaymentPlanned
            {
                Date = GetPaymentDate(x),
                Condition = x,
                Sum = Cost * x.Part
            });

        #endregion

    }
}
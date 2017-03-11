using System;
using System.Collections.Generic;
using HVTApp.Model.Services;

namespace HVTApp.Model
{
    public class PaymentPlanned : PaymentBase, IPaymentBase
    {
        static readonly Dictionary<PaymentConditionPoint, string> PointDateDictionary = new Dictionary<PaymentConditionPoint, string>
        {
            [PaymentConditionPoint.ProductionStart] = nameof(DateInfo.DateOrderInTakeCalculated),
            [PaymentConditionPoint.ProductionEnd] = nameof(DateInfo.DateEndProductionCalculated),
            [PaymentConditionPoint.Shipment] = nameof(DateInfo.DateShipmentCalculated),
            [PaymentConditionPoint.Delivery] = nameof(DateInfo.DateDeliveryCalculated)
        };

        public DateTime Date
        {
            get
            {
                if (ExpectedPaymentDate != null && ExpectedPaymentDate.Value >= DateTime.Today)
                    return ExpectedPaymentDate.Value;
                return CalculatedPaymentDate;
            }
        }

        /// <summary>
        /// ��������� ���� �������.
        /// </summary>
        public DateTime? ExpectedPaymentDate { get; set; }

        /// <summary>
        /// �������, � ������� ������ ����������� ������.
        /// </summary>
        public PaymentsCondition PaymentsCondition { get; set; }

        /// <summary>
        /// ��������� ���� ��������� �������.
        /// </summary>
        public DateTime CalculatedPaymentDate => CalculatePaymentDateMethod();

        private DateTime CalculatePaymentDateMethod()
        {
            //��������� � �������� ����. �������� ����� �� �������.
            DateTime resultDate = (DateTime)typeof(DateInfo).GetProperty(PointDateDictionary[PaymentsCondition.PaymentConditionPoint]).GetValue(PaymentsInfo.Product.DateInfo);
            //��������� ��������������� ���������� ����.
            resultDate = resultDate.AddDays(PaymentsCondition.DaysToPoint);
            //���������� ��������
            DateServices.GetTodayIfDateToEarlyAndSkipWeekend(resultDate);
            return resultDate;
        }


    }
}
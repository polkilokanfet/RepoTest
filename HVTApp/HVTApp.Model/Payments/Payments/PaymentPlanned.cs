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
        /// Ожидаемая дата платежа.
        /// </summary>
        public DateTime? ExpectedPaymentDate { get; set; }

        /// <summary>
        /// Условие, с которым связан планируемый платеж.
        /// </summary>
        public PaymentsCondition PaymentsCondition { get; set; }

        /// <summary>
        /// Расчетная дата планового платежа.
        /// </summary>
        public DateTime CalculatedPaymentDate => CalculatePaymentDateMethod();

        private DateTime CalculatePaymentDateMethod()
        {
            //связанная с условием дата. Свойство берем из словаря.
            DateTime resultDate = (DateTime)typeof(DateInfo).GetProperty(PointDateDictionary[PaymentsCondition.PaymentConditionPoint]).GetValue(PaymentsInfo.Product.DateInfo);
            //добавляем соответствующее количество дней.
            resultDate = resultDate.AddDays(PaymentsCondition.DaysToPoint);
            //пропускаем выходные
            DateServices.GetTodayIfDateToEarlyAndSkipWeekend(resultDate);
            return resultDate;
        }


    }
}
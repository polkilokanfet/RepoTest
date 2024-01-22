using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.POCOs
{
    [Designation("Условия оплаты")]
    public partial class PaymentConditionSet : BaseEntity
    {
        [Designation("Список условий"), NotForListView]
        public virtual List<PaymentCondition> PaymentConditions { get; set; } = new List<PaymentCondition>();

        //мудрое требование КВН
        public string GetStringForOffer(DateTime date)
        {
            var sb = new StringBuilder();

            var start = PaymentConditions
                .Where(x => x.PaymentConditionPoint.PaymentConditionPointEnum == PaymentConditionPointEnum.ProductionStart)
                .OrderBy(x => x)
                .ToList();

            if (start.Any())
            {
                var firstStart = start.First();
                sb.AppendLine($" - {firstStart.Part * 100}% в срок до {date.ToShortDateString()} г.;");
                foreach (var condition in start.Skip(1))
                {
                    sb.AppendLine($" - {condition.Part * 100}% в течение {condition.DaysToPoint - firstStart.DaysToPoint} дн. с момента первого платежа ({condition.DaysToPointToString()});");
                }
            }

            foreach (var condition in PaymentConditions.Except(start))
            {
                if (condition.DaysToPoint == 0 && condition.PaymentConditionPoint.PaymentConditionPointEnum == PaymentConditionPointEnum.ProductionEnd)
                {
                    sb.AppendLine($" - {condition.Part * 100}% в течение 10 рабочих дней после получения уведомления о готовности оборудования, до отгрузки;");
                    continue;
                }

                sb.AppendLine($" - {condition};");
            }

            return sb.Remove(sb.Length - 3, 3).Append(".").ToString();
        }

        public override string ToString()
        {
            return PaymentConditions.Any()
                ? PaymentConditions.OrderBy(condition => condition).ToStringEnum()
                : "Пустой набор условий оплаты" ;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentConditionSet paymentConditionSet)) return false;
            return this.PaymentConditions.MembersAreSame(paymentConditionSet.PaymentConditions);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PaymentConditionsSet
{
    public class PaymentConditionWrapper2 : PaymentConditionWrapper
    {
        private readonly PaymentConditionSetWrapper _paymentConditionSetWrapper;
        private int _daysTo;
        private bool _isBefore;

        public int DaysTo
        {
            get => _daysTo;
            set
            {
                if (Equals(_daysTo, value)) return;
                _daysTo = value;
                RefreshDaysToPoint();
                OnPropertyChanged();
            }
        }

        public bool IsBefore
        {
            get => _isBefore;
            set
            {
                if (Equals(_isBefore, value)) return;
                _isBefore = value;
                RefreshDaysToPoint();
                OnPropertyChanged();
            }
        }

        private void RefreshDaysToPoint()
        {
            var k = IsBefore ? -1 : 1;
            this.DaysToPoint = k * DaysTo;
        }

        public PaymentConditionWrapper2(PaymentCondition paymentCondition, PaymentConditionSetWrapper paymentConditionSetWrapper) 
            : base(paymentCondition)
        {
            _paymentConditionSetWrapper = paymentConditionSetWrapper;
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (Part < 0.0)
                yield return new ValidationResult("Процент не должен быть меньше 0", new[] { nameof(Part) });

            if (Part > 1.0)
                yield return new ValidationResult("Процент не должен быть больше 100", new[] { nameof(Part) });

            if (_paymentConditionSetWrapper != null)
            {
                List<PaymentCondition> conditions = _paymentConditionSetWrapper.Model.PaymentConditions.ToList();
                conditions.Add(this.Model);
                if (Math.Abs(conditions.Sum(paymentCondition => paymentCondition.Part) - 1) > 0.00001)
                    yield return new ValidationResult("Сумма всех условий не равна 100%", new[] { nameof(Part) });
            }
        }
    }
}
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
        private readonly PaymentConditionSet _conditionSet;
        private int _daysTo;
        private bool _isBefore = true;

        public int DaysTo
        {
            get => _daysTo;
            set
            {
                if (Equals(_daysTo, value)) return;
                _daysTo = value;
                this.DaysToPoint = (IsBefore ? -1 : 1) * DaysTo;
                RaisePropertyChanged();
            }
        }

        public bool IsBefore
        {
            get => _isBefore;
            set
            {
                if (Equals(_isBefore, value)) return;
                _isBefore = value;
                this.DaysToPoint = (IsBefore ? -1 : 1) * DaysTo;
                RaisePropertyChanged();
            }
        }

        public PaymentConditionWrapper2(PaymentConditionSet paymentConditionSet) 
            : base(new PaymentCondition())
        {
            _conditionSet = paymentConditionSet;
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (_conditionSet != null)
            {
                List<PaymentCondition> conditions = _conditionSet.PaymentConditions.ToList();
                conditions.Add(this.Model);
                if (conditions.Sum(condition => condition.Part) > 1)
                {
                    yield return new ValidationResult("Сумма всех условий не должна быть больше 100%", new[] { nameof(Part) });
                }
            }

            foreach (var validationResult in base.ValidateOther().ToList())
            {
                yield return validationResult;
            }
        }
    }
}
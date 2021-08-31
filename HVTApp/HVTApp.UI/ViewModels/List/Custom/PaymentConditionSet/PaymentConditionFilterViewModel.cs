using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public class PaymentConditionFilterViewModel
    {
        private readonly PaymentConditionPointEnum _point;
        private double? _part;
        private int? _daysTo;
        private bool _isBefore = true;

        public double? Part
        {
            get => _part;
            set
            {
                if (Equals(_part, value)) return;
                _part = value;
                this.IsChanged?.Invoke();
            }
        }

        public int? DaysTo
        {
            get => _daysTo;
            set
            {
                if (Equals(_daysTo, value)) return;
                _daysTo = value;
                this.IsChanged?.Invoke();
            }
        }

        public bool IsBefore
        {
            get => _isBefore;
            set
            {
                if (Equals(_isBefore, value)) return;
                _isBefore = value;
                this.IsChanged?.Invoke();
            }
        }

        public PaymentConditionFilter PaymentConditionFilter
        {
            get
            {
                var k = IsBefore ? -1 : 1;

                if (Part.HasValue && DaysTo.HasValue)
                    return new PaymentConditionFilter(_point, Part.Value, k * DaysTo.Value);

                if (Part.HasValue)
                    return new PaymentConditionFilter(_point, Part.Value);

                if (DaysTo.HasValue)
                    return new PaymentConditionFilter(_point, k * DaysTo.Value);

                return null;
            }
        }

        public PaymentConditionFilterViewModel(PaymentConditionPointEnum point)
        {
            _point = point;
        }

        public event Action IsChanged;
    }
}
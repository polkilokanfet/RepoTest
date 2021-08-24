using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public class PaymentConditionFilterViewModel
    {
        private readonly PaymentConditionPointEnum _point;
        private double? _part = 50.0;
        private int? _daysTo;

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

        public PaymentConditionFilter PaymentConditionFilter
        {
            get
            {
                if (Part.HasValue && DaysTo.HasValue)
                    return new PaymentConditionFilter(_point, Part.Value / 100.0, DaysTo.Value);

                if (Part.HasValue)
                    return new PaymentConditionFilter(_point, Part.Value / 100.0);

                if (DaysTo.HasValue)
                    return new PaymentConditionFilter(_point, DaysTo.Value);

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
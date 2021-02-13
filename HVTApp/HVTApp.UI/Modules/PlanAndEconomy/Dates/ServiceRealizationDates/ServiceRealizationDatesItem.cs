using System;
using System.Runtime.CompilerServices;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates.ServiceRealizationDates
{
    public class ServiceRealizationDatesItem : WrapperBase<SalesUnit>
    {

        public DateTime? RealizationDate
        {
            get => GetValue<DateTime?>();
            set => SetValueNew(value);
        }

        public bool HasFullInformation => RealizationDate.HasValue;

        public ServiceRealizationDatesItem(SalesUnit model) : base(model)
        {
        }

        public event Action SettedValueToProperty;

        private void SetValueNew<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            this.SetValue(newValue, propertyName);
            OnPropertyChanged(nameof(HasFullInformation));
            SettedValueToProperty?.Invoke();
        }
    }
}
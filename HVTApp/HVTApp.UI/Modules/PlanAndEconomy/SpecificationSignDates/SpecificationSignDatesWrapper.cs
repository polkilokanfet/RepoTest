using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.PlanAndEconomy.SpecificationSignDates
{
    public partial class SpecificationSignDatesWrapper : WrapperBase<Specification>
    {
        public SpecificationSignDatesWrapper(Specification model) : base(model) { }

        public DateTime? SignDate
        {
            get => Model.SignDate;
            set => SetValue(value);
        }
        public DateTime? SignDateOriginalValue => GetOriginalValue<DateTime?>(nameof(SignDate));
        public bool SignDateIsChanged => GetIsChanged(nameof(SignDate));
    }
}
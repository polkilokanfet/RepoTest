using System;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.UI.Commands;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public class UpdateStructureCostNumberTaskForDesignDepartmentHeadViewModel : WrapperBase<UpdateStructureCostNumberTask>
    {
        public ICommand AcceptNumberCommand { get; }
        public ICommand RejectNumberCommand { get; }

        public UpdateStructureCostNumberTaskForDesignDepartmentHeadViewModel(UpdateStructureCostNumberTask model) :
            base(model)
        {
            AcceptNumberCommand = new DelegateLogCommand(
                () => { this.IsAccepted = true; },
                () => this.MomentFinish.HasValue == false);

            RejectNumberCommand = new DelegateLogCommand(
                () => { this.IsAccepted = false; },
                () => this.MomentFinish.HasValue == false);
        }

        public DateTime? MomentFinish
        {
            get => Model.MomentFinish;
            set => SetValue(value);
        }
        public DateTime? MomentFinishOriginalValue => GetOriginalValue<DateTime?>(nameof(MomentFinish));
        public bool MomentFinishIsChanged => GetIsChanged(nameof(MomentFinish));

        public bool? IsAccepted
        {
            get => Model.IsAccepted;
            set => SetValue(value);
        }
        public bool? IsAcceptedOriginalValue => GetOriginalValue<bool?>(nameof(IsAccepted));
        public bool IsAcceptedIsChanged => GetIsChanged(nameof(IsAccepted));
    }
}
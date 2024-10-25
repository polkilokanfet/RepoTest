using System;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.UI.Commands;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public class UpdateStructureCostNumberTaskForDesignDepartmentHeadViewModel : WrapperBase<UpdateStructureCostNumberTask>
    {
        private readonly TaskViewModelBaseDesignDepartmentHead _vm;

        public ICommand AcceptNumberCommand { get; }
        public ICommand RejectNumberCommand { get; }

        public UpdateStructureCostNumberTaskForDesignDepartmentHeadViewModel(
            UpdateStructureCostNumberTask model,
            TaskViewModelBaseDesignDepartmentHead vm) :
            base(model)
        {
            _vm = vm;
            AcceptNumberCommand = new DelegateLogCommand(
                () => { NumberCommand(true); },
                () => this.MomentFinish.HasValue == false);

            RejectNumberCommand = new DelegateLogCommand(
                () => { NumberCommand(false); },
                () => this.MomentFinish.HasValue == false);
        }

        private void NumberCommand(bool isAccepted)
        {
            this.MomentFinish = DateTime.Now;
            this.IsAccepted = isAccepted;
            this.Model.ProductBlock.StructureCostNumber = isAccepted
                ? this.Model.StructureCostNumber
                : this.Model.StructureCostNumberOriginal;
            _vm.SaveCommand.Execute();
            var s = isAccepted ? "Согласовано" : "Отклонено";
            _vm.Messenger.SendMessage($"{s} изменение номера стракчакоста: {this.Model.ToString()}");
            ((DelegateLogCommand)AcceptNumberCommand).RaiseCanExecuteChanged();
            ((DelegateLogCommand)RejectNumberCommand).RaiseCanExecuteChanged();
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
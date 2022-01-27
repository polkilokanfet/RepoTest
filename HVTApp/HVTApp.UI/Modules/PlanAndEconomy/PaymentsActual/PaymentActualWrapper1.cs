using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class PaymentActualWrapper1 : WrapperBase<PaymentActual>
    {
        public PaymentActualWrapper1(PaymentActual model) : base(model) { }

        #region SimpleProperties

        //Date
        public DateTime Date
        {
            get => Model.Date;
            set
            {
                if (value > DateTime.Today.AddYears(50)) return;
                SetValue(value);
            }
        }
        public DateTime DateOriginalValue => GetOriginalValue<DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        //Sum
        public double Sum
        {
            get => Model.Sum;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }
        public double SumOriginalValue => GetOriginalValue<double>(nameof(Sum));
        public bool SumIsChanged => GetIsChanged(nameof(Sum));

        //Comment
        public string Comment
        {
            get => Model.Comment;
            set => SetValue(value);
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        //Id
        public Guid Id => Model.Id;

        #endregion

    }
}
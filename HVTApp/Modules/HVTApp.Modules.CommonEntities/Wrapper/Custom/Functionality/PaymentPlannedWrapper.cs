using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentPlannedWrapper : IPayment
    {
        public int Year => this.Date.Year;
        public int Month => this.Date.Month;
        public PaymentPlannedListWrapper PaymentPlannedList { get; set; }

        protected override void RunInConstructor()
        {
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Date)) return;
                OnPropertyChanged(nameof(Year));
                OnPropertyChanged(nameof(Month));
            };
        }
    }
}
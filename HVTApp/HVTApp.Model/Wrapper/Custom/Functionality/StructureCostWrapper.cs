namespace HVTApp.Model.Wrapper
{
    public partial class StructureCostWrapper
    {
        public override void InitializeOther()
        {
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(UnitPrice))
                {
                    RaisePropertyChanged(nameof(Total));
                }

                if (args.PropertyName == nameof(AmountNumerator) || args.PropertyName == nameof(AmountDenomerator))
                {
                    RaisePropertyChanged(nameof(Amount));
                    RaisePropertyChanged(nameof(Total));
                }
            };
        }
    }
}
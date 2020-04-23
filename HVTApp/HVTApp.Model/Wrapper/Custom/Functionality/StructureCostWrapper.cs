namespace HVTApp.Model.Wrapper
{
    public partial class StructureCostWrapper
    {
        public override void InitializeOther()
        {
            this.PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName == nameof(UnitPrice))
                    OnPropertyChanged(nameof(Total));
            };
        }
    }
}
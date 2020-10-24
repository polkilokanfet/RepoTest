namespace HVTApp.Model.Wrapper
{
    public partial class StructureCostWrapper
    {
        private double _amountUp;
        private double _amountDown = 1;

        /// <summary>
        /// Количество (числитель)
        /// </summary>
        public double AmountUp
        {
            get { return _amountUp; }
            set
            {
                if (Equals(_amountUp, value)) return;
                if (value <= 0) return;

                _amountUp = value;
                Amount = AmountUp / AmountDown;
            }
        }

        /// <summary>
        /// Количество (знаменатель)
        /// </summary>
        public double AmountDown
        {
            get { return _amountDown; }
            set
            {
                if (Equals(_amountDown, value)) return;
                if (value <= 0) return;

                _amountDown = value;
                Amount = AmountUp / AmountDown;

            }
        }

        public override void InitializeOther()
        {
            _amountUp = Model.Amount;

            this.PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName == nameof(UnitPrice))
                    OnPropertyChanged(nameof(Total));
            };
        }
    }
}
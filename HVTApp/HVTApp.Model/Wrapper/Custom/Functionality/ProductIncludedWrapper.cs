using System;
using System.ComponentModel;

namespace HVTApp.Model.Wrapper
{
    public partial class ProductIncludedWrapper
    {
        public double Count => (double)Amount / ParentsCount;

        public override void InitializeOther()
        {
            this.PropertyChanged += OnParentCountChanged;
        }

        private void OnParentCountChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ParentsCount)) return;
            RaisePropertyChanged(nameof(Count));
        }
    }
}
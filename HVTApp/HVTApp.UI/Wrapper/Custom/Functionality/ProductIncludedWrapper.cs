using System;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductIncludedWrapper
    {
        public double Count => (double)Amount / ParentsCount;

        private int _parentsCount = 1;
        public int ParentsCount
        {
            get { return _parentsCount; }
            set
            {
                _parentsCount = value;
                ParentsCountChanged?.Invoke(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(Count));
            }
        }

        public event Action<ProductIncludedWrapper> ParentsCountChanged;
    }
}
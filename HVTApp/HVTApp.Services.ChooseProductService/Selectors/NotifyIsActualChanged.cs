using System;

namespace HVTApp.Services.GetProductService
{
    public abstract class NotifyIsActualChanged : NotifyPropertyChanged
    {
        private bool _isActual = true;
        public bool IsActual
        {
            get { return _isActual; }
            set
            {
                if (Equals(_isActual, value)) return;
                _isActual = value;
                OnIsActualChanged(this);
                OnPropertyChanged();
            }
        }

        public event Action<object> IsActualChanged;

        protected virtual void OnIsActualChanged(object sender)
        {
            IsActualChanged?.Invoke(sender);
        }
        
    }
}
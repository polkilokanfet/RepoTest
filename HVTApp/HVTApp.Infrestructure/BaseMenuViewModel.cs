using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace HVTApp.Infrastructure
{
    public abstract class BaseMenuViewModel : BindableBase
    {
        private ObservableCollection<NavigationItem> _items;
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        protected BaseMenuViewModel()
        {
            Items = new ObservableCollection<NavigationItem>();
            GenerateMenu();
        }

        protected abstract void GenerateMenu();

    }
}

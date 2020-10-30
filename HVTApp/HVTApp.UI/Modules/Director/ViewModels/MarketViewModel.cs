using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Director.ViewModels
{
    public class MarketViewModel : ViewModelBase
    {
        private bool _isLoaded = false;
        public ObservableCollection<MarketUnit> MarketUnits { get; } = new ObservableCollection<MarketUnit>();

        public bool IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public ICommand ReloadCommand { get; }
        public ICommand ExpandCommand { get; }
        public ICommand CollapseCommand { get; }

        public event Action<bool> ExpandCollapseEvent; 

        public MarketViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            ExpandCommand = new DelegateCommand(() => { ExpandCollapseEvent?.Invoke(true); });
            CollapseCommand = new DelegateCommand(() => { ExpandCollapseEvent?.Invoke(false); });
        }

        public void Load()
        {
            IsLoaded = false;

            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsRemoved && x.Project.ForReport);

            MarketUnits.Clear();
            MarketUnits.AddRange(
                salesUnits
                .GroupBy(x => new {x.Project.Id, x.OrderInTakeDate})
                .OrderBy(x => x.Key.OrderInTakeDate)
                .Select(x => new MarketUnit(x)));

            IsLoaded = true;
        }
    }
}

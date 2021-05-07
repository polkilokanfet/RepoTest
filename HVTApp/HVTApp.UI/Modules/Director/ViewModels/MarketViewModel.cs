using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Director.ViewModels
{
    public class MarketViewModel : ViewModelBase
    {
        private bool _isLoaded = false;
        public ObservableCollection<MarketUnit> MarketUnits { get; } = new ObservableCollection<MarketUnit>();

        public bool IsLoaded
        {
            get => _isLoaded;
            private set
            {
                _isLoaded = value;
                RaisePropertyChanged();
            }
        }

        public DelegateLogCommand ReloadCommand { get; }
        public DelegateLogCommand ExpandCommand { get; }
        public DelegateLogCommand CollapseCommand { get; }

        public event Action<bool> ExpandCollapseEvent; 

        public MarketViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateLogCommand(Load);
            ExpandCommand = new DelegateLogCommand(() => { ExpandCollapseEvent?.Invoke(true); });
            CollapseCommand = new DelegateLogCommand(() => { ExpandCollapseEvent?.Invoke(false); });
        }

        public void Load()
        {
            IsLoaded = false;

            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsRemoved && x.Project.ForReport);

            MarketUnits.Clear();
            MarketUnits.AddRange(
                salesUnits
                .GroupBy(salesUnit => new {salesUnit.Project.Id, salesUnit.OrderInTakeDate})
                .OrderBy(x => x.Key.OrderInTakeDate)
                .Select(x => new MarketUnit(x)));

            IsLoaded = true;
        }
    }
}

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Director.ViewModels
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

        public MarketViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(async () => { await Load(); });
        }

        public async Task Load()
        {
            IsLoaded = false;

            var salesUnits = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync()).Where(x => x.Project.ForReport);

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

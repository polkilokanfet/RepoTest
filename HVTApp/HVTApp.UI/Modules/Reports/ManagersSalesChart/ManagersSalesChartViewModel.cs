using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.ManagersSalesChart
{
    public class ManagersSalesChartViewModel : ViewModelBase
    {
        private List<SalesUnit> _salesUnits;

        public ObservableCollection<ManagersSalesChartItem> Items { get; } = new ObservableCollection<ManagersSalesChartItem>();

        public ManagersSalesChartViewModel(IUnityContainer container) : base(container)
        {
            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            _salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon);

            var items = _salesUnits
                .GroupBy(x => x.Project.Manager)
                .Select(x => new ManagersSalesChartItem(x))
                .OrderByDescending(x => x.Sum)
                .ToList();

            Items.Clear();
            Items.AddRange(items);
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.Reference
{
    public class ReferenceViewModel : LoadableExportableViewModel
    {
        private IEnumerable<ReferenceItem> _items;
        public ObservableCollection<ReferenceItem> Items { get; } = new ObservableCollection<ReferenceItem>();

        public ReferenceViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void GetData()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon);
            var groups = salesUnits.GroupBy(x => x, new SalesUnitsReferenceComparer());
            _items = groups.Select(x => new ReferenceItem(x)).OrderBy(x => x.ShipmentDate);
        }

        protected override void AfterGetData()
        {
            Items.Clear();
            Items.AddRange(_items);
        }
    }
}
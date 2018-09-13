using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationsViewModel : SpecificationLookupListViewModel
    {
        private List<SalesUnit> _units;
        private IUnitOfWork _unitOfWork;

        public ObservableCollection<ProductUnitsGroup> Groups { get; set; } = new ObservableCollection<ProductUnitsGroup>();

        public SpecificationsViewModel(IUnityContainer container) : base(container)
        {
            this.SelectedLookupChanged += lookup => RefreshGroups();
        }

        protected override async Task<IEnumerable<SpecificationLookup>> GetLookups()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();
            _units = _unitOfWork.GetRepository<SalesUnit>().Find(x => x.Specification != null);
            var specs = _units.Select(x => x.Specification).Distinct();
            return specs.Select(x => new SpecificationLookup(x));
        }

        private void RefreshGroups()
        {
                Groups.Clear();
                if (SelectedItem != null)
                    Groups.AddRange(ProductUnitsGroup.Grouping(_units.Where(x => x.Specification.Id == SelectedItem.Id)));
        }
    }
}
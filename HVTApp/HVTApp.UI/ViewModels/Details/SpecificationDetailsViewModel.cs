using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public partial class SpecificationDetailsViewModel
    {
        public ObservableCollection<UnitGroup> UnitGroups { get; } = new ObservableCollection<UnitGroup>();

        //protected override async Task LoadOtherAsync()
        //{
        //    var groups = (await WrapperDataService.GetRepository<SalesUnit>().FindAsync(x => x.Specification?.Id == Item.Id)).
        //        Select(x => new SalesUnitWrapper(x)).ToUnitGroups();
        //    UnitGroups.AddRange(groups);
        //}
    }
}
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class ContractDetailsViewModel
    {
        public ObservableCollection<Specification> Specifications { get; } = new ObservableCollection<Specification>();

        //protected override async Task LoadOtherAsync()
        //{
        //    Specifications.AddRange(await WrapperDataService.GetRepository<Specification>().FindAsync(x => x.Contract.Id == Item.Id));
        //}
    }
}
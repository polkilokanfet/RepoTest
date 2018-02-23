using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class ContractDetailsViewModel
    {
        public ObservableCollection<Specification> Specifications { get; } = new ObservableCollection<Specification>();
        protected override async Task LoadOtherAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                Specifications.AddRange(UnitOfWork.GetRepository<Specification>().Find(x => x.Contract.Id == Item.Id));
            });
        }
    }
}
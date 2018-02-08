using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Extantions;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        public ObservableCollection<OfferUnitGroup> OfferUnitGroups { get; } = new ObservableCollection<OfferUnitGroup>();

        protected override async Task LoadOtherAsync()
        {
            var offerUnits = await UnitOfWork.GetRepository<OfferUnit>().GetAllAsync();
            offerUnits.ConvertToGroup().ToList().ForEach(OfferUnitGroups.Add);
            var pp = 1;
        }

    }
}
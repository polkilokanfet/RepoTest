using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using HVTApp.UI.Converter;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        public ObservableCollection<IUnitGroup> OfferUnits { get; } = new ObservableCollection<IUnitGroup>();

        protected override async Task LoadOtherAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                //OfferUnits.AddRange(Item.SalesUnits.ToUnitGroups());
            });
        }
    }
}
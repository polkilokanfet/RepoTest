using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        public ValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }
        public IEnumerable<IProductUnit> SelectedOfferUnits { get; set; }


        protected override void InitSpecialCommands()
        {
        }

        protected override async Task LoadOtherAsync()
        {
            var offerUnits = await UnitOfWork.GetRepository<OfferUnit>().FindAsync(x => Equals(x.Offer, Item.Model));
            OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(offerUnits.Select(x => new OfferUnitWrapper(x)));
            OnPropertyChanged(nameof(OfferUnits));
            foreach (var offerUnit in OfferUnits)
            {
                offerUnit.PropertyChanged += (sender, args) =>
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                };
            }
        }

        protected override bool SaveCommand_CanExecute()
        {
            return base.SaveCommand_CanExecute() || OfferUnits.IsChanged || OfferUnits.Any(x => x.IsChanged);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        public ValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }
        public IEnumerable<IProductUnit> SelectedOfferUnits { get; set; }

        public ICommand ChangeFacilityCommand { get; private set; }


        protected override void InitSpecialCommands()
        {
            ChangeFacilityCommand = new DelegateCommand(ChangeFacilityCommand_Execute);
            OnPropertyChanged(nameof(ChangeFacilityCommand));
        }

        private async void ChangeFacilityCommand_Execute()
        {
            var facilities = await UnitOfWork.GetRepository<Facility>().GetAllAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities);
            if (facility == null) return;
            var facilityWrapper = new FacilityWrapper(facility);
            foreach (var offerUnit in OfferUnits)
            {
                offerUnit.Facility = facilityWrapper;
            }
            OnPropertyChanged(nameof(OfferUnits));
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
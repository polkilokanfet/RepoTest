using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        private IEnumerable<IProductUnit> _selectedOfferUnits;
        public IEnumerable<IProductUnit> SelectedOfferUnits
        {
            get { return _selectedOfferUnits; }
            set
            {
                _selectedOfferUnits = value;
                ((DelegateCommand)ChangeFacilityCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand ChangeFacilityCommand { get; private set; }


        protected override void InitSpecialCommands()
        {
            ChangeFacilityCommand = new DelegateCommand(ChangeFacilityCommand_Execute, () => SelectedOfferUnits!= null && SelectedOfferUnits.Any());
        }

        private async void ChangeFacilityCommand_Execute()
        {
            //var vw = Container.Resolve<OfferUnitsDetailsViewModel>();
            //Container.Resolve<IDialogService>().ShowDialog(vw);

            //var facilities = await WrapperDataService.GetRepository<Facility>().GetAllAsync();
            //var facility = await Container.Resolve<ISelectService>().SelectItem(facilities);
            //if (facility == null) return;
            //var facilityWrapper = await WrapperDataService.GetWrapperRepository<Facility, FacilityWrapper>().GetByIdAsync(facility.Id);
            //foreach (var offerUnit in SelectedOfferUnits)
            //    offerUnit.Facility = facilityWrapper;

            var offerUnit = (OfferUnitWrapper)SelectedOfferUnits.First();
            await Container.Resolve<IUpdateDetailsService>().UpdateDetails(offerUnit.Model);

            OnPropertyChanged(nameof(Item));
        }
    }
}
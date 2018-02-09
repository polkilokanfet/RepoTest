using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Extantions;
using HVTApp.UI.Wrapper;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        public ObservableCollection<OfferUnitGroupWrapper> OfferUnitGroups { get; } = new ObservableCollection<OfferUnitGroupWrapper>();

        protected override async Task LoadOtherAsync()
        {
            var offerUnits = await UnitOfWork.GetRepository<OfferUnit>().GetAllAsync();
            foreach (var offerUnitGroup in offerUnits.ConvertToGroup())
            {
                var wrapper = new OfferUnitGroupWrapper(offerUnitGroup);
                OfferUnitGroups.Add(wrapper);
                wrapper.PropertyChanged += OfferUnitGroupOnPropertyChanged;
            }
        }

        private void OfferUnitGroupOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override bool SaveCommand_CanExecute()
        {
            return base.SaveCommand_CanExecute() || (
                   OfferUnitGroups.Any(x => x.IsChanged) &&
                   OfferUnitGroups.All(x => x.IsValid));
        }
    }
}
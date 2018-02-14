using System;
using System.Collections.Generic;
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
        //public ObservableCollection<OfferUnitsGrouped> OfferUnitsGroupedCollection { get; } = new ObservableCollection<OfferUnitsGrouped>();


        protected override async Task LoadOtherAsync()
        {
            //_offerUnitWrappers = (await UnitOfWork.GetRepository<OfferUnit>().GetAllAsync())
            //    .Where(x => x.OfferId == Item.Id).Select(x => new OfferUnitWrapper(x)).ToList();
            //foreach (var offerUnitsGrouped in _offerUnitWrappers.ConvertToGroup())
            //{
            //    OfferUnitsGroupedCollection.Add(offerUnitsGrouped);
            //    foreach (var offerUnitWrapper in offerUnitsGrouped.UnitWrappers)
            //    {
            //        offerUnitWrapper.PropertyChanged += OfferUnitGroupOnPropertyChanged;
            //    }
            //}
        }

        private void OfferUnitGroupOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        //protected override bool SaveCommand_CanExecute()
        //{
        //    return base.SaveCommand_CanExecute() || (
        //           OfferUnitsGroupedCollection.SelectMany(x => x.UnitWrappers).Any(x => x.IsChanged) &&
        //           OfferUnitsGroupedCollection.SelectMany(x => x.UnitWrappers).All(x => x.IsValid));
        //}
    }
}
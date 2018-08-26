using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapper
    {
        public ValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; }

        public OfferWrapper(Offer offer, IEnumerable<OfferUnitWrapper> offerUnitWrappers) : this(offer)
        {
            OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(offerUnitWrappers);
            RegisterCollectionWithoutSynch(OfferUnits);
        }

        public double VatProc
        {
            get { return this.Vat * 100; }
            set
            {
                if (value < 0) return;
                this.Vat = value / 100;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(SumWithVat));
            }
        }

        //public double Sum => OfferUnits.Sum(x => x.Cost);
        //public double SumWithVat => Sum * (1 + Vat);
    }
}

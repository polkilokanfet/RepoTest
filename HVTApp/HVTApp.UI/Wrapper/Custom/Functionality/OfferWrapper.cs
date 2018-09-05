using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapper : IWrapperWithUnits<OfferUnitWrapper>
    {
        public ValidatableChangeTrackingCollection<OfferUnitWrapper> Units { get; }

        public OfferWrapper(Offer offer, IEnumerable<OfferUnitWrapper> offerUnitWrappers) : this(offer)
        {
            Units = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(offerUnitWrappers);
            RegisterCollectionWithoutSynch(Units);
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

        public double Sum => Units.Sum(x => x.Cost);
        public double SumWithVat => Sum * (1 + Vat);
    }
}

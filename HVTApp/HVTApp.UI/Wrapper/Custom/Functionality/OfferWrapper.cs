using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Converter;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapper
    {
        //public IEnumerable<OfferUnitsGroup> OfferUnitsGroups => OfferUnits.ToUnitGroups();

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapper
    {
        public double VatProc
        {
            get { return this.Vat * 100; }
            set
            {
                if (value < 0) return;
                this.Vat = value / 100;
                OnPropertyChanged();
            }
        }

        public double TotalCost => OfferUnits.Sum(x => x.Cost);
        public double TotalCostWithVat => TotalCost * (1 + Vat);
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public class ProductUnitsGroup : INotifyPropertyChanged
    {
        private double _marginalIncome;
        public ProductUnitsGroup(IEnumerable<IProductUnit> productUnits)
        {
            ProductUnits = new List<IProductUnit>(productUnits);
            //_marginalIncome = (1 - Product.GetPrice() / Cost) * 100;
        }

        public List<IProductUnit> ProductUnits { get; }

        public FacilityWrapper Facility
        {
            get { return ProductUnits.First().Facility; }
            set
            {
                if(Equals(Facility, value)) return;
                ProductUnits.ForEach(x => x.Facility = value);
                OnPropertyChanged();
            }
        }

        public ProductWrapper Product
        {
            get { return ProductUnits.First().Product; }
            set
            {
                if (Equals(Product, value)) return;
                ProductUnits.ForEach(x => x.Product = value);
                OnPropertyChanged();
            }
        }

        public double Cost
        {
            get { return ProductUnits.First().Cost; }
            set
            {
                if (Equals(value, Cost)) return;
                if (value < 0) return;

                ProductUnits.ForEach(x => x.Cost = value);
                //MarginalIncome = (1 - Product.GetPrice() / value) * 100;
                OnPropertyChanged();
            }
        }




        public int Amount => ProductUnits.Count;
        public double Total => Cost * Amount;



        public double MarginalIncome
        {
            get { return _marginalIncome; }
            set
            {
                if (Equals(_marginalIncome, value)) return;
                if (value >= 100) return;

                _marginalIncome = value;
                //Cost = Product.GetPrice() / (1 - value / 100);
                OnPropertyChanged();
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
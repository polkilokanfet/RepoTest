using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Converter
{
    public interface IProductUnitsGroup
    {
        List<IProductUnit> ProductUnits { get; }
        ObservableCollection<IProductUnitsGroup> Groups { get; }
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        double Cost { get; set; }
        int Amount { get; }
        double Total { get; }
        double MarginalIncome { get; set; }
    }

    public class ProductUnitsGroup : INotifyPropertyChanged, IProductUnitsGroup
    {
        public ProductUnitsGroup(IEnumerable<IProductUnit> productUnits)
        {
            ProductUnits = productUnits.ToList();
            if (ProductUnits.Count > 1)
                Groups.AddRange(ProductUnits.Select(x => new ProductUnitsGroup(new List<IProductUnit> {x})));

            ProductUnits.First().PriceChanged += () => { MarginalIncome = (1 - Price / Cost) * 100; };
        }

        private double _marginalIncome;

        public int Amount => ProductUnits.Count;
        public double Price => ProductUnits.First().Price;
        public double Total => Cost * Amount;

        public List<IProductUnit> ProductUnits { get; }
        public ObservableCollection<IProductUnitsGroup> Groups { get; } = new ObservableCollection<IProductUnitsGroup>();

        public FacilityWrapper Facility
        {
            get { return ProductUnits.First().Facility; }
            set
            {
                if(Equals(Facility, value)) return;
                Groups.ForEach(x => x.Facility = value);
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
                Groups.ForEach(x => x.Product = value);
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

                Groups.ForEach(x => x.Cost = value);
                ProductUnits.ForEach(x => x.Cost = value);
                MarginalIncome = (1 - Price / value) * 100;
                OnPropertyChanged();
            }
        }

        public double MarginalIncome
        {
            get { return _marginalIncome; }
            set
            {
                if (Equals(_marginalIncome, value)) return;
                if (value >= 100) return;

                _marginalIncome = value;
                Cost = Price / (1 - value / 100);
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
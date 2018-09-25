using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Groups
{
    public class SalesUnitsGroup : BindableBase, IValidatableChangeTracking
    {
        private double _price;
        private readonly SalesUnitWrapper _unit;

        public ValidatableChangeTrackingCollection<SalesUnitsGroup> Groups { get; }

        public int Amount => Groups?.Count ?? 1;

        public double Total => Groups?.Sum(x => x.Cost) ?? _unit.Cost;

        public FacilityWrapper Facility
        {
            get { return GetValue<FacilityWrapper>(); }
            set { SetValue(value); }
        }

        public ProductWrapper Product
        {
            get { return GetValue<ProductWrapper>(); }
            set { SetValue(value); }
        }

        public SpecificationWrapper Specification
        {
            get { return GetValue<SpecificationWrapper>(); }
            set { SetValue(value); }
        }

        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get { return GetValue<PaymentConditionSetWrapper>(); }
            set { SetValue(value); }
        }

        public DateTime OrderInTakeDate => GetValue<DateTime>();

        public SalesUnit Model => GetValue<SalesUnit>();

        public double Cost
        {
            get { return GetValue<double>(); }
            set
            {
                if (value < 0) return;
                SetValue(value);
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (Math.Abs(_price - value) < 0.00001) return;
                _price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MarginalIncome));
            }
        }

        public double? MarginalIncome
        {
            get { return Cost <= 0 ? default(double?) : (1.0 - Price / Cost) * 100.0; }
            set
            {
                if (!value.HasValue || value >= 100) return;
                Cost = Price / (1.0 - value.Value / 100.0);
                OnPropertyChanged();
            }
        }

        public int ProductionTerm
        {
            get { return GetValue<int>(); }
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public DateTime DeliveryDateExpected
        {
            get { return GetValue<DateTime>(); }
            set
            {
                if(value < DateTime.Today) return;
                SetValue(value);
            }
        }

        public SalesUnitsGroup(IEnumerable<SalesUnit> units)
        {
            var salesUnits = units as SalesUnit[] ?? units.ToArray();

            if (salesUnits.Count() == 1)
            {
                _unit = new SalesUnitWrapper(salesUnits.First());
                _unit.PropertyChanged += UnitOnPropertyChanged;
                return;
            }
            
            //создаем группы
            var groups = salesUnits.Select(x => new SalesUnitsGroup(new[] {x}));
            Groups = new ValidatableChangeTrackingCollection<SalesUnitsGroup>(groups);

            Groups.ForEach(x => x.PropertyChanged += UnitOnPropertyChanged);
            Groups.CollectionChanged += GroupsOnCollectionChanged;
        }

        private void UnitOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Cost))
                OnPropertyChanged(nameof(Cost));

            if (args.PropertyName == nameof(IsChanged))
                OnPropertyChanged(nameof(IsChanged));

            if (args.PropertyName == nameof(IsValid))
                OnPropertyChanged(nameof(IsValid));
        }

        public IEnumerable<ProductIncludedWrapper> ProductsIncluded => GetValue<IEnumerable<ProductIncludedWrapper>>();

        //добавление зависимого оборудования
        public void AddProductIncluded(ProductIncluded productIncluded)
        {
            if (Groups == null)
            {
                _unit.ProductsIncluded.Add(new ProductIncludedWrapper(productIncluded));
            }
            else
            {
                foreach (var salesUnitsGroup in Groups)
                {
                    var pi = new ProductIncluded { Product = productIncluded.Product, Amount = productIncluded.Amount };
                    salesUnitsGroup.AddProductIncluded(pi);
                }
            }

            OnPropertyChanged(nameof(ProductsIncluded));
        }

        //удаление зависимого оборудования
        public void RemoveProductIncluded(ProductIncludedWrapper productIncluded)
        {
            if (Groups == null)
            {
                _unit.ProductsIncluded.Remove(productIncluded);
            }
            else
            {
                foreach (var salesUnitsGroup in Groups)
                {
                    var pi = salesUnitsGroup.ProductsIncluded.First(x => x.Product.Id == productIncluded.Product.Id && 
                                                                         x.Amount == productIncluded.Amount);
                    salesUnitsGroup.RemoveProductIncluded(pi);
                }
            }

            OnPropertyChanged(nameof(ProductsIncluded));
        }

        //изменение коллекции групп
        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            OnPropertyChanged(nameof(Amount));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(IsChanged));
            OnPropertyChanged(nameof(IsValid));
        }

        private T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var obj = _unit ?? (object)Groups.First();
            return (T)obj.GetType().GetProperty(propertyName).GetValue(obj);
        }

        private void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (_unit != null)
            {
                var propInfo = _unit.GetType().GetProperty(propertyName);
                if(Equals(value, propInfo.GetValue(_unit))) return;
                propInfo.SetValue(_unit, value);
            }

            if (Groups != null)
            {
                foreach (var salesUnitsGroup in Groups)
                {
                    salesUnitsGroup.GetType().GetProperty(propertyName).SetValue(salesUnitsGroup, value);
                }
            }
            OnPropertyChanged(propertyName);
        }

        public void AcceptChanges()
        {
            _unit?.AcceptChanges();
            Groups?.ForEach(x => x.AcceptChanges());
        }

        public bool IsChanged => _unit?.IsChanged ?? Groups.IsChanged || Groups.Any(x => x.IsChanged) ;

        public void RejectChanges()
        {
            _unit?.RejectChanges();
            Groups?.ForEach(x => x.RejectChanges());
        }

        public bool IsValid => _unit?.IsValid ?? Groups.All(x => x.IsValid);
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Groups
{
    public class BaseWrappersGroup<TSubGroup, TModel, TWrapper> : BindableBase, IGroupValidatableChangeTracking<TModel>
        where TSubGroup : class, IGroupValidatableChangeTracking<TModel>
        where TModel : class, IUnitPoco
        where TWrapper : class, IWrapperGroup<TModel>
    {
        private readonly TWrapper _unit;
        private double _price;

        public TModel Model => GetValue<TModel>();

        public IValidatableChangeTrackingCollection<TSubGroup> Groups { get; }

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

        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get { return GetValue<PaymentConditionSetWrapper>(); }
            set { SetValue(value); }
        }


        public double Cost
        {
            get { return Total / Amount; }
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


        public BaseWrappersGroup(List<TModel> units)
        {
            if (units.Count() == 1)
            {
                _unit = (TWrapper)Activator.CreateInstance(typeof(TWrapper), units.First());
                _unit.PropertyChanged += UnitOnPropertyChanged;
                return;
            }

            //создаем подгруппы
            var groups = units.Select(x => (TSubGroup)Activator.CreateInstance(typeof(TSubGroup), new List<TModel> {x})).ToList();
            Groups = new ValidatableChangeTrackingCollection<TSubGroup>(groups);

            //подписка на события
            Groups.PropertyChanged += UnitOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;
        }

        /// <summary>
        /// изменение члена коллекции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void UnitOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Cost)) OnPropertyChanged(nameof(Cost));
            if (args.PropertyName == nameof(IsChanged)) OnPropertyChanged(nameof(IsChanged));
            if (args.PropertyName == nameof(IsValid)) OnPropertyChanged(nameof(IsValid));
        }

        /// <summary>
        /// изменение коллекции групп
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            OnPropertyChanged(nameof(Amount));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(IsChanged));
            OnPropertyChanged(nameof(IsValid));
        }

        public IEnumerable<ProductIncludedWrapper> ProductsIncluded => GetValue<IEnumerable<ProductIncludedWrapper>>();

        /// <summary>
        /// добавление зависимого оборудования
        /// </summary>
        /// <param name="productIncluded"></param>
        /// <param name="isForEach"></param>
        public void AddProductIncluded(ProductIncluded productIncluded, bool isForEach = true)
        {
            //если вложенных групп нет
            if (Groups == null)
            {
                _unit.ProductsIncluded.Add(new ProductIncludedWrapper(productIncluded));
            }

            //если есть вложенные группы
            else
            {
                //если в каждой группе должен быть свой уникальный включенный продукт
                if (isForEach)
                {
                    foreach (var grp in Groups)
                    {
                        var pi = new ProductIncluded { Product = productIncluded.Product, Amount = productIncluded.Amount };
                        grp.AddProductIncluded(pi, true);
                    }
                }

                //если один включенный продукт на все группы
                else
                {
                    foreach (var grp in Groups)
                    {
                        grp.AddProductIncluded(productIncluded, false);
                    }
                }
            }

            OnPropertyChanged(nameof(ProductsIncluded));
        }

        /// <summary>
        /// удаление зависимого оборудования
        /// </summary>
        /// <param name="productIncluded"></param>
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


        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var obj = _unit ?? (object)Groups.First();
            return (T)obj.GetType().GetProperty(propertyName).GetValue(obj);
        }

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (_unit != null)
            {
                var propInfo = _unit.GetType().GetProperty(propertyName);
                if (!Equals(value, propInfo.GetValue(_unit)))
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
            if (_unit != null)
            {
                _unit?.AcceptChanges();
                return;
            }

            //отписка от событий
            Groups.PropertyChanged -= UnitOnPropertyChanged;
            Groups.CollectionChanged -= GroupsOnCollectionChanged;

            Groups?.ForEach(x => x.AcceptChanges());

            //подписка на события
            Groups.PropertyChanged += UnitOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;

        }

        public bool IsChanged => _unit?.IsChanged ?? Groups.IsChanged || Groups.Any(x => x.IsChanged);

        public void RejectChanges()
        {
            _unit?.RejectChanges();
            Groups?.ForEach(x => x.RejectChanges());
        }

        public bool IsValid => _unit?.IsValid ?? Groups.All(x => x.IsValid);
    }
}
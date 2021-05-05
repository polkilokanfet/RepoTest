using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.Model.Wrapper.Groups
{
    public abstract class BaseWrappersGroup<TMember, TModel, TWrapper> : BindableBase, IGroupValidatableChangeTracking<TModel>
        where TMember : class, IGroupValidatableChangeTracking<TModel>
        where TModel : class, IUnit
        where TWrapper : class, IWrapperGroup<TModel>
    {

        #region Fields

        private readonly TWrapper _unit;
        private double _price;
        private double _fixedCost = 0;

        #endregion

        #region Public properties

        public IValidatableChangeTrackingCollection<TMember> Groups { get; }

        public TModel Model => GetValue<TModel>();

        public int Amount => Groups?.Count ?? 1;

        public double Total => Groups?.Sum(x => x.Cost) ?? _unit.Cost;


        public string Comment
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public FacilitySimpleWrapper Facility
        {
            get => _unit != null ? _unit.Facility : Groups.First().Facility;
            set => SetValue(value);
        }

        public ProductSimpleWrapper Product
        {
            get => _unit != null ? _unit.Product : Groups.First().Product;
            set => SetValue(value);
        }

        public PaymentConditionSetSimpleWrapper PaymentConditionSet
        {
            get => GetValue<PaymentConditionSetSimpleWrapper>();
            set => SetValue(value);
        }

        public double Cost
        {
            get => Total / Amount;
            set
            {
                if (value < CostMin) return;
                SetValue(value);
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(Total));
            }
        }

        /// <summary>
        /// ��������� �������� ������ ������������
        /// </summary>
        public double? CostDelivery
        {
            get
            {
                if (Groups == null)
                    return _unit.Model.CostDelivery;
                return Groups.Sum(x => x.CostDelivery);
            }
            set
            {
                if (value.HasValue && value.Value < 0) return;

                SetValue(value / Amount);
                CheckCost();
                OnPropertyChanged(nameof(MarginalIncome));
            }
        }

        public bool CostDeliveryIncluded
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public double FixedCost
        {
            get => _fixedCost;
            set
            {
                if (Math.Abs(_fixedCost - value) < 0.00001) return;
                _fixedCost = value;
                CheckCost();
                OnPropertyChanged();
                OnPropertyChanged(nameof(MarginalIncome));
            }
        }

        private void CheckCost()
        {
            if (Cost < CostMin)
                Cost = CostMin;
        }

        /// <summary>
        /// ���������� ��������� ���� ������� ������������ (�������� � ������������� ����� + ��������� ��������)
        /// </summary>
        private double CostMin => CostDelivery.HasValue 
            ? FixedCost + CostDelivery.Value / Amount
            : FixedCost;

        public double Price
        {
            get => _price;
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
            get
            {
                return Cost - CostMin <= 0
                    ? default(double?)
                    : (1.0 - Price / (Cost - CostMin)) * 100.0;
            }
            set
            {
                if (!value.HasValue || value >= 100) return;

                var marginalIncome = value.Value;
                Cost = Price / (1.0 - marginalIncome / 100.0) + CostMin;
                OnPropertyChanged();
            }
        }

        public int ProductionTerm
        {
            get => GetValue<int>();
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }


        public bool IsChanged => _unit?.IsChanged ?? Groups.IsChanged || Groups.Any(x => x.IsChanged);

        public bool IsValid => _unit?.IsValid ?? Groups.All(x => x.IsValid);

        #endregion

        #region Ctor

        protected BaseWrappersGroup(List<TModel> units)
        {
            //���� �������� ������ ���� ����
            if (units.Count == 1)
            {
                _unit = (TWrapper)Activator.CreateInstance(typeof(TWrapper), units.First());
                _unit.PropertyChanged += UnitOnPropertyChanged;
                return;
            }

            //���� ��������� ����� ������ �����
            //������� ���������
            var groups = units.Select(x => (TMember)Activator.CreateInstance(typeof(TMember), new List<TModel> {x}))
                              .OrderBy(x => x.Cost);
            Groups = new ValidatableChangeTrackingCollection<TMember>(groups);

            //������������� �� ������� �� �������
            Groups.PropertyChanged += UnitOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;
        }

        #endregion

        #region On Changed

        /// <summary>
        /// ��������� ����� ���������
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
        /// ��������� ��������� �����
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

        #endregion

        #region Included Products

        public IEnumerable<ProductIncludedSimpleWrapper> ProductsIncluded
        {
            get
            {
                return _unit?.ProductsIncluded 
                       ?? Groups
                           .SelectMany(x => x.ProductsIncluded)
                           .Select(x => x.Model)
                           .Distinct()
                           .Select(productIncluded => new ProductIncludedSimpleWrapper(productIncluded));
            }
        }
        
        /// <summary>
        /// ���������� ���������� ������������
        /// </summary>
        /// <param name="productIncluded"></param>
        /// <param name="isForEach"></param>
        public void AddProductIncluded(ProductIncluded productIncluded, bool isForEach = true)
        {
            //���� ��������� ����� ���
            if (Groups == null)
            {
                _unit.ProductsIncluded.Add(new ProductIncludedSimpleWrapper(productIncluded));
            }

            //���� ���� ��������� ������
            else
            {
                //���� � ������ ������ ������ ���� ���� ���������� ���������� �������
                if (isForEach)
                {
                    foreach (var grp in Groups)
                    {
                        var pi = new ProductIncluded { Product = productIncluded.Product, Amount = productIncluded.Amount };
                        grp.AddProductIncluded(pi, true);
                    }
                }

                //���� ���� ���������� ������� �� ��� ������
                else
                {
                    //��������� ���������� ��������� ����������� ��������
                    productIncluded.ParentsCount = Groups.Count;
                    //��������� ������� � ������ ������
                    foreach (var grp in Groups)
                    {
                        grp.AddProductIncluded(productIncluded, false);
                    }
                }
            }

            OnPropertyChanged(nameof(ProductsIncluded));
        }

        /// <summary>
        /// �������� ���������� ������������
        /// </summary>
        /// <param name="productIncluded"></param>
        public void RemoveProductIncluded(ProductIncludedSimpleWrapper productIncluded)
        {
            if (Groups == null)
            {
                _unit.ProductsIncluded.Remove(productIncluded);
            }
            else
            {
                foreach (var grp in Groups)
                {
                    var pi = grp.ProductsIncluded.First(x => x.Model.Product.Id == productIncluded.Model.Product.Id &&
                                                                         x.Model.Amount == productIncluded.Model.Amount);
                    grp.RemoveProductIncluded(pi);
                }
            }

            OnPropertyChanged(nameof(ProductsIncluded));
        }

        #endregion

        #region Private Methods

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

        #endregion

        #region Public Methods

        public void AcceptChanges()
        {
            if (_unit != null)
            {
                _unit?.AcceptChanges();
                return;
            }

            //������� �� �������
            Groups.PropertyChanged -= UnitOnPropertyChanged;
            Groups.CollectionChanged -= GroupsOnCollectionChanged;

            Groups?.ForEach(x => x.AcceptChanges());

            //�������� �� �������
            Groups.PropertyChanged += UnitOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;
        }

        public void RejectChanges()
        {
            _unit?.RejectChanges();
            Groups?.ForEach(x => x.RejectChanges());
        }

        #endregion

        public SalesUnit SalesUnit => GetSalesUnit();

        protected abstract SalesUnit GetSalesUnit();
    }
}
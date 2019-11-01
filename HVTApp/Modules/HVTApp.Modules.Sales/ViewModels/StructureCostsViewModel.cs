using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class StructureCostsViewModel : BindableBaseCanExportToExcel
    {
        private object _selectedItem;


        public ICommand SaveCommand { get; }
        public ICommand AddStructureCostCommand { get; }
        public ICommand RemoveStructureCostCommand { get; }

        public string Project => SalesUnitWrappers.First().Project.ToString();
        public string Contragent => SalesUnitWrappers.First().Specification?.Contract.Contragent.ToString();

        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnitWrappers { get; private set; }

        public ObservableCollection<StructureCostsGroupWrapper> StructureCostsGroupWrappers { get; } = new ObservableCollection<StructureCostsGroupWrapper>();

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand) AddStructureCostCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) RemoveStructureCostCommand).RaiseCanExecuteChanged();
            }
        }

        public StructureCostsViewModel(IUnityContainer container) : base(container)
        {
            //сохранение изменений
            SaveCommand = new DelegateCommand(
                async () =>
                {
                    SalesUnitWrappers.AcceptChanges();
                    await UnitOfWork.SaveChangesAsync();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }, 
                () => SalesUnitWrappers.IsValid && SalesUnitWrappers.IsChanged);

            //добавление стракчакоста
            AddStructureCostCommand = new DelegateCommand(
                () =>
                {
                    var structureCost = new StructureCost
                    {
                        Comment = "StructureCostNew"
                    };
                    (SelectedItem as StructureCostsGroupWrapper).StructureCosts.StructureCostsList.Add(new StructureCostWrapper(structureCost));
                },
                () => SelectedItem is StructureCostsGroupWrapper);

            //удаление стракчакоста
            RemoveStructureCostCommand = new DelegateCommand(
                () =>
                {
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить StructureCost?");
                    if (result != MessageDialogResult.Yes) return;

                    var structureCost = SelectedItem as StructureCostWrapper;
                    var structureCostsGroupWrapper = StructureCostsGroupWrappers.Single(x => x.StructureCosts.StructureCostsList.Contains(structureCost));
                    structureCostsGroupWrapper.StructureCosts.StructureCostsList.Remove(structureCost);
                },
                () => SelectedItem is StructureCostWrapper);
        }

        public void Load(Project project)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //получаем все юниты проекта
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id);
            SalesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits.Select(x => new SalesUnitWrapper(x)));

            //реакция на изменения в любом юните
            SalesUnitWrappers.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
            };

            //группируем юниты
            var groups = SalesUnitWrappers.GroupBy(x => x, new SalesUnitWrappersComparer());

            foreach (var salesUnitWrappersGroup in groups)
            {
                //если не прикреплены стракчакосты, создаем их
                if (salesUnitWrappersGroup.Key.StructureCosts == null)
                {
                    var structureCosts = new StructureCosts();

                    //стракчакост основного оборудования
                    structureCosts.StructureCostsList.Add(new StructureCost
                    {
                        Comment = salesUnitWrappersGroup.Key.Product.Designation,
                        Amount = 1
                    });

                    //стракчакосты включённого оборудования
                    foreach (var productIncluded in salesUnitWrappersGroup.Key.ProductsIncluded)
                    {
                        structureCosts.StructureCostsList.Add(new StructureCost
                        {
                            Comment = productIncluded.Product.Designation,
                            Amount = productIncluded.Amount
                        });
                    }

                    //прикрепляем
                    var structureCostsWrapper = new StructureCostsWrapper(structureCosts);
                    salesUnitWrappersGroup.ForEach(x => x.StructureCosts = structureCostsWrapper);
                }
            }

            StructureCostsGroupWrappers.Clear();
            StructureCostsGroupWrappers.AddRange(groups.Select(x => new StructureCostsGroupWrapper(x.Key.StructureCosts, x)));
        }
    }

    public class StructureCostsGroupWrapper
    {
        public string Facility { get; }
        public string Product { get; }
        public int Amount { get; }
        public double Cost { get; }
        public string PaymentConditions { get; }
        public DateTime OrderInTakeDate { get; }
        public DateTime RealizationDate { get; }

        public StructureCostsWrapper StructureCosts { get; }

        public IEnumerable<StructureCostWrapper> StructureCostWrappers => StructureCosts.StructureCostsList;
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; }
        public StructureCostsGroupWrapper(StructureCostsWrapper structureCosts, IEnumerable<SalesUnitWrapper> salesUnits)
        {
            StructureCosts = structureCosts;
            
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits);

            SalesUnits.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (SalesUnitWrapper removed in args.OldItems)
                    {
                        removed.StructureCosts = null;
                    }
                }
            };

            Facility = SalesUnits.First().Facility.ToString();
            Product = SalesUnits.First().Product.ToString();
            Cost = SalesUnits.First().Cost;
            PaymentConditions = SalesUnits.First().PaymentConditionSet.ToString();
            OrderInTakeDate = SalesUnits.First().OrderInTakeDate;
            RealizationDate = SalesUnits.First().RealizationDateCalculated;

            Amount = SalesUnits.Count;
        }
    }

    public class SalesUnitWrappersComparer : IEqualityComparer<SalesUnitWrapper>
    {
        public bool Equals(SalesUnitWrapper x, SalesUnitWrapper y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Project.Id, y.Project.Id)) return false;
            if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
            if (!Equals(x.Product.Id, y.Product.Id)) return false;
            if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;
            if (!Equals(x.OrderInTakeDate, y.OrderInTakeDate)) return false;
            if (!Equals(x.RealizationDateCalculated, y.RealizationDateCalculated)) return false;
            if (!Equals(x.StructureCosts?.Id, y.StructureCosts?.Id)) return false;

            var productsInclX = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();
            var productsInclY = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();

            if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
            if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;


            return true;
        }

        public int GetHashCode(SalesUnitWrapper salesUnit)
        {
            return 0;
        }


        private class ProductAmount
        {
            public Guid ProductId { get; }
            public int Amount { get; }

            public ProductAmount(Guid productId, int amount)
            {
                ProductId = productId;
                Amount = amount;
            }

            public override bool Equals(object obj)
            {
                var other = obj as ProductAmount;
                return other != null && Equals(this.ProductId, other.ProductId) && this.Amount == other.Amount;
            }
        }

        private class ProductAmountComparer : IEqualityComparer<ProductAmount>
        {
            public bool Equals(ProductAmount x, ProductAmount y)
            {
                return Equals(x.ProductId, y.ProductId) && Equals(x.Amount, y.Amount);
            }

            public int GetHashCode(ProductAmount obj)
            {
                return 0;
            }
        }

    }
}
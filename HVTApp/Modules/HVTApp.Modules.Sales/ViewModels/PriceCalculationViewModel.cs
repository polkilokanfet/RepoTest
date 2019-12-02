using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PriceCalculationViewModel : ViewModelBaseCanExportToExcel
    {
        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand)AddStructureCostCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveStructureCostCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand AddStructureCostCommand { get; }
        public ICommand RemoveStructureCostCommand { get; }

        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public PriceCalculation2Wrapper PriceCalculationWrapper { get; private set; }

        public ObservableCollection<SalesUnitsPriceCalculationGroup> Groups { get; } = new ObservableCollection<SalesUnitsPriceCalculationGroup>();

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
            //���������� ���������
            SaveCommand = new DelegateCommand(
                async () =>
                {
                    PriceCalculationWrapper.TaskOpenMoment = DateTime.Now;
                    PriceCalculationWrapper.AcceptChanges();
                    await UnitOfWork.SaveChangesAsync();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () => PriceCalculationWrapper.IsValid && PriceCalculationWrapper.IsChanged);

            //���������� ������������
            AddStructureCostCommand = new DelegateCommand(
                () =>
                {
                    var structureCost = new StructureCost { Comment = "No title" };
                    var structureCostWrapper = new StructureCostWrapper(structureCost);
                    (SelectedItem as SalesUnitsPriceCalculationGroup).StructureCostsWrapper.StructureCostsList.Add(structureCostWrapper);
                },
                () => SelectedItem is SalesUnitsPriceCalculationGroup);

            //�������� ������������
            RemoveStructureCostCommand = new DelegateCommand(
                () =>
                {
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "������������� ������ ������� StructureCost?");
                    if (result != MessageDialogResult.Yes) return;

                    var structureCost = SelectedItem as StructureCostWrapper;
                    var structureCostsGroupWrapper = Groups.Single(x => x.StructureCostsWrapper.StructureCostsList.Contains(structureCost));
                    structureCostsGroupWrapper.StructureCostsWrapper.StructureCostsList.Remove(structureCost);
                },
                () => SelectedItem is StructureCostWrapper);

            //���������� ������ ������������
            AddGroupCommand = new DelegateCommand(
                () =>
                {
                    //������������� ������
                    var groups = UnitOfWork.Repository<SalesUnit>()
                            .Find(x => x.Project.Manager.IsAppCurrentUser())
                            .Except(PriceCalculationWrapper.SalesUnits.Select(x => x.Model))
                            .Select(x => new SalesUnitPriceCalculationWrapper(x))
                            .GroupBy(x => x, new SalesUnitPriceCalculationComparer())
                            .Select(x => new SalesUnitsPriceCalculationGroupSimple(x));

                    //����� ������
                    var viewModel = new SalesUnitsPriceCalculationGroupsViewModel(groups);
                    var dialogResult = Container.Resolve<IDialogService>().ShowDialog(viewModel);

                    //���������� ������
                    if (dialogResult.HasValue && dialogResult.Value)
                    {
                        Groups.AddRange(viewModel.SelectedGroups.Select(x => new SalesUnitsPriceCalculationGroup(x.SalesUnitPriceCalculationWrappers)));
                    }
                });

            //�������� ������
            RemoveGroupCommand = new DelegateCommand(
                () =>
                {
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "������������� ������ ������� �� ������� ������ ������������?");
                    if (result != MessageDialogResult.Yes) return;

                    var selectedGroup = SelectedItem as SalesUnitsPriceCalculationGroup;
                    selectedGroup.SalesUnitPriceCalculationWrappers.ForEach(x => x.RejectChanges());
                    Groups.Remove(selectedGroup);
                },
                () => SelectedItem is SalesUnitsPriceCalculationGroup);


        }

        /// <summary>
        /// �������� ��� �������� ������ �������
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitWrappers = salesUnits
                .Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.Id))
                .Select(x => new SalesUnitPriceCalculationWrapper(x))
                .ToList();

            //������� ������
            PriceCalculationWrapper = new PriceCalculation2Wrapper(new PriceCalculation {Author = GlobalAppProperties.User});
            
            //������� �� ��������� � ������
            PriceCalculationWrapper.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            };

            //������� �� ��������� � ��������� �����
            Groups.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    args.NewItems.Cast<SalesUnitsPriceCalculationGroup>()
                        .SelectMany(x => x.SalesUnitPriceCalculationWrappers)
                        .ForEach(x => PriceCalculationWrapper.SalesUnits.Add(x));
                }

                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    args.OldItems.Cast<SalesUnitsPriceCalculationGroup>()
                        .SelectMany(x => x.SalesUnitPriceCalculationWrappers)
                        .ForEach(x => PriceCalculationWrapper.SalesUnits.Remove(x));
                }
            };

            //���������� �����
            var groups = salesUnitWrappers.GroupBy(x => x, new SalesUnitPriceCalculationComparer());
            Groups.AddRange(groups.Select(x => new SalesUnitsPriceCalculationGroup(x)).ToList());
        }
    }

    public class SalesUnitsPriceCalculationGroupSimple
    {
        public List<SalesUnitPriceCalculationWrapper> SalesUnitPriceCalculationWrappers { get; }

        public Project Project { get; }
        public Facility Facility { get; }
        public Product Product { get; }
        public int Amount { get; }
        public DateTime OrderInTakeDate { get; }
        public DateTime RealizationDate { get; }
        public PaymentConditionSet PaymentConditionSet { get; }

        public SalesUnitsPriceCalculationGroupSimple(IEnumerable<SalesUnitPriceCalculationWrapper> salesUnitWrappers)
        {
            SalesUnitPriceCalculationWrappers = salesUnitWrappers.ToList();
            var salesUnit = SalesUnitPriceCalculationWrappers.First().Model;

            Project = salesUnit.Project;
            Facility = salesUnit.Facility;
            Product = salesUnit.Product;
            Amount = SalesUnitPriceCalculationWrappers.Count;
            OrderInTakeDate = salesUnit.OrderInTakeDate;
            RealizationDate = salesUnit.RealizationDateCalculated;
            PaymentConditionSet = salesUnit.PaymentConditionSet;
        }
    }

    public class SalesUnitsPriceCalculationGroup : SalesUnitsPriceCalculationGroupSimple
    {
        public StructureCostsWrapper StructureCostsWrapper { get; }
        public IValidatableChangeTrackingCollection<StructureCostWrapper> StructureCostWrappers => StructureCostsWrapper.StructureCostsList;

        public SalesUnitsPriceCalculationGroup(IEnumerable<SalesUnitPriceCalculationWrapper> salesUnitWrappers) : base(salesUnitWrappers)
        {
            var salesUnit = SalesUnitPriceCalculationWrappers.First().Model;

            StructureCostsWrapper = salesUnit.StructureCosts == null 
                ? new StructureCostsWrapper(new StructureCosts()) 
                : new StructureCostsWrapper(salesUnit.StructureCosts);

            if (salesUnit.StructureCosts == null)
            {
                SalesUnitPriceCalculationWrappers.ForEach(x => x.StructureCosts = StructureCostsWrapper);

                //�������� ��������� ������������
                var structureCost = new StructureCost
                {
                    Comment = $"{Product}"
                };
                StructureCostsWrapper.StructureCostsList.Add(new StructureCostWrapper(structureCost));

                //�������� ������������� ���.������������
                foreach (var productIncluded in salesUnit.ProductsIncluded)
                {
                    var structureCostPrIncl = new StructureCost
                    {
                        Comment = $"{productIncluded.Product}",
                        Amount = (double)productIncluded.Amount/Amount
                    };
                    StructureCostsWrapper.StructureCostsList.Add(new StructureCostWrapper(structureCostPrIncl));
                }
            }
        }
    }

    public class SalesUnitPriceCalculationWrapper : WrapperBase<SalesUnit>
    {
        public SalesUnitPriceCalculationWrapper(SalesUnit model) : base(model) { }

        public StructureCostsWrapper StructureCosts
        {
            get { return GetWrapper<StructureCostsWrapper>(); }
            set { SetComplexValue<StructureCosts, StructureCostsWrapper>(StructureCosts, value); }
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(StructureCosts), Model.StructureCosts == null ? null : new StructureCostsWrapper(Model.StructureCosts));
        }
    }

    public class PriceCalculation2Wrapper : WrapperBase<PriceCalculation>
    {
        public PriceCalculation2Wrapper(PriceCalculation model) : base(model) { }

        #region SimpleProperties

        public DateTime? TaskOpenMoment
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? TaskOpenMomentOriginalValue => GetOriginalValue<DateTime?>(nameof(TaskOpenMoment));
        public bool TaskOpenMomentIsChanged => GetIsChanged(nameof(TaskOpenMoment));


        public DateTime? TaskCloseMoment
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? TaskCloseMomentOriginalValue => GetOriginalValue<DateTime?>(nameof(TaskCloseMoment));
        public bool TaskCloseMomentIsChanged => GetIsChanged(nameof(TaskCloseMoment));


        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));


        public bool IsNeedExcelFile
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool IsNeedExcelFileOriginalValue => GetOriginalValue<bool>(nameof(IsNeedExcelFile));
        public bool IsNeedExcelFileIsChanged => GetIsChanged(nameof(IsNeedExcelFile));

        #endregion

        #region ComplexProperties

        public UserWrapper Author
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Author, value); }
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<SalesUnitPriceCalculationWrapper> SalesUnits { get; private set; }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Author), Model.Author == null ? null : new UserWrapper(Model.Author));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitPriceCalculationWrapper>(Model.SalesUnits.Select(e => new SalesUnitPriceCalculationWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }

    }

    public class SalesUnitPriceCalculationComparer : IEqualityComparer<SalesUnitPriceCalculationWrapper>
    {
        public bool Equals(SalesUnitPriceCalculationWrapper x, SalesUnitPriceCalculationWrapper y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Model.Project.Id, y.Model.Project.Id)) return false;
            if (!Equals(x.Model.Facility.Id, y.Model.Facility.Id)) return false;
            if (!Equals(x.Model.Product.Id, y.Model.Product.Id)) return false;
            if (!Equals(x.Model.PaymentConditionSet.Id, y.Model.PaymentConditionSet.Id)) return false;
            if (!Equals(x.Model.OrderInTakeDate, y.Model.OrderInTakeDate)) return false;
            if (!Equals(x.Model.RealizationDateCalculated, y.Model.RealizationDateCalculated)) return false;
            if (!Equals(x.StructureCosts?.Id, y.StructureCosts?.Id)) return false;

            var productsInclX = x.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();
            var productsInclY = y.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();

            if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
            if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;


            return true;
        }

        public int GetHashCode(SalesUnitPriceCalculationWrapper salesUnit)
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
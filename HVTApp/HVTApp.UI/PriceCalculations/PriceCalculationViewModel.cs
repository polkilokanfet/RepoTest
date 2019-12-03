using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations
{
    public class PriceCalculationViewModel : ViewModelBaseCanExportToExcel
    {
        private object _selectedItem;
        private PriceCalculation2Wrapper _priceCalculationWrapper;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                ((DelegateCommand)AddStructureCostCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveStructureCostCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)FinishCommand).RaiseCanExecuteChanged();
            }
        }
        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsPricer=> GlobalAppProperties.User.RoleCurrent == Role.Pricer;

        public bool IsStarted => PriceCalculationWrapper?.TaskOpenMoment != null;
        public bool IsFinished => PriceCalculationWrapper?.TaskCloseMoment != null;

        public bool CanChangePrice => CurrentUserIsPricer && !IsFinished;

        public ICommand SaveCommand { get; }
        public ICommand AddStructureCostCommand { get; }
        public ICommand RemoveStructureCostCommand { get; }

        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public ICommand StartCommand { get; }
        public ICommand FinishCommand { get; }

        public ICommand CancelCommand { get; }

        public PriceCalculation2Wrapper PriceCalculationWrapper
        {
            get { return _priceCalculationWrapper; }
            private set
            {
                _priceCalculationWrapper = value;
                //реакция на изменения в задаче
                PriceCalculationWrapper.PropertyChanged += (sender, args) =>
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                };
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));
            }
        }

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
            #region SaveCommand
          
            //сохранение изменений
            SaveCommand = new DelegateCommand(
                () =>
                {
                    PriceCalculationWrapper.AcceptChanges();

                    var pc = UnitOfWork.Repository<PriceCalculation>().GetById(PriceCalculationWrapper.Model.Id);
                    if (pc == null)
                    {
                        UnitOfWork.Repository<PriceCalculation>().Add(PriceCalculationWrapper.Model);
                    }

                    UnitOfWork.SaveChanges();

                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () => PriceCalculationWrapper.IsValid && PriceCalculationWrapper.IsChanged);

            #endregion

            #region AddStructureCostCommand

            //добавление стракчакоста
            AddStructureCostCommand = new DelegateCommand(
                () =>
                {
                    var structureCost = new StructureCost { Comment = "No title" };
                    var structureCostWrapper = new StructureCostWrapper(structureCost);
                    (SelectedItem as PriceCalculationItem2Wrapper).StructureCosts.Add(structureCostWrapper);
                },
                () => SelectedItem is PriceCalculationItem2Wrapper && !IsStarted);

            #endregion

            #region RemoveStructureCostCommand

            //удаление стракчакоста
            RemoveStructureCostCommand = new DelegateCommand(
                () =>
                {
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить StructureCost?");
                    if (result != MessageDialogResult.Yes) return;

                    var structureCost = SelectedItem as StructureCostWrapper;
                    var calculationItem2Wrapper = PriceCalculationWrapper.PriceCalculationItems.Single(x => x.StructureCosts.Contains(structureCost));
                    calculationItem2Wrapper.StructureCosts.Remove(structureCost);
                },
                () => SelectedItem is StructureCostWrapper && !IsStarted);

            #endregion

            #region AddGroupCommand

            //добавление группы оборудования
            AddGroupCommand = new DelegateCommand(
                () =>
                {
                    //потенциальные группы
                    var items = UnitOfWork.Repository<SalesUnit>()
                            .Find(x => x.Project.Manager.IsAppCurrentUser())
                            .Except(PriceCalculationWrapper.PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Model))
                            .Select(x => new SalesUnit2Wrapper(x))
                            .GroupBy(x => x, new SalesUnit2Comparer())
                            .Select(x => GetPriceCalculationItem2Wrapper(x));

                    //выбор группы
                    var viewModel = new PriceCalculationItemsViewModel(items);
                    var dialogResult = Container.Resolve<IDialogService>().ShowDialog(viewModel);

                    //добавление группы
                    if (dialogResult.HasValue && dialogResult.Value)
                    {
                        viewModel.SelectedItemWrappers.ForEach(x => PriceCalculationWrapper.PriceCalculationItems.Add(x));
                    }
                },
                () =>  !IsStarted);

            #endregion

            #region RemoveGroupCommand

            //удаление группы
            RemoveGroupCommand = new DelegateCommand(
                () =>
                {
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить из расчета группу оборудования?");
                    if (result != MessageDialogResult.Yes) return;

                    var selectedGroup = SelectedItem as PriceCalculationItem2Wrapper;
                    PriceCalculationWrapper.PriceCalculationItems.Remove(selectedGroup);
                },
                () => SelectedItem is PriceCalculationItem2Wrapper && !IsStarted);

            #endregion

            #region StartCommand

            StartCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите стартовать задачу?");
                    if (dr != MessageDialogResult.Yes) return;

                    PriceCalculationWrapper.TaskOpenMoment = DateTime.Now;
                    SaveCommand.Execute(null);

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(PriceCalculationWrapper.Model);

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)AddStructureCostCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)RemoveStructureCostCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
                },
                () => !IsStarted && PriceCalculationWrapper.IsValid);

            #endregion

            #region FinishCommand

            FinishCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите завершить задачу?");
                    if (dr != MessageDialogResult.Yes) return;

                    PriceCalculationWrapper.TaskCloseMoment = DateTime.Now;
                    SaveCommand.Execute(null);

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(PriceCalculationWrapper.Model);

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));

                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () => !IsFinished && PriceCalculationWrapper.IsValid && PriceCalculationWrapper.PriceCalculationItems.SelectMany(x => x.StructureCosts).All(x => x.UnitPrice.HasValue));

            #endregion

            #region CancelCommand

            CancelCommand = new DelegateCommand(() =>
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите остановить задачу?");
                if (dr != MessageDialogResult.Yes) return;

                PriceCalculationWrapper.TaskOpenMoment = null;
                PriceCalculationWrapper.TaskCloseMoment = null;
                SaveCommand.Execute(null);

                Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(PriceCalculationWrapper.Model);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddStructureCostCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveStructureCostCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();

            },
            () => IsStarted);

            #endregion

            PriceCalculationWrapper = new PriceCalculation2Wrapper(new PriceCalculation());
        }

        public void Load(PriceCalculation priceCalculation)
        {
            var priceCalculationLoaded = UnitOfWork.Repository<PriceCalculation>().GetById(priceCalculation.Id);

            PriceCalculationWrapper = priceCalculationLoaded != null 
                ? new PriceCalculation2Wrapper(priceCalculationLoaded) 
                : new PriceCalculation2Wrapper(priceCalculation);
        }

        /// <summary>
        /// Загрузка при создании нового расчета по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitWrappers = salesUnits
                .Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.Id))
                .Select(x => new SalesUnit2Wrapper(x))
                .ToList();
            
            salesUnitWrappers.GroupBy(x => x, new SalesUnit2Comparer())
                             .ForEach(x => { PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(x)); });
        }

        private PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(IEnumerable<SalesUnit2Wrapper> salesUnits)
        {
            var priceCalculationItem2Wrapper = new PriceCalculationItem2Wrapper(new PriceCalculationItem());
            salesUnits.ForEach(x => priceCalculationItem2Wrapper.SalesUnits.Add(x));

            //создание основного стракчакоста
            var structureCost = new StructureCost
            {
                Comment = $"{priceCalculationItem2Wrapper.Product}",
                Amount = 1
            };
            priceCalculationItem2Wrapper.StructureCosts.Add(new StructureCostWrapper(structureCost));

            //создание стракчакостов доп.оборудования
            foreach (var productIncluded in priceCalculationItem2Wrapper.SalesUnits.First().Model.ProductsIncluded)
            {
                var structureCostPrIncl = new StructureCost
                {
                    Comment = $"{productIncluded.Product}",
                    Amount = (double)productIncluded.Amount / priceCalculationItem2Wrapper.SalesUnits.Count
                };
                priceCalculationItem2Wrapper.StructureCosts.Add(new StructureCostWrapper(structureCostPrIncl));
            }

            return priceCalculationItem2Wrapper;
        }
    }

    public class SalesUnit2Wrapper : WrapperBase<SalesUnit>
    {
        public SalesUnit2Wrapper(SalesUnit model) : base(model) { }
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

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<PriceCalculationItem2Wrapper> PriceCalculationItems { get; private set; }

        #endregion

        #region GetProperties

        public string Name => GetValue<string>();

        #endregion

        protected override void InitializeCollectionProperties()
        {
            if (Model.PriceCalculationItems == null) throw new ArgumentException("PriceCalculationItems cannot be null");
            PriceCalculationItems = new ValidatableChangeTrackingCollection<PriceCalculationItem2Wrapper>(Model.PriceCalculationItems.Select(e => new PriceCalculationItem2Wrapper(e)));
            RegisterCollection(PriceCalculationItems, Model.PriceCalculationItems);
        }
    }

    public class PriceCalculationItem2Wrapper : WrapperBase<Model.POCOs.PriceCalculationItem>
    {
        public Project Project => Model.SalesUnits.FirstOrDefault()?.Project;
        public Facility Facility => Model.SalesUnits.FirstOrDefault()?.Facility;
        public Product Product => Model.SalesUnits.FirstOrDefault()?.Product;
        public int Amount => Model.SalesUnits.Count;
        public DateTime? OrderInTakeDate => Model.SalesUnits.FirstOrDefault()?.OrderInTakeDate;
        public DateTime? RealizationDate => Model.SalesUnits.FirstOrDefault()?.RealizationDateCalculated;
        public PaymentConditionSet PaymentConditionSet => Model.SalesUnits.FirstOrDefault()?.PaymentConditionSet;

        public PriceCalculationItem2Wrapper(Model.POCOs.PriceCalculationItem model) : base(model)
        {
            this.SalesUnits.CollectionChanged += (sender, args) => { OnPropertyChanged(string.Empty); };
        }

        #region SimpleProperties

        public System.Guid PriceCalculationId
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid PriceCalculationIdOriginalValue => GetOriginalValue<System.Guid>(nameof(PriceCalculationId));
        public bool PriceCalculationIdIsChanged => GetIsChanged(nameof(PriceCalculationId));

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<SalesUnit2Wrapper> SalesUnits { get; private set; }

        public IValidatableChangeTrackingCollection<StructureCostWrapper> StructureCosts { get; private set; }

        #endregion

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnit2Wrapper>(Model.SalesUnits.Select(e => new SalesUnit2Wrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);

            if (Model.StructureCosts == null) throw new ArgumentException("StructureCosts cannot be null");
            StructureCosts = new ValidatableChangeTrackingCollection<StructureCostWrapper>(Model.StructureCosts.Select(e => new StructureCostWrapper(e)));
            RegisterCollection(StructureCosts, Model.StructureCosts);
        }

    }


    public class SalesUnit2Comparer : IEqualityComparer<SalesUnit2Wrapper>
    {
        public bool Equals(SalesUnit2Wrapper x, SalesUnit2Wrapper y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Model.Project.Id, y.Model.Project.Id)) return false;
            if (!Equals(x.Model.Facility.Id, y.Model.Facility.Id)) return false;
            if (!Equals(x.Model.Product.Id, y.Model.Product.Id)) return false;
            if (!Equals(x.Model.PaymentConditionSet.Id, y.Model.PaymentConditionSet.Id)) return false;
            if (!Equals(x.Model.OrderInTakeDate, y.Model.OrderInTakeDate)) return false;
            if (!Equals(x.Model.RealizationDateCalculated, y.Model.RealizationDateCalculated)) return false;

            var productsInclX = x.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();
            var productsInclY = y.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();

            if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
            if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;


            return true;
        }

        public int GetHashCode(SalesUnit2Wrapper salesUnit)
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
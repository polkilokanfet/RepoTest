using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1
{
    public class PriceCalculationViewModel : ViewModelBaseCanExportToExcel
    {
        private object _selectedItem;
        private PriceCalculation2Wrapper _priceCalculationWrapper;

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                AddStructureCostCommand.RaiseCanExecuteChanged();
                RemoveStructureCostCommand.RaiseCanExecuteChanged();
                RemoveGroupCommand.RaiseCanExecuteChanged();
                FinishCommand.RaiseCanExecuteChanged();
                DivideCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;
        public bool CurrentUserIsPricer => GlobalAppProperties.User.RoleCurrent == Role.Pricer;

        public bool StartVisibility => CurrentUserIsManager || CurrentUserIsBackManager;

        public bool IsStarted => PriceCalculationWrapper?.TaskOpenMoment != null;
        public bool IsFinished => PriceCalculationWrapper?.TaskCloseMoment != null;

        public bool CanChangePrice => CurrentUserIsPricer && !IsFinished;

        public bool CalculationHasFile => PriceCalculationWrapper != null && PriceCalculationWrapper.Files.Any();

        #region ICommand

        public SaveCommand SaveCommand { get; }
        public AddStructureCostCommand AddStructureCostCommand { get; }
        public RemoveStructureCostCommand RemoveStructureCostCommand { get; }

        public AddGroupCommand AddGroupCommand { get; }
        public RemoveGroupCommand RemoveGroupCommand { get; }

        public StartCommand StartCommand { get; }
        public FinishCommand FinishCommand { get; }

        public CancelCommand CancelCommand { get; }


        public MeregeCommand MeregeCommand { get; }
        public DivideCommand DivideCommand { get; }

        public LoadFileToDbCommand LoadFileToDbCommand { get; }
        public LoadFileFromDbCommand LoadFileFromDbCommand { get; }

        #endregion

        public PriceCalculation2Wrapper PriceCalculationWrapper
        {
            get => _priceCalculationWrapper;
            private set
            {
                _priceCalculationWrapper = value;
                //реакция на изменения в задаче
                PriceCalculationWrapper.PropertyChanged += (sender, args) =>
                {
                    SaveCommand.RaiseCanExecuteChanged();
                    StartCommand.RaiseCanExecuteChanged();
                    FinishCommand.RaiseCanExecuteChanged();
                    LoadFileFromDbCommand.RaiseCanExecuteChanged();
                };
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CalculationHasFile)));
            }
        }

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new SaveCommand(this, this.UnitOfWork, this.Container); //сохранение изменений
            AddStructureCostCommand = new AddStructureCostCommand(this); //добавление стракчакоста
            RemoveStructureCostCommand = new RemoveStructureCostCommand(this, this.Container, this.UnitOfWork); //удаление стракчакоста
            AddGroupCommand = new AddGroupCommand(this, this.Container, this.UnitOfWork); //добавление группы оборудования
            RemoveGroupCommand = new RemoveGroupCommand(this, this.Container, this.UnitOfWork); //удаление группы
            StartCommand = new StartCommand(this, this.Container);
            FinishCommand = new FinishCommand(this, this.Container);
            CancelCommand = new CancelCommand(this, this.Container);
            MeregeCommand = new MeregeCommand(this, this.Container, this.UnitOfWork);
            DivideCommand = new DivideCommand(this, this.Container);
            LoadFileToDbCommand = new LoadFileToDbCommand(this, this.Container);
            LoadFileFromDbCommand = new LoadFileFromDbCommand(this, this.Container);

            PriceCalculationWrapper = new PriceCalculation2Wrapper(new PriceCalculation());
        }

        /// <summary>
        /// Загрузка существующей калькуляции или создание новой
        /// </summary>
        /// <param name="priceCalculation"></param>
        public void Load(PriceCalculation priceCalculation)
        {
            var priceCalculationLoaded = UnitOfWork.Repository<PriceCalculation>().GetById(priceCalculation.Id);

            PriceCalculationWrapper = priceCalculationLoaded != null
                ? new PriceCalculation2Wrapper(priceCalculationLoaded)
                : new PriceCalculation2Wrapper(priceCalculation);
        }

        /// <summary>
        /// Создание копии калькуляции
        /// </summary>
        /// <param name="priceCalculation">Какой расчет скопировать</param>
        /// <param name="technicalRequrementsTask2">Из какой задачи</param>
        public void CreateCopy(PriceCalculation priceCalculation, TechnicalRequrementsTask technicalRequrementsTask2)
        {
            CreateCopy(priceCalculation);
            var technicalRequrementsTask = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask2.Id);
            technicalRequrementsTask.PriceCalculations.Add(PriceCalculationWrapper.Model);
        }

        /// <summary>
        /// Создание копии калькуляции
        /// </summary>
        /// <param name="priceCalculation2">Какой расчет скопировать</param>
        public void CreateCopy(PriceCalculation priceCalculation2)
        {
            var priceCalculation = UnitOfWork.Repository<PriceCalculation>().GetById(priceCalculation2.Id);

            PriceCalculationWrapper = new PriceCalculation2Wrapper(new PriceCalculation())
            {
                Initiator = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)),
                Comment = priceCalculation.Comment,
                IsNeedExcelFile = priceCalculation.IsNeedExcelFile
            };

            foreach (var calculationItem in priceCalculation.PriceCalculationItems)
            {
                var priceCalculationItem2Wrapper = new PriceCalculationItem2Wrapper(new PriceCalculationItem())
                {
                    PriceCalculationId = PriceCalculationWrapper.Model.Id,
                    OrderInTakeDate = calculationItem.OrderInTakeDate,
                    RealizationDate = calculationItem.RealizationDate,
                    PaymentConditionSet = calculationItem.PaymentConditionSet
                };

                foreach (var salesUnit in calculationItem.SalesUnits)
                {
                    priceCalculationItem2Wrapper.SalesUnits.Add(new SalesUnitEmptyWrapper(salesUnit));
                }

                foreach (var structureCost in calculationItem.StructureCosts)
                {
                    var structureCostWrapper = new StructureCostWrapper(new StructureCost())
                    {
                        Amount = structureCost.Amount,
                        Number = structureCost.Number,
                        Comment = structureCost.Comment,
                        PriceCalculationItemId = priceCalculationItem2Wrapper.Model.Id,
                        UnitPrice = structureCost.UnitPrice
                    };

                    priceCalculationItem2Wrapper.StructureCosts.Add(structureCostWrapper);
                }

                PriceCalculationWrapper.PriceCalculationItems.Add(priceCalculationItem2Wrapper);
            }
        }


        /// <summary>
        /// Загрузка при создании нового расчета по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitWrappers = salesUnits
                .Select(salesUnit => UnitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                .Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit))
                .ToList();
            
            salesUnitWrappers.GroupBy(x => x, new SalesUnit2Comparer())
                             .ForEach(x => { PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(x)); });

            //инициатор задачи
            if(this.PriceCalculationWrapper.Initiator == null)
                this.PriceCalculationWrapper.Initiator = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
        }

        public void Load(TechnicalRequrementsTask technicalRequrementsTask)
        {
            technicalRequrementsTask = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask.Id);
            if (!technicalRequrementsTask.PriceCalculations.ContainsById(PriceCalculationWrapper.Model))
            {
                technicalRequrementsTask.PriceCalculations.Add(this.PriceCalculationWrapper.Model);
            }

            foreach (var technicalRequrements in technicalRequrementsTask.Requrements)
            {
                var saleUnits = technicalRequrements.SalesUnits.Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit));
                PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(saleUnits));
            }

            //инициатор задачи
            if (this.PriceCalculationWrapper.Initiator == null)
                this.PriceCalculationWrapper.Initiator = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
        }

        public PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(IEnumerable<SalesUnitEmptyWrapper> salesUnits)
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
            foreach (var productIncluded in priceCalculationItem2Wrapper.SalesUnits.First().Model.ProductsIncluded.Where(x => !x.Product.ProductBlock.IsService))
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

        public void RefreshCommands()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            StartCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            AddStructureCostCommand.RaiseCanExecuteChanged();
            AddGroupCommand.RaiseCanExecuteChanged();
            RemoveStructureCostCommand.RaiseCanExecuteChanged();
            RemoveGroupCommand.RaiseCanExecuteChanged();
            MeregeCommand.RaiseCanExecuteChanged();
            DivideCommand.RaiseCanExecuteChanged();
        }

        public void CanChangePriceOnPropertyChanged()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));
        }

        public void CalculationHasFileOnPropertyChanged()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CalculationHasFile)));
        }
    }
}
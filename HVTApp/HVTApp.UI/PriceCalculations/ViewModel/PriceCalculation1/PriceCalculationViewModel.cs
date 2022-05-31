using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
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

        public bool StartVisibility
        {
            get
            {
                if (CurrentUserIsManager == false && CurrentUserIsBackManager == false) return false;
                return SccIsExpandable;
            }
        }

        public bool SccIsExpandable
        {
            get
            {
                if (this.PriceCalculationWrapper == null)
                    return false;

                if (this.PriceCalculationWrapper.Model.IsTceConnected && 
                    this.PriceCalculationWrapper.Model.LastHistoryItem?.Type == PriceCalculationHistoryItemType.Create)
                    return false;

                return true;
            }
        }

        public bool IsStarted => 
            PriceCalculationWrapper != null && 
            PriceCalculationWrapper.Model.History.Any() &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type != PriceCalculationHistoryItemType.Reject &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type != PriceCalculationHistoryItemType.Stop &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type != PriceCalculationHistoryItemType.Create;

        public bool IsFinished =>
            PriceCalculationWrapper != null &&
            PriceCalculationWrapper.Model.History.Any() &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type == PriceCalculationHistoryItemType.Finish;

        public bool IsRejected =>
            PriceCalculationWrapper != null &&
            PriceCalculationWrapper.Model.History.Any() &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type == PriceCalculationHistoryItemType.Reject;

        public bool CanChangePrice => CurrentUserIsPricer && !IsFinished;

        public bool CalculationHasFile => PriceCalculationWrapper != null && PriceCalculationWrapper.Files.Any();

        /// <summary>
        /// Элемент истории
        /// </summary>
        public PriceCalculationHistoryItemWrapper HistoryItem { get; private set; }

        //костыль для команд
        public IUnitOfWork UnitOfWork1 => this.UnitOfWork;

        #region ICommand

        public SaveCommand SaveCommand { get; }
        public AddStructureCostCommand AddStructureCostCommand { get; }
        public RemoveStructureCostCommand RemoveStructureCostCommand { get; }

        public AddGroupCommand AddGroupCommand { get; }
        public RemoveGroupCommand RemoveGroupCommand { get; }

        public StartCommand StartCommand { get; }
        public FinishCommand FinishCommand { get; }

        public CancelCommand CancelCommand { get; }
        public RejectCommand RejectCommand { get; }


        public MeregeCommand MeregeCommand { get; }
        public DivideCommand DivideCommand { get; }

        public LoadFileToDbCommand LoadFileToDbCommand { get; }
        public LoadFileFromDbCommand LoadFileFromDbCommand { get; }

        public DelegateLogCommand ChangePaymentsCommand { get; }

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
                PriceCalculationWrapper.History.CollectionChanged += (sender, args) =>
                {
                    RaisePropertyChanged(nameof(StartVisibility));
                    RaisePropertyChanged(nameof(SccIsExpandable));
                    this.ChangePaymentsCommand.RaiseCanExecuteChanged();
                };
                RaisePropertyChanged(nameof(IsStarted));
                RaisePropertyChanged(nameof(CanChangePrice));
                RaisePropertyChanged(nameof(CalculationHasFile));
                RaisePropertyChanged(nameof(StartVisibility));
                RaisePropertyChanged(nameof(SccIsExpandable));
                this.ChangePaymentsCommand.RaiseCanExecuteChanged();
            }
        }

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new SaveCommand(this, this.UnitOfWork, this.Container); //сохранение изменений
            AddStructureCostCommand = new AddStructureCostCommand(this); //добавление стракчакоста
            RemoveStructureCostCommand = new RemoveStructureCostCommand(this, this.Container); //удаление стракчакоста
            AddGroupCommand = new AddGroupCommand(this, this.Container, this.UnitOfWork); //добавление группы оборудования
            RemoveGroupCommand = new RemoveGroupCommand(this, this.Container); //удаление группы

            StartCommand = new StartCommand(this, this.Container);
            FinishCommand = new FinishCommand(this, this.Container);
            CancelCommand = new CancelCommand(this, this.Container);
            RejectCommand = new RejectCommand(this, this.Container);

            MeregeCommand = new MeregeCommand(this, this.Container);
            DivideCommand = new DivideCommand(this, this.Container);
            LoadFileToDbCommand = new LoadFileToDbCommand(this, this.Container);
            LoadFileFromDbCommand = new LoadFileFromDbCommand(this, this.Container);

            ChangePaymentsCommand = new DelegateLogCommand(
                () =>
                {
                    if (SelectedItem is PriceCalculationItem2Wrapper priceCalculationItem)
                    {
                        var paymentConditionSets = UnitOfWork.Repository<PaymentConditionSet>().GetAll();
                        var paymentConditionSet = container.Resolve<ISelectService>().SelectItem(paymentConditionSets);
                        if (paymentConditionSet != null && paymentConditionSet.Id !=
                            priceCalculationItem.PaymentConditionSet?.Model.Id)
                        {
                            priceCalculationItem.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(paymentConditionSet);
                        }
                    }
                },
                () => IsStarted == false && IsFinished == false);

            PriceCalculationWrapper = new PriceCalculation2Wrapper(new PriceCalculation());
            GenerateNewHistoryItem();
        }

        public void GenerateNewHistoryItem()
        {
            HistoryItem = new PriceCalculationHistoryItemWrapper(
                new PriceCalculationHistoryItem()
                {
                    User = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)
                });
            
            RaisePropertyChanged(nameof(HistoryItem));
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
                Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)),
                IsNeedExcelFile = priceCalculation.IsNeedExcelFile
            };

            foreach (var calculationItem in priceCalculation.PriceCalculationItems)
            {
                var priceCalculationItem2Wrapper = new PriceCalculationItem2Wrapper(new PriceCalculationItem())
                {
                    OrderInTakeDate = calculationItem.OrderInTakeDate,
                    RealizationDate = calculationItem.RealizationDate,
                    PaymentConditionSet = new PaymentConditionSetEmptyWrapper(calculationItem.PaymentConditionSet)
                };

                foreach (var salesUnit in calculationItem.SalesUnits)
                {
                    priceCalculationItem2Wrapper.SalesUnits.Add(new SalesUnitEmptyWrapper(salesUnit));
                }

                foreach (var structureCost in calculationItem.StructureCosts)
                {
                    var structureCostWrapper = new StructureCost2Wrapper(new StructureCost())
                    {
                        AmountNumerator = structureCost.AmountNumerator,
                        AmountDenomerator = structureCost.AmountDenomerator,
                        Number = structureCost.Number,
                        Comment = structureCost.Comment,
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
            
            salesUnitWrappers.GroupBy(salesUnitEmptyWrapper => salesUnitEmptyWrapper, new SalesUnit2Comparer())
                             .ForEach(x => { PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(x)); });

            //инициатор задачи
            if(this.PriceCalculationWrapper.Initiator == null)
                this.PriceCalculationWrapper.Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
        }

        public void Load(TechnicalRequrementsTask technicalRequrementsTask)
        {
            //Загружаем задачу ТСЕ
            technicalRequrementsTask = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask.Id);
            //добавляем расчет ПЗ в загруженную задачу, если он еще не добавлен
            if (!technicalRequrementsTask.PriceCalculations.ContainsById(PriceCalculationWrapper.Model))
            {
                technicalRequrementsTask.PriceCalculations.Add(this.PriceCalculationWrapper.Model);
            }

            //добавляем в расчет ПЗ оборудование
            foreach (var technicalRequrements in technicalRequrementsTask.Requrements.Where(technicalRequrements => technicalRequrements.IsActual))
            {
                var saleUnits = technicalRequrements.SalesUnits.Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit));
                PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(saleUnits, technicalRequrements.OrderInTakeDate.Value, technicalRequrements.RealizationDate.Value));
            }

            //инициатор задачи
            if (this.PriceCalculationWrapper.Initiator == null)
                this.PriceCalculationWrapper.Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));

            //необходимость файла excel
            this.PriceCalculationWrapper.IsNeedExcelFile = technicalRequrementsTask.ExcelFileIsRequired;
        }

        public void Load(PriceEngineeringTasks priceEngineeringTasks, bool isTceConnected)
        {
            //Загружаем задачу
            priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);

            //добавляем в расчет ПЗ оборудование
            foreach (var priceEngineeringTask in priceEngineeringTasks.ChildPriceEngineeringTasks)
            {
                PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(priceEngineeringTasks, priceEngineeringTask));
            }

            //добавляем расчет ПЗ в загруженную задачу
            priceEngineeringTasks.PriceCalculations.Add(this.PriceCalculationWrapper.Model);
            
            //инициатор задачи
            PriceCalculationWrapper.Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
            
            //необходимость файла excel
            this.PriceCalculationWrapper.IsNeedExcelFile = isTceConnected;

            //связь расчёта с ТСЕ
            this.PriceCalculationWrapper.Model.IsTceConnected = isTceConnected;

            if (priceEngineeringTasks.IsAccepted == false || 
                priceEngineeringTasks.ChildPriceEngineeringTasks.Any(x => x.HasSccInTce == false))
            {
                //добавдение статуса "Создано"
                HistoryItem.Type = PriceCalculationHistoryItemType.Create;
                PriceCalculationWrapper.History.Add(HistoryItem);
            }
        }


        public void RegenerateScc(PriceCalculation calculation)
        {
            this.Load(calculation);

            var priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(calculation.PriceEngineeringTasksId.Value);

            foreach (var priceCalculationItem in PriceCalculationWrapper.PriceCalculationItems)
            {
                //удаляем старые стракчакосты
                priceCalculationItem.StructureCosts.ForEach(x => UnitOfWork.Repository<StructureCost>().Delete(x.Model));
                priceCalculationItem.StructureCosts.Clear();

                //генерируем новые стракчакосты
                var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(priceCalculationItem.Model.PriceEngineeringTaskId.Value);
                var sccList = priceEngineeringTask.GetStructureCosts(priceEngineeringTasks.TceNumber);
                priceCalculationItem.StructureCosts.AddRange(sccList.Select(x => new StructureCost2Wrapper(x)));
            }
        }


        public PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(IEnumerable<SalesUnitEmptyWrapper> salesUnits, DateTime orderInTakeDate, DateTime realizationDate)
        {
            var result = this.GetPriceCalculationItem2Wrapper(salesUnits);
            result.OrderInTakeDate = orderInTakeDate;
            result.RealizationDate = realizationDate;
            return result;
        }

        public PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(IEnumerable<SalesUnitEmptyWrapper> salesUnits)
        {
            var priceCalculationItem2Wrapper = new PriceCalculationItem2Wrapper(new PriceCalculationItem());
            salesUnits.ForEach(salesUnitEmptyWrapper => priceCalculationItem2Wrapper.SalesUnits.Add(salesUnitEmptyWrapper));

            //создание основного стракчакоста
            var structureCost = new StructureCost
            {
                Comment = $"{priceCalculationItem2Wrapper.Product}",
                AmountNumerator = 1,
                AmountDenomerator = 1
            };
            priceCalculationItem2Wrapper.StructureCosts.Add(new StructureCost2Wrapper(structureCost));

            //создание стракчакостов доп.оборудования
            foreach (var productIncluded in priceCalculationItem2Wrapper.SalesUnits.First().Model.ProductsIncluded.Where(x => !x.Product.ProductBlock.IsService))
            {
                var structureCostPrIncl = new StructureCost
                {
                    Comment = $"{productIncluded.Product}",
                    AmountNumerator = (double)productIncluded.Amount / priceCalculationItem2Wrapper.SalesUnits.Count,
                    AmountDenomerator = 1
                };
                priceCalculationItem2Wrapper.StructureCosts.Add(new StructureCost2Wrapper(structureCostPrIncl));
            }

            return priceCalculationItem2Wrapper;
        }

        private PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(PriceEngineeringTasks priceEngineeringTasks, PriceEngineeringTask priceEngineeringTask)
        {
            var item = new PriceCalculationItem2Wrapper(new PriceCalculationItem());
            item.SalesUnits.AddRange(priceEngineeringTask.SalesUnits.Select(x => new SalesUnitEmptyWrapper(x)));
            item.OrderInTakeDate = priceEngineeringTask.SalesUnits.First().OrderInTakeDate;
            item.RealizationDate = priceEngineeringTask.SalesUnits.First().RealizationDateCalculated;
            item.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(priceEngineeringTask.SalesUnits.First().PaymentConditionSet);
            item.Model.PriceEngineeringTaskId = priceEngineeringTask.Id;

            foreach (var structureCost in priceEngineeringTask.GetStructureCosts(priceEngineeringTasks.TceNumber))
            {
                item.StructureCosts.Add(new StructureCost2Wrapper(structureCost));
            }

            return item;
        }


        public void RefreshCommands()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            StartCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            RejectCommand.RaiseCanExecuteChanged();
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
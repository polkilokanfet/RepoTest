using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class PriceCalculationViewModel : ViewModelBaseCanExportToExcel
    {
        private readonly IMessageService _messageService;
        private object _selectedItem;
        private PriceCalculation2Wrapper _priceCalculationWrapper;

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                (AddStructureCostCommand).RaiseCanExecuteChanged();
                (RemoveStructureCostCommand).RaiseCanExecuteChanged();
                (RemoveGroupCommand).RaiseCanExecuteChanged();
                (FinishCommand).RaiseCanExecuteChanged();
                (DivideCommand).RaiseCanExecuteChanged();
            }
        }
        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;
        public bool CurrentUserIsPricer => GlobalAppProperties.User.RoleCurrent == Role.Pricer;

        public bool StartVisibility => CurrentUserIsManager || CurrentUserIsBackManager;

        public bool IsStarted => PriceCalculationWrapper?.TaskOpenMoment != null;
        public bool IsFinished => PriceCalculationWrapper?.TaskCloseMoment != null;

        public bool CanChangePrice => CurrentUserIsPricer && !IsFinished;

        public bool CalculationHasFile
        {
            get
            {
                if (PriceCalculationWrapper != null)
                {
                    return PriceCalculationWrapper.Files.Any();
                }
                return false;
            }
        }

        #region ICommand

        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogCommand AddStructureCostCommand { get; }
        public DelegateLogCommand RemoveStructureCostCommand { get; }

        public DelegateLogCommand AddGroupCommand { get; }
        public DelegateLogCommand RemoveGroupCommand { get; }

        public DelegateLogCommand StartCommand { get; }
        public DelegateLogCommand FinishCommand { get; }

        public DelegateLogCommand CancelCommand { get; }


        public DelegateLogCommand MeregeCommand { get; }
        public DelegateLogCommand DivideCommand { get; }

        public DelegateLogCommand LoadFileToDbCommand { get; }
        public DelegateLogCommand LoadFileFromDbCommand { get; }

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
                    (SaveCommand).RaiseCanExecuteChanged();
                    (StartCommand).RaiseCanExecuteChanged();
                    (FinishCommand).RaiseCanExecuteChanged();
                    (LoadFileFromDbCommand).RaiseCanExecuteChanged();
                };
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CalculationHasFile)));
            }
        }

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            #region SaveCommand
          
            //сохранение изменений
            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    PriceCalculationWrapper.AcceptChanges();

                    var priceCalculation = UnitOfWork.Repository<PriceCalculation>().GetById(PriceCalculationWrapper.Model.Id);
                    if (priceCalculation == null)
                    {
                        UnitOfWork.Repository<PriceCalculation>().Add(PriceCalculationWrapper.Model);
                    }

                    UnitOfWork.SaveChanges();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(PriceCalculationWrapper.Model);

                    (SaveCommand).RaiseCanExecuteChanged();
                },
                () => PriceCalculationWrapper.IsValid && PriceCalculationWrapper.IsChanged);

            #endregion

            #region AddStructureCostCommand

            //добавление стракчакоста
            AddStructureCostCommand = new DelegateLogCommand(
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
            RemoveStructureCostCommand = new DelegateLogCommand(
                () =>
                {
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить StructureCost?", defaultNo:true);
                    if (result != MessageDialogResult.Yes) return;

                    var structureCost = SelectedItem as StructureCostWrapper;
                    var calculationItem2Wrapper = PriceCalculationWrapper.PriceCalculationItems.Single(x => x.StructureCosts.Contains(structureCost));
                    calculationItem2Wrapper.StructureCosts.Remove(structureCost);
                    if (UnitOfWork.Repository<StructureCost>().GetById(structureCost.Id) != null)
                        UnitOfWork.Repository<StructureCost>().Delete(structureCost.Model);
                },
                () => SelectedItem is StructureCostWrapper && !IsStarted);
//            () => SelectedItem is StructureCostWrapper && !IsStarted && ((StructureCostWrapper)SelectedItem).Model.UnitPrice == null);

            #endregion

            #region AddGroupCommand

            //добавление группы оборудования
            AddGroupCommand = new DelegateLogCommand(
                () =>
                {
                    //потенциальные группы
                    var items = UnitOfWork.Repository<SalesUnit>()
                            .Find(x => x.Project.Manager.IsAppCurrentUser())
                            .Except(PriceCalculationWrapper.PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Model))
                            .Select(x => new SalesUnitEmptyWrapper(x))
                            .GroupBy(x => x, new SalesUnit2Comparer())
                            .Select(GetPriceCalculationItem2Wrapper);

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
            RemoveGroupCommand = new DelegateLogCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить из расчета группу оборудования?", defaultNo:true);
                    if (result != MessageDialogResult.Yes) return;

                    var selectedGroup = SelectedItem as PriceCalculationItem2Wrapper;

                    var salesUnits = selectedGroup.SalesUnits.ToList();
                    
                    //единицы, которы нельзя удалить из расчета, т.к. они размещены в производстве
                    var salesUnitsNotForRemove = salesUnits
                        .Where(x => x.Model.SignalToStartProduction.HasValue)
                        .Where(x => x.Model.ActualPriceCalculationItem(UnitOfWork)?.Id == selectedGroup.Model.Id)
                        .ToList();

                    if (salesUnitsNotForRemove.Any())
                    {
                        _messageService.ShowOkMessageDialog("Удаление", "Вы не можете удалить некоторые строки, т.к. они размещены в производстве.");

                        var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                        salesUnitsToRemove.ForEach(x => selectedGroup.SalesUnits.Remove(x));
                        if(!selectedGroup.SalesUnits.Any())
                            PriceCalculationWrapper.PriceCalculationItems.Remove(selectedGroup);
                    }
                    else
                    {
                        PriceCalculationWrapper.PriceCalculationItems.Remove(selectedGroup);
                    }
                },
                () => SelectedItem is PriceCalculationItem2Wrapper && !IsStarted);

            #endregion

            #region StartCommand

            StartCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите стартовать задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    PriceCalculationWrapper.TaskOpenMoment = DateTime.Now;
                    SaveCommand.Execute();

                    container.Resolve<IEventAggregator>().GetEvent<AfterStartPriceCalculationEvent>().Publish(this.PriceCalculationWrapper.Model);

                    //уведомление по почте
                    //Container.Resolve<IEmailService>().SendMail("kos@uetm.ru", $"{GlobalAppProperties.User.Employee.Person} отправил новое задание на расчет", "test");

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                    (StartCommand).RaiseCanExecuteChanged();
                    (CancelCommand).RaiseCanExecuteChanged();
                    (AddStructureCostCommand).RaiseCanExecuteChanged();
                    (AddGroupCommand).RaiseCanExecuteChanged();
                    (RemoveStructureCostCommand).RaiseCanExecuteChanged();
                    (RemoveGroupCommand).RaiseCanExecuteChanged();
                    (MeregeCommand).RaiseCanExecuteChanged();
                    (DivideCommand).RaiseCanExecuteChanged();
                },
                () => !IsStarted && PriceCalculationWrapper.IsValid && GlobalAppProperties.User.Id == PriceCalculationWrapper.Initiator?.Id);

            #endregion

            #region FinishCommand

            FinishCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите завершить задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    PriceCalculationWrapper.TaskCloseMoment = DateTime.Now;
                    SaveCommand.Execute();

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));

                    (SaveCommand).RaiseCanExecuteChanged();
                    container.Resolve<IEventAggregator>().GetEvent<AfterFinishPriceCalculationEvent>().Publish(this.PriceCalculationWrapper.Model);
                },
                () =>
                {
                    if (PriceCalculationWrapper == null)
                    {
                        return false;
                    }

                    if (PriceCalculationWrapper.IsNeedExcelFile && !CalculationHasFile)
                    {
                        return false;
                    }

                    return IsStarted &&
                           !IsFinished && 
                           PriceCalculationWrapper.IsValid && 
                           PriceCalculationWrapper.PriceCalculationItems.SelectMany(item => item.StructureCosts).All(structureCost => structureCost.UnitPrice.HasValue);
                });

            #endregion

            #region CancelCommand

            CancelCommand = new DelegateLogCommand(() =>
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите остановить задачу?", defaultNo:true);
                if (dr != MessageDialogResult.Yes) return;

                PriceCalculationWrapper.TaskOpenMoment = null;
                PriceCalculationWrapper.TaskCloseMoment = null;
                SaveCommand.Execute();

                container.Resolve<IEventAggregator>().GetEvent<AfterCancelPriceCalculationEvent>().Publish(this.PriceCalculationWrapper.Model);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                (StartCommand).RaiseCanExecuteChanged();
                (CancelCommand).RaiseCanExecuteChanged();
                (AddStructureCostCommand).RaiseCanExecuteChanged();
                (AddGroupCommand).RaiseCanExecuteChanged();
                (RemoveStructureCostCommand).RaiseCanExecuteChanged();
                (RemoveGroupCommand).RaiseCanExecuteChanged();
                (MeregeCommand).RaiseCanExecuteChanged();
                (DivideCommand).RaiseCanExecuteChanged();
            },
            () => IsStarted && GlobalAppProperties.User.Id == PriceCalculationWrapper.Initiator?.Id);

            #endregion

            #region MergeCommand

            MeregeCommand = new DelegateLogCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("Слияние", "Действительно хотите слить строки, выделенные галкой?", defaultYes:true);
                    if (result != MessageDialogResult.Yes) return;

                    //айтемы для слияния
                    var items = PriceCalculationWrapper.PriceCalculationItems.Where(x => x.IsChecked).ToList();

                    if (items.Select(x => x.Facility.Id).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными Объектами поставки.");
                        return;
                    }

                    if (items.Select(x => x.Product.Id).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными Продуктами поставки.");
                        return;
                    }

                    var itemToSave = items.First();
                    items.Remove(itemToSave);

                    foreach (var item in items)
                    {
                        item.SalesUnits.ForEach(x => itemToSave.SalesUnits.Add(x));
                        PriceCalculationWrapper.PriceCalculationItems.Remove(item);
                        if (UnitOfWork.Repository<PriceCalculationItem>().GetById(item.Model.Id) != null)
                            UnitOfWork.Repository<PriceCalculationItem>().Delete(item.Model);
                    }
                },
                () =>
                {
                    return !IsStarted && PriceCalculationWrapper.PriceCalculationItems.Count(x => x.IsChecked) > 1;
                });

            #endregion

            #region DivideCommand

            DivideCommand = new DelegateLogCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("Разбиение", "Действительно хотите разбить выбранную строку?", defaultNo:true);
                    if (result != MessageDialogResult.Yes) return;

                    var selectedItem = (PriceCalculationItem2Wrapper) SelectedItem;
                    var salesUnit = selectedItem.SalesUnits.First();

                    var salesUnitsToDivide = selectedItem.SalesUnits.ToList();
                    salesUnitsToDivide.Remove(salesUnit);

                    foreach (var unit in salesUnitsToDivide)
                    {
                        selectedItem.SalesUnits.Remove(unit);

                        var priceCalculationItem = new PriceCalculationItem
                        {
                            OrderInTakeDate = selectedItem.OrderInTakeDate,
                            RealizationDate = selectedItem.RealizationDate,
                            PaymentConditionSet = selectedItem.PaymentConditionSet
                        };
                        var priceCalculationItemWrapper = new PriceCalculationItem2Wrapper(priceCalculationItem);
                        priceCalculationItemWrapper.SalesUnits.Add(unit);
                        foreach (var structureCost in selectedItem.StructureCosts)
                        {
                            var sc = new StructureCost
                            {
                                Comment = structureCost.Comment,
                                Amount = structureCost.Amount,
                                Number = structureCost.Number, 
                                UnitPrice = structureCost.UnitPrice
                            };
                            priceCalculationItemWrapper.StructureCosts.Add(new StructureCostWrapper(sc));
                        }

                        PriceCalculationWrapper.PriceCalculationItems.Add(priceCalculationItemWrapper);
                    }

                },
                () => !IsStarted && SelectedItem is PriceCalculationItem2Wrapper && ((PriceCalculationItem2Wrapper)SelectedItem).Amount > 1);

            #endregion

            #region LoadFileCommand

            LoadFileToDbCommand = new DelegateLogCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var rootDirectoryPath = GlobalAppProperties.Actual.PriceCalculationsFilesPath;

                        //копируем каждый файл
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            var fileWrapper = new PriceCalculationFileWrapper(new PriceCalculationFile());
                            try
                            {
                                File.Copy(fileName, $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(fileName)}");
                                PriceCalculationWrapper.Files.Add(fileWrapper);
                            }
                            catch (Exception e)
                            {
                                _messageService.ShowOkMessageDialog("Exception", e.GetAllExceptions());
                            }
                        }
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(CalculationHasFile)));

                        //костыль
                        if ((SaveCommand).CanExecute())
                        {
                            (SaveCommand).Execute();
                        }
                    }
                },
                () => CurrentUserIsPricer);

            LoadFileFromDbCommand = new DelegateLogCommand(
                () =>
                {
                    var file = PriceCalculationWrapper.Files.First().Model;
                    if (PriceCalculationWrapper.Files.Count > 1)
                    {
                        var selectService = Container.Resolve<ISelectService>();
                        file = selectService.SelectItem(PriceCalculationWrapper.Files.Select(x => x.Model));
                        if (file == null)
                            return;
                    }

                    var storageDirectory = GlobalAppProperties.Actual.PriceCalculationsFilesPath;
                    string addToFileName = $"{file.CreationMoment.ToShortDateString()} {file.CreationMoment.ToShortTimeString()}";
                    FilesStorage.CopyFileFromStorage(file.Id, _messageService, storageDirectory, addToFileName: addToFileName.ReplaceUncorrectSimbols("-"));

                },
                () => CalculationHasFile);

            #endregion

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

        private PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(IEnumerable<SalesUnitEmptyWrapper> salesUnits)
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
    }
}
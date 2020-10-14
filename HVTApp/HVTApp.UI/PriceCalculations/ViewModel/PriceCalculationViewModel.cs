using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class PriceCalculationViewModel : ViewModelBaseCanExportToExcel
    {
        private TechnicalRequrementsTask _technicalRequrementsTask = null;
        private readonly IMessageService _messageService;
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
                ((DelegateCommand)DivideCommand).RaiseCanExecuteChanged();
            }
        }
        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsPricer => GlobalAppProperties.User.RoleCurrent == Role.Pricer;

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


        public ICommand MeregeCommand { get; }
        public ICommand DivideCommand { get; }

        public ICommand LoadFileToDbCommand { get; }

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
                    ((DelegateCommand)LoadFileToDbCommand).RaiseCanExecuteChanged();
                };
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));
            }
        }

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            #region SaveCommand
          
            //сохранение изменений
            SaveCommand = new DelegateCommand(
                () =>
                {
                    PriceCalculationWrapper.AcceptChanges();

                    var priceCalculation = UnitOfWork.Repository<PriceCalculation>().GetById(PriceCalculationWrapper.Model.Id);
                    if (priceCalculation == null)
                    {
                        UnitOfWork.Repository<PriceCalculation>().Add(PriceCalculationWrapper.Model);
                    }

                    _technicalRequrementsTask?.PriceCalculations.Add(priceCalculation);

                    UnitOfWork.SaveChanges();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationSyncEvent>().Publish(PriceCalculationWrapper.Model);

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
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить StructureCost?", defaultNo:true);
                    if (result != MessageDialogResult.Yes) return;

                    var structureCost = SelectedItem as StructureCostWrapper;
                    var calculationItem2Wrapper = PriceCalculationWrapper.PriceCalculationItems.Single(x => x.StructureCosts.Contains(structureCost));
                    calculationItem2Wrapper.StructureCosts.Remove(structureCost);
                    if (UnitOfWork.Repository<StructureCost>().GetById(structureCost.Id) != null)
                        UnitOfWork.Repository<StructureCost>().Delete(structureCost.Model);
                },
                () => SelectedItem is StructureCostWrapper && !IsStarted && ((StructureCostWrapper)SelectedItem).Model.UnitPrice == null);

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
            RemoveGroupCommand = new DelegateCommand(
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

            StartCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите стартовать задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    PriceCalculationWrapper.TaskOpenMoment = DateTime.Now;
                    SaveCommand.Execute(null);

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(PriceCalculationWrapper.Model);

                    //уведомление по почте
                    //Container.Resolve<IEmailService>().SendMail("kos@uetm.ru", $"{GlobalAppProperties.User.Employee.Person} отправил новое задание на расчет", "test");

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
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите завершить задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    PriceCalculationWrapper.TaskCloseMoment = DateTime.Now;
                    SaveCommand.Execute(null);

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(PriceCalculationWrapper.Model);

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));

                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)LoadFileToDbCommand).RaiseCanExecuteChanged();
                },
                () => !IsFinished && PriceCalculationWrapper.IsValid && PriceCalculationWrapper.PriceCalculationItems.SelectMany(x => x.StructureCosts).All(x => x.UnitPrice.HasValue));

            #endregion

            #region CancelCommand

            CancelCommand = new DelegateCommand(() =>
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите остановить задачу?", defaultNo:true);
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

            #region MergeCommand

            MeregeCommand = new DelegateCommand(
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

            DivideCommand = new DelegateCommand(
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
                            var sc = new StructureCost()
                            {
                                Comment = structureCost.Comment,
                                Amount = structureCost.Amount,
                                Number = structureCost.Number
                            };
                            priceCalculationItemWrapper.StructureCosts.Add(new StructureCostWrapper(sc));
                        }

                        PriceCalculationWrapper.PriceCalculationItems.Add(priceCalculationItemWrapper);
                    }

                },
                () => !IsStarted && SelectedItem is PriceCalculationItem2Wrapper && ((PriceCalculationItem2Wrapper)SelectedItem).Amount > 1);

            #endregion

            #region LoadFileToDbCommand

            LoadFileToDbCommand = new DelegateCommand(
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
                                ((PriceCalculation2Wrapper)SelectedItem).File = fileWrapper;
                            }
                            catch (Exception e)
                            {
                                _messageService.ShowOkMessageDialog("Exception", e.GetAllExceptions());
                            }
                        }
                    }
                },
                () => this.PriceCalculationWrapper.TaskCloseMoment != null);

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
                .Select(x => new SalesUnitEmptyWrapper(x))
                .ToList();
            
            salesUnitWrappers.GroupBy(x => x, new SalesUnit2Comparer())
                             .ForEach(x => { PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(x)); });
        }

        public void Load(TechnicalRequrementsTask technicalRequrementsTask)
        {
            _technicalRequrementsTask = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask.Id);
            foreach (var requrement in _technicalRequrementsTask.Requrements)
            {
                var saleUnits = requrement.SalesUnits.Select(x => new SalesUnitEmptyWrapper(x));
                PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(saleUnits));
            }
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
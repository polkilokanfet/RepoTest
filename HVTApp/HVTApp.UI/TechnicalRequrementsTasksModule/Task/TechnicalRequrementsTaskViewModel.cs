using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class SaveCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public SaveCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();

            var trt = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(ViewModel.TechnicalRequrementsTaskWrapper.Model.Id);
            if (trt == null)
            {
                UnitOfWork.Repository<TechnicalRequrementsTask>().Add(ViewModel.TechnicalRequrementsTaskWrapper.Model);
            }

            UnitOfWork.SaveChanges();

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            this.RaiseCanExecuteChanged();
        }
    }

    public class TechnicalRequrementsTaskViewModel : ViewModelBaseCanExportToExcel
    {
        private TechnicalRequrementsTask2Wrapper _technicalRequrementsTaskWrapper;

        private object _selectedItem;
        private readonly IMessageService _messageService;
        private readonly List<TechnicalRequrementsFile> _removedFiles = new List<TechnicalRequrementsFile>();
        private AnswerFileTceWrapper _selectedAnswerFile;
        private PriceCalculation _selectedCalculation;

        //костыль
        public IUnitOfWork UnitOfWork1 => this.UnitOfWork;

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                AddNewFileCommand.RaiseCanExecuteChanged();
                AddOldFileCommand.RaiseCanExecuteChanged();
                RemoveFileCommand.RaiseCanExecuteChanged();
                RemoveGroupCommand.RaiseCanExecuteChanged();
                DivideCommand.RaiseCanExecuteChanged();
                LoadFileCommand.RaiseCanExecuteChanged();
            }
        }

        public AnswerFileTceWrapper SelectedAnswerFile
        {
            get => _selectedAnswerFile;
            set
            {
                _selectedAnswerFile = value;
                RemoveFileAnswerCommand.RaiseCanExecuteChanged();
                LoadFileAnswerCommand.RaiseCanExecuteChanged();
            }
        }

        public PriceCalculation SelectedCalculation
        {
            get => _selectedCalculation;
            set
            {
                _selectedCalculation = value;
                OpenPriceCalculationCommand.RaiseCanExecuteChanged();
                CopyPriceCalculationCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss;


        public bool IsStarted => TechnicalRequrementsTaskWrapper?.Start != null;

        public bool WasStarted => TechnicalRequrementsTaskWrapper?.Model.FirstStartMoment != null;

        public bool IsRejected => TechnicalRequrementsTaskWrapper?.RejectByBackManagerMoment != null;

        public bool AllowInstruct => CurrentUserIsBackManagerBoss && IsStarted && TechnicalRequrementsTaskWrapper.BackManager == null;

        public string ValidationResult => TechnicalRequrementsTaskWrapper?.ValidationResult;

        #region Commands

        public DelegateLogCommand SaveCommand { get; }

        /// <summary>
        /// Добавление нового файла
        /// </summary>
        public DelegateLogCommand AddNewFileCommand { get; }

        /// <summary>
        /// Добавление нового приложения к ответу ОГК
        /// </summary>
        public DelegateLogCommand AddNewFileAnswersCommand { get; }

        /// <summary>
        /// Добавление существующего файла
        /// </summary>
        public DelegateLogCommand AddOldFileCommand { get; }
        public DelegateLogCommand RemoveFileCommand { get; }
        public DelegateLogCommand RemoveFileAnswerCommand { get; }

        public DelegateLogCommand AddGroupCommand { get; }
        public DelegateLogCommand RemoveGroupCommand { get; }

        public DelegateLogCommand StartCommand { get; }

        public DelegateLogCommand EditCommand { get; }

        public DelegateLogCommand CancelCommand { get; }

        /// <summary>
        /// Разбить строку
        /// </summary>
        public DelegateLogCommand MeregeCommand { get; }

        /// <summary>
        /// Слить строки
        /// </summary>
        public DelegateLogCommand DivideCommand { get; }

        public DelegateLogCommand LoadFileCommand { get; }
        public DelegateLogCommand LoadAllFilesCommand { get; }

        public DelegateLogCommand LoadFileAnswerCommand { get; }
        public DelegateLogCommand LoadAllFileAnswersCommand { get; }

        public DelegateLogCommand CreatePriceCalculationCommand { get; }

        /// <summary>
        /// Отклонить задачу
        /// </summary>
        public DelegateLogCommand RejectCommand { get; }

        /// <summary>
        /// Создать копию расчета ПЗ
        /// </summary>
        public DelegateLogCommand CopyPriceCalculationCommand { get; }

        public DelegateLogCommand OpenPriceCalculationCommand { get; }

        public DelegateLogCommand OpenAnswerCommand { get; }

        public DelegateLogCommand OpenFileCommand { get; }

        public DelegateLogCommand InstructCommand { get; }

        #endregion

        public TechnicalRequrementsTask2Wrapper TechnicalRequrementsTaskWrapper
        {
            get => _technicalRequrementsTaskWrapper;
            private set
            {
                _technicalRequrementsTaskWrapper = value;
                //реакция на изменения в задаче
                _technicalRequrementsTaskWrapper.PropertyChanged += (sender, args) =>
                {
                    SaveCommand.RaiseCanExecuteChanged();
                    StartCommand.RaiseCanExecuteChanged();
                    RejectCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(nameof(ValidationResult));
                };
                RaisePropertyChanged();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            }
        }

        public TechnicalRequrementsTaskViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            #region SaveCommand
          
            //сохранение изменений
            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    TechnicalRequrementsTaskHistoryElement element = new TechnicalRequrementsTaskHistoryElement();
                    TechnicalRequrementsTaskWrapper.
                    TechnicalRequrementsTaskWrapper.AcceptChanges();

                    var trt = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(TechnicalRequrementsTaskWrapper.Model.Id);
                    if (trt == null)
                    {
                        UnitOfWork.Repository<TechnicalRequrementsTask>().Add(TechnicalRequrementsTaskWrapper.Model);
                    }

                    UnitOfWork.SaveChanges();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                    SaveCommand.RaiseCanExecuteChanged();
                },
                () => TechnicalRequrementsTaskWrapper != null && 
                      TechnicalRequrementsTaskWrapper.IsValid && 
                      TechnicalRequrementsTaskWrapper.IsChanged &&
                      !WasStarted);

            #endregion

            #region AddFileCommand

            AddNewFileCommand = new DelegateLogCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var rootDirectoryPath = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;

                        //копируем каждый файл
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            var fileWrapper = new TechnicalRequrementsFileWrapper(new TechnicalRequrementsFile());
                            try
                            {
                                File.Copy(fileName, $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(fileName)}");
                                fileWrapper.Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50);
                                ((TechnicalRequrements2Wrapper)SelectedItem).Files.Add(fileWrapper);
                            }
                            catch (Exception e)
                            {
                                _messageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                            }
                        }
                    }
                },
                () => !IsStarted && SelectedItem is TechnicalRequrements2Wrapper);

            AddOldFileCommand = new DelegateLogCommand(
                () =>
                {
                    var selectedRequirements = (TechnicalRequrements2Wrapper)SelectedItem;

                    var files = TechnicalRequrementsTaskWrapper.Requrements
                        .Where(x => x.Model.Id != selectedRequirements.Model.Id)
                        .SelectMany(x => x.Files)
                        .Select(x => x.Model)
                        .Distinct();

                    var selectService = Container.Resolve<ISelectService>();
                    var file = selectService.SelectItem(files);

                    //добавляем файл в выбранные требования
                    if (file != null)
                    {
                        var fileWrapper = TechnicalRequrementsTaskWrapper.Requrements
                            .SelectMany(x => x.Files)
                            .Distinct()
                            .Single(x => x.Id == file.Id);

                        selectedRequirements.Files.Add(fileWrapper);
                    }
                },
                () => !IsStarted && SelectedItem is TechnicalRequrements2Wrapper);

            #endregion

            #region RemoveFileCommand

            //удаление файла
            RemoveFileCommand = new DelegateLogCommand(
                () =>
                {
                    //диалог
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить файл?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    //поиск конкретной задачи
                    var file =  (TechnicalRequrementsFileWrapper)SelectedItem;
                    var technicalRequrements2Wrapper = TechnicalRequrementsTaskWrapper.Requrements.Single(x => x.Files.Contains(file));

                    //удаление
                    technicalRequrements2Wrapper.Files.Remove(file);
                    if (UnitOfWork.Repository<TechnicalRequrementsFile>().GetById(file.Id) != null)
                    {
                        //если используется только в одном месте
                        if (TechnicalRequrementsTaskWrapper.Requrements.Count(x => x.Files.Select(f => f.Model).ContainsById(file.Model)) == 1)
                        {
                            UnitOfWork.Repository<TechnicalRequrementsFile>().Delete(file.Model);
                        }
                    }
                    _removedFiles.Add(file.Model);
                    SelectedItem = null;
                },
                () => !WasStarted && !IsStarted && SelectedItem is TechnicalRequrementsFileWrapper);

            #endregion

            #region AddGroupCommand

            //добавление группы оборудования
            AddGroupCommand = new DelegateLogCommand(
                () =>
                {
                    _messageService.ShowYesNoMessageDialog("Информация", "Пока эта функция не работает. Она реально тут нужна?");
                    ////потенциальные группы
                    //var items = UnitOfWork.Repository<SalesUnit>()
                    //        .Find(x => x.Project.Manager.IsAppCurrentUser())
                    //        .Except(TechnicalRequrementsTaskWrapper.Requrements.SelectMany(x => x.SalesUnits).Select(x => x.Model))
                    //        .Select(x => new SalesUnitEmptyWrapper(x))
                    //        .GroupBy(x => x, new SalesUnit2Comparer())
                    //        .Select(GetPriceCalculationItem2Wrapper);

                    ////выбор группы
                    //var viewModel = new PriceCalculationItemsViewModel(items);
                    //var dialogResult = Container.Resolve<IDialogService>().ShowDialog(viewModel);

                    ////добавление группы
                    //if (dialogResult.HasValue && dialogResult.Value)
                    //{
                    //    viewModel.SelectedItemWrappers.ForEach(x => TechnicalRequrementsTaskWrapper.PriceCalculationItems.Add(x));
                    //}
                },
                () => !WasStarted && !IsStarted);

            #endregion

            #region RemoveGroupCommand

            //удаление группы
            RemoveGroupCommand = new DelegateLogCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить из задачи это оборудование?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    var selectedGroup = SelectedItem as TechnicalRequrements2Wrapper;

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
                        if (!selectedGroup.SalesUnits.Any())
                            TechnicalRequrementsTaskWrapper.Requrements.Remove(selectedGroup);
                    }
                    else
                    {
                        TechnicalRequrementsTaskWrapper.Requrements.Remove(selectedGroup);
                    }
                },
                () => !WasStarted && !IsStarted && SelectedItem is TechnicalRequrements2Wrapper);

            #endregion

            #region StartCommand

            StartCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите стартовать задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    TechnicalRequrementsTaskWrapper.Start = DateTime.Now;
                    if (!TechnicalRequrementsTaskWrapper.Model.FirstStartMoment.HasValue)
                    {
                        TechnicalRequrementsTaskWrapper.Model.FirstStartMoment = TechnicalRequrementsTaskWrapper.Start;
                    }
                    SaveCommand.Execute();

                    //уведомление по почте
                    //Container.Resolve<IEmailService>().SendMail("kos@uetm.ru", $"{GlobalAppProperties.User.Employee.Person} отправил новое задание на расчет", "test");

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                    RaiseCanExecuteChange();

                    container.Resolve<IEventAggregator>().GetEvent<AfterStartTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);
                },
                () =>
                {
                    if (TechnicalRequrementsTaskWrapper != null)
                    {
                        //если уже стартовано
                        if (IsStarted)
                            return false;

                        //для рестарта
                        if (TechnicalRequrementsTaskWrapper.Model.FirstStartMoment.HasValue)
                            return TechnicalRequrementsTaskWrapper.IsValid && TechnicalRequrementsTaskWrapper.IsChanged;

                        //для старта
                        return TechnicalRequrementsTaskWrapper.IsValid;
                    }
                    return false;
                });

            EditCommand = new DelegateLogCommand(
                () =>
                {
                    TechnicalRequrementsTaskWrapper.Model.Start = null;
                    TechnicalRequrementsTaskWrapper.Model.RejectByBackManagerMoment = null;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                    RaiseCanExecuteChange();
                },
                () => CurrentUserIsManager && IsStarted);

            #endregion

            #region CancelCommand

            CancelCommand = new DelegateLogCommand(() =>
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите остановить задачу?", defaultNo:true);
                if (dr != MessageDialogResult.Yes) return;

                TechnicalRequrementsTaskWrapper.Start = null;
                SaveCommand.Execute();

                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                RaiseCanExecuteChange();

                container.Resolve<IEventAggregator>().GetEvent<AfterCancelTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);
            },
            () => IsStarted);

            #endregion

            #region MergeCommand

            MeregeCommand = new DelegateLogCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("Слияние", "Действительно хотите слить строки, выделенные галкой?", defaultYes: true);
                    if (result != MessageDialogResult.Yes) return;

                    //айтемы для слияния
                    var items = TechnicalRequrementsTaskWrapper.Requrements.Where(x => x.IsChecked).ToList();

                    if (items.Select(x => x.SalesUnit.Facility.Id).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными Объектами поставки.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.Product.Id).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными Продуктами поставки.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.OrderInTakeDate).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными датами ОИТ.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.RealizationDateCalculated).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными датами реализации.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.Producer).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными производителями.");
                        return;
                    }

                    var itemToSave = items.First();
                    items.Remove(itemToSave);

                    foreach (var item in items)
                    {
                        item.SalesUnits.ForEach(x => itemToSave.SalesUnits.Add(x));
                        TechnicalRequrementsTaskWrapper.Requrements.Remove(item);
                        if (UnitOfWork.Repository<TechnicalRequrements>().GetById(item.Model.Id) != null)
                            UnitOfWork.Repository<TechnicalRequrements>().Delete(item.Model);
                    }
                },
                () =>
                {
                    return !IsStarted && TechnicalRequrementsTaskWrapper.Requrements.Count(x => x.IsChecked) > 1;
                });

            #endregion

            #region DivideCommand

            DivideCommand = new DelegateLogCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("Разбиение", "Действительно хотите разбить выбранную строку?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    var technicalRequrementsWrapper = (TechnicalRequrements2Wrapper)SelectedItem;
                    var salesUnit = technicalRequrementsWrapper.SalesUnits.First();

                    var salesUnitsToDivide = technicalRequrementsWrapper.SalesUnits.ToList();
                    salesUnitsToDivide.Remove(salesUnit);

                    //создаем новые строки
                    foreach (var unit in salesUnitsToDivide)
                    {
                        technicalRequrementsWrapper.SalesUnits.Remove(unit);

                        //создаем новую строку
                        var newTechnicalRequrements = new TechnicalRequrements
                        {
                            SalesUnits = new List<SalesUnit> { unit.Model },
                            Comment = technicalRequrementsWrapper.Comment
                        };
                        var newTechnicalRequrementsWrapper = new TechnicalRequrements2Wrapper(newTechnicalRequrements);

                        //добавляем в новую строку файлы
                        foreach (var fileWrapper in technicalRequrementsWrapper.Files)
                        {
                            newTechnicalRequrementsWrapper.Files.Add(fileWrapper);
                        }

                        TechnicalRequrementsTaskWrapper.Requrements.Add(newTechnicalRequrementsWrapper);
                    }

                },
                () => !IsStarted && SelectedItem is TechnicalRequrements2Wrapper && ((TechnicalRequrements2Wrapper)SelectedItem).Amount > 1);

            #endregion

            #region LoadFileCommand

            LoadFileCommand = new DelegateLogCommand(
                () =>
                {
                    var fileWrapper = (TechnicalRequrementsFileWrapper) SelectedItem;
                    var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
                    string addToFileName = $"{fileWrapper.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                    FilesStorage.CopyFileFromStorage(fileWrapper.Id, _messageService, storageDirectory, addToFileName: addToFileName);
                },
                () => SelectedItem is TechnicalRequrementsFileWrapper);

            LoadAllFilesCommand = new DelegateLogCommand(
                () =>
                {
                    using (var fdb = new FolderBrowserDialog())
                    {
                        var result = fdb.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                        {
                            var taskPath = fdb.SelectedPath;
                            foreach (var requrement in this.TechnicalRequrementsTaskWrapper.Requrements.Where(x => x.IsActual.HasValue && x.IsActual.Value))
                            {
                                var reqDirName = $"{requrement.Model.Id} {requrement.SalesUnit.Product.Designation.ReplaceUncorrectSimbols().LimitLengh()} ({requrement.Amount} шт.)";
                                var dirPath = Path.Combine(taskPath, reqDirName);
                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }

                                foreach (var file in requrement.Files.Where(technicalRequrementsFileWrapper => technicalRequrementsFileWrapper.IsActual))
                                {
                                    var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
                                    string addToFileName = $"{file.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                                    FilesStorage.CopyFileFromStorage(file.Id, _messageService, storageDirectory, dirPath, addToFileName, false);
                                }
                            }

                            Process.Start("explorer.exe", taskPath);                            
                        }
                    }
                });

            #endregion

            #region CreatePriceCalculationCommand

            CreatePriceCalculationCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters
                    {
                        { nameof(TechnicalRequrementsTask), this.TechnicalRequrementsTaskWrapper.Model }
                    });
                });

            TechnicalRequrementsTaskWrapper = new TechnicalRequrementsTask2Wrapper(new TechnicalRequrementsTask());

            #endregion

            #region RejectCommand

            RejectCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(TechnicalRequrementsTaskWrapper.RejectComment))
                    {
                        _messageService.ShowOkMessageDialog("Информация", "Перед отклонением необходимо заполнить причину отклонения");
                        return;
                    }

                    TechnicalRequrementsTaskWrapper.RejectByBackManagerMoment = DateTime.Now;
                    TechnicalRequrementsTaskWrapper.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    container.Resolve<IEventAggregator>().GetEvent<AfterRejectTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                },
                () => !IsRejected);

            #endregion

            #region OpenPriceCalculationCommand

            OpenPriceCalculationCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters
                    {
                        {nameof(PriceCalculation), SelectedCalculation}
                    });
                },
                () => SelectedCalculation != null);

            #endregion

            #region CopyPriceCalculationCommand

            CopyPriceCalculationCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters
                    {
                        {nameof(PriceCalculation), SelectedCalculation},
                        {nameof(TechnicalRequrementsTask), this.TechnicalRequrementsTaskWrapper.Model}
                    });
                },
                () => SelectedCalculation != null);

            #endregion

            #region AnswerCommands

            LoadFileAnswerCommand = new DelegateLogCommand(
                () =>
                {
                    var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;
                    string addToFileName = $"{SelectedAnswerFile.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                    FilesStorage.CopyFileFromStorage(SelectedAnswerFile.Id, _messageService, storageDirectory, addToFileName: addToFileName);
                }, 
                () => SelectedAnswerFile != null);

            LoadAllFileAnswersCommand = new DelegateLogCommand(
                () =>
                {
                    using (var fdb = new FolderBrowserDialog())
                    {
                        var result = fdb.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                        {
                            var targetDirectoryPath = fdb.SelectedPath;

                            foreach (var answerFile in this.TechnicalRequrementsTaskWrapper.AnswerFiles)
                            {

                                var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;
                                string addToFileName = $"{answerFile.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                                FilesStorage.CopyFileFromStorage(answerFile.Id, _messageService, storageDirectory, targetDirectoryPath, addToFileName, false);
                            }

                            Process.Start("explorer.exe", targetDirectoryPath);
                        }
                    }
                }, 
                () => this.TechnicalRequrementsTaskWrapper.AnswerFiles.Any());

            AddNewFileAnswersCommand = new DelegateLogCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var rootDirectoryPath = GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;

                        //копируем каждый файл
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            try
                            {
                                var fileWrapper = new AnswerFileTceWrapper(new AnswerFileTce())
                                {
                                    Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50)
                                };
                                File.Copy(fileName, $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(fileName)}");
                                this.TechnicalRequrementsTaskWrapper.AnswerFiles.Add(fileWrapper);

                                this.TechnicalRequrementsTaskWrapper.AcceptChanges();
                                UnitOfWork.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                _messageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                            }
                        }
                    }

                    (LoadAllFileAnswersCommand).RaiseCanExecuteChanged();
                });

            RemoveFileAnswerCommand = new DelegateLogCommand(
                () =>
                {
                    //диалог
                    var dr = _messageService.ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите удалить выделенное приложение?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    try
                    {
                        //удаление
                        FileInfo fileInfo = FilesStorage.FindFile(SelectedAnswerFile.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath);
                        File.Delete(fileInfo.FullName);
                        UnitOfWork.Repository<AnswerFileTce>().Delete(SelectedAnswerFile.Model);
                        this.TechnicalRequrementsTaskWrapper.AnswerFiles.Remove(SelectedAnswerFile);

                        //сохранение
                        this.TechnicalRequrementsTaskWrapper.AcceptChanges();
                        UnitOfWork.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        _messageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                    }

                    SelectedAnswerFile = null;
                }, 
                () => SelectedAnswerFile != null);

            #endregion

            #region OpenAnswerCommand

            OpenAnswerCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        FilesStorage.OpenFileFromStorage(SelectedAnswerFile.Id, _messageService, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath, SelectedAnswerFile.Name);
                    }
                    catch (Exception e)
                    {
                        _messageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                    }
                }, 
                () => SelectedAnswerFile != null);

            #endregion

            #region OpenFileCommand

            OpenFileCommand = new DelegateLogCommand(
                    () =>
                    {
                        try
                        {
                            TechnicalRequrementsFileWrapper fileWrapper = (TechnicalRequrementsFileWrapper) SelectedItem;
                            FilesStorage.OpenFileFromStorage(fileWrapper.Id, _messageService, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath, fileWrapper.Name);
                        }
                        catch (Exception e)
                        {
                            _messageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                        }
                    },
                    () => SelectedItem is TechnicalRequrementsFileWrapper);

            #endregion

            #region InstructCommand

            InstructCommand = new DelegateLogCommand(
                () =>
                {
                    if (TechnicalRequrementsTaskWrapper.Model.BackManager != null)
                    {
                        var dr = _messageService.ShowYesNoMessageDialog("Информация", "Back manager уже назначен. Вы хотите его сменить?");
                        if (dr != MessageDialogResult.Yes) return;
                    }

                    var backManagers = UnitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManager));
                    var selectService = Container.Resolve<ISelectService>();
                    var backManager = selectService.SelectItem(backManagers, TechnicalRequrementsTaskWrapper.Model.BackManager?.Id);

                    if (backManager != null)
                    {
                        TechnicalRequrementsTaskWrapper.BackManager = new UserWrapper(backManager);
                        TechnicalRequrementsTaskWrapper.AcceptChanges();
                        UnitOfWork.SaveChanges();
                        container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);
                        container.Resolve<IEventAggregator>().GetEvent<AfterInstructTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                        RaisePropertyChanged(nameof(AllowInstruct));
                    }
                },
                () => CurrentUserIsBackManagerBoss);


            #endregion
        }

        private void RaiseCanExecuteChange()
        {
            StartCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            AddNewFileCommand.RaiseCanExecuteChanged();
            AddOldFileCommand.RaiseCanExecuteChanged();
            AddGroupCommand.RaiseCanExecuteChanged();
            RemoveFileCommand.RaiseCanExecuteChanged();
            RemoveGroupCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Загрузка существующего расчета
        /// </summary>
        /// <param name="technicalRequrementsTask"></param>
        public void Load(TechnicalRequrementsTask technicalRequrementsTask)
        {
            var technicalRequrementsTaskLoaded = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask.Id);

            TechnicalRequrementsTaskWrapper = technicalRequrementsTaskLoaded != null 
                ? new TechnicalRequrementsTask2Wrapper(technicalRequrementsTaskLoaded) 
                : new TechnicalRequrementsTask2Wrapper(technicalRequrementsTask);

            //обновление момента просмотра задания бэк-менеджером
            if (GlobalAppProperties.User.Id == TechnicalRequrementsTaskWrapper.BackManager?.Id)
            {
                TechnicalRequrementsTaskWrapper.Model.LastOpenBackManagerMoment = DateTime.Now;
                UnitOfWork.SaveChanges();
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
            
            var requirements = salesUnitWrappers
                .GroupBy(x => x, new SalesUnitForTechnicalRequrementsTaskComparer())
                .Select(x => new TechnicalRequrements2Wrapper(new TechnicalRequrements {SalesUnits = x.Select(u => u.Model).ToList()}))
                .ToList();

            foreach (var requirement in requirements)
            {
                TechnicalRequrementsTaskWrapper.Requrements.Add(requirement);
            }
        }
    }
}
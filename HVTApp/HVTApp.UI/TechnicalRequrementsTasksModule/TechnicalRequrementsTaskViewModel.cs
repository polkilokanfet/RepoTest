using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTaskViewModel : ViewModelBaseCanExportToExcel
    {
        private TechnicalRequrementsTask2Wrapper _technicalRequrementsTaskWrapper;

        private object _selectedItem;
        private readonly IMessageService _messageService;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                ((DelegateCommand)AddNewFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddOldFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DivideCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)LoadFileCommand).RaiseCanExecuteChanged();
            }
        }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;

        public bool IsStarted => TechnicalRequrementsTaskWrapper?.Start != null;

        #region ICommand

        public ICommand SaveCommand { get; }

        /// <summary>
        /// Добавление нового файла
        /// </summary>
        public ICommand AddNewFileCommand { get; }

        /// <summary>
        /// Добавление существующего файла
        /// </summary>
        public ICommand AddOldFileCommand { get; }
        public ICommand RemoveFileCommand { get; }

        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand CancelCommand { get; }

        /// <summary>
        /// Разбить строку
        /// </summary>
        public ICommand MeregeCommand { get; }

        /// <summary>
        /// Слить строки
        /// </summary>
        public ICommand DivideCommand { get; }

        public ICommand LoadFileCommand { get; }
        public ICommand LoadAllFilesCommand { get; }


        #endregion

        public TechnicalRequrementsTask2Wrapper TechnicalRequrementsTaskWrapper
        {
            get { return _technicalRequrementsTaskWrapper; }
            private set
            {
                _technicalRequrementsTaskWrapper = value;
                //реакция на изменения в задаче
                _technicalRequrementsTaskWrapper.PropertyChanged += (sender, args) =>
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                };
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            }
        }

        public TechnicalRequrementsTaskViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            #region SaveCommand
          
            //сохранение изменений
            SaveCommand = new DelegateCommand(
                () =>
                {
                    TechnicalRequrementsTaskWrapper.AcceptChanges();

                    var trt = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(TechnicalRequrementsTaskWrapper.Model.Id);
                    if (trt == null)
                    {
                        UnitOfWork.Repository<TechnicalRequrementsTask>().Add(TechnicalRequrementsTaskWrapper.Model);
                    }

                    UnitOfWork.SaveChanges();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () => TechnicalRequrementsTaskWrapper.IsValid && TechnicalRequrementsTaskWrapper.IsChanged);

            #endregion

            #region AddFileCommand

            AddNewFileCommand = new DelegateCommand(
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
                                fileWrapper.Name = Path.GetFileNameWithoutExtension(fileName);
                                ((TechnicalRequrements2Wrapper)SelectedItem).Files.Add(fileWrapper);
                            }
                            catch (Exception e)
                            {
                                _messageService.ShowOkMessageDialog("Exception", e.GetAllExceptions());
                            }
                        }
                    }
                },
                () => !IsStarted && SelectedItem is TechnicalRequrements2Wrapper);

            AddOldFileCommand = new DelegateCommand(
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
            RemoveFileCommand = new DelegateCommand(
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

                    SelectedItem = null;
                },
                () => !IsStarted && SelectedItem is TechnicalRequrementsFileWrapper);

            #endregion

            #region AddGroupCommand

            //добавление группы оборудования
            AddGroupCommand = new DelegateCommand(
                () =>
                {
                    ////потенциальные группы
                    //var items = UnitOfWork.Repository<SalesUnit>()
                    //        .Find(x => x.Project.Manager.IsAppCurrentUser())
                    //        .Except(TechnicalRequrementsTaskWrapper.PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Model))
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
                () => !IsStarted);

            #endregion

            #region RemoveGroupCommand

            //удаление группы
            RemoveGroupCommand = new DelegateCommand(
                () =>
                {
                    //var result = _messageService.ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить из расчета группу оборудования?", defaultNo: true);
                    //if (result != MessageDialogResult.Yes) return;

                    //var selectedGroup = SelectedItem as PriceCalculationItem2Wrapper;

                    //var salesUnits = selectedGroup.SalesUnits.ToList();

                    ////единицы, которы нельзя удалить из расчета, т.к. они размещены в производстве
                    //var salesUnitsNotForRemove = salesUnits
                    //    .Where(x => x.Model.SignalToStartProduction.HasValue)
                    //    .Where(x => x.Model.ActualPriceCalculationItem(UnitOfWork)?.Id == selectedGroup.Model.Id)
                    //    .ToList();

                    //if (salesUnitsNotForRemove.Any())
                    //{
                    //    _messageService.ShowOkMessageDialog("Удаление", "Вы не можете удалить некоторые строки, т.к. они размещены в производстве.");

                    //    var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                    //    salesUnitsToRemove.ForEach(x => selectedGroup.SalesUnits.Remove(x));
                    //    if (!selectedGroup.SalesUnits.Any())
                    //        TechnicalRequrementsTaskWrapper.PriceCalculationItems.Remove(selectedGroup);
                    //}
                    //else
                    //{
                    //    TechnicalRequrementsTaskWrapper.PriceCalculationItems.Remove(selectedGroup);
                    //}
                },
                () => SelectedItem is PriceCalculationItem2Wrapper && !IsStarted);

            #endregion

            #region StartCommand

            StartCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите стартовать задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    TechnicalRequrementsTaskWrapper.Start = DateTime.Now;
                    SaveCommand.Execute(null);

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                    //уведомление по почте
                    //Container.Resolve<IEmailService>().SendMail("kos@uetm.ru", $"{GlobalAppProperties.User.Employee.Person} отправил новое задание на расчет", "test");

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)AddNewFileCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
                },
                () => !IsStarted && TechnicalRequrementsTaskWrapper.IsValid);

            #endregion

            #region CancelCommand

            CancelCommand = new DelegateCommand(() =>
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите остановить задачу?", defaultNo:true);
                if (dr != MessageDialogResult.Yes) return;

                TechnicalRequrementsTaskWrapper.Start = null;
                SaveCommand.Execute(null);

                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddNewFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();

            },
            () => IsStarted);

            #endregion

            #region MergeCommand

            MeregeCommand = new DelegateCommand(
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

            DivideCommand = new DelegateCommand(
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
                        var newTechnicalRequrements = new TechnicalRequrements { Comment = technicalRequrementsWrapper.Comment };
                        var newTechnicalRequrementsWrapper = new TechnicalRequrements2Wrapper(newTechnicalRequrements);
                        newTechnicalRequrementsWrapper.SalesUnits.Add(unit);

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

            LoadFileCommand = new DelegateCommand(
                () =>
                {
                    var fileWrapper = (TechnicalRequrementsFileWrapper) SelectedItem;
                    var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
                    string addToFileName = $"{fileWrapper.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                    FilesStorage.CopyFileFromStorage(fileWrapper.Id, _messageService, storageDirectory, addToFileName: addToFileName);
                },
                () => SelectedItem is TechnicalRequrementsFileWrapper);

            LoadAllFilesCommand = new DelegateCommand(
                () =>
                {
                    using (var fdb = new FolderBrowserDialog())
                    {
                        var result = fdb.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                        {
                            var taskPath = fdb.SelectedPath;
                            foreach (var requrement in this.TechnicalRequrementsTaskWrapper.Requrements)
                            {
                                var reqDirName = $"{requrement.Model.Id} {requrement.SalesUnit.Product.Designation.ReplaceUncorrectSimbols().LimitLengh()} ({requrement.Amount} шт.)";
                                var dirPath = Path.Combine(taskPath, reqDirName);
                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }

                                foreach (var file in requrement.Files)
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

            TechnicalRequrementsTaskWrapper = new TechnicalRequrementsTask2Wrapper(new TechnicalRequrementsTask());
        }

        public void Load(TechnicalRequrementsTask technicalRequrementsTask)
        {
            var technicalRequrementsTaskLoaded = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask.Id);

            TechnicalRequrementsTaskWrapper = technicalRequrementsTaskLoaded != null 
                ? new TechnicalRequrementsTask2Wrapper(technicalRequrementsTaskLoaded) 
                : new TechnicalRequrementsTask2Wrapper(technicalRequrementsTask);
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
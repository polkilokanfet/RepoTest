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
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTaskViewModel : ViewModelBaseCanExportToExcel
    {
        private TechnicalRequrementsTask2Wrapper _technicalRequrementsTaskWrapper;

        private object _selectedItem;
        private readonly IMessageService _messageService;
        private readonly List<TechnicalRequrementsFile> _removedFiles = new List<TechnicalRequrementsFile>();

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
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;

        public bool IsStarted => TechnicalRequrementsTaskWrapper?.Start != null;

        public bool WasStarted => TechnicalRequrementsTaskWrapper?.Model.FirstStartMoment != null;

        public bool IsRejected => TechnicalRequrementsTaskWrapper?.RejectByBackManagerMoment != null;

        public string ValidationResult => TechnicalRequrementsTaskWrapper?.ValidationResult;

        #region ICommand

        public ICommand SaveCommand { get; }

        /// <summary>
        /// ���������� ������ �����
        /// </summary>
        public ICommand AddNewFileCommand { get; }

        /// <summary>
        /// ���������� ������������� �����
        /// </summary>
        public ICommand AddOldFileCommand { get; }
        public ICommand RemoveFileCommand { get; }

        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand EditCommand { get; }

        public ICommand CancelCommand { get; }

        /// <summary>
        /// ������� ������
        /// </summary>
        public ICommand MeregeCommand { get; }

        /// <summary>
        /// ����� ������
        /// </summary>
        public ICommand DivideCommand { get; }

        public ICommand LoadFileCommand { get; }
        public ICommand LoadAllFilesCommand { get; }

        public ICommand CreatePriceCalculationCommand { get; }

        /// <summary>
        /// ��������� ������
        /// </summary>
        public ICommand RejectCommand { get; }

        public ICommand OpenPriceCalculationCommand { get; }

        #endregion

        public TechnicalRequrementsTask2Wrapper TechnicalRequrementsTaskWrapper
        {
            get { return _technicalRequrementsTaskWrapper; }
            private set
            {
                _technicalRequrementsTaskWrapper = value;
                //������� �� ��������� � ������
                _technicalRequrementsTaskWrapper.PropertyChanged += (sender, args) =>
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)RejectCommand).RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(ValidationResult));
                };
                OnPropertyChanged();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            }
        }

        public TechnicalRequrementsTaskViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            #region SaveCommand
          
            //���������� ���������
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
                () => TechnicalRequrementsTaskWrapper != null && 
                      TechnicalRequrementsTaskWrapper.IsValid && 
                      TechnicalRequrementsTaskWrapper.IsChanged &&
                      !WasStarted);

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

                        //�������� ������ ����
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

                    //��������� ���� � ��������� ����������
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

            //�������� �����
            RemoveFileCommand = new DelegateCommand(
                () =>
                {
                    //������
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "������������� ������ ������� ����?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    //����� ���������� ������
                    var file =  (TechnicalRequrementsFileWrapper)SelectedItem;
                    var technicalRequrements2Wrapper = TechnicalRequrementsTaskWrapper.Requrements.Single(x => x.Files.Contains(file));

                    //��������
                    technicalRequrements2Wrapper.Files.Remove(file);
                    if (UnitOfWork.Repository<TechnicalRequrementsFile>().GetById(file.Id) != null)
                    {
                        //���� ������������ ������ � ����� �����
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

            //���������� ������ ������������
            AddGroupCommand = new DelegateCommand(
                () =>
                {
                    _messageService.ShowYesNoMessageDialog("����������", "���� ��� ������� �� ��������. ��� ������� ��� �����?");
                    ////������������� ������
                    //var items = UnitOfWork.Repository<SalesUnit>()
                    //        .Find(x => x.Project.Manager.IsAppCurrentUser())
                    //        .Except(TechnicalRequrementsTaskWrapper.Requrements.SelectMany(x => x.SalesUnits).Select(x => x.Model))
                    //        .Select(x => new SalesUnitEmptyWrapper(x))
                    //        .GroupBy(x => x, new SalesUnit2Comparer())
                    //        .Select(GetPriceCalculationItem2Wrapper);

                    ////����� ������
                    //var viewModel = new PriceCalculationItemsViewModel(items);
                    //var dialogResult = Container.Resolve<IDialogService>().ShowDialog(viewModel);

                    ////���������� ������
                    //if (dialogResult.HasValue && dialogResult.Value)
                    //{
                    //    viewModel.SelectedItemWrappers.ForEach(x => TechnicalRequrementsTaskWrapper.PriceCalculationItems.Add(x));
                    //}
                },
                () => !WasStarted && !IsStarted);

            #endregion

            #region RemoveGroupCommand

            //�������� ������
            RemoveGroupCommand = new DelegateCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("��������", "������������� ������ ������� �� ������ ��� ������������?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    var selectedGroup = SelectedItem as TechnicalRequrements2Wrapper;

                    var salesUnits = selectedGroup.SalesUnits.ToList();

                    //�������, ������ ������ ������� �� �������, �.�. ��� ��������� � ������������
                    var salesUnitsNotForRemove = salesUnits
                        .Where(x => x.Model.SignalToStartProduction.HasValue)
                        .Where(x => x.Model.ActualPriceCalculationItem(UnitOfWork)?.Id == selectedGroup.Model.Id)
                        .ToList();

                    if (salesUnitsNotForRemove.Any())
                    {
                        _messageService.ShowOkMessageDialog("��������", "�� �� ������ ������� ��������� ������, �.�. ��� ��������� � ������������.");

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

            StartCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("�������������", "�� �������, ��� ������ ���������� ������?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    TechnicalRequrementsTaskWrapper.Start = DateTime.Now;
                    if (!TechnicalRequrementsTaskWrapper.Model.FirstStartMoment.HasValue)
                    {
                        TechnicalRequrementsTaskWrapper.Model.FirstStartMoment = TechnicalRequrementsTaskWrapper.Start;
                    }
                    SaveCommand.Execute(null);

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                    //����������� �� �����
                    //Container.Resolve<IEmailService>().SendMail("kos@uetm.ru", $"{GlobalAppProperties.User.Employee.Person} �������� ����� ������� �� ������", "test");

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                    RaiseCanExecuteChange();
                },
                () =>
                {
                    if (TechnicalRequrementsTaskWrapper != null)
                    {
                        //���� ��� ����������
                        if (IsStarted)
                            return false;

                        //��� ��������
                        if (TechnicalRequrementsTaskWrapper.Model.FirstStartMoment.HasValue)
                            return TechnicalRequrementsTaskWrapper.IsValid && TechnicalRequrementsTaskWrapper.IsChanged;

                        //��� ������
                        return TechnicalRequrementsTaskWrapper.IsValid;
                    }
                    return false;
                });

            EditCommand = new DelegateCommand(
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

            CancelCommand = new DelegateCommand(() =>
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("�������������", "�� �������, ��� ������ ���������� ������?", defaultNo:true);
                if (dr != MessageDialogResult.Yes) return;

                TechnicalRequrementsTaskWrapper.Start = null;
                SaveCommand.Execute(null);

                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                RaiseCanExecuteChange();
            },
            () => IsStarted);

            #endregion

            #region MergeCommand

            MeregeCommand = new DelegateCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("�������", "������������� ������ ����� ������, ���������� ������?", defaultYes: true);
                    if (result != MessageDialogResult.Yes) return;

                    //������ ��� �������
                    var items = TechnicalRequrementsTaskWrapper.Requrements.Where(x => x.IsChecked).ToList();

                    if (items.Select(x => x.SalesUnit.Facility.Id).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("�������", "�� �� ������ ���������� ������ � ������� ��������� ��������.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.Product.Id).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("�������", "�� �� ������ ���������� ������ � ������� ���������� ��������.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.OrderInTakeDate).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("�������", "�� �� ������ ���������� ������ � ������� ������ ���.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.RealizationDateCalculated).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("�������", "�� �� ������ ���������� ������ � ������� ������ ����������.");
                        return;
                    }

                    if (items.Select(x => x.SalesUnit.Producer).Distinct().Count() > 1)
                    {
                        _messageService.ShowOkMessageDialog("�������", "�� �� ������ ���������� ������ � ������� ���������������.");
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
                    var result = _messageService.ShowYesNoMessageDialog("���������", "������������� ������ ������� ��������� ������?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    var technicalRequrementsWrapper = (TechnicalRequrements2Wrapper)SelectedItem;
                    var salesUnit = technicalRequrementsWrapper.SalesUnits.First();

                    var salesUnitsToDivide = technicalRequrementsWrapper.SalesUnits.ToList();
                    salesUnitsToDivide.Remove(salesUnit);

                    //������� ����� ������
                    foreach (var unit in salesUnitsToDivide)
                    {
                        technicalRequrementsWrapper.SalesUnits.Remove(unit);

                        //������� ����� ������
                        var newTechnicalRequrements = new TechnicalRequrements
                        {
                            SalesUnits = new List<SalesUnit> { unit.Model },
                            Comment = technicalRequrementsWrapper.Comment
                        };
                        var newTechnicalRequrementsWrapper = new TechnicalRequrements2Wrapper(newTechnicalRequrements);

                        //��������� � ����� ������ �����
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
                            foreach (var requrement in this.TechnicalRequrementsTaskWrapper.Requrements.Where(x => x.IsActual.HasValue && x.IsActual.Value))
                            {
                                var reqDirName = $"{requrement.Model.Id} {requrement.SalesUnit.Product.Designation.ReplaceUncorrectSimbols().LimitLengh()} ({requrement.Amount} ��.)";
                                var dirPath = Path.Combine(taskPath, reqDirName);
                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }

                                foreach (var file in requrement.Files.Where(x => x.IsActual))
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

            CreatePriceCalculationCommand = new DelegateCommand(
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

            RejectCommand = new DelegateCommand(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(TechnicalRequrementsTaskWrapper.RejectComment))
                    {
                        _messageService.ShowOkMessageDialog("����������", "����� ����������� ���������� ��������� ������� ����������");
                        return;
                    }

                    TechnicalRequrementsTaskWrapper.RejectByBackManagerMoment = DateTime.Now;
                    TechnicalRequrementsTaskWrapper.AcceptChanges();
                    UnitOfWork.SaveChanges();
                },
                () => !IsRejected);

            #endregion

            #region OpenPriceCalculationCommand

            OpenPriceCalculationCommand = new DelegateCommand(
                () =>
                {
                    var priceCalculation = Container.Resolve<ISelectService>().SelectItem(TechnicalRequrementsTaskWrapper.Model.PriceCalculations);
                    if (priceCalculation != null)
                    {
                        RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters
                        {
                            {nameof(PriceCalculation), priceCalculation}
                        });
                    }
                });

            #endregion
        }

        private void RaiseCanExecuteChange()
        {
            ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddNewFileCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddOldFileCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// �������� ������������� �������
        /// </summary>
        /// <param name="technicalRequrementsTask"></param>
        public void Load(TechnicalRequrementsTask technicalRequrementsTask)
        {
            var technicalRequrementsTaskLoaded = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask.Id);

            TechnicalRequrementsTaskWrapper = technicalRequrementsTaskLoaded != null 
                ? new TechnicalRequrementsTask2Wrapper(technicalRequrementsTaskLoaded) 
                : new TechnicalRequrementsTask2Wrapper(technicalRequrementsTask);

            //���������� ������� ��������� ������� ���-����������
            if (GlobalAppProperties.User.Id == TechnicalRequrementsTaskWrapper.BackManager?.Id)
            {
                TechnicalRequrementsTaskWrapper.Model.LastOpenBackManagerMoment = DateTime.Now;
                UnitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// �������� ��� �������� ������ ������� �� �������� ������
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
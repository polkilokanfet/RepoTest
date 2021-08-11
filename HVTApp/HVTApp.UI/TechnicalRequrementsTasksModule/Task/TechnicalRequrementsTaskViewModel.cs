using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTaskViewModel : ViewModelBaseCanExportToExcel
    {
        private TechnicalRequrementsTask2Wrapper _technicalRequrementsTaskWrapper;

        private object _selectedItem;
        private readonly List<TechnicalRequrementsFile> _removedFiles = new List<TechnicalRequrementsFile>();
        private AnswerFileTceWrapper _selectedAnswerFile;
        private PriceCalculation _selectedCalculation;
        private TechnicalRequrementsTaskHistoryElementWrapper _historyElementWrapper;
        private ShippingCostFileWrapper _selectedShippingCalculationFile;

        //�������
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

        public ShippingCostFileWrapper SelectedShippingCalculationFile
        {
            get => _selectedShippingCalculationFile;
            set
            {
                _selectedShippingCalculationFile = value;
                LoadShippingCalculationFileCommand.RaiseCanExecuteChanged();
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

        /// <summary>
        /// ������ �������� ��
        /// </summary>
        public bool IsStarted => TechnicalRequrementsTaskWrapper != null && 
                                 TechnicalRequrementsTaskWrapper.Model.IsStarted;

        /// <summary>
        /// ������ ��������� ��
        /// </summary>
        public bool IsRejected => TechnicalRequrementsTaskWrapper != null && 
                                  TechnicalRequrementsTaskWrapper.Model.IsRejected;

        /// <summary>
        /// ������ ����������� ��
        /// </summary>
        public bool IsStopped => TechnicalRequrementsTaskWrapper != null && 
                                 TechnicalRequrementsTaskWrapper.Model.IsStopped;

        /// <summary>
        /// ������ ����������� ��
        /// </summary>
        public bool IsFinished => TechnicalRequrementsTaskWrapper != null && 
                                  TechnicalRequrementsTaskWrapper.Model.IsFinished;

        /// <summary>
        /// ������ ������� �� � ��
        /// </summary>
        public bool IsAccepted => TechnicalRequrementsTaskWrapper != null && 
                                  TechnicalRequrementsTaskWrapper.Model.IsAccepted;

        public bool WasStarted => IsStarted || IsRejected || IsStopped || IsFinished || IsAccepted;

        #region Allow
        
        /// <summary>
        /// ����� �������� ����������
        /// </summary>
        public bool AllowInstruct => CurrentUserIsBackManagerBoss && IsStarted && !IsFinished;

        /// <summary>
        /// ����� ��������� ����������
        /// </summary>
        public bool AllowReject => CurrentUserIsBackManager && IsStarted && !IsFinished && !IsRejected;

        /// <summary>
        /// ����� ��������� ����������
        /// </summary>
        public bool AllowFinish => CurrentUserIsBackManager && IsStarted && !IsFinished && !IsRejected;

        /// <summary>
        /// ����� ������� ����������
        /// </summary>
        public bool AllowAccept => IsFinished && !IsAccepted;

        /// <summary>
        /// ����� ���������� ����������
        /// </summary>
        public bool AllowCancel => IsStarted || IsRejected;

        #endregion



        public string ValidationResult => TechnicalRequrementsTaskWrapper?.ValidationResult;

        #region Commands

        public SaveCommand SaveCommand { get; }

        /// <summary>
        /// ���������� ������ �����
        /// </summary>
        public AddNewFileCommand AddNewFileCommand { get; }

        /// <summary>
        /// ���������� ������ ���������� � ������ ���
        /// </summary>
        public AddNewFileAnswersCommand AddNewFileAnswersCommand { get; }

        /// <summary>
        /// ���������� ������������� �����
        /// </summary>
        public AddOldFileCommand AddOldFileCommand { get; }
        public DelegateLogCommand RemoveFileCommand { get; }
        public RemoveFileAnswerCommand RemoveFileAnswerCommand { get; }

        public DelegateLogCommand AddGroupCommand { get; }
        public RemoveGroupCommand RemoveGroupCommand { get; }

        public StartCommand StartCommand { get; }

        /// <summary>
        /// ��������� ������
        /// </summary>
        public RejectCommand RejectCommand { get; }

        public StopCommand StopCommand { get; }

        public FinishCommand FinishCommand { get; }

        /// <summary>
        /// ������� ������
        /// </summary>
        public MeregeCommand MeregeCommand { get; }

        /// <summary>
        /// ����� ������
        /// </summary>
        public DivideCommand DivideCommand { get; }

        public LoadFileCommand LoadFileCommand { get; }
        public LoadAllFilesCommand LoadAllFilesCommand { get; }

        public LoadFileAnswerCommand LoadFileAnswerCommand { get; }
        public LoadAllFileAnswersCommand LoadAllFileAnswersCommand { get; }

        public CreatePriceCalculationCommand CreatePriceCalculationCommand { get; }

        /// <summary>
        /// ������� ����� ������� ��
        /// </summary>
        public CopyPriceCalculationCommand CopyPriceCalculationCommand { get; }

        public OpenPriceCalculationCommand OpenPriceCalculationCommand { get; }

        public OpenAnswerCommand OpenAnswerCommand { get; }

        public OpenFileCommand OpenFileCommand { get; }

        public InstructCommand InstructCommand { get; }

        public AddShippingCalculationFileCommand AddShippingCalculationFileCommand { get; }

        public LoadShippingCalculationFileCommand LoadShippingCalculationFileCommand { get; }

        #endregion

        public TechnicalRequrementsTask2Wrapper TechnicalRequrementsTaskWrapper
        {
            get => _technicalRequrementsTaskWrapper;
            private set
            {
                _technicalRequrementsTaskWrapper = value;

                //������� �� ��������� � ������
                _technicalRequrementsTaskWrapper.PropertyChanged += (sender, args) =>
                {
                    SaveCommand.RaiseCanExecuteChanged();
                    StartCommand.RaiseCanExecuteChanged();
                    RejectCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(nameof(ValidationResult));
                };

                //������� �� �������� ��������� ��������
                _technicalRequrementsTaskWrapper.PropertyChangeAccepted += (task, s) =>
                {
                    RaisePropertyChanged(nameof(AllowInstruct));
                };

                RaisePropertyChanged();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            }
        }

        public TechnicalRequrementsTaskHistoryElementWrapper HistoryElementWrapper
        {
            get => _historyElementWrapper;
            set
            {
                _historyElementWrapper = value;
                RaisePropertyChanged();
            }
        }

        public TechnicalRequrementsTaskViewModel(IUnityContainer container) : base(container)
        {
            var messageService = container.Resolve<IMessageService>();

            //���������� ���������
            SaveCommand = new SaveCommand(this, this.Container);
            AddNewFileCommand = new AddNewFileCommand(this, this.Container);
            AddOldFileCommand = new AddOldFileCommand(this, this.Container);


            #region RemoveFileCommand

            //�������� �����
            RemoveFileCommand = new DelegateLogCommand(
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
            AddGroupCommand = new DelegateLogCommand(
                () =>
                {
                    messageService.ShowYesNoMessageDialog("����������", "���� ��� ������� �� ��������. ��� ������� ��� �����?");
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

            //�������� ������
            RemoveGroupCommand = new RemoveGroupCommand(this, this.Container);

            StartCommand = new StartCommand(this, this.Container);
            StopCommand = new StopCommand(this, this.Container);

            MeregeCommand = new MeregeCommand(this, this.Container);
            DivideCommand = new DivideCommand(this, this.Container);
            LoadFileCommand = new LoadFileCommand(this, this.Container);
            LoadAllFilesCommand = new LoadAllFilesCommand(this, this.Container);
            CreatePriceCalculationCommand = new CreatePriceCalculationCommand(this, this.Container);

            TechnicalRequrementsTaskWrapper = new TechnicalRequrementsTask2Wrapper(new TechnicalRequrementsTask());

            RejectCommand = new RejectCommand(this, this.Container);
            FinishCommand = new FinishCommand(this, this.Container);
            OpenPriceCalculationCommand = new OpenPriceCalculationCommand(this, this.Container);
            CopyPriceCalculationCommand = new CopyPriceCalculationCommand(this, this.Container);
            LoadFileAnswerCommand = new LoadFileAnswerCommand(this, this.Container);
            LoadAllFileAnswersCommand = new LoadAllFileAnswersCommand(this, this.Container);
            AddNewFileAnswersCommand = new AddNewFileAnswersCommand(this, this.Container);
            RemoveFileAnswerCommand = new RemoveFileAnswerCommand(this, this.Container);
            OpenAnswerCommand = new OpenAnswerCommand(this, this.Container);
            OpenFileCommand = new OpenFileCommand(this, this.Container);
            InstructCommand = new InstructCommand(this, this.Container);
            AddShippingCalculationFileCommand = new AddShippingCalculationFileCommand(this, this.Container);
            LoadShippingCalculationFileCommand = new LoadShippingCalculationFileCommand(this, this.Container);
        }

        private void RaiseCanExecuteChange()
        {
            StartCommand.RaiseCanExecuteChanged();
            RejectCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
            AddNewFileCommand.RaiseCanExecuteChanged();
            AddOldFileCommand.RaiseCanExecuteChanged();
            AddGroupCommand.RaiseCanExecuteChanged();
            RemoveFileCommand.RaiseCanExecuteChanged();
            RemoveGroupCommand.RaiseCanExecuteChanged();
            AddNewFileAnswersCommand.RaiseCanExecuteChanged();
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

            HistoryElementWrapper = new TechnicalRequrementsTaskHistoryElementWrapper(new TechnicalRequrementsTaskHistoryElement());

            //��� ��
            if (CurrentUserIsBackManager)
            {
                //���������� ������� ��������� ������� ���-����������
                if (GlobalAppProperties.User.Id == TechnicalRequrementsTaskWrapper.BackManager?.Id)
                {
                    TechnicalRequrementsTaskWrapper.Model.LastOpenBackManagerMoment = DateTime.Now;
                    UnitOfWork.SaveChanges();
                }
            }
        }

        /// <summary>
        /// �������� ��� �������� ������ ������� �� �������� ������
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


            //���������� ������ � �������� ������ � �������
            TechnicalRequrementsTaskHistoryElementWrapper historyElement1 =
                new TechnicalRequrementsTaskHistoryElementWrapper(
                    new TechnicalRequrementsTaskHistoryElement
                    {
                        Type = TechnicalRequrementsTaskHistoryElementType.Create,
                        Comment = "������� �������"
                    });
            TechnicalRequrementsTaskWrapper.HistoryElements.Add(historyElement1);

        }
    }
}
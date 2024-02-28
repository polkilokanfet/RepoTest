using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

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

        public string ValidationResult => TechnicalRequrementsTaskWrapper?.ValidationResult;

        #region Selected

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
                StartProductionCommand.RaiseCanExecuteChanged();
                MakeInvoiceForPaymentTaskCommand.RaiseCanExecuteChanged();
            }
        }

        public ShippingCostFileWrapper SelectedShippingCalculationFile
        {
            get => _selectedShippingCalculationFile;
            set
            {
                _selectedShippingCalculationFile = value;
                LoadShippingCalculationFileCommand.RaiseCanExecuteChanged();
                OpenShippingCalculationFileCommand.RaiseCanExecuteChanged();
                RemoveShippingCalculationFileCommand.RaiseCanExecuteChanged();
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
        
        #endregion

        #region Is

        public bool CurrentUserIsManager => GlobalAppProperties.UserIsManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.UserIsBackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.UserIsBackManagerBoss;

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

        public bool IsValid => TechnicalRequrementsTaskWrapper != null &&
                               HistoryElementWrapper != null &&

                               TechnicalRequrementsTaskWrapper.IsValid &&
                               HistoryElementWrapper.IsValid;

        public bool IsChanged => TechnicalRequrementsTaskWrapper != null &&
                                 HistoryElementWrapper != null &&

                                 (TechnicalRequrementsTaskWrapper.IsChanged ||
                                  HistoryElementWrapper.IsChanged);
        #endregion

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

        /// <summary>
        /// ����� ������������� ������ ��
        /// </summary>
        public bool AllowEditByBackManager =>
            this.TechnicalRequrementsTaskWrapper?.Model.LastHistoryElement?.Type == TechnicalRequrementsTaskHistoryElementType.Start ||
            this.TechnicalRequrementsTaskWrapper?.Model.LastHistoryElement?.Type == TechnicalRequrementsTaskHistoryElementType.Instruct;

        #endregion

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

        /// <summary>
        /// ���������� ������
        /// </summary>
        public StartCommand StartCommand { get; }

        /// <summary>
        /// ��������� ������ (back-manager)
        /// </summary>
        public RejectCommandByBackManager RejectCommandByBackManager { get; }

        /// <summary>
        /// ��������� ������ (front-manager)
        /// </summary>
        public RejectCommandByFrontManager RejectCommandByFrontManager { get; }

        public StopCommand StopCommand { get; }

        public FinishCommand FinishCommand { get; }

        public AcceptCommand AcceptCommand { get; }

        public ICommandRaiseCanExecuteChanged StartProductionCommand { get; }

        /// <summary>
        /// ������� ������
        /// </summary>
        public MergeCommand MergeCommand { get; }

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

        public OpenShippingCalculationFileCommand OpenShippingCalculationFileCommand { get; }

        public RemoveShippingCalculationFileCommand RemoveShippingCalculationFileCommand { get; }

        /// <summary>
        /// ������ �� ������������ �����
        /// </summary>
        public virtual DelegateLogConfirmationCommand MakeInvoiceForPaymentTaskCommand { get; }

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
                    this.RaiseCanExecuteChangeInAllCommands();
                    RaisePropertyChanged(nameof(ValidationResult));
                    RaisePropertyChanged(nameof(AllowEditByBackManager));
                };

                //������� �� �������� ��������� ��������
                _technicalRequrementsTaskWrapper.PropertyChangeAccepted += (task, s) =>
                {
                    this.RaiseCanExecuteChangeInAllCommands();
                    RaisePropertyChanged(nameof(AllowInstruct));
                    RaisePropertyChanged(nameof(AllowEditByBackManager));
                };

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AllowEditByBackManager));

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            }
        }

        public TechnicalRequrementsTaskHistoryElementWrapper HistoryElementWrapper
        {
            get => _historyElementWrapper;
            set
            {
                _historyElementWrapper = value;

                //������� �� ���������
                if (value != null)
                {
                    _historyElementWrapper.PropertyChanged += (sender, args) =>
                    {
                        this.RaiseCanExecuteChangeInAllCommands();
                        RaisePropertyChanged(nameof(ValidationResult));
                        this.StartProductionCommand.RaiseCanExecuteChanged();
                        this.MakeInvoiceForPaymentTaskCommand.RaiseCanExecuteChanged();
                    };
                }

                RaisePropertyChanged();
            }
        }

        public void SetNewHistoryElement()
        {
            HistoryElementWrapper = new TechnicalRequrementsTaskHistoryElementWrapper(
                new TechnicalRequrementsTaskHistoryElement
                {
                    User = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)
                });
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
                    var dr = Container.Resolve<IMessageService>().ConfirmationDialog("��������", "������������� ������ ������� ����?", defaultNo: true);
                    if (dr == false) return;

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
                    messageService.ConfirmationDialog("����������", "���� ��� ������� �� ��������. ��� ������� ��� �����?");
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

            MergeCommand = new MergeCommand(this, this.Container);
            DivideCommand = new DivideCommand(this, this.Container);

            LoadFileCommand = new LoadFileCommand(this, this.Container);
            LoadAllFilesCommand = new LoadAllFilesCommand(this, this.Container);

            CreatePriceCalculationCommand = new CreatePriceCalculationCommand(this, this.Container);

            TechnicalRequrementsTaskWrapper = new TechnicalRequrementsTask2Wrapper(new TechnicalRequrementsTask());

            RejectCommandByBackManager = new RejectCommandByBackManager(this, this.Container);
            RejectCommandByFrontManager = new RejectCommandByFrontManager(this, this.Container);
            FinishCommand = new FinishCommand(this, this.Container);
            AcceptCommand = new AcceptCommand(this, this.Container);

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
            OpenShippingCalculationFileCommand = new OpenShippingCalculationFileCommand(this, this.Container);
            RemoveShippingCalculationFileCommand = new RemoveShippingCalculationFileCommand(this, this.Container);

            StartProductionCommand = new DelegateLogConfirmationCommand(messageService,
                "�� �������, ��� ������ ������� ������������?",
                () =>
                {
                    var item = (TechnicalRequrements2Wrapper) SelectedItem;
                    if (item.Model.SalesUnits.Any(salesUnit => salesUnit.SignalToStartProduction.HasValue))
                    {
                        messageService.Message("����������", "�����. ����� ������������ ��� ���� ��, ������� � ������������");
                        return;
                    }

                    var unitOfWork = container.Resolve<IUnitOfWork>();
                    var salesUnits = item.Model.SalesUnits
                        .Select(salesUnit => unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                        .ToList();
                    var files = item.Model.Files
                        .Where(file => file.IsActual)
                        //.Select(file => unitOfWork.Repository<TechnicalRequrementsFile>().GetById(file.Id))
                        .ToList();
                    var manager = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);

                    //������ ������
                    var task = new PriceEngineeringTask();
                    task.ProductBlockManager = task.ProductBlockEngineer = salesUnits.First().Product.ProductBlock;
                    task.TcePosition = item.PositionInTeamCenter?.ToString();
                    task.Amount = salesUnits.Count;
                    task.SalesUnits.AddRange(salesUnits);
                    task.SalesUnits.ForEach(salesUnit => salesUnit.SignalToStartProduction = DateTime.Now);
                    foreach (var file in files)
                    {
                        var file1 = unitOfWork.Repository<PriceEngineeringTaskFileTechnicalRequirements>().GetById(file.Id) ??
                            new PriceEngineeringTaskFileTechnicalRequirements
                            {
                                CreationMoment = file.Date,
                                Name = file.Name,
                                Id = file.Id
                            };
                        task.FilesTechnicalRequirements.Add(file1);
                    }
                    task.Statuses.Add(new PriceEngineeringTaskStatus
                    {
                        Moment = DateTime.Now,
                        StatusEnum = ScriptStep.Create.Value
                    });
                    task.Messages.Add(new PriceEngineeringTaskMessage
                    {
                        Author = manager,
                        Message = "������� ��������������� �������� � Team Center.",
                        Moment = DateTime.Now.AddSeconds(1)
                    });
                    task.Statuses.Add(new PriceEngineeringTaskStatus
                    {
                        Moment = DateTime.Now.AddSeconds(2),
                        StatusEnum = ScriptStep.Start.Value
                    });
                    task.Statuses.Add(new PriceEngineeringTaskStatus
                    {
                        Moment = DateTime.Now.AddSeconds(3),
                        StatusEnum = ScriptStep.ProductionRequestStart.Value
                    });

                    //��������� ������ �����
                    var tasks = new PriceEngineeringTasks();
                    tasks.ChildPriceEngineeringTasks.Add(task);
                    tasks.UserManager = manager;
                    tasks.BackManager = this.TechnicalRequrementsTaskWrapper.Model.BackManager == null
                        ? null
                        : unitOfWork.Repository<User>().GetById(this.TechnicalRequrementsTaskWrapper.Model.BackManager.Id);
                    tasks.TceNumber = this.TechnicalRequrementsTaskWrapper.Model.TceNumber;

                    //��������� ������
                    if (unitOfWork.SaveEntity(tasks).OperationCompletedSuccessfully)
                    {
                        RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(
                            new NavigationParameters
                            {
                                { nameof(PriceEngineeringTasks), tasks }
                            });
                    }
                },
                () => 
                    GlobalAppProperties.UserIsManager &&
                    this.TechnicalRequrementsTaskWrapper != null &&
                    this.TechnicalRequrementsTaskWrapper.HistoryElements.Any() &&
                    this.TechnicalRequrementsTaskWrapper.HistoryElements.OrderBy(historyItem => historyItem.Moment).Last().Model.Type == TechnicalRequrementsTaskHistoryElementType.Accept &&
                    SelectedItem is TechnicalRequrements2Wrapper);

            MakeInvoiceForPaymentTaskCommand = new DelegateLogConfirmationCommand(
                messageService, 
                "�� �������, ��� ������ ������� ������ �� �������� �����?",
                () =>
                {
                    if (string.IsNullOrEmpty(this.TechnicalRequrementsTaskWrapper.TceNumber))
                    {
                        messageService.Message("�����", "����� ���������� ��� � Team Center");
                        return;
                    }

                    if (((TechnicalRequrements2Wrapper)SelectedItem).Model.SalesUnits.Any(salesUnit => salesUnit.Specification == null))
                    {
                        messageService.Message("�����", "�������� ����� ���� ������������");
                        return;
                    }

                    var unitOfWork = container.Resolve<IUnitOfWork>();

                    unitOfWork.SaveEntity(new InvoiceForPaymentTask
                    {
                        TechnicalRequrements = unitOfWork.Repository<TechnicalRequrements>().GetById(((TechnicalRequrements2Wrapper)SelectedItem).Model.Id)
                    });

                    messageService.Message("�����!", "������ �� �������� ����� ������� ������!");

                },
                () =>
                    GlobalAppProperties.UserIsManager &&
                    this.TechnicalRequrementsTaskWrapper != null &&
                    this.TechnicalRequrementsTaskWrapper.HistoryElements.Any() &&
                    this.TechnicalRequrementsTaskWrapper.HistoryElements.OrderBy(historyItem => historyItem.Moment).Last().Model.Type == TechnicalRequrementsTaskHistoryElementType.Accept &&
                    SelectedItem is TechnicalRequrements2Wrapper);

            SetNewHistoryElement();
        }

        private void RaiseCanExecuteChangeInAllCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            AddNewFileCommand.RaiseCanExecuteChanged();
            AddNewFileAnswersCommand.RaiseCanExecuteChanged();
            AddOldFileCommand.RaiseCanExecuteChanged();
            RemoveFileCommand.RaiseCanExecuteChanged();
            RemoveFileAnswerCommand.RaiseCanExecuteChanged();
            AddGroupCommand.RaiseCanExecuteChanged();
            RemoveGroupCommand.RaiseCanExecuteChanged();
            StartCommand.RaiseCanExecuteChanged();
            RejectCommandByBackManager.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
            FinishCommand.RaiseCanExecuteChanged();
            AcceptCommand.RaiseCanExecuteChanged();
            MergeCommand.RaiseCanExecuteChanged();
            DivideCommand.RaiseCanExecuteChanged();
            LoadFileCommand.RaiseCanExecuteChanged();
            LoadAllFilesCommand.RaiseCanExecuteChanged();
            LoadFileAnswerCommand.RaiseCanExecuteChanged();
            LoadAllFileAnswersCommand.RaiseCanExecuteChanged();
            CreatePriceCalculationCommand.RaiseCanExecuteChanged();
            CopyPriceCalculationCommand.RaiseCanExecuteChanged();
            OpenPriceCalculationCommand.RaiseCanExecuteChanged();
            OpenAnswerCommand.RaiseCanExecuteChanged();
            OpenFileCommand.RaiseCanExecuteChanged();
            InstructCommand.RaiseCanExecuteChanged();
            AddShippingCalculationFileCommand.RaiseCanExecuteChanged();
            LoadShippingCalculationFileCommand.RaiseCanExecuteChanged();
            OpenShippingCalculationFileCommand.RaiseCanExecuteChanged();
            RemoveShippingCalculationFileCommand.RaiseCanExecuteChanged();

            RaisePropertyChanged(nameof(AllowFinish));
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
                .GroupBy(salesUnit => salesUnit, new SalesUnitForTechnicalRequrementsTaskComparer())
                .Select(x => new TechnicalRequrements2Wrapper(new TechnicalRequrements {SalesUnits = x.Select(u => u.Model).ToList()}))
                .ToList();
            TechnicalRequrementsTaskWrapper.Requrements.AddRange(requirements);

            //���������� ������ � �������� ������ � �������
            TechnicalRequrementsTaskWrapper.HistoryElements.Add(new TechnicalRequrementsTaskHistoryElementWrapper(new TechnicalRequrementsTaskHistoryElement
            {
                Type = TechnicalRequrementsTaskHistoryElementType.Create,
                User = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)
            }));
        }
    }
}
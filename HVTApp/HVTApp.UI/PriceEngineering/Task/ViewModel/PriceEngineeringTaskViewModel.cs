using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModel : PriceEngineeringTaskWrapper1, IDisposable
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        private PriceEngineeringTaskViewModel _parent;
        private PriceEngineeringTaskFileTechnicalRequirementsWrapper _selectedTechnicalRequrementsFile;
        private PriceEngineeringTaskFileAnswerWrapper _selectedFileAnswer;
        private PriceEngineeringTaskProductBlockAddedWrapper1 _selectedBlockAdded;

        #region Commands

        public DelegateLogCommand OpenTechnicalRequrementsFileCommand { get; private set; }
        public DelegateLogCommand LoadTechnicalRequrementsFilesCommand { get; private set; }

        public DelegateLogCommand OpenAnswerFileCommand { get; private set; }
        public DelegateLogCommand LoadAnswerFilesCommand { get; private set; }

        public DelegateLogCommand SaveCommand { get; protected set; }

        public DelegateLogCommand StartCommand { get; private set; }

        public event Action TaskIsStarted;

        public DelegateLogCommand ShowReportCommand { get; private set; }

        #endregion

        /// <summary>
        /// ���������� ������ ��� ��������?
        /// </summary>
        public abstract bool IsExpanded { get; }

        /// <summary>
        /// ���������� �������� ������ ��� ��������?
        /// </summary>
        public abstract bool IsExpendedChildPriceEngineeringTasks { get; }

        /// <summary>
        /// ��� ������ �������� �������� ������������
        /// </summary>
        public abstract bool IsTarget { get; }

        /// <summary>
        /// ������ � ������ ��������������
        /// </summary>
        public abstract bool IsEditMode { get; }

        /// <summary>
        /// ����� �� ������������� ����������� �����
        /// </summary>
        public virtual bool AllowEditAddedBlocks => false;

        /// <summary>
        /// ������
        /// </summary>
        public PriceEngineeringTaskStatusEnum Status => this.Model.Status;

        /// <summary>
        /// ������������ �������
        /// </summary>
        public PriceEngineeringTaskViewModel Parent
        {
            get => _parent;
            set
            {
                _parent = value;

                if (_parent == null) return;

                //������������� �� ������� ���������� ������ �� � ������������ �������
                _parent.FilesTechnicalRequirements.CollectionChanged += (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (var file in args.NewItems.Cast<PriceEngineeringTaskFileTechnicalRequirementsWrapper>())
                        {
                            if(IsEditMode)
                                this.FilesTechnicalRequirements.Add(file);
                        }
                    }
                };
            }
        }

        public PriceEngineeringTaskProductBlockAddedWrapper1 SelectedBlockAdded
        {
            get => _selectedBlockAdded;
            set
            {
                if (Equals(_selectedBlockAdded, value)) return;
                _selectedBlockAdded = value;
                SelectedBlockAddedIsChanged?.Invoke();
            }
        }

        /// <summary>
        /// ��������� ���� ��
        /// </summary>
        public PriceEngineeringTaskFileTechnicalRequirementsWrapper SelectedTechnicalRequrementsFile
        {
            get => _selectedTechnicalRequrementsFile;
            set
            {
                if (Equals(value, _selectedTechnicalRequrementsFile)) return;
                _selectedTechnicalRequrementsFile = value;
                SelectedTechnicalRequrementsFileIsChanged?.Invoke();
            }
        }

        public PriceEngineeringTaskFileAnswerWrapper SelectedFileAnswer
        {
            get => _selectedFileAnswer;
            set
            {
                if (Equals(value, _selectedFileAnswer)) return;
                _selectedFileAnswer = value;
                SelectedAnswerFileIsChanged?.Invoke();
            }
        }


        /// <summary>
        /// ������� ��������� ���������� ������������ �����
        /// </summary>
        protected event Action SelectedBlockAddedIsChanged;

        /// <summary>
        /// ������� ��������� ���������� ����� ��
        /// </summary>
        protected event Action SelectedTechnicalRequrementsFileIsChanged;

        /// <summary>
        /// ������� ��������� ���������� ����� ������ ���
        /// </summary>
        protected event Action SelectedAnswerFileIsChanged;

        public event Action<PriceEngineeringTask> PriceEngineeringTaskSaved;

        public event Action<PriceEngineeringTask> PriceEngineeringTaskAccepted;

        protected void InvokePriceEngineeringTaskAccepted()
        {
            this.PriceEngineeringTaskAccepted?.Invoke(this.Model);
        }

        public PriceEngineeringTaskMessenger Messenger { get; private set; }

        #region ctors

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, container, unitOfWork)
        {
            Container = container;
            UnitOfWork = unitOfWork;
            InCtor();
        }

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : this(container, unitOfWork, salesUnits.First().Product)
        {
            this.SalesUnits.AddRange(salesUnits.Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit)));
        }

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : this(container, unitOfWork)
        {
            ProductBlockEngineer = new ProductBlockStructureCostWrapper(product.ProductBlock);
            ProductBlockManager = new ProductBlockEmptyWrapper(product.ProductBlock);

            foreach (var dependentProduct in product.DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    var priceEngineeringTaskViewModel = PriceEngineeringTaskViewModelFactory.GetInstance(Container, UnitOfWork, dependentProduct.Product);
                    this.ChildPriceEngineeringTasks.Add(priceEngineeringTaskViewModel);
                    priceEngineeringTaskViewModel.Parent = this;
                }
            }

            //����
            var department = UnitOfWork.Repository<DesignDepartment>().Find(x => x.ProductBlockIsSuitable(this.ProductBlockEngineer.Model)).FirstOrDefault();
            if (department != null)
                this.DesignDepartment = new DesignDepartmentEmptyWrapper(department);
        }

        private PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork) : base(new PriceEngineeringTask(), container, unitOfWork)
        {
            Container = container;
            UnitOfWork = unitOfWork;
            InCtor();
        }

        #endregion

        /// <summary>
        /// ����� ����������� � ����� ������� ������������
        /// </summary>
        protected virtual void InCtor()
        {
            this.Statuses.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(Status));

            //���� ������ � �������� ��������, ����� �������� ��������������� ������
            if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) == null)
            {
                this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus {StatusEnum = PriceEngineeringTaskStatusEnum.Created}));
            }

            OpenTechnicalRequrementsFileCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //���� ���� ��� � ���������
                        if (string.IsNullOrEmpty(SelectedTechnicalRequrementsFile.Path))
                        {
                            Container.Resolve<IFilesStorageService>().OpenFileFromStorage(SelectedTechnicalRequrementsFile.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath, SelectedTechnicalRequrementsFile.Name);
                        }
                        //���� ���� ��� �� �������� � ���������
                        else
                        {
                            Process.Start(SelectedTechnicalRequrementsFile.Path);
                        }
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("������ ��� �������� ����� ��", e.PrintAllExceptions());
                    }
                },
                () => SelectedTechnicalRequrementsFile != null);

            LoadTechnicalRequrementsFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var files = this.Model.FilesTechnicalRequirements
                        .Where(x => x.IsActual).ToList();
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
                });

            OpenAnswerFileCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //���� ���� ��� � ���������
                        if (string.IsNullOrEmpty(SelectedFileAnswer.Path))
                        {
                            Container.Resolve<IFilesStorageService>().OpenFileFromStorage(SelectedFileAnswer.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath, SelectedFileAnswer.Name);
                        }
                        //���� ���� ��� �� �������� � ���������
                        else
                        {
                            Process.Start(SelectedFileAnswer.Path);
                        }
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("������ ��� �������� �����", e.PrintAllExceptions());
                    }
                },
                () => SelectedFileAnswer != null);


            LoadAnswerFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var files = this.Model.FilesAnswers
                        .Where(x => x.IsActual).ToList();
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath);
                });

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    this.LoadNewTechnicalRequirementFilesInStorage();
                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
                    this.PriceEngineeringTaskSaved?.Invoke(this.Model);
                });

            StartCommand = new DelegateLogCommand(() => { StartCommandExecute(true); },
                                                () => 
                                                    this.IsValid && 
                                                    this.IsChanged && 
                                                    (Status == PriceEngineeringTaskStatusEnum.Created || Status == PriceEngineeringTaskStatusEnum.Stopped) &&
                                                    UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) != null);

            this.PropertyChanged += (sender, args) => StartCommand.RaiseCanExecuteChanged();
            this.Statuses.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(IsEditMode));

            ShowReportCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IDialogService>().Show(this.Model, $"����� �� ���������� ����� {this.Model.ProductBlockEngineer.Designation}");
                    //if (GlobalAppProperties.User.Login == "sivkov")
                    //{
                    //    var blocks = this.ProductBlocksAdded.Select(x => x.Model).ToList();
                    //    Container.Resolve<IJsonService>().WriteJsonFile(blocks, Path.Combine(@"D:\test.json"));
                    //}
                });

            //������������� ���������
            Messenger = new PriceEngineeringTaskMessenger(Container, this);
            Messenger.SendedMessageInNewTask += (authorId, moment, message1) =>
            {
                var message = new PriceEngineeringTaskMessage
                {
                    Author = UnitOfWork.Repository<User>().GetById(authorId),
                    Moment = moment,
                    Message = message1
                };
                var messageWrapper = new PriceEngineeringTaskMessageWrapper(message);
                this.Messages.Add(messageWrapper);
            };

            this.Statuses.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(AllowEditAddedBlocks));
        }

        /// <summary>
        /// ����� ������
        /// </summary>
        /// <param name="saveChanges">��������� � ����� � ������� ���������?</param>
        public void StartCommandExecute(bool saveChanges)
        {
            if (saveChanges)
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("���������� ������", "�� �������?", defaultNo: true) != MessageDialogResult.Yes)
                    return;
            }

            this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus
            {
                StatusEnum = PriceEngineeringTaskStatusEnum.Started
            }));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("������ �������� �� ����������.");
            if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) != null)
            {
                if (this.FilesTechnicalRequirements.IsChanged)
                {
                    sb.AppendLine("������� ��������� � ��.");
                }

                var actualFiles = FilesTechnicalRequirements.Where(x => x.IsActual).OrderBy(x => x.CreationMoment).ToList();
                if (actualFiles.Any())
                {
                    sb.AppendLine("���������� �����:");
                    foreach (var file in actualFiles)
                    {
                        sb.AppendLine($" + {file.CreationMoment} {file.Name}");
                    }
                }

                var notActualFiles = FilesTechnicalRequirements.Where(x => x.IsActual == false).OrderBy(x => x.CreationMoment).ToList();
                if (notActualFiles.Any())
                {
                    sb.AppendLine("�� ���������� �����:");
                    foreach (var file in notActualFiles)
                    {
                        sb.AppendLine($" - {file.CreationMoment} {file.Name}");
                    }
                }

            }

            this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage
            {
                Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                Message = sb.ToString().TrimEnd('\n', '\r')
            }));


            //���� ����������� ������ ���������� ������
            if (saveChanges)
            {
                this.SaveCommand.Execute();
                Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Publish(this.Model);
            }
            //���� ����������� ��� ������ � �������
            else
            {
                this.ChildPriceEngineeringTasks.ForEach(x => x.StartCommandExecute(false));
            }

            StartCommand.RaiseCanExecuteChanged();
            TaskIsStarted?.Invoke();
        }

        /// <summary>
        /// ��������� ��� ����������� ����� �� � ���������
        /// </summary>
        public void LoadNewTechnicalRequirementFilesInStorage()
        {
            foreach (var fileWrapper in this.FilesTechnicalRequirements.AddedItems)
            {
                var destFileName = $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}";
                if (File.Exists(destFileName) == false && string.IsNullOrEmpty(fileWrapper.Path) == false)
                {
                    File.Copy(fileWrapper.Path, destFileName);
                    fileWrapper.Path = null;
                }
            }

            foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
            {
                childPriceEngineeringTask.LoadNewTechnicalRequirementFilesInStorage();
            }
        }

        public IEnumerable<PriceEngineeringTaskViewModel> GetAllPriceEngineeringTaskViewModels()
        {
            yield return this;

            foreach (var childPriceEngineeringTask in ChildPriceEngineeringTasks)
            {
                foreach (var engineeringTaskViewModel in childPriceEngineeringTask.GetAllPriceEngineeringTaskViewModels())
                {
                    yield return engineeringTaskViewModel;
                }
            }
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}
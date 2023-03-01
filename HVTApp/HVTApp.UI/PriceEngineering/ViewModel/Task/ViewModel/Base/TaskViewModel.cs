using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModel : TaskViewModelBase, IDisposable
    {
        private TaskViewModel _parent;
        private PriceEngineeringTaskFileTechnicalRequirementsWrapper _selectedTechnicalRequirementsFile;
        private PriceEngineeringTaskFileAnswerWrapper _selectedFileAnswer;
        private bool _isVisible = true;

        #region Props

        public bool IsVisible
        {
            get => _isVisible;
            set => this.SetProperty(ref _isVisible, value);
        }

        /// <summary>
        /// ��� ������ �������� �������� ������������
        /// </summary>
        public abstract bool IsTarget { get; }

        /// <summary>
        /// ������ � ������ ��������������
        /// </summary>
        public virtual bool IsEditMode => this.Status.PossibleNextSteps.Any();

        /// <summary>
        /// ����� �� ������������� ����������� �����
        /// </summary>
        public virtual bool AllowEditAddedBlocks => false;

        public PriceEngineeringTaskMessenger Messenger { get; private set; }

        /// <summary>
        /// ������������ �������
        /// </summary>
        public TaskViewModel Parent
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
                            if (IsEditMode)
                                this.FilesTechnicalRequirements.Add(file);
                        }
                    }
                };
            }
        }

        /// <summary>
        /// ��������� ���� ��
        /// </summary>
        public PriceEngineeringTaskFileTechnicalRequirementsWrapper SelectedTechnicalRequrementsFile
        {
            get => _selectedTechnicalRequirementsFile;
            set => this.SetProperty(ref _selectedTechnicalRequirementsFile, value, () => SelectedTechnicalRequrementsFileIsChanged?.Invoke());
        }

        public PriceEngineeringTaskFileAnswerWrapper SelectedFileAnswer
        {
            get => _selectedFileAnswer;
            set => this.SetProperty(ref _selectedFileAnswer, value, () => SelectedAnswerFileIsChanged?.Invoke());
        }

        #endregion

        #region Commands

        public DelegateLogCommand OpenTechnicalRequirementsFileCommand { get; private set; }
        public DelegateLogCommand LoadTechnicalRequirementsFilesCommand { get; private set; }

        public DelegateLogCommand OpenAnswerFileCommand { get; private set; }
        public DelegateLogCommand LoadAnswerFilesCommand { get; private set; }

        public DelegateLogCommand SaveCommand { get; private set; }

        public DelegateLogCommand ShowReportCommand { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// ������� ��������� ���������� ����� ��
        /// </summary>
        protected event Action SelectedTechnicalRequrementsFileIsChanged;

        /// <summary>
        /// ������� ��������� ���������� ����� ������ ���
        /// </summary>
        protected event Action SelectedAnswerFileIsChanged;

        #endregion

        #region ctors

        /// <summary>
        /// ��� �������� � �������������� ������������ ������
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected TaskViewModel(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, container.Resolve<IUnitOfWork>(), priceEngineeringTaskId)
        {
        }

        /// <summary>
        /// ��� �������� ����� ������
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        protected TaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        protected override void InCtor()
        {
            base.InCtor();

            #region Commands

            OpenTechnicalRequirementsFileCommand = new DelegateLogCommand(
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

            LoadTechnicalRequirementsFilesCommand = new DelegateLogCommand(
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
                        .Where(fileAnswer => fileAnswer.IsActual).ToList();
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath);
                });

            SaveCommand = new DelegateLogCommand(SaveCommand_ExecuteMethod, SaveCommand_CanExecuteMethod);

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

            #endregion

            //������������� ���������
            Messenger = new PriceEngineeringTaskMessenger(this, Container);

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(IsEditMode));
            };
        }

        #endregion

        protected virtual void SaveCommand_ExecuteMethod()
        {
            this.AcceptChanges();
            UnitOfWork.SaveChanges();
            Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
        }

        protected virtual bool SaveCommand_CanExecuteMethod()
        {
            return this.IsValid && this.IsChanged;
        }

        public IEnumerable<TaskViewModel> GetAllPriceEngineeringTaskViewModels()
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
            Messenger.Dispose();
        }
    }
}
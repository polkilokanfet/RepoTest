using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.ParametersService1;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelConstructor : TaskWrapperConstructor
    {
        private TaskProductBlockAddedWrapperConstructor _selectedBlockAdded;
        private readonly TaskViewModelConstructor _parentTask;

        public override bool IsTarget => Equals(Model.UserConstructor?.Id, GlobalAppProperties.User.Id);

        public override bool IsEditMode
        {
            get
            {
                if (IsTarget == false) return false;

                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Started:
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                    case PriceEngineeringTaskStatusEnum.VerificationRejectedByHead:
                        return true;
                }
                return false;
            }
        }

        public override bool AllowEditAddedBlocks => IsEditMode;

        public TaskProductBlockAddedWrapperConstructor SelectedBlockAdded
        {
            get => _selectedBlockAdded;
            set
            {
                if (Equals(_selectedBlockAdded, value)) return;
                _selectedBlockAdded = value;
                RemoveBlockAddedCommand.RaiseCanExecuteChanged();
            }
        }

        #region Commands

        /// <summary>
        /// Выбрать блок продукта
        /// </summary>
        public DelegateLogCommand SelectProductBlockCommand { get; private set; }
        /// <summary>
        /// Команда добавления зависиммых блоков (например ЗИПов)
        /// </summary>
        public DelegateLogCommand AddBlockAddedCommand { get; private set; }
        public DelegateLogCommand AddBlockAddedComplectCommand { get; private set; }
        public DelegateLogConfirmationCommand RemoveBlockAddedCommand { get; private set; }
        public DelegateLogCommand AddAnswerFilesCommand { get; private set; }
        public DelegateLogConfirmationCommand RemoveAnswerFileCommand { get; private set; }
        public DelegateLogConfirmationCommand FinishCommand { get; private set; }
        public DelegateLogConfirmationCommand RejectCommand { get; private set; }
        public DelegateLogCommand BlockAddedNewParameterCommand { get; private set; }
        public DelegateLogCommand BlockNewParameterCommand { get; private set; }

        /// <summary>
        /// Команда создания подзадачи (например, добавление площадки обслуживания).
        /// </summary>
        public DelegateLogCommand CreateSubTaskCommand { get; private set; }
        /// <summary>
        /// Команда удаления подзадачи (например, добавление площадки обслуживания).
        /// </summary>
        public DelegateLogConfirmationCommand RemoveSubTaskCommand { get; }

        public DelegateLogCommand LoadJsonFileCommand { get; private set; }

        #endregion

        #region ctors

        public TaskViewModelConstructor(IUnityContainer container, Guid priceEngineeringTaskId, TaskViewModelConstructor parentTask) : base(container, priceEngineeringTaskId)
        {
            _parentTask = parentTask;
            var messageService = container.Resolve<IMessageService>();

            var vms = Model.ChildPriceEngineeringTasks.Select(priceEngineeringTask => new TaskViewModelConstructor(container, priceEngineeringTask.Id, this));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(vms);

            //Обязательные параметры главного блока продукта задачи
            var productBlockRequiredParameters = Model.DesignDepartment
                .ParameterSets
                .FirstOrDefault(x => x.Parameters.AllContainsInById(Model.ProductBlockManager.Parameters))?
                .Parameters.ToList();

            #region Commands

            SelectProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var originProductBlock = this.ProductBlockEngineer.Model;
                    var selectedProductBlock = Container.Resolve<IGetProductService>().GetProductBlock(originProductBlock, productBlockRequiredParameters);
                    if (originProductBlock.Id != selectedProductBlock.Id)
                    {
                        this.ProductBlockEngineer.RejectChanges();

                        //если выбрали пустой КТТ
                        var emptyParameter = GlobalAppProperties.Actual.EmptyParameterCurrentTransformersSet;
                        if (emptyParameter != null && selectedProductBlock.Parameters.ContainsById(emptyParameter))
                        {
                            var dr = messageService.ShowYesNoMessageDialog("Пустой КТТ", "Вы выбрали КТТ без ТТ. Хотите ли Вы запустить подбор ТТ?", defaultYes:true);
                            if (dr == MessageDialogResult.Yes)
                            {
                                var rp = productBlockRequiredParameters.ToList();
                                rp.Add(GlobalAppProperties.Actual.ParameterCurrentTransformersSetCustom);
                                var product = Container.Resolve<IGetProductService>().GetProduct(rp);
                                if (product != null)
                                {
                                    foreach (var block in product.GetBlocks())
                                    {
                                        if (productBlockRequiredParameters.AllContainsInById(block.Parameters))
                                        {
                                            continue;
                                        }
                                        AddAddedBlock(block);
                                    }
                                }
                            }
                        }

                        this.ProductBlockEngineer.RejectChanges();
                        this.ProductBlockEngineer = new ProductBlockStructureCostWrapperConstructor(UnitOfWork.Repository<ProductBlock>().GetById(selectedProductBlock.Id));
                    }
                },
                () => IsTarget && IsEditMode);

            AddBlockAddedCommand = new DelegateLogCommand(
                () =>
                {
                    var block = Container.Resolve<IGetProductService>().GetProductBlock(Model.DesignDepartment.ParameterSetsAddedBlocks);
                    if (block == null) return;
                    AddAddedBlock(block);
                },
                () => IsTarget && IsEditMode);

            AddBlockAddedComplectCommand = new DelegateLogCommand(
                () =>
                {
                    var complect = Container.Resolve<IGetProductService>().GetComplect();
                    if (complect == null) return;
                    complect = UnitOfWork.Repository<Product>().GetById(complect.Id);
                    var wrapper = new TaskProductBlockAddedWrapperConstructor(new PriceEngineeringTaskProductBlockAdded())
                    {
                        ProductBlock = new ProductBlockStructureCostWrapperConstructor(complect.ProductBlock)
                    };
                    this.ProductBlocksAdded.Add(wrapper);
                },
                () => IsTarget && IsEditMode);

            RemoveBlockAddedCommand = new DelegateLogConfirmationCommand(
                messageService, 
                "Вы уверены, что хотите удалить выделенное дополнительное оборудование?",
                () =>
                {
                    SelectedBlockAdded.RejectChanges();

                    if (SelectedBlockAdded.Model.StructureCostVersions.Any())
                    {
                        SelectedBlockAdded.IsRemoved = true;
                    }
                    else
                    {
                        if (UnitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().GetById(SelectedBlockAdded.Model.Id) != null)
                        {
                            UnitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().Delete(SelectedBlockAdded.Model);
                        }

                        this.ProductBlocksAdded.Remove(SelectedBlockAdded);
                    }
                    SelectedBlockAdded = null;
                },
                () => IsTarget && IsEditMode && SelectedBlockAdded != null);

            AddAnswerFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //копируем каждый файл
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            var fileWrapper = new PriceEngineeringTaskFileAnswerWrapper(new PriceEngineeringTaskFileAnswer())
                            {
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLength(50),
                                Path = fileName
                            };
                            this.FilesAnswers.Add(fileWrapper);
                        }
                    }
                },
                () => IsTarget && IsEditMode == true);

            RemoveAnswerFileCommand = new DelegateLogConfirmationCommand(
                messageService,
                "Вы уверены, что хотите удалить выделенный файл?",
                () =>
                {
                    if (string.IsNullOrEmpty(SelectedFileAnswer.Path))
                    {
                        SelectedFileAnswer.IsActual = false;
                    }
                    else
                    {
                        this.FilesAnswers.Remove(SelectedFileAnswer);
                    }
                }, 
                () => IsTarget && IsEditMode && SelectedFileAnswer != null);

            FinishCommand = new DelegateLogConfirmationCommand(
                messageService,
                "Вы уверены, что хотите завершить проработку?",
                () =>
                {
                    if (this.Model.RequestForVerificationFromHead == false)
                    {
                        var dr = messageService.ShowYesNoMessageDialog("Проверка", "Хотите проверить результаты проработки?", defaultNo: true);
                        this.RequestForVerificationFromConstructor = dr == MessageDialogResult.Yes;
                    }

                    var needVerification = this.Model.RequestForVerificationFromHead || this.RequestForVerificationFromConstructor;

                    var sb = new StringBuilder()
                        .AppendLine("Информация о результатах проработки.")
                        .AppendLine("Основной блок:")
                        .AppendLine($" - {this.ProductBlockEngineer.PrintToMessage()}");

                    var pba = this.ProductBlocksAdded.Where(x => x.Model.IsRemoved == false).ToList();
                    if (pba.Any())
                    {
                        sb.AppendLine("Добавленные блоки:");
                        pba.ForEach(x => sb.AppendLine($" - {x}"));
                    }

                    var fa = this.FilesAnswers.Where(x => x.IsActual).ToList();
                    if (fa.Any())
                    {
                        sb.AppendLine("Актуальные приложенные файлы ОГК:");
                        fa.ForEach(x => sb.AppendLine($" - {x}"));
                    }

                    var statusEnum = needVerification
                        ? PriceEngineeringTaskStatusEnum.VerificationRequestedByConstructor
                        : PriceEngineeringTaskStatusEnum.FinishedByConstructor;
                    Statuses.Add(statusEnum);
                    Messenger.SendMessage(sb.ToString().TrimEnd('\n', '\r'));
                    SaveCommand.Execute();

                    AddAnswerFilesCommand.RaiseCanExecuteChanged();
                    RemoveAnswerFileCommand.RaiseCanExecuteChanged();

                    if (needVerification)
                    {
                        Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Publish(this.Model);
                    }
                    else
                    {
                        Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(this.Model);
                    }
                },
                () => IsTarget && IsEditMode && this.IsValid);

            RejectCommand = new DelegateLogConfirmationCommand(
                messageService,
"Вы уверены, что хотите отклонить проработку задачи?",
                () =>
                {
                    this.RejectChanges();
                    this.Statuses.Add(PriceEngineeringTaskStatusEnum.RejectedByConstructor);
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Publish(this.Model);
                },
                () => IsEditMode);

            BlockAddedNewParameterCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IDialogService>().ShowDialog(new ParametersServiceViewModel(Container, this.Model.DesignDepartment));
                },
                () => IsEditMode);

            BlockNewParameterCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IDialogService>().ShowDialog(new ParametersServiceViewModel(Container, this.Model.ProductBlockEngineer, productBlockRequiredParameters));
                },
                () => IsEditMode);

            CreateSubTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var getProductService = Container.Resolve<IGetProductService>();

                    var block = getProductService.GetProductBlock(Model.DesignDepartment.ParameterSetsSubTask);
                    if (block == null) return;

                    var unitOfWork = this.Container.Resolve<IUnitOfWork>();
                    block = unitOfWork.Repository<ProductBlock>().GetById(block.Id);
                    var product = getProductService.GetProduct(unitOfWork, new Product { ProductBlock = block });

                    var taskViewModel = new TaskViewModelManagerNew(Container, unitOfWork, product)
                    {
                        ParentPriceEngineeringTaskId = this.Model.Id,
                        Amount = 1
                    };
                    taskViewModel.Model.UserConstructorInitiator = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                    taskViewModel.FilesTechnicalRequirements.AddRange(this.FilesTechnicalRequirements.Where(x => x.IsActual).Select(x => new PriceEngineeringTaskFileTechnicalRequirementsWrapper(unitOfWork.Repository<PriceEngineeringTaskFileTechnicalRequirements>().GetById(x.Id))));

                    if (taskViewModel.StartCommandExecute(true))
                    {
                        this.ChildPriceEngineeringTasks.Add(new TaskViewModelConstructor(this.Container, taskViewModel.Model.Id, this));
                    }
                },
                () => IsTarget && IsEditMode && this.Model.DesignDepartment.ParameterSetsSubTask.Any());

            RemoveSubTaskCommand = new DelegateLogConfirmationCommand(
                messageService,
                "Вы уверены, что хотите удалить созданную Вами подзадачу?",
                () =>
                {
                    _parentTask.RemoveChildTask(this.Model);
                },
                () => _parentTask != null && this.Model.UserConstructorInitiator?.Id == GlobalAppProperties.User.Id);

            LoadJsonFileCommand = new DelegateLogCommand(
                () =>
                {
                    var fPath = Container.Resolve<IFilesStorageService>().GetFolderPath();
                    var blocks = Container.Resolve<IJsonService>().ReadJsonFile<List<PriceEngineeringTaskProductBlockAdded>>($"{fPath}\\test.json");
                    this.ProductBlocksAdded.AddRange(blocks.Select(x => new TaskProductBlockAddedWrapperConstructor(x)));
                });

            #endregion

            this.SelectedAnswerFileIsChanged += () => RemoveAnswerFileCommand.RaiseCanExecuteChanged();

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(AllowEditAddedBlocks));
            };
        }

        #endregion

        protected override void SaveCommand_ExecuteMethod()
        {
            this.LoadNewAnswerFilesInStorage();
            base.SaveCommand_ExecuteMethod();
        }

        protected override bool SaveCommand_CanExecuteMethod()
        {
            return IsTarget && IsEditMode && this.IsValid && this.IsChanged;
        }


        private void AddAddedBlock(ProductBlock block)
        {
            block = UnitOfWork.Repository<ProductBlock>().GetById(block.Id);
            var wrapper = new TaskProductBlockAddedWrapperConstructor(new PriceEngineeringTaskProductBlockAdded())
            {
                ProductBlock = new ProductBlockStructureCostWrapperConstructor(block)
            };
            this.ProductBlocksAdded.Add(wrapper);
        }

        /// <summary>
        /// Загрузить все добавленные ответы ОГК в хранилище
        /// </summary>
        public void LoadNewAnswerFilesInStorage()
        {
            foreach (var file in this.FilesAnswers.AddedItems.Where(x => string.IsNullOrWhiteSpace(x.Path) == false))
            {
                file.LoadToStorage(GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath);
            }
        }

        public void RemoveChildTask(PriceEngineeringTask priceEngineeringTask)
        {
            var taskViewModel = this.ChildPriceEngineeringTasks.Single(x => x.Model.Id == priceEngineeringTask.Id);
            ChildPriceEngineeringTasks.Remove(taskViewModel);

            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var task = unitOfWork.Repository<PriceEngineeringTask>().GetById(taskViewModel.Model.Id);
            task.FilesTechnicalRequirements.Clear();

            //task.FilesAnswers.ForEach(x => unitOfWork.RemoveEntity(x));
            //task.Messages.ForEach(x => unitOfWork.RemoveEntity(x));
            //task.ProductBlocksAdded.ForEach(x => unitOfWork.RemoveEntity(x));
            //task.Statuses.ForEach(x => unitOfWork.RemoveEntity(x));

            unitOfWork.RemoveEntity(task);

            unitOfWork.SaveChanges();
        }
    }
}
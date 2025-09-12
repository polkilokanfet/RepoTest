using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.ParametersService1;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelConstructor : TaskViewModelBaseConstructor
    {
        private TaskProductBlockAddedWrapperConstructor _selectedBlockAdded;
        private readonly TaskViewModelConstructor _parentTask;

        public override bool IsTarget => Equals(Model.UserConstructor?.Id, GlobalAppProperties.User.Id);

        public override bool IsEditMode
        {
            get
            {
                if (IsTarget == false) return false;
                
                var steps = new List<ScriptStep>
                {
                    ScriptStep.Start,
                    ScriptStep.RejectByManager,
                    ScriptStep.VerificationReject
                };

                return steps.Contains(Status);
            }
        }

        public override bool AllowEditAddedBlocks => IsEditMode;

        public TaskProductBlockAddedWrapperConstructor SelectedBlockAdded
        {
            get => _selectedBlockAdded;
            set => this.SetProperty(ref _selectedBlockAdded, value,
                () =>
                {
                    RemoveBlockAddedCommand.RaiseCanExecuteChanged();
                    CopyProductBlockAddedCommand.RaiseCanExecuteChanged();
                });
        }

        #region Buffer

        private static ProductBlock BufferProductBlock;
        private static PriceEngineeringTaskProductBlockAdded BufferProductBlockAdded;
        private static PriceEngineeringTask BufferPriceEngineeringTask;

        #endregion

        #region Commands

        /// <summary>
        /// ������� ���� ��������
        /// </summary>
        public DelegateLogCommand SelectProductBlockCommand { get; }
        /// <summary>
        /// ������� ���������� ���������� ������ (�������� �����)
        /// </summary>
        public DelegateLogCommand AddBlockAddedCommand { get; }
        public DelegateLogCommand AddBlockAddedComplectCommand { get; }
        public DelegateLogConfirmationCommand RemoveBlockAddedCommand { get; }
        public DelegateLogCommand AddAnswerFilesCommand { get; }
        public DelegateLogConfirmationCommand RemoveAnswerFileCommand { get; }
        public ICommandRaiseCanExecuteChanged FinishCommand { get; }
        public ICommandRaiseCanExecuteChanged RejectCommand { get; }
        public DelegateLogCommand BlockAddedNewParameterCommand { get; }
        public DelegateLogCommand BlockNewParameterCommand { get; }

        /// <summary>
        /// ������� �������� ��������� (��������, ���������� �������� ������������).
        /// </summary>
        public DelegateLogCommand CreateSubTaskCommand { get; }
        /// <summary>
        /// ������� �������� ��������� (��������, ���������� �������� ������������).
        /// </summary>
        public DelegateLogConfirmationCommand RemoveSubTaskCommand { get; }

        public DelegateLogCommand LoadJsonFileCommand { get; }

        /// <summary>
        /// ������� ����������� ��������� ����� � ����� ������
        /// </summary>
        public DelegateLogCommand CopyProductBlockCommand { get; }

        /// <summary>
        /// ������� ������� ��������� ����� �� ������ ������
        /// </summary>
        public DelegateLogConfirmationCommand PasteProductBlockCommand { get; }

        /// <summary>
        /// ������� ����������� ������������ ����� � ����� �����
        /// </summary>
        public DelegateLogCommand CopyProductBlockAddedCommand { get; }

        /// <summary>
        /// ������� ������� ������������ ����� �� ������ ������
        /// </summary>
        public DelegateLogCommand PasteProductBlockAddedCommand { get; }

        /// <summary>
        /// ������� ����������� ���� ������ � ����� ������
        /// </summary>
        public DelegateLogCommand CopyPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// ������� ������� ���� ������ �� ������ ������
        /// </summary>
        public DelegateLogConfirmationCommand PastePriceEngineeringTaskCommand { get; }

        #endregion

        #region ctor

        private readonly IMessageService _messageService;

        public TaskViewModelConstructor(IUnityContainer container, Guid priceEngineeringTaskId, TaskViewModelConstructor parentTask) : base(container, priceEngineeringTaskId)
        {
            _parentTask = parentTask;
            _messageService = container.Resolve<IMessageService>();
            var eventAggregator = container.Resolve<IEventAggregator>();

            var vms = Model.ChildPriceEngineeringTasks.Select(priceEngineeringTask => new TaskViewModelConstructor(container, priceEngineeringTask.Id, this));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(vms);

            //������������ ��������� �������� ����� �������� ������
            var productBlockRequiredParameters = Model.DesignDepartment
                .ParameterSets
                .FirstOrDefault(parameters => parameters.Parameters.AllContainsInById(Model.ProductBlockManager.Parameters))?
                .Parameters.ToList();

            #region Commands

            SelectProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    if (this.Model.ProductBlock.IsService)
                    {
                        _messageService.Message("������ �� ������� ������� ��. ��������� ��� ������� ���������� ��������� � ������� �������������� �����.");
                        return;
                    }

                    var originProductBlock = this.ProductBlockEngineer.Model;
                    var selectedProductBlock = this.DesignDepartment.IsKitDepartment 
                        ? Container.Resolve<IGetProductService>().GetKit(this.DesignDepartment)?.ProductBlock
                        : Container.Resolve<IGetProductService>().GetProductBlock(originProductBlock, productBlockRequiredParameters);

                    if (selectedProductBlock == null || 
                        originProductBlock.Id == selectedProductBlock.Id) return;

                    this.ProductBlockEngineer.RejectChanges();

                    //���� ������� ������ ���
                    var emptyParameter = GlobalAppProperties.Actual.EmptyParameterCurrentTransformersSet;
                    if (emptyParameter != null && selectedProductBlock.Parameters.ContainsById(emptyParameter))
                    {
                        var dr = _messageService.ConfirmationDialog("������ ���", "�� ������� ��� ��� ��. ������ �� �� ��������� ������ ��?", defaultYes:true);
                        if (dr)
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
                },
                () => IsTarget && IsEditMode);

            AddBlockAddedCommand = new DelegateLogCommand(
                () =>
                {
                    var addedBlocksList = Model.DesignDepartment.ParameterSetsAddedBlocks;
                    if (addedBlocksList.Any() == false)
                    {
                        _messageService.Message("������ �� �� ��������� �� ������ ��������������� �����. ���� ���������� �� ��������, ���������� � ���������� �.�.");
                        return;
                    }

                    var block = Container.Resolve<IGetProductService>().GetProductBlock(addedBlocksList);
                    if (block == null) return;
                    AddAddedBlock(block);
                },
                () => IsTarget && IsEditMode);

            AddBlockAddedComplectCommand = new DelegateLogCommand(
                () =>
                {
                    var kit = Container.Resolve<IGetProductService>().GetKit(this.DesignDepartment);
                    if (kit == null) return;
                    kit = UnitOfWork.Repository<Product>().GetById(kit.Id);
                    if (kit.DesignDepartmentsKits.ContainsById(this.Model.DesignDepartment) == false)
                    {
                        kit.DesignDepartmentsKits.Add(UnitOfWork.Repository<DesignDepartment>().GetById(this.Model.DesignDepartment.Id));
                    }
                    var wrapper = new TaskProductBlockAddedWrapperConstructor(new PriceEngineeringTaskProductBlockAdded())
                    {
                        ProductBlock = new ProductBlockStructureCostWrapperConstructor(kit.ProductBlock)
                    };
                    this.ProductBlocksAdded.Add(wrapper);
                },
                () => IsTarget && IsEditMode);

            RemoveBlockAddedCommand = new DelegateLogConfirmationCommand(
                _messageService, 
                "�� �������, ��� ������ ������� ���������� �������������� ������������?",
                () =>
                {
                    this.RemoveBlockAdded(SelectedBlockAdded.Model);
                    SelectedBlockAdded = null;
                },
                () => IsTarget && IsEditMode && SelectedBlockAdded != null);

            AddAnswerFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var fileNames = container.Resolve<IGetFilePaths>().GetFilePaths().ToList();
                    if (fileNames.Any() == false) return;

                    //�������� ������ ����
                    foreach (var fileName in fileNames)
                    {
                        var fileWrapper = new PriceEngineeringTaskFileAnswerWrapper(new PriceEngineeringTaskFileAnswer())
                        {
                            Name = Path.GetFileNameWithoutExtension(fileName).LimitLength(50),
                            Path = fileName
                        };
                        this.FilesAnswers.Add(fileWrapper);
                    }
                },
                () => IsTarget && IsEditMode == true);

            RemoveAnswerFileCommand = new DelegateLogConfirmationCommand(
                _messageService,
                "�� �������, ��� ������ ������� ���������� ����?",
                () => { this.FilesAnswers.Remove(SelectedFileAnswer); }, 
                () => IsTarget && IsEditMode && SelectedFileAnswer != null);

            FinishCommand = new DoStepCommandFinishByConstructor(this, container);

            RejectCommand = new DoStepCommandRejectedByConstructor(this, container);

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
                    if (this.Model.DesignDepartment.ParameterSetsSubTask.Any() == false)
                    {
                        _messageService.Message("����������", "���� �� �� ����� ��������� ��� ���������.");
                        return;
                    }

                    var getProductService = Container.Resolve<IGetProductService>();

                    var block = getProductService.GetProductBlock(Model.DesignDepartment.ParameterSetsSubTask);
                    if (block == null) return;

                    using (var unitOfWork = Container.Resolve<IUnitOfWorkFactory>().GetUnitOfWork())
                    {
                        block = unitOfWork.Repository<ProductBlock>().GetById(block.Id);
                        var product = getProductService.GetProduct(unitOfWork, new Product { ProductBlock = block });

                        var taskViewModel = new TaskViewModelManagerNew(Container, unitOfWork, product)
                        {
                            ParentPriceEngineeringTaskId = this.Model.Id,
                            Amount = 1
                        };

                        if (taskViewModel.DesignDepartment == null)
                        {
                            _messageService.Message("������", $"������� �������� �� ��������� ��. ���������� ��� ���������� � ��������������:\n{product.ProductBlock.ParametersOrdered.ToStringEnum("\n")}");
                            return;
                        }

                        taskViewModel.Model.Statuses.Where(status => status.StatusEnum == ScriptStep.Create.Value).ForEach(status => status.Moment = DateTime.Now.AddSeconds(-3));
                        taskViewModel.Model.UserConstructorInitiator = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                        taskViewModel.FilesTechnicalRequirements.AddRange(this.FilesTechnicalRequirements.Where(file => file.IsActual).Select(fileWrapper => new PriceEngineeringTaskFileTechnicalRequirementsWrapper(unitOfWork.Repository<PriceEngineeringTaskFileTechnicalRequirements>().GetById(fileWrapper.Id))));

                        taskViewModel.StartCommand.ExecuteWithoutConfirmation();
                        this.ChildPriceEngineeringTasks.Add(new TaskViewModelConstructor(this.Container, taskViewModel.Model.Id, this));
                    }
                },
                () => IsTarget && IsEditMode);

            RemoveSubTaskCommand = new DelegateLogConfirmationCommand(
                _messageService,
                "�� �������, ��� ������ ������� ��������� ���� ���������?",
                () =>
                {
                    _parentTask.RemoveChildTask(this.Model);
                },
                () => _parentTask != null && this.Model.UserConstructorInitiator?.Id == GlobalAppProperties.User.Id);

            LoadJsonFileCommand = new DelegateLogCommand(
                () =>
                {
                    var fPath = Container.Resolve<IFilesStorageService>().GetDirectoryPath();
                    var blocks = Container.Resolve<IJsonService>().ReadJsonFile<List<PriceEngineeringTaskProductBlockAdded>>($"{fPath}\\test.json");
                    this.ProductBlocksAdded.AddRange(blocks.Select(x => new TaskProductBlockAddedWrapperConstructor(x)));
                });


            eventAggregator.GetEvent<CopyProductBlockEvent>().Subscribe(() =>
            {
                this.PasteProductBlockCommand.RaiseCanExecuteChanged();
            });

            eventAggregator.GetEvent<CopyProductBlockAddedEvent>().Subscribe(() =>
            {
                this.PasteProductBlockAddedCommand.RaiseCanExecuteChanged();
            });

            eventAggregator.GetEvent<CopyPriceEngineeringTaskEvent>().Subscribe(() =>
            {
                this.PastePriceEngineeringTaskCommand.RaiseCanExecuteChanged();
            });

            CopyProductBlockCommand = new DelegateLogCommand(() =>
            {
                BufferProductBlock = this.Model.ProductBlock;
                eventAggregator.GetEvent<CopyProductBlockEvent>().Publish();
            });

            CopyProductBlockAddedCommand = new DelegateLogCommand(() =>
            {
                BufferProductBlockAdded = this.SelectedBlockAdded.Model;
                eventAggregator.GetEvent<CopyProductBlockAddedEvent>().Publish();
            }, () => this.IsTarget && this.SelectedBlockAdded != null);

            CopyPriceEngineeringTaskCommand = new DelegateLogCommand(() =>
            {
                BufferPriceEngineeringTask = this.Model;
                eventAggregator.GetEvent<CopyPriceEngineeringTaskEvent>().Publish();
            }, () => this.IsTarget);

            PasteProductBlockCommand = new DelegateLogConfirmationCommand(
                _messageService,
                "�� �������, ��� ������ �������� ���� ������������ �� ������ ������?",
                () => this.PasteProductBlock(BufferProductBlock),
                () => this.IsEditMode && BufferProductBlock != null);

            PasteProductBlockAddedCommand = new DelegateLogCommand(
                () => this.PasteProductBlockAdded(BufferProductBlockAdded),
                () => this.IsEditMode && BufferProductBlockAdded != null);

            PastePriceEngineeringTaskCommand = new DelegateLogConfirmationCommand(
                _messageService,
                "�� �������, ��� ������ �������� ��� ������ ������ �� ������ ������?",
                () =>
                {
                    if (!this.PasteProductBlock(BufferPriceEngineeringTask.ProductBlock)) return;

                    foreach (var blockAdded in this.ProductBlocksAdded.Select(ba => ba.Model).ToList())
                    {
                        this.RemoveBlockAdded(blockAdded);
                    }

                    foreach (var blockAdded in BufferPriceEngineeringTask.ProductBlocksAdded)
                    {
                        this.PasteProductBlockAdded(blockAdded);
                    }
                },
                () => this.IsEditMode && BufferPriceEngineeringTask != null);


            #endregion

            this.SelectedAnswerFileIsChanged += () => RemoveAnswerFileCommand.RaiseCanExecuteChanged();

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(AllowEditAddedBlocks));
            };
        }

        #endregion

        private bool RemoveBlockAdded(PriceEngineeringTaskProductBlockAdded ba)
        {
            var blockAdded = this.ProductBlocksAdded.Single(x => x.Model.Id == ba.Id);
            blockAdded.RejectChanges();
            if (blockAdded.Model.StructureCostVersions.Any())
            {
                blockAdded.IsRemoved = true;
            }
            else
            {
                if (blockAdded.Model.PriceEngineeringTaskId == this.Model.Id &&
                    UnitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().GetById(blockAdded.Model.Id) != null)
                {
                    UnitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().Delete(blockAdded.Model);
                }

                this.ProductBlocksAdded.Remove(blockAdded);
            }

            return true;
        }

        private bool PasteProductBlock(ProductBlock productBlock)
        {
            if (this.Model.DesignDepartment.ProductBlockIsSuitable(productBlock) == false)
            {
                _messageService.Message($"���� <{productBlock.Designation}> �� �������� ��� �� <{this.Model.DesignDepartment}>");
                return false;
            }

            this.ProductBlockEngineer = new ProductBlockStructureCostWrapperConstructor(this.UnitOfWork.Repository<ProductBlock>().GetById(productBlock.Id));
            return true;
        }

        private bool PasteProductBlockAdded(PriceEngineeringTaskProductBlockAdded blockAdded)
        {
            this.ProductBlocksAdded.Add(
                new TaskProductBlockAddedWrapperConstructor(
                    new PriceEngineeringTaskProductBlockAdded
                    {
                        Amount = blockAdded.Amount,
                        ProductBlock = this.UnitOfWork.Repository<ProductBlock>().GetById(blockAdded.ProductBlock.Id),
                        IsOnBlock = blockAdded.IsOnBlock
                    }));

            return true;
        }

        protected override void SaveCommand_ExecuteMethod()
        {
            //���������� ����� �� ����� ������������
            var targetProductBlocks = ProductBlocksAdded
                .Where(x => x.ProductBlock.StructureCostNumberIsChanged)
                .Where(x => string.IsNullOrWhiteSpace(x.ProductBlock.StructureCostNumberOriginalValue) == false)
                .Select(x => x.ProductBlock)
                .ToList();
            if (ProductBlockEngineer.StructureCostNumberIsChanged)
                if (string.IsNullOrWhiteSpace(ProductBlockEngineer.StructureCostNumberOriginalValue) == false)
                    targetProductBlocks.Add(ProductBlockEngineer);

            if (targetProductBlocks.Any())
            {
                var sb = new StringBuilder();
                sb.AppendLine("������� �� ��������� ������� �������������:");
                var moment = DateTime.Now;

                foreach (var pb in targetProductBlocks)
                {
                    var ut = new UpdateStructureCostNumberTaskForConstructorViewModel(new UpdateStructureCostNumberTask())
                    {
                        ProductBlock = new ProductBlockEmptyWrapper(pb.Model),
                        MomentStart = moment,
                        StructureCostNumber = pb.StructureCostNumber,
                        StructureCostNumberOriginal = pb.StructureCostNumberOriginalValue
                    };
                    this.UpdateStructureCostNumberTasks.Add(ut);
                    sb.AppendLine($" - {ut.Model.ToString()};");
                }

                Messenger.SendMessage(sb.ToString(), false);
            }

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

            task.ProductBlocksAdded
                .SelectMany(blockAdded => blockAdded.StructureCostVersions)
                .ForEach(structureCostVersion => unitOfWork.RemoveEntity(structureCostVersion));

            task.UpdateStructureCostNumberTasks
                .ToList()
                .ForEach(updateStructureCostNumberTask => unitOfWork.Repository<UpdateStructureCostNumberTask>().Delete(updateStructureCostNumberTask));

            unitOfWork.RemoveEntity(task);

            unitOfWork.SaveChanges();
        }
    }

    public class CopyProductBlockEvent : PubSubEvent { }
    public class CopyProductBlockAddedEvent : PubSubEvent { }
    public class CopyPriceEngineeringTaskEvent : PubSubEvent { }
}
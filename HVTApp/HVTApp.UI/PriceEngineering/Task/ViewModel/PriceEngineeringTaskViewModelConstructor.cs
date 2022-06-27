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
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelConstructor : PriceEngineeringTaskViewModel
    {
        public override bool IsExpanded => UserConstructor != null && this.Model.GetSuitableTasksForWork(GlobalAppProperties.User).Any();

        public override bool IsExpendedChildPriceEngineeringTasks
        {
            get
            {
                var priceEngineeringTasks = this.Model.GetSuitableTasksForWork(GlobalAppProperties.User).ToList();
                priceEngineeringTasks.RemoveIfContainsById(this.Model);
                return priceEngineeringTasks.Any();
            }
        }

        public override bool IsTarget => UserConstructor != null && Equals(Model.UserConstructor.Id, GlobalAppProperties.User.Id);

        public override bool IsEditMode
        {
            get
            {
                if (IsTarget == false) return false;

                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Started:
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                    case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                        return true;
                }
                return false;
            }
        }

        public override bool AllowEditAddedBlocks => IsEditMode;

        private List<Parameter> ProductBlockRequiredParameters { get; set; }

        #region Commands

        /// <summary>
        /// Выбрать блок продукта
        /// </summary>
        public DelegateLogCommand SelectProductBlockCommand { get; private set; }
        public DelegateLogCommand AddBlockAddedCommand { get; private set; }
        public DelegateLogCommand AddBlockAddedComplectCommand { get; private set; }
        public DelegateLogCommand RemoveBlockAddedCommand { get; private set; }
        public DelegateLogCommand AddAnswerFilesCommand { get; private set; }
        public DelegateLogCommand RemoveAnswerFileCommand { get; private set; }
        public DelegateLogCommand FinishCommand { get; private set; }
        public DelegateLogCommand RejectCommand { get; private set; }
        public DelegateLogCommand BlockAddedNewParameterCommand { get; private set; }
        public DelegateLogCommand BlockNewParameterCommand { get; private set; }

        /// <summary>
        /// Команда создания подзадачи (например, добавление площадки обслуживания).
        /// </summary>
        public DelegateLogCommand CreateSubTaskCommand { get; private set; }

        public DelegateLogCommand LoadJsonFileCommand { get; private set; }

        #endregion

        #region ctors

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, PriceEngineeringTask priceEngineeringTask) : base(container, priceEngineeringTask)
        {
            var vms = Model.ChildPriceEngineeringTasks.Select(x => new PriceEngineeringTaskViewModelConstructor(container, x));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(vms);
            //RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }

        #endregion

        protected override void InCtor()
        {
            base.InCtor();

            ProductBlockRequiredParameters = DesignDepartment
                .Model
                .ParameterSets
                .FirstOrDefault(x => x.Parameters.AllContainsInById(ProductBlockManager.Model.Parameters))?
                .Parameters.ToList();

            SelectProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var originProductBlock = this.ProductBlockEngineer.Model;
                    var selectedProductBlock = Container.Resolve<IGetProductService>().GetProductBlock(originProductBlock, ProductBlockRequiredParameters);
                    if (originProductBlock.Id != selectedProductBlock.Id)
                    {
                        this.ProductBlockEngineer.RejectChanges();

                        //если выбрали пустой КТТ
                        var emptyParameter = GlobalAppProperties.Actual.EmptyParameterCurrentTransformersSet;
                        if (emptyParameter != null && selectedProductBlock.Parameters.ContainsById(emptyParameter))
                        {
                            var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Пустой КТТ", "Вы выбрали КТТ без ТТ. Хотите ли Вы запустить подбор ТТ?", defaultYes:true);
                            if (dr == MessageDialogResult.Yes)
                            {
                                var rp = ProductBlockRequiredParameters.ToList();
                                rp.Add(GlobalAppProperties.Actual.ParameterCurrentTransformersSetCustom);
                                var product = Container.Resolve<IGetProductService>().GetProduct(rp);
                                if (product != null)
                                {
                                    foreach (var block in product.GetBlocks())
                                    {
                                        if (ProductBlockRequiredParameters.AllContainsInById(block.Parameters))
                                        {
                                            continue;
                                        }
                                        AddAddedBlock(block);
                                    }
                                }
                            }
                        }

                        this.ProductBlockEngineer.RejectChanges();
                        this.ProductBlockEngineer = new ProductBlockStructureCostWrapper(UnitOfWork.Repository<ProductBlock>().GetById(selectedProductBlock.Id), true);
                    }
                },
                () => IsTarget && IsEditMode);

            AddBlockAddedCommand = new DelegateLogCommand(
                () =>
                {
                    var block = Container.Resolve<IGetProductService>().GetProductBlock(DesignDepartment.Model.ParameterSetsAddedBlocks);
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
                    var wrapper = new PriceEngineeringTaskProductBlockAddedWrapper1(new PriceEngineeringTaskProductBlockAdded())
                    {
                        ProductBlock = new ProductBlockStructureCostWrapper(complect.ProductBlock, true)
                    };
                    this.ProductBlocksAdded.Add(wrapper);
                },
                () => IsTarget && IsEditMode);

            RemoveBlockAddedCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

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
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50),
                                Path = fileName
                            };
                            this.FilesAnswers.Add(fileWrapper);
                        }
                    }
                },
                () => IsTarget && IsEditMode == true);

            RemoveAnswerFileCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

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

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    this.LoadNewAnswerFilesInStorage();
                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
                },
                () => IsTarget && IsEditMode && this.IsValid && this.IsChanged);

            FinishCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Завершение проработки", "Вы уверены, что хотите завершить проработку?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

                    if (this.RequestForVerificationFromHead == false)
                    {
                        var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Проверка", "Хотите проверить результаты проработки?", defaultNo: true);
                        this.RequestForVerificationFromConstructor = dr == MessageDialogResult.Yes;
                    }

                    bool needVerification = this.RequestForVerificationFromHead || this.RequestForVerificationFromConstructor;

                    var sb = new StringBuilder()
                        .AppendLine(needVerification ? "Проработка направлена на проверку руководителю." : "Проработка завершена.")
                        .AppendLine("Основной блок:")
                        .AppendLine(this.ProductBlockEngineer.PrintToMessage());

                    var pba = this.ProductBlocksAdded.Where(x => x.IsRemoved == false).ToList();
                    if (pba.Any())
                    {
                        sb.AppendLine("Добавленные блоки:");
                        pba.ForEach(x => sb.AppendLine(x.ToString()));
                    }

                    Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus
                    {
                        StatusEnum = needVerification ? PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification : PriceEngineeringTaskStatusEnum.FinishedByConstructor
                    }));
                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = sb.ToString().TrimEnd('\n', '\r')
                    }));
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

            RejectCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Вы уверены, что хотите отклонить проработку задачи?", defaultNo: true) != MessageDialogResult.Yes)
                        return;
                    this.RejectChanges();
                    this.RejectByConstructor();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Publish(this.Model);
                },
                () => IsEditMode);

            BlockAddedNewParameterCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IDialogService>().ShowDialog(new ParametersServiceViewModel(Container, this.DesignDepartment.Model));
                },
                () => IsEditMode);

            BlockNewParameterCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IDialogService>().ShowDialog(new ParametersServiceViewModel(Container, this.Model.ProductBlockEngineer, ProductBlockRequiredParameters));
                },
                () => IsEditMode);

            CreateSubTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var getProductService = Container.Resolve<IGetProductService>();

                    var block = getProductService.GetProductBlock(DesignDepartment.Model.ParameterSetsSubTask);
                    if (block == null) return;

                    var unitOfWork = this.Container.Resolve<IUnitOfWork>();
                    block = unitOfWork.Repository<ProductBlock>().GetById(block.Id);
                    var product = getProductService.GetProduct(unitOfWork, new Product { ProductBlock = block });

                    var taskViewModel = new PriceEngineeringTaskViewModelManager(Container, unitOfWork, product)
                    {
                        ParentPriceEngineeringTaskId = this.Id,
                        Amount = 1
                    };
                    taskViewModel.Model.UserConstructorInitiator = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                    taskViewModel.FilesTechnicalRequirements.AddRange(this.FilesTechnicalRequirements.Where(x => x.IsActual).Select(x => new PriceEngineeringTaskFileTechnicalRequirementsWrapper(unitOfWork.Repository<PriceEngineeringTaskFileTechnicalRequirements>().GetById(x.Id))));

                    if (taskViewModel.StartCommandExecute(true))
                    {
                        this.ChildPriceEngineeringTasks.Add(new PriceEngineeringTaskViewModelConstructor(this.Container, taskViewModel.Model));
                    }
                },
                () => IsTarget && IsEditMode && this.Model.DesignDepartment.ParameterSetsSubTask.Any());


            LoadJsonFileCommand = new DelegateLogCommand(
                () =>
                {
                    var fPath = Container.Resolve<IFilesStorageService>().GetFolderPath();
                    var blocks = Container.Resolve<IJsonService>().ReadJsonFile<List<PriceEngineeringTaskProductBlockAdded>>($"{fPath}\\test.json");
                    this.ProductBlocksAdded.AddRange(blocks.Select(x => new PriceEngineeringTaskProductBlockAddedWrapper1(x)));
                });


            this.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
                FinishCommand.RaiseCanExecuteChanged();
                RejectCommand.RaiseCanExecuteChanged();
                CreateSubTaskCommand.RaiseCanExecuteChanged();
                //BlockAddedNewParameterCommand.RaiseCanExecuteChanged();
            };

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                SelectProductBlockCommand.RaiseCanExecuteChanged();

                AddAnswerFilesCommand.RaiseCanExecuteChanged();
                RemoveAnswerFileCommand.RaiseCanExecuteChanged();

                AddBlockAddedCommand.RaiseCanExecuteChanged();
                RemoveBlockAddedCommand.RaiseCanExecuteChanged();
                AddBlockAddedComplectCommand.RaiseCanExecuteChanged();

                BlockAddedNewParameterCommand.RaiseCanExecuteChanged();
                BlockNewParameterCommand.RaiseCanExecuteChanged();
            };

            this.SelectedAnswerFileIsChanged += () => RemoveAnswerFileCommand.RaiseCanExecuteChanged();
            this.SelectedBlockAddedIsChanged += () => RemoveBlockAddedCommand.RaiseCanExecuteChanged();
        }

        private void AddAddedBlock(ProductBlock block)
        {
            block = UnitOfWork.Repository<ProductBlock>().GetById(block.Id);
            var wrapper = new PriceEngineeringTaskProductBlockAddedWrapper1(new PriceEngineeringTaskProductBlockAdded())
            {
                ProductBlock = new ProductBlockStructureCostWrapper(block, true)
            };
            this.ProductBlocksAdded.Add(wrapper);
        }


        /// <summary>
        /// Загрузить все добавленные ответы ОГК в хранилище
        /// </summary>
        public void LoadNewAnswerFilesInStorage()
        {
            foreach (var fileWrapper in this.FilesAnswers.AddedItems)
            {
                var destFileName = $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}";
                if (File.Exists(destFileName) == false && string.IsNullOrEmpty(fileWrapper.Path) == false)
                {
                    File.Copy(fileWrapper.Path, destFileName);
                    fileWrapper.Path = null;
                }
            }
        }

    }
}
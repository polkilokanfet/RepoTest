using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.ParametersService1;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelConstructor : PriceEngineeringTaskViewModel
    {
        public override bool IsTarget => UserConstructor != null && Equals(Model.UserConstructor.Id, GlobalAppProperties.User.Id);

        public override bool IsEditMode
        {
            get
            {
                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Started:
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return true;

                    case PriceEngineeringTaskStatusEnum.Created:
                    case PriceEngineeringTaskStatusEnum.Stopped:
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return false;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private List<Parameter> ProductBlockRequiredParameters { get; set; }

        /// <summary>
        /// Выбрать блок продукта
        /// </summary>
        public DelegateLogCommand SelectProductBlockCommand { get; private set; }
        public DelegateLogCommand AddBlockAddedCommand { get; private set; }
        public DelegateLogCommand RemoveBlockAddedCommand { get; private set; }
        public DelegateLogCommand AddAnswerFilesCommand { get; private set; }
        public DelegateLogCommand RemoveAnswerFileCommand { get; private set; }
        public DelegateLogCommand FinishCommand { get; private set; }
        public DelegateLogCommand RejectCommand { get; private set; }
        public DelegateLogCommand BlockAddedNewParameterCommand { get; private set; }
        public DelegateLogCommand BlockNewParameterCommand { get; private set; }

        #region ctors

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(container, unitOfWork, priceEngineeringTask)
        {
        }

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
            throw new System.NotImplementedException();
        }

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
            throw new System.NotImplementedException();
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
                        this.ProductBlockEngineer = new ProductBlockStructureCostWrapper(UnitOfWork.Repository<ProductBlock>().GetById(selectedProductBlock.Id), true);
                    }
                },
                () => IsTarget && IsEditMode);

            AddBlockAddedCommand = new DelegateLogCommand(
                () =>
                {
                    var block = Container.Resolve<IGetProductService>().GetProductBlock(DesignDepartment.Model.ParameterSetsAddedBlocks);
                    if (block == null) return;
                    block = UnitOfWork.Repository<ProductBlock>().GetById(block.Id);
                    var wrapper = new PriceEngineeringTaskProductBlockAddedWrapper1(new PriceEngineeringTaskProductBlockAdded())
                        {
                            ProductBlock = new ProductBlockStructureCostWrapper(block, true)
                        };
                    this.ProductBlocksAdded.Add(wrapper);
                },
                () => IsTarget && IsEditMode);

            RemoveBlockAddedCommand = new DelegateLogCommand(
                () =>
                {
                    if(UnitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().GetById(SelectedBlockAdded.Model.Id) != null)
                        UnitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().Delete(SelectedBlockAdded.Model);

                    this.ProductBlocksAdded.Remove(SelectedBlockAdded);
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
                    Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus {StatusEnum = PriceEngineeringTaskStatusEnum.FinishedByConstructor}));
                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "Проработка завершена"
                    }));
                    SaveCommand.Execute();

                    AddAnswerFilesCommand.RaiseCanExecuteChanged();
                    RemoveAnswerFileCommand.RaiseCanExecuteChanged();
                },
                () => IsTarget && IsEditMode && this.IsValid);

            RejectCommand = new DelegateLogCommand(
                () =>
                {
                    this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus { StatusEnum = PriceEngineeringTaskStatusEnum.RejectedByConstructor }));
                    this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "Задача отклонена ОГК."
                    }));
                    SaveCommand.Execute();
                },
                () => IsEditMode && this.IsValid);

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


            this.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
                FinishCommand.RaiseCanExecuteChanged();
                RejectCommand.RaiseCanExecuteChanged();
                SelectProductBlockCommand.RaiseCanExecuteChanged();
                AddAnswerFilesCommand.RaiseCanExecuteChanged();
                RemoveAnswerFileCommand.RaiseCanExecuteChanged();
                BlockAddedNewParameterCommand.RaiseCanExecuteChanged();
            };

            this.SelectedAnswerFileIsChanged += () => RemoveAnswerFileCommand.RaiseCanExecuteChanged();
            this.SelectedBlockAddedIsChanged += () => RemoveBlockAddedCommand.RaiseCanExecuteChanged();
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
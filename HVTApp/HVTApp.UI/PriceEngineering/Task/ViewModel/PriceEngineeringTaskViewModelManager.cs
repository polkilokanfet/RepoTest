using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskWithStartCommandViewModel
    {
        public override bool IsTarget => true;

        public override bool IsEditMode
        {
            get
            {
                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                    case PriceEngineeringTaskStatusEnum.Stopped:
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return true;
                }

                return false;
            }
        }

        #region Commands

        public DelegateLogCommand AddTechnicalRequrementsFilesCommand { get; private set; }
        public DelegateLogConfirmationCommand RemoveTechnicalRequrementsFilesCommand { get; private set; }

        public DelegateLogConfirmationCommand AcceptCommand { get; private set; }
        public DelegateLogConfirmationCommand RejectCommand { get; private set; }
        public DelegateLogConfirmationCommand StopCommand { get; private set; }

        public DelegateLogConfirmationCommand StartProductionCommand { get; private set; }

        /// <summary>
        /// Замена продукта в SalesUnit на продукты из задачи
        /// </summary>
        public DelegateLogConfirmationCommand ReplaceProductCommand { get; private set; }


        #endregion

        #region Events

        /// <summary>
        /// Событие принятия задачи менеджером
        /// </summary>
        public override event Action<PriceEngineeringTask> TaskAcceptedByManagerAction;

        /// <summary>
        /// Событие полного принятия задачи менеджером
        /// </summary>
        public event Action<PriceEngineeringTask> TaskTotalAcceptedByManagerAction;

        #endregion

        #region ctors

        /// <summary>
        /// Для загрузки (редактирования) созданной задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected PriceEngineeringTaskViewModelManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId) { }

        /// <summary>
        /// Для создания новой задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        protected PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork) { }
        
        protected override void InCtor()
        {
            base.InCtor();
            
            #region Commands

            var messageService = this.Container.Resolve<IMessageService>();


            AddTechnicalRequrementsFilesCommand = new DelegateLogCommand(
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
                            var fileWrapper = new PriceEngineeringTaskFileTechnicalRequirementsWrapper(new PriceEngineeringTaskFileTechnicalRequirements())
                            {
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50),
                                Path = fileName
                            };
                            this.FilesTechnicalRequirements.Add(fileWrapper);
                        }
                    }
                }, 
                () => IsEditMode);

            RemoveTechnicalRequrementsFilesCommand = new DelegateLogConfirmationCommand(
                messageService,
                "Вы уверены, что хотите удалить выделенное ТЗ?",
                () =>
                {
                    if (string.IsNullOrEmpty(SelectedTechnicalRequrementsFile.Path))
                    {
                        SelectedTechnicalRequrementsFile.IsActual = false;
                    }
                    else
                    {
                        this.FilesTechnicalRequirements.Remove(SelectedTechnicalRequrementsFile);
                    }
                },
                () => IsEditMode && this.SelectedTechnicalRequrementsFile != null);

            AcceptCommand = new DelegateLogConfirmationCommand(messageService,
                "Вы уверены, что хотите принять проработку задачи?",
                () =>
                {
                    this.Accept();
                    SaveCommand.Execute();
                    this.OnTaskAcceptedByManagerAction(this.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(this.Model);
                },
                () => this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor && this.IsValid);

            RejectCommand = new DelegateLogConfirmationCommand(
                messageService,
                "Вы уверены, что хотите отклонить проработку задачи?",
                () =>
                {
                    this.RejectedByManager();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(this.Model);
                },
                () => this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor && this.IsValid);

            StopCommand = new DelegateLogConfirmationCommand(messageService,
                "Вы уверены, что хотите остановить проработку задачи?",
                () =>
                {
                    this.Stop();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(this.Model);
                },
                () => this.Status != PriceEngineeringTaskStatusEnum.Created && this.Status != PriceEngineeringTaskStatusEnum.Stopped && this.IsValid);

            ReplaceProductCommand = new DelegateLogConfirmationCommand(messageService, 
                "Вы уверены, что хотите заменить продукт в проекте на продукт из этой задачи?",
                () => { this.ReplaceProduct(this.Model); });

            StartProductionCommand = new DelegateLogConfirmationCommand(messageService,
                "Вы уверены, что хотите запустить производство этого оборудования?",
                () => {});


            #endregion

            this.SelectedTechnicalRequrementsFileIsChanged += () =>
            {
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
            };

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                //SelectDesignDepartmentCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
                AddTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
                AcceptCommand.RaiseCanExecuteChanged();
                RejectCommand.RaiseCanExecuteChanged();
            };
        }

        #endregion

        protected void OnTaskAcceptedByManagerAction(PriceEngineeringTask task)
        {
            //если эта задача головная
            if (this.Model.ParentPriceEngineeringTaskId == null)
            {
                var priceEngineeringTask = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(Model.Id);

                //если задача полностью принята менеджером
                if (priceEngineeringTask.IsTotalAccepted)
                {
                    this.TaskTotalAcceptedByManagerAction?.Invoke(this.Model);

                    var ms = Container.Resolve<IMessageService>();
                    if (ms.ShowYesNoMessageDialog("Хотите синхронизировать?") == MessageDialogResult.Yes)
                    {
                        //синхронизируем продукты
                        this.ReplaceProduct(priceEngineeringTask);
                    }
                }
            }

            //прокидываем событие на уровень выше
            this.TaskAcceptedByManagerAction?.Invoke(task);
        }

        private void ReplaceProduct(PriceEngineeringTask priceEngineeringTask)
        {
            if (priceEngineeringTask.SalesUnits.Any() == false) return;

            var getProductService = Container.Resolve<IGetProductService>();
            var unitOfWork = Container.Resolve<IUnitOfWork>();

            priceEngineeringTask = unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTask.Id);

            var product = getProductService.GetProduct(unitOfWork, priceEngineeringTask.GetProduct());
            var salesUnits = priceEngineeringTask.SalesUnits;

            var productBlocksAdded = priceEngineeringTask
                .GetAllPriceEngineeringTasks()
                .SelectMany(x => x.ProductBlocksAdded)
                .Where(x => x.IsRemoved == false)
                .ToList();

            //Включённое оборудование на всё количество
            var productsIncludedOnAmount = productBlocksAdded
                .Where(x => x.IsOnBlock == false)
                .Select(x => new ProductIncluded
                {
                    Product = getProductService.GetProduct(unitOfWork, x.GetProduct()),
                    Amount = x.Amount
                })
                .ToList();


            foreach (var salesUnit in salesUnits)
            {
                //заменяем продукт
                salesUnit.Product = product;

                //заменяем включёное оборудование
                //удаляем старое
                foreach (var productIncluded in
                    salesUnit.ProductsIncluded
                        .Where(x => x.Product == null || x.Product.ProductBlock.IsSupervision == false)
                        .ToList())
                {
                    salesUnit.ProductsIncluded.Remove(productIncluded);
                    unitOfWork.Repository<ProductIncluded>().Delete(productIncluded);
                }

                //Включённое оборудование на каждый блок
                var productsIncludedOnBlock = productBlocksAdded
                    .Where(x => x.IsOnBlock == true)
                    .Select(x => new ProductIncluded
                    {
                        Product = getProductService.GetProduct(unitOfWork, x.GetProduct()),
                        Amount = x.Amount
                    })
                    .ToList();

                salesUnit.ProductsIncluded.AddRange(productsIncludedOnBlock);
                salesUnit.ProductsIncluded.AddRange(productsIncludedOnAmount);
            }

            Container.Resolve<IMessageService>().ShowOkMessageDialog("Уведомдение",
                unitOfWork.SaveChanges().OperationCompletedSuccessfully
                    ? $"Заменен продукт в {salesUnits.First()}"
                    : $"Не заменен продукт в {salesUnits.First()}");
        }

        #region Actions

        /// <summary>
        /// Принять задачу
        /// </summary>
        protected void Accept()
        {
            this.MakeAction(PriceEngineeringTaskStatusEnum.Accepted, "Проработка задачи принята.");
        }

        /// <summary>
        /// Отклонить проработку задачи
        /// </summary>
        protected void RejectedByManager()
        {
            this.MakeAction(PriceEngineeringTaskStatusEnum.RejectedByManager, "Проработка задачи отклонена.");
        }

        /// <summary>
        /// Остановить проработку задачи
        /// </summary>
        protected void Stop()
        {
            this.MakeAction(PriceEngineeringTaskStatusEnum.Stopped, "Проработка задачи остановлена.");
        }

        #endregion
    }
}
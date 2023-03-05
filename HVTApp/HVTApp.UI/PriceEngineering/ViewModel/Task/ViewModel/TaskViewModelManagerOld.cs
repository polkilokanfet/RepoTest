using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelManagerOld : TaskViewModelManager
    {
        #region Commands

        /// <summary>
        /// Принять техническую проработку задачи
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute AcceptCommand { get; }

        /// <summary>
        /// Отклонить техническую проработку задачи
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute RejectCommand { get; }

        /// <summary>
        /// Остановить техническую проработку задачи
        /// </summary>
        public DoStepCommandStopByManager StopCommand { get; }

        /// <summary>
        /// Загрузить техническую проработку задачи в ТСЕ
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute LoadToTceStartCommand { get; }

        /// <summary>
        /// Запросить открытие производства
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute StartProductionCommand { get; }

        /// <summary>
        /// Замена продукта в SalesUnit на продукты из ТСП
        /// </summary>
        public override DelegateLogConfirmationCommand ReplaceProductCommand { get; }

        #endregion

        #region Events

        /// <summary>
        /// Событие принятия задачи менеджером
        /// </summary>
        public event Action<PriceEngineeringTask> TaskAcceptedByManagerAction;

        /// <summary>
        /// Событие полного принятия задачи менеджером
        /// </summary>
        public event Action<PriceEngineeringTask> TaskTotalAcceptedByManagerAction;

        #endregion

        public TaskViewModelManagerOld(IUnityContainer container, PriceEngineeringTask priceEngineeringTask) : base(container, priceEngineeringTask.Id)
        {
            var vms = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new TaskViewModelManagerOld(Container, engineeringTask));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(vms);
            //RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);

            //реакция на событие принятия дочерней задачи
            foreach (var priceEngineeringTaskViewModel in ChildPriceEngineeringTasks)
            {
                if (priceEngineeringTaskViewModel is TaskViewModelManagerOld petvmm)
                {
                    petvmm.TaskAcceptedByManagerAction += OnTaskAcceptedByManagerAction;
                }
            }

            //подписка на событие принятия менеджером дочерней задачи
            foreach (var priceEngineeringTaskViewModel in ChildPriceEngineeringTasks)
            {
                if (priceEngineeringTaskViewModel is TaskViewModelManagerOld vmOld)
                {
                    if (this is TaskViewModelManagerOld vmThis)

                        //прокидываем событие выше
                        vmOld.TaskAcceptedByManagerAction += task => vmThis.TaskAcceptedByManagerAction?.Invoke(task);
                }
            }

            #region Commands

            AcceptCommand = new DoStepCommandAcceptedByManager(this, container, () =>  this.OnTaskAcceptedByManagerAction(this.Model));
            RejectCommand = new DoStepCommandRejectedByManager(this, container);
            StopCommand = new DoStepCommandStopByManager(this, container);
            LoadToTceStartCommand = new DoStepCommandLoadToTceStart(this, container);
            StartProductionCommand = new DoStepCommandProductionRequestStart(this, container);

            ReplaceProductCommand = new DelegateLogConfirmationCommand(container.Resolve<IMessageService>(),
                "Вы уверены, что хотите заменить продукт в проекте на продукт из этой задачи?",
                () => { this.ReplaceProduct(this.Model); });

            #endregion
        }

        /// <summary>
        /// Заменить продукт в SalesUnit на продукт из ТСП
        /// </summary>
        /// <param name="priceEngineeringTask"></param>
        private void ReplaceProduct(PriceEngineeringTask priceEngineeringTask)
        {
            try
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

                try
                {
                    Container.Resolve<IMessageService>().ShowOkMessageDialog("Уведомдение",
                        unitOfWork.SaveChanges().OperationCompletedSuccessfully
                            ? $"Заменен продукт в {salesUnits.First()}"
                            : $"Не заменен продукт в {salesUnits.First()}");
                }
                catch (Exception e)
                {
                    Container.Resolve<IMessageService>().ShowOkMessageDialog("Уведомдение", e.PrintAllExceptions());
                }
            }
            catch (Exception e)
            {
                Container.Resolve<IMessageService>().ShowOkMessageDialog("Уведомдение", e.PrintAllExceptions());
            }
        }

        protected void OnTaskAcceptedByManagerAction(PriceEngineeringTask task)
        {
            //если эта задача головная
            if (this.Model.ParentPriceEngineeringTaskId == null)
            {
                var priceEngineeringTask = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(Model.Id);

                //если задача полностью принята менеджером
                if (priceEngineeringTask.IsAcceptedTotal)
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
    }
}
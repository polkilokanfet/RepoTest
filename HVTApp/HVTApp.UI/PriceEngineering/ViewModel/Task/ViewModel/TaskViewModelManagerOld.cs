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
        /// ������� ����������� ���������� ������
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute AcceptCommand { get; }

        /// <summary>
        /// ��������� ����������� ���������� ������
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute RejectCommand { get; }

        /// <summary>
        /// ���������� ����������� ���������� ������
        /// </summary>
        public DoStepCommandStopByManager StopCommand { get; }

        /// <summary>
        /// ��������� ����������� ���������� ������ � ���
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute LoadToTceStartCommand { get; }

        /// <summary>
        /// ��������� �������� ������������
        /// </summary>
        public override ICommandIsVisibleWhenCanExecute StartProductionCommand { get; }

        /// <summary>
        /// ������ �������� � SalesUnit �� �������� �� ���
        /// </summary>
        public override DelegateLogConfirmationCommand ReplaceProductCommand { get; }

        #endregion

        #region Events

        /// <summary>
        /// ������� �������� ������ ����������
        /// </summary>
        public event Action<PriceEngineeringTask> TaskAcceptedByManagerAction;

        /// <summary>
        /// ������� ������� �������� ������ ����������
        /// </summary>
        public event Action<PriceEngineeringTask> TaskTotalAcceptedByManagerAction;

        #endregion

        public TaskViewModelManagerOld(IUnityContainer container, PriceEngineeringTask priceEngineeringTask) : base(container, priceEngineeringTask.Id)
        {
            var vms = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new TaskViewModelManagerOld(Container, engineeringTask));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(vms);
            //RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);

            //������� �� ������� �������� �������� ������
            foreach (var priceEngineeringTaskViewModel in ChildPriceEngineeringTasks)
            {
                if (priceEngineeringTaskViewModel is TaskViewModelManagerOld petvmm)
                {
                    petvmm.TaskAcceptedByManagerAction += OnTaskAcceptedByManagerAction;
                }
            }

            //�������� �� ������� �������� ���������� �������� ������
            foreach (var priceEngineeringTaskViewModel in ChildPriceEngineeringTasks)
            {
                if (priceEngineeringTaskViewModel is TaskViewModelManagerOld vmOld)
                {
                    if (this is TaskViewModelManagerOld vmThis)

                        //����������� ������� ����
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
                "�� �������, ��� ������ �������� ������� � ������� �� ������� �� ���� ������?",
                () => { this.ReplaceProduct(this.Model); });

            #endregion
        }

        /// <summary>
        /// �������� ������� � SalesUnit �� ������� �� ���
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

                //���������� ������������ �� �� ����������
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
                    //�������� �������
                    salesUnit.Product = product;

                    //�������� ��������� ������������
                    //������� ������
                    foreach (var productIncluded in
                        salesUnit.ProductsIncluded
                            .Where(x => x.Product == null || x.Product.ProductBlock.IsSupervision == false)
                            .ToList())
                    {
                        salesUnit.ProductsIncluded.Remove(productIncluded);
                        unitOfWork.Repository<ProductIncluded>().Delete(productIncluded);
                    }

                    //���������� ������������ �� ������ ����
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
                    Container.Resolve<IMessageService>().ShowOkMessageDialog("�����������",
                        unitOfWork.SaveChanges().OperationCompletedSuccessfully
                            ? $"������� ������� � {salesUnits.First()}"
                            : $"�� ������� ������� � {salesUnits.First()}");
                }
                catch (Exception e)
                {
                    Container.Resolve<IMessageService>().ShowOkMessageDialog("�����������", e.PrintAllExceptions());
                }
            }
            catch (Exception e)
            {
                Container.Resolve<IMessageService>().ShowOkMessageDialog("�����������", e.PrintAllExceptions());
            }
        }

        protected void OnTaskAcceptedByManagerAction(PriceEngineeringTask task)
        {
            //���� ��� ������ ��������
            if (this.Model.ParentPriceEngineeringTaskId == null)
            {
                var priceEngineeringTask = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(Model.Id);

                //���� ������ ��������� ������� ����������
                if (priceEngineeringTask.IsAcceptedTotal)
                {
                    this.TaskTotalAcceptedByManagerAction?.Invoke(this.Model);

                    var ms = Container.Resolve<IMessageService>();
                    if (ms.ShowYesNoMessageDialog("������ ����������������?") == MessageDialogResult.Yes)
                    {
                        //�������������� ��������
                        this.ReplaceProduct(priceEngineeringTask);
                    }
                }
            }

            //����������� ������� �� ������� ����
            this.TaskAcceptedByManagerAction?.Invoke(task);
        }
    }
}
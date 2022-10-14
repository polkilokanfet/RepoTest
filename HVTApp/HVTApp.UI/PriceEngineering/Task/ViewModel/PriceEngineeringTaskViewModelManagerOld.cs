using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManagerOld : PriceEngineeringTaskViewModelManager
    {
        #region Commands

        public DelegateLogConfirmationCommand AcceptCommand { get; private set; }
        public DelegateLogConfirmationCommand RejectCommand { get; private set; }
        public DelegateLogConfirmationCommand StopCommand { get; private set; }

        public DelegateLogConfirmationCommand StartProductionCommand { get; private set; }

        /// <summary>
        /// ������ �������� � SalesUnit �� �������� �� ���
        /// </summary>
        public DelegateLogConfirmationCommand ReplaceProductCommand { get; private set; }

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

        public PriceEngineeringTaskViewModelManagerOld(IUnityContainer container, PriceEngineeringTask priceEngineeringTask) : base(container, priceEngineeringTask.Id)
        {
            var vms = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new PriceEngineeringTaskViewModelManagerOld(Container, engineeringTask));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(vms);
            //RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);

            //������� �� ������� �������� �������� ������
            foreach (var priceEngineeringTaskViewModel in ChildPriceEngineeringTasks)
            {
                if (priceEngineeringTaskViewModel is PriceEngineeringTaskViewModelManagerOld petvmm)
                {
                    petvmm.TaskAcceptedByManagerAction += OnTaskAcceptedByManagerAction;
                }
            }

            //�������� �� ������� �������� ���������� �������� ������
            foreach (var priceEngineeringTaskViewModel in ChildPriceEngineeringTasks)
            {
                if (priceEngineeringTaskViewModel is PriceEngineeringTaskViewModelManagerOld vmOld)
                {
                    if (this is PriceEngineeringTaskViewModelManagerOld vmThis)

                        //����������� ������� ����
                        vmOld.TaskAcceptedByManagerAction += task => vmThis.TaskAcceptedByManagerAction?.Invoke(task);
                }
            }


            #region Commands

            var messageService = this.Container.Resolve<IMessageService>();
            var eventAggregator = Container.Resolve<IEventAggregator>();

            AcceptCommand = new DelegateLogConfirmationCommand(
                messageService,
                "�� �������, ��� ������ ������� ���������� ������?",
                () =>
                {
                    this.Statuses.Add(PriceEngineeringTaskStatusEnum.Accepted);
                    SaveCommand.Execute();
                    this.OnTaskAcceptedByManagerAction(this.Model);
                    eventAggregator.GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(this.Model);
                },
                () => (this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor || this.Status == PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead) && this.IsValid);

            RejectCommand = new DelegateLogConfirmationCommand(
                messageService,
                "�� �������, ��� ������ ��������� ���������� ������?",
                () =>
                {
                    this.Statuses.Add(PriceEngineeringTaskStatusEnum.RejectedByManager);
                    SaveCommand.Execute();
                    eventAggregator.GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(this.Model);
                },
                () => (this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor || this.Status == PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead) && this.IsValid);

            StopCommand = new DelegateLogConfirmationCommand(messageService,
                "�� �������, ��� ������ ���������� ���������� ������?",
                () =>
                {
                    this.Statuses.Add(PriceEngineeringTaskStatusEnum.Stopped);
                    SaveCommand.Execute();
                    eventAggregator.GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(this.Model);
                },
                () => this.Status != PriceEngineeringTaskStatusEnum.Created && this.Status != PriceEngineeringTaskStatusEnum.Stopped && this.IsValid);

            ReplaceProductCommand = new DelegateLogConfirmationCommand(messageService,
                "�� �������, ��� ������ �������� ������� � ������� �� ������� �� ���� ������?",
                () => { this.ReplaceProduct(this.Model); });

            StartProductionCommand = new DelegateLogConfirmationCommand(messageService,
                "�� �������, ��� ������ ��������� ������������ ����� ������������?",
                () => { });

            #endregion

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                StopCommand.RaiseCanExecuteChanged();
                AcceptCommand.RaiseCanExecuteChanged();
                RejectCommand.RaiseCanExecuteChanged();
            };

        }

        /// <summary>
        /// �������� ������� � SalesUnit �� ������� �� ���
        /// </summary>
        /// <param name="priceEngineeringTask"></param>
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

            Container.Resolve<IMessageService>().ShowOkMessageDialog("�����������",
                unitOfWork.SaveChanges().OperationCompletedSuccessfully
                    ? $"������� ������� � {salesUnits.First()}"
                    : $"�� ������� ������� � {salesUnits.First()}");
        }

        protected void OnTaskAcceptedByManagerAction(PriceEngineeringTask task)
        {
            //���� ��� ������ ��������
            if (this.Model.ParentPriceEngineeringTaskId == null)
            {
                var priceEngineeringTask = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(Model.Id);

                //���� ������ ��������� ������� ����������
                if (priceEngineeringTask.IsTotalAccepted)
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
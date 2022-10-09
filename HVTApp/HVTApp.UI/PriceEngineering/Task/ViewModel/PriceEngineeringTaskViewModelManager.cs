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
        /// ������ �������� � SalesUnit �� �������� �� ������
        /// </summary>
        public DelegateLogConfirmationCommand ReplaceProductCommand { get; private set; }


        #endregion

        #region Events

        /// <summary>
        /// ������� �������� ������ ����������
        /// </summary>
        public override event Action<PriceEngineeringTask> TaskAcceptedByManagerAction;

        /// <summary>
        /// ������� ������� �������� ������ ����������
        /// </summary>
        public event Action<PriceEngineeringTask> TaskTotalAcceptedByManagerAction;

        #endregion

        #region ctors

        /// <summary>
        /// ��� �������� (��������������) ��������� ������
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected PriceEngineeringTaskViewModelManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId) { }

        /// <summary>
        /// ��� �������� ����� ������
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
                        //�������� ������ ����
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
                "�� �������, ��� ������ ������� ���������� ��?",
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
                "�� �������, ��� ������ ������� ���������� ������?",
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
                "�� �������, ��� ������ ��������� ���������� ������?",
                () =>
                {
                    this.RejectedByManager();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(this.Model);
                },
                () => this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor && this.IsValid);

            StopCommand = new DelegateLogConfirmationCommand(messageService,
                "�� �������, ��� ������ ���������� ���������� ������?",
                () =>
                {
                    this.Stop();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(this.Model);
                },
                () => this.Status != PriceEngineeringTaskStatusEnum.Created && this.Status != PriceEngineeringTaskStatusEnum.Stopped && this.IsValid);

            ReplaceProductCommand = new DelegateLogConfirmationCommand(messageService, 
                "�� �������, ��� ������ �������� ������� � ������� �� ������� �� ���� ������?",
                () => { this.ReplaceProduct(this.Model); });

            StartProductionCommand = new DelegateLogConfirmationCommand(messageService,
                "�� �������, ��� ������ ��������� ������������ ����� ������������?",
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

        #region Actions

        /// <summary>
        /// ������� ������
        /// </summary>
        protected void Accept()
        {
            this.MakeAction(PriceEngineeringTaskStatusEnum.Accepted, "���������� ������ �������.");
        }

        /// <summary>
        /// ��������� ���������� ������
        /// </summary>
        protected void RejectedByManager()
        {
            this.MakeAction(PriceEngineeringTaskStatusEnum.RejectedByManager, "���������� ������ ���������.");
        }

        /// <summary>
        /// ���������� ���������� ������
        /// </summary>
        protected void Stop()
        {
            this.MakeAction(PriceEngineeringTaskStatusEnum.Stopped, "���������� ������ �����������.");
        }

        #endregion
    }
}
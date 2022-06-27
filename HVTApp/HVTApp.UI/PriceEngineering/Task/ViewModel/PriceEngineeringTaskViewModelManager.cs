using System.Collections.Generic;
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
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskViewModel
    {
        public override bool IsExpanded => true;

        public override bool IsExpendedChildPriceEngineeringTasks => true;

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

        public DelegateLogCommand SelectDesignDepartmentCommand { get; private set; }
        
        public DelegateLogCommand AddTechnicalRequrementsFilesCommand { get; private set; }
        public DelegateLogCommand RemoveTechnicalRequrementsFilesCommand { get; private set; }

        public DelegateLogCommand AcceptCommand { get; private set; }
        public DelegateLogCommand RejectCommand { get; private set; }
        public DelegateLogCommand StopCommand { get; private set; }


        /// <summary>
        /// Замена продукта в SalesUnit на продукты из задачи
        /// </summary>
        public DelegateLogCommand ReplaceProductCommand { get; private set; }

        #endregion

        #region ctors

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) 
            : this(container, unitOfWork, salesUnits.First().Product)
        {
            this.SalesUnits.AddRange(salesUnits.Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit)));
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, Product product) 
            : base(container, unitOfWork)
        {
            ProductBlockEngineer = new ProductBlockStructureCostWrapper(product.ProductBlock);
            ProductBlockManager = new ProductBlockEmptyWrapper(product.ProductBlock);

            //бюро
            var department = UnitOfWork.Repository<DesignDepartment>().Find(x => x.ProductBlockIsSuitable(this.ProductBlockEngineer.Model)).FirstOrDefault();
            if (department != null)
            {
                this.DesignDepartment = new DesignDepartmentEmptyWrapper(department);
            }

            var vms = Model.ChildPriceEngineeringTasks.Select(x => new PriceEngineeringTaskViewModelManager(container, x));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(vms);
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
            
            foreach (var dependentProduct in product.DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    var vm = new PriceEngineeringTaskViewModelManager(container, unitOfWork, dependentProduct.Product);
                    this.ChildPriceEngineeringTasks.Add(vm);
                    vm.Parent = this;
                }
            }
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, PriceEngineeringTask priceEngineeringTask) 
            : base(container, priceEngineeringTask)
        {
            var vms = Model.ChildPriceEngineeringTasks.Select(x => new PriceEngineeringTaskViewModelManager(Container, x));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(vms);
            //RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }

        #endregion

        protected override void InCtor()
        {
            base.InCtor();
            
            #region Commands

            SelectDesignDepartmentCommand = new DelegateLogCommand(
                () =>
                {
                    var departments = UnitOfWork.Repository<DesignDepartment>().Find(designDepartment => designDepartment.ProductBlockIsSuitable(this.Model.ProductBlockEngineer));
                    var department = Container.Resolve<ISelectService>().SelectItem(departments);
                    if (department != null)
                    {
                        this.DesignDepartment = new DesignDepartmentEmptyWrapper(UnitOfWork.Repository<DesignDepartment>().GetById(department.Id));
                    }
                },
                () => IsEditMode);

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

            RemoveTechnicalRequrementsFilesCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

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

            AcceptCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Вы уверены, что хотите принять проработку задачи?", defaultNo: true) != MessageDialogResult.Yes) return;
                    this.Accept();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(this.Model);

                    var mainTask = this.GetMainTask(this.Model);
                    if (mainTask.IsTotalAccepted)
                    {
                        ReplaceProduct(mainTask);
                        Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedTotalEvent>().Publish(mainTask);
                    }
                },
                () => this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor && this.IsValid);

            RejectCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Вы уверены, что хотите отклонить проработку?", defaultNo: true) != MessageDialogResult.Yes)
                        return;
                    this.RejectedByManager();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(this.Model);
                },
                () => this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor && this.IsValid);

            StopCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Вы уверены, что хотите остановить проработку?", defaultNo: true) != MessageDialogResult.Yes)
                        return;
                    this.Stop();
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(this.Model);
                },
                () => this.Status != PriceEngineeringTaskStatusEnum.Created && this.Status != PriceEngineeringTaskStatusEnum.Stopped && this.IsValid);

            ReplaceProductCommand = new DelegateLogCommand(() => { this.ReplaceProduct(this.Model); });


            #endregion

            this.SelectedTechnicalRequrementsFileIsChanged += () =>
            {
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
            };

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                SelectDesignDepartmentCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
                AddTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
                AcceptCommand.RaiseCanExecuteChanged();
                RejectCommand.RaiseCanExecuteChanged();
            };
        }

        /// <summary>
        /// Найти главную задачу в цепочке (самую первую)
        /// </summary>
        /// <param name="priceEngineeringTask"></param>
        /// <returns></returns>
        private PriceEngineeringTask GetMainTask(PriceEngineeringTask priceEngineeringTask)
        {
            var result = priceEngineeringTask;
            while (result.ParentPriceEngineeringTaskId.HasValue)
            {
                result = UnitOfWork.Repository<PriceEngineeringTask>().GetById(result.ParentPriceEngineeringTaskId.Value);
            }
            return result;
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
                        .Where(x => x.Product.ProductBlock.IsSupervision == false)
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
    }
}
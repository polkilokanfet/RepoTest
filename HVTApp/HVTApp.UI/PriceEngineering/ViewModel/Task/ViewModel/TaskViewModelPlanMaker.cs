using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelPlanMaker : TaskViewModelLoadFilesCommand
    {
        public TasksWrapperPlanMaker TasksWrapper { get; }

        public override bool IsTarget => this.Model.Status.Equals(ScriptStep.ProductionRequestStart);

        public override bool IsEditMode => this.Model.Status.Equals(ScriptStep.ProductionRequestStart);

        #region Commands

        public ICommandRaiseCanExecuteChanged ProductionRequestFinishCommand { get; }

        #endregion

        #region Order

        public OrderWrapper Order => SalesUnits.First().Order;

        public new SalesUnitsCollection SalesUnits { get; private set; }

        public event Action ProductionRequestFinishedEvent;

        protected override void InitializeCollectionProperties()
        {
            base.InitializeCollectionProperties();

            if (Model.SalesUnits == null) throw new ArgumentException("SU cannot be null");
            SalesUnits = new SalesUnitsCollection(this.Model, Model.Status.Equals(ScriptStep.ProductionRequestStart));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }

        #endregion

        public event Action SavedEvent;

        public TaskViewModelPlanMaker(TasksWrapperPlanMaker tasksWrapper, IUnityContainer container, Guid priceEngineeringTaskId)
            : base(container, priceEngineeringTaskId)
        {
            TasksWrapper = tasksWrapper;

            ProductionRequestFinishCommand = new DoStepCommandProductionRequestFinish(this, container, () => this.ProductionRequestFinishedEvent?.Invoke());

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(IsEditMode));
                ProductionRequestFinishCommand.RaiseCanExecuteChanged();
            };

            this.TasksWrapper.PropertyChanged += (sender, args) =>
            {
                ProductionRequestFinishCommand.RaiseCanExecuteChanged();
            };

            if (Model.Status.Equals(ScriptStep.ProductionRequestStart) && Order == null)
            {
                SalesUnits.AddOrder(new Order { DateOpen = DateTime.Now });
            }
        }

        protected override void SaveCommand_ExecuteMethod()
        {
            this.AcceptChanges();
            UnitOfWork.SaveChanges();
            Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
            SaveCommand.RaiseCanExecuteChanged();
            SavedEvent?.Invoke();
        }

        protected override bool SaveCommand_CanExecuteMethod()
        {
            if (this.Model.Status.Equals(ScriptStep.ProductionRequestFinish) == false)
                return false;
            return this.IsChanged || this.TasksWrapper.IsChanged;
        }

        public class SalesUnitWithOrderWrapper : SalesUnitWithSignalToStartProductionWrapper
        {
            private readonly bool _orderIsRequired;

            #region ComplexProperties

            /// <summary>
            /// �����
            /// </summary>
            public OrderWrapper Order
            {
                get => GetWrapper<OrderWrapper>();
                set => SetComplexValue<Order, OrderWrapper>(Order, value);
            }

            #endregion

            public SalesUnitWithOrderWrapper(SalesUnit model, bool orderIsRequired) : base(model)
            {
                _orderIsRequired = orderIsRequired;
            }

            public override void InitializeComplexProperties()
            {
                InitializeComplexProperty(nameof(Order), Model.Order == null ? null : new OrderWrapper(Model.Order));
            }

            protected override IEnumerable<ValidationResult> ValidateOther()
            {
                if (_orderIsRequired)
                {
                    if (Order == null)
                        yield return new ValidationResult($"{nameof(Order)} is required", new[] { nameof(Order) });
                }
            }
        }

        public class SalesUnitsCollection : ValidatableChangeTrackingCollection<SalesUnitWithOrderWrapper>
        {
            public SalesUnitsCollection(PriceEngineeringTask priceEngineeringTask, bool orderIsRequired)
                : base(priceEngineeringTask.SalesUnits.Select(salesUnit => new SalesUnitWithOrderWrapper(salesUnit, orderIsRequired)))
            {
            }

            public void AddOrder(Order order)
            {
                var orderWrapper = new OrderWrapper(order);
                this.Items.ForEach(x => x.Order = orderWrapper);
            }
        }
    }
}
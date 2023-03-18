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

        public DateTime EndProductionPlanDate
        {
            get => this.SalesUnits.FirstOrDefault()?.EndProductionPlanDate?.Date ?? DateTime.Today.AddDays(120);
            set => SalesUnits.ForEach(salesUnit => salesUnit.EndProductionPlanDate = value);
        }

        public DateTime SignalToStartProductionDone
        {
            set => SalesUnits.ForEach(salesUnit => salesUnit.SignalToStartProductionDone = value);
        }

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

            if (Model.Status.Equals(ScriptStep.ProductionRequestStart))
            {
                int i = 1;
                foreach (var salesUnit in this.SalesUnits)
                {
                    salesUnit.EndProductionPlanDate = salesUnit.Model.EndProductionDateCalculated.Date;
                    salesUnit.OrderPosition = i.ToString();
                    i++;
                }

                RaisePropertyChanged(nameof(EndProductionPlanDate));
            }
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

            #region SimpleProperties

            #region OrderPosition

            /// <summary>
            /// Позиция
            /// </summary>
            public string OrderPosition
            {
                get => GetValue<string>();
                set => SetValue(value);
            }
            public string OrderPositionOriginalValue => GetOriginalValue<string>(nameof(OrderPosition));
            public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));

            #endregion

            #region EndProductionPlanDate;

            /// <summary>
            /// Плановая дата окончания производства
            /// </summary>
            public DateTime? EndProductionPlanDate
            {
                get => GetValue<DateTime?>();
                set => SetValue(value);
            }
            public DateTime? EndProductionPlanDateOriginalValue => GetOriginalValue<DateTime?>(nameof(EndProductionPlanDate));
            public bool EndProductionPlanDateIsChanged => GetIsChanged(nameof(EndProductionPlanDate));

            #endregion

            #endregion

            #region ComplexProperties

            /// <summary>
            /// Заказ
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
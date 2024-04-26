using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelPlanMaker : TaskViewModel
    {
        private bool _isNotUniqueOrderData = true;
        private bool _isFillingRest;

        #region TaskViewModelPlanMaker

        public TasksWrapperPlanMaker TasksWrapper { get; }

        public override bool IsTarget => this.Model.Status.Equals(ScriptStep.ProductionRequestStart);

        public override bool IsEditMode => this.Model.Status.Equals(ScriptStep.ProductionRequestStart);

        /// <summary>
        /// Данные по заводскому заказу не едины для всех позиций
        /// </summary>
        public bool IsNotUniqueOrderData
        {
            get => _isNotUniqueOrderData;
            set
            {
                _isNotUniqueOrderData = value;
                RaisePropertyChanged(nameof(IsNotUniqueOrderData));
            }
        }

        /// <summary>
        /// Режим заполнения остатков
        /// </summary>
        public bool IsFillingRest
        {
            get => _isFillingRest;
            set
            {
                _isFillingRest = value;
                if (_isFillingRest) Sub();
                else UnSub();
            }
        }

        private void Sub()
        {
            foreach (var salesUnit in this.SalesUnits)
            {
                salesUnit.PropertyChanged += SalesUnitOnPropertyChanged;
            }
        }
        private void UnSub()
        {
            foreach (var salesUnit in this.SalesUnits)
            {
                salesUnit.PropertyChanged -= SalesUnitOnPropertyChanged;
            }
        }

        private void SalesUnitOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsNotUniqueOrderData) return;

            UnSub();

            var su = sender as SalesUnitWithOrderWrapper;

            if (e.PropertyName == nameof(SalesUnitWithOrderWrapper.DateOpen))
            {
                for (int i = this.SalesUnits.IndexOf(su); i < this.SalesUnits.Count; i++)
                {
                    this.SalesUnits[i].DateOpen = su.DateOpen;
                }
            }
            else if (e.PropertyName == nameof(SalesUnitWithOrderWrapper.EndProductionPlanDate))
            {
                for (int i = this.SalesUnits.IndexOf(su); i < this.SalesUnits.Count; i++)
                {
                    this.SalesUnits[i].EndProductionPlanDate = su.EndProductionPlanDate;
                }
            }
            else if (e.PropertyName == nameof(SalesUnitWithOrderWrapper.OrderNumber))
            {
                for (int i = this.SalesUnits.IndexOf(su); i < this.SalesUnits.Count; i++)
                {
                    this.SalesUnits[i].OrderNumber = su.OrderNumber;
                }
            }
            else if (e.PropertyName == nameof(SalesUnitWithOrderWrapper.OrderPosition))
            {
                int startPosition;
                if (int.TryParse(su.OrderPosition, out startPosition))
                {
                    for (int i = this.SalesUnits.IndexOf(su); i < this.SalesUnits.Count; i++)
                    {
                        this.SalesUnits[i].OrderPosition = startPosition.ToString();
                        startPosition++;
                    }
                }
            }

            Sub();
        }

        #region Commands

        public ICommandRaiseCanExecuteChanged ProductionRequestFinishCommand { get; }

        #endregion

        #region Order

        /// <summary>
        /// Номер заводского заказа
        /// </summary>
        public string OrderNumber
        {
            get => SalesUnits.First().OrderNumber;
            set
            {
                this.SalesUnits.ForEach(x => x.OrderNumber = value);
                Validate();
                RaisePropertyChanged(nameof(OrderNumber));
                RaisePropertyChanged(nameof(OrderNumberIsChanged));
            }
        }
        public string OrderNumberOriginalValue { get; }
        public bool OrderNumberIsChanged => OrderNumberOriginalValue != OrderNumber;

        /// <summary>
        /// Дата открытия заказа
        /// </summary>
        public DateTime DateOpen
        {
            get => SalesUnits.First().DateOpen;
            set
            {
                this.SalesUnits.ForEach(x => x.DateOpen = value);
                RaisePropertyChanged(nameof(OrderNumber));
            }
        }

        /// <summary>
        /// Плановые даты поставки (от менеджера)
        /// </summary>
        public string DeliveryDatesExpected => SalesUnits
            .Select(x => x.DeliveryDateExpected)
            .Distinct()
            .OrderBy(x => x)
            .Select(x => x.ToShortDateString())
            .ToStringEnum();

        public new SalesUnitsCollection SalesUnits { get; private set; }

        public event Action ProductionRequestFinishedEvent;

        protected override void InitializeCollectionProperties()
        {
            base.InitializeCollectionProperties();

            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new SalesUnitsCollection(this.Model, Model.Status.Equals(ScriptStep.ProductionRequestStart));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }

        #endregion

        #region IgnatenkoInfo

        public Specification Specification => this.Model?.SalesUnits.FirstOrDefault()?.Specification;
        public double? Cost => this.Model?.SalesUnits.FirstOrDefault()?.Cost;

        #endregion

        /// <summary>
        /// Плановая единая дата окончания производства
        /// </summary>
        public DateTime EndProductionPlanDate
        {
            get => this.SalesUnits.First().EndProductionPlanDate.Value;
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

            OrderNumberOriginalValue = OrderNumber;

            if (this.Model.Status.Equals(ScriptStep.ProductionRequestStart) == false)
            {
                if (SalesUnits.GroupBy(x => x.Model.Order).Count() > 1)
                {
                    IsNotUniqueOrderData = false;
                }
            }

            this.IsFillingRest = true;
        }

        protected override bool SaveCommand_CanExecuteMethod()
        {
            if (this.Model.Status.Equals(ScriptStep.ProductionRequestFinish) == false)
                return false;
            return this.IsChanged || this.TasksWrapper.IsChanged;
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (IsNotUniqueOrderData)
            {
                if (string.IsNullOrWhiteSpace(this.OrderNumber))
                    yield return new ValidationResult($"{nameof(OrderNumber)} is required", new[] { nameof(OrderNumber) });
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            this.UnSub();
        }

        #endregion

        public class SalesUnitWithOrderWrapper : SalesUnitWithSignalToStartProductionWrapper
        {
            private readonly bool _orderIsRequired;

            #region SimpleProperties

            #region OrderNumber

            private string _orderNumber;
            public string OrderNumber
            {
                get => _orderNumber;
                set
                {
                    if (value == _orderNumber) return;
                    _orderNumber = value;
                    RaisePropertyChanged(nameof(OrderNumber));
                    this.Validate();
                }
            }

            public string OrderNumberOriginalValue { get; }
            public bool OrderNumberIsChanged => OrderNumberOriginalValue != OrderNumber;

            #endregion

            #region DateOpen

            private DateTime _dateOpen;
            public DateTime DateOpen
            {
                get => _dateOpen;
                set
                {
                    _dateOpen = value;
                    RaisePropertyChanged(nameof(DateOpen));
                }
            }

            #endregion

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

            #region EndProductionPlanDate;

            /// <summary>
            /// Плановая дата поставки
            /// </summary>
            public DateTime DeliveryDateExpected => Model.DeliveryDateExpected;

            #endregion


            #endregion

            #region ComplexProperties
            #endregion

            public SalesUnitWithOrderWrapper(SalesUnit model, bool orderIsRequired) : base(model)
            {
                _orderIsRequired = orderIsRequired;
                _orderNumber = OrderNumberOriginalValue = model.Order?.Number;
                DateOpen = model.Order?.DateOpen ?? DateTime.Today;
                EndProductionPlanDate = model.EndProductionPlanDate ?? DateTime.Today.AddDays(120);
            }

            protected override IEnumerable<ValidationResult> ValidateOther()
            {
                if (_orderIsRequired)
                {
                    if (string.IsNullOrWhiteSpace(this.OrderNumber))
                        yield return new ValidationResult($"{nameof(OrderNumber)} is required", new[] { nameof(OrderNumber) });
                    if (string.IsNullOrWhiteSpace(this.OrderPosition))
                        yield return new ValidationResult($"{nameof(OrderPosition)} is required", new[] { nameof(OrderPosition) });
                    if (this.EndProductionPlanDate == null)
                        yield return new ValidationResult($"{nameof(EndProductionPlanDate)} is required", new[] { nameof(EndProductionPlanDate) });
                }
            }
        }

        public class SalesUnitsCollection : ValidatableChangeTrackingCollection<SalesUnitWithOrderWrapper>
        {
            public SalesUnitsCollection(PriceEngineeringTask priceEngineeringTask, bool orderIsRequired)
                : base(priceEngineeringTask.SalesUnits.Select(salesUnit => new SalesUnitWithOrderWrapper(salesUnit, orderIsRequired)))
            {
                //ставим позиции заказа
                if (priceEngineeringTask.Status.Equals(ScriptStep.ProductionRequestStart) &&
                    this.Items.Any(salesUnit => string.IsNullOrWhiteSpace(salesUnit.OrderPosition)))
                {
                    int i = 1;
                    foreach (var salesUnit in this)
                    {
                        salesUnit.EndProductionPlanDate = salesUnit.Model.EndProductionDateCalculated.Date;
                        salesUnit.OrderPosition = i.ToString();
                        i++;
                    }
                }
            }
        }
    }
}
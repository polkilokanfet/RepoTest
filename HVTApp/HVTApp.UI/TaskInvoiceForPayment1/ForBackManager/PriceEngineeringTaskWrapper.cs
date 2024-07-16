using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class PriceEngineeringTaskWrapper : WrapperBase<PriceEngineeringTask>
    {
        public PriceEngineeringTaskWrapper(PriceEngineeringTask model) : base(model) { }

        #region CollectionProperties

        /// <summary>
        /// SalesUnits
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }

        #endregion

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.SalesUnits.Select(e => new SalesUnitWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }
    }

    public class SalesUnitWrapper : WrapperBase<SalesUnit>
    {

        /// <summary>
        /// Позиция
        /// </summary>
        public string OrderPosition
        {
            get => Model.OrderPosition;
            set => SetValue(value);
        }
        public string OrderPositionOriginalValue => GetOriginalValue<string>(nameof(OrderPosition));
        public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));

        /// <summary>
        /// Заказ
        /// </summary>
	    public OrderWrapper Order
        {
            get => GetWrapper<OrderWrapper>();
            set => SetComplexValue<Order, OrderWrapper>(Order, value);
        }

        public SalesUnitWrapper(SalesUnit model) : base(model) { }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Order), Model.Order == null ? null : new OrderWrapper(Model.Order));
        }
    }


    public class OrderWrapper : WrapperBase<Order>
    {
        public OrderWrapper(Order model) : base(model) { }

        #region SimpleProperties

        /// <summary>
        /// Номер
        /// </summary>
        public System.String Number
        {
            get { return Model.Number; }
            set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        /// <summary>
        /// Дата открытия
        /// </summary>
        public System.DateTime DateOpen
        {
            get { return Model.DateOpen; }
            set { SetValue(value); }
        }
        public System.DateTime DateOpenOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateOpen));
        public bool DateOpenIsChanged => GetIsChanged(nameof(DateOpen));

        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id
        {
            get { return Model.Id; }
            set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
    }


}
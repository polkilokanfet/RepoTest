using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.PlanAndEconomy.Supervision
{
    public class SupervisionWr: WrapperBase<Model.POCOs.Supervision>
    {
        public bool IsNew { get; set; } = false;

        public SupervisionWr(Model.POCOs.Supervision model) : base(model) { }

        public SupervisionWr(SalesUnit product) : this(new Model.POCOs.Supervision { SalesUnit = product })
        {
            IsNew = true;
        }

        public SupervisionWr(SalesUnit product, SalesUnit productSupervision) : this(new Model.POCOs.Supervision { SalesUnit = product, SupervisionUnit = productSupervision })
        {
            IsNew = true;
        }


        #region SimpleProperties

        public System.Nullable<System.DateTime> DateFinish
        {
            get { return GetValue<System.Nullable<System.DateTime>>(); }
            set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DateFinishOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateFinish));
        public bool DateFinishIsChanged => GetIsChanged(nameof(DateFinish));

        public System.Nullable<System.DateTime> DateRequired
        {
            get { return GetValue<System.Nullable<System.DateTime>>(); }
            set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DateRequiredOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateRequired));
        public bool DateRequiredIsChanged => GetIsChanged(nameof(DateRequired));

        public System.String ClientOrderNumber
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }
        public System.String ClientOrderNumberOriginalValue => GetOriginalValue<System.String>(nameof(ClientOrderNumber));
        public bool ClientOrderNumberIsChanged => GetIsChanged(nameof(ClientOrderNumber));

        public System.String ServiceOrderNumber
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }
        public System.String ServiceOrderNumberOriginalValue => GetOriginalValue<System.String>(nameof(ServiceOrderNumber));
        public bool ServiceOrderNumberIsChanged => GetIsChanged(nameof(ServiceOrderNumber));

        public System.Guid Id
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties

        public SalesUnitWrapper SupervisionUnit
        {
            get { return GetWrapper<SalesUnitWrapper>(); }
            set { SetComplexValue<SalesUnit, SalesUnitWrapper>(SupervisionUnit, value); }
        }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<SalesUnitWrapper>(nameof(SupervisionUnit), Model.SupervisionUnit == null ? null : new SalesUnitWrapper(Model.SupervisionUnit));
        }

    }
}
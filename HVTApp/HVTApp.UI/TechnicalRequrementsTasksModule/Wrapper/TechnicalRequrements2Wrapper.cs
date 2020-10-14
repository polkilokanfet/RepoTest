using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceCalculations.ViewModel;

namespace HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper
{
    public partial class TechnicalRequrements2Wrapper : WrapperBase<TechnicalRequrements>
    {
        public bool IsChecked { get; set; }

        public SalesUnit SalesUnit { get; }

        public int Amount => SalesUnits.Count;

        public string DeliveryType
        {
            get
            {
                if (SalesUnit.CostDelivery.HasValue && SalesUnit.CostDelivery > 0)
                {
                    return "доставка";
                }
                return "самовывоз";
            }
        }

        #region SimpleProperties

        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; private set; }

        public IValidatableChangeTrackingCollection<TechnicalRequrementsFileWrapper> Files { get; private set; }

        #endregion

        public TechnicalRequrements2Wrapper(TechnicalRequrements model) : base(model)
        {
            SalesUnit = model.SalesUnits.First();
        }

        protected override void InitializeCollectionProperties()
        {

            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitEmptyWrapper>(Model.SalesUnits.Select(e => new SalesUnitEmptyWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);

            if (Model.Files == null) throw new ArgumentException("Files cannot be null");
            Files = new ValidatableChangeTrackingCollection<TechnicalRequrementsFileWrapper>(Model.Files.Select(e => new TechnicalRequrementsFileWrapper(e)));
            RegisterCollection(Files, Model.Files);

        }

    }
}
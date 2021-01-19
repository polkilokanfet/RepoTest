using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.Model;

namespace HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper
{
    public class TechnicalRequrements2Wrapper : WrapperBase<TechnicalRequrements>
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

        public string DeliveryAddress => SalesUnit.GetDeliveryAddress();

        public string FacilityOwners
        {
            get
            {
                var owners = new List<Company> { SalesUnit.Facility.OwnerCompany };
                owners.AddRange(SalesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
                return owners.Distinct().ToStringEnum(" <= ");
            }
        }

        #region SimpleProperties
        public bool? IsActual
        {
            get { return GetValue<bool?>(); }
            set { SetValue(value); }
        }
        public bool? IsActualOriginalValue => GetOriginalValue<bool?>(nameof(IsActual));
        public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));


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
            this.SalesUnits.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Amount));
            };
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


        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (IsActual.HasValue && IsActual.Value)
            {
                if (!Files.Any(x => x.IsActual))
                {
                    yield return new ValidationResult("Нет ни одного актуального файла.", new []{nameof(Files)});
                }
            }
        }
    }
}
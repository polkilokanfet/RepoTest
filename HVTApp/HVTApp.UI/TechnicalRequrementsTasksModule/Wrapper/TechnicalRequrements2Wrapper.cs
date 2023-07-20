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
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;

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

        public string DeliveryAddress => SalesUnit.GetDeliveryAddressString();

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
        public DateTime? OrderInTakeDate
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }
        public DateTime? OrderInTakeDateOriginalValue => GetOriginalValue<DateTime?>(nameof(OrderInTakeDate));
        public bool OrderInTakeDateIsChanged => GetIsChanged(nameof(OrderInTakeDate));

        public DateTime? RealizationDate
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }
        public DateTime? RealizationDateOriginalValue => GetOriginalValue<DateTime?>(nameof(RealizationDate));
        public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));


        public bool? IsActual
        {
            get => GetValue<bool?>();
            set => SetValue(value);
        }
        public bool? IsActualOriginalValue => GetOriginalValue<bool?>(nameof(IsActual));
        public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));

        public int? PositionInTeamCenter
        {
            get => GetValue<int?>();
            set => SetValue(value);
        }
        public int? PositionInTeamCenterValue => GetOriginalValue<int?>(nameof(PositionInTeamCenter));
        public bool PositionInTeamCenterIsChanged => GetIsChanged(nameof(PositionInTeamCenter));


        public string Comment
        {
            get => GetValue<string>();
            set => SetValue(value);
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

            if (this.OrderInTakeDate == null) this.OrderInTakeDate = SalesUnit.OrderInTakeDate;
            if (this.RealizationDate == null) this.RealizationDate = SalesUnit.RealizationDateCalculated;
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
            if (IsActual.HasValue && IsActual.Value == true)
            {
                if (!Files.Any(technicalRequrementsFileWrapper => technicalRequrementsFileWrapper.IsActual))
                {
                    yield return new ValidationResult("Нет ни одного актуального файла требований.", new []{nameof(Files)});
                }

                if (this.OrderInTakeDate.HasValue && this.RealizationDate.HasValue && OrderInTakeDate > RealizationDate)
                {
                    yield return new ValidationResult("Дата реализации раньше даты ОИТ.", new[] { nameof(OrderInTakeDate), nameof(RealizationDate) });
                }
            }
        }
    }
}
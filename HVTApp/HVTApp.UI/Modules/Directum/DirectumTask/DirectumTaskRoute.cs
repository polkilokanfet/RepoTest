using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTaskRoute : BaseEntity
    {
        [Required]
        public List<DirectumTaskRouteItem> Items { get; } = new List<DirectumTaskRouteItem>();
    }

    public class DirectumTaskRouteWrapper : WrapperBase<DirectumTaskRoute>
    {
        [Required]
        public IValidatableChangeTrackingCollection<DirectumTaskRouteItemWrapper> Items { get; private set; }

        public DirectumTaskRouteWrapper(DirectumTaskRoute model) : base(model)
        {
        }

        protected override void InitializeCollectionProperties()
        {

            if (Model.Items == null) throw new ArgumentException("Items cannot be null");
            Items = new ValidatableChangeTrackingCollection<DirectumTaskRouteItemWrapper>(Model.Items.Select(e => new DirectumTaskRouteItemWrapper(e)));
            RegisterCollection(Items, Model.Items);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if(!Items.Any())
                yield return new ValidationResult("Не назначен ни один исполнитель.");
        }

        public override string ToString()
        {
            return !Items.Any() 
                ? "Is empty" 
                : Items.Select(x => x.Performer).ToStringEnum();
        }
    }


    public class DirectumTaskRouteItem : BaseEntity
    {
        [Required]
        public int Index { get; set; }
        [Required]
        public User Performer { get; set; }
        [Required]
        public DateTime FinishPlan { get; set; }
    }

    public class DirectumTaskRouteItemWrapper : WrapperBase<DirectumTaskRouteItem>
    {

        [Required]
        public int Index
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public int IndexOriginalValue => GetOriginalValue<int>(nameof(Index));
        public bool IndexIsChanged => GetIsChanged(nameof(Index));


        [Required]
        public DateTime FinishPlan
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
        public DateTime FinishPlanOriginalValue => GetOriginalValue<DateTime>(nameof(FinishPlan));
        public bool FinishPlanIsChanged => GetIsChanged(nameof(FinishPlan));

        [Required]
        public UserWrapper Performer
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Performer, value); }
        }

        public DirectumTaskRouteItemWrapper(DirectumTaskRouteItem model) : base(model)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Performer), Model.Performer == null ? null : new UserWrapper(Model.Performer));
        }
    }
}
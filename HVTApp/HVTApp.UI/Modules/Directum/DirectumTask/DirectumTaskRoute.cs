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

        public bool IsParallel { get; set; } = true;
    }

    public class DirectumTaskRouteWrapper : WrapperBase<DirectumTaskRoute>
    {
        public bool IsParallel
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

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
        public User Performer { get; set; }
        [Required]
        public DateTime FinishPlan { get; set; }
    }

    public class DirectumTaskRouteItemWrapper : WrapperBase<DirectumTaskRouteItem>
    {
        private bool _isParallel = true;

        public bool IsParallel
        {
            get { return _isParallel; }
            set
            {
                _isParallel = value;
                OnPropertyChanged();
            }
        }

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
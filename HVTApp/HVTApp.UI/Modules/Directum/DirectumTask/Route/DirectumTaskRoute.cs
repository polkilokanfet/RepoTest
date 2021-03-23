using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.Directum
{
    /// <summary>
    /// Маршрут задачи
    /// </summary>
    public class DirectumTaskRoute : BaseEntity
    {
        [Required]
        public List<DirectumTaskRouteItem> Items { get; } = new List<DirectumTaskRouteItem>();

        public bool IsParallel { get; set; } = true;

        public override string ToString()
        {
            if (Items.Any() == false)
            {
                return "Для формирования маршрута нажмите кнопку \"Маршрут\". Она слева.";
            }

            if (Items.Count > 1)
            {
                if (IsParallel)
                {
                    return $"Параллельный: {Items.OrderBy(directumTaskRouteItem => directumTaskRouteItem.Performer.ToString()).Select(routeItem => routeItem.Performer).ToStringEnum()}";
                }

                return $"Последовательный: {Items.OrderBy(routeItem => routeItem.FinishPlan).ThenBy(directumTaskRouteItem => directumTaskRouteItem.Performer.ToString()).Select(routeItem => routeItem.Performer).ToStringEnum(" => ")}";
            }

            return $"{Items.Select(routeItem => routeItem.Performer).ToStringEnum()}";
        }
    }

    public class DirectumTaskRouteWrapper : WrapperBase<DirectumTaskRoute>
    {
        public bool IsParallel
        {
            get => GetValue<bool>();
            set => SetValue(value);
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
    }



    /// <summary>
    /// Этап маршрута задачи
    /// </summary>
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
            get => _isParallel;
            set
            {
                _isParallel = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public DateTime FinishPlan
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public DateTime FinishPlanOriginalValue => GetOriginalValue<DateTime>(nameof(FinishPlan));
        public bool FinishPlanIsChanged => GetIsChanged(nameof(FinishPlan));

        [Required]
        public UserWrapper Performer
        {
            get => GetWrapper<UserWrapper>();
            set => SetComplexValue<User, UserWrapper>(Performer, value);
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
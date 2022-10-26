using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.PriceEngineering.Items
{
    public abstract class PriceEngineeringTasksListItemBase<TChildTask> : LookupItem<PriceEngineeringTasks>
        where TChildTask: PriceEngineeringTaskListItemBase
    {
        [Designation("Объекты"), OrderStatus(5000)]
        public string Facilities =>
            Entity.ChildPriceEngineeringTasks
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct()
                .OrderBy(x => x.Name)
                .ToStringEnum();

        [Designation("Блоки"), OrderStatus(4000)]
        public string ProductBlocks =>
            Entity.ChildPriceEngineeringTasks
                .Select(x => x.ProductBlockEngineer)
                .Distinct()
                .OrderBy(x => x.Designation)
                .ToStringEnum();

        [Designation("Исполнители"), OrderStatus(3000)]
        public string Users => ChildPriceEngineeringTasks
            .Select(x => x.Entity.UserConstructor)
            .Where(x => x != null)
            .Distinct()
            .ToStringEnum();

        public string StatusString => ChildPriceEngineeringTasks
            .Select(x => x.StatusString)
            .Distinct()
            .OrderBy(x => x)
            .ToStringEnum();

        public bool ToShow => ChildPriceEngineeringTasks.Any(x => x.ToShow);

        public bool ToShowTce
        {
            get
            {
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                    {
                        return true;
                    }
                    case Role.BackManager:
                    {
                        return Entity.PriceCalculations.Any(x => x.IsTceConnected && x.LastHistoryItem.Type == PriceCalculationHistoryItemType.Create);
                    }
                    case Role.BackManagerBoss:
                    {
                        return Entity.BackManager == null;
                    }
                }

                return true;
            }
        }

        #region Model props

        #region SimpleProperties

        [OrderStatus(2000)]
        public System.String TceNumber => Entity.TceNumber;

        [OrderStatus(1500)]
        public System.DateTime WorkUpTo => Entity.WorkUpTo;

        [OrderStatus(1400)]
        public System.String Comment => Entity.Comment;

        [OrderStatus(1)]
        public System.Boolean IsAccepted => Entity.IsAccepted;

        [OrderStatus(2000)]
        public System.Nullable<System.DateTime> StartMoment => Entity.StartMoment;

        #endregion

        #region ComplexProperties
        [OrderStatus(1900)]
        public UserLookup UserManager { get { return GetLookup<UserLookup>(); } }

        [OrderStatus(1800)]
        public UserLookup BackManager { get { return GetLookup<UserLookup>(); } }

        #endregion

        public IEnumerable<TChildTask> ChildPriceEngineeringTasks { get; }

        #endregion

        protected PriceEngineeringTasksListItemBase(PriceEngineeringTasks entity) : base(entity)
        {
            ChildPriceEngineeringTasks = GetChildTasks();
        }

        protected abstract IEnumerable<TChildTask> GetChildTasks();

    }
}
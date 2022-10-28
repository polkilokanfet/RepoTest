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
        public abstract string Facilities { get; }
            
        [Designation("Блоки"), OrderStatus(4000)]
        public string ProductBlocks =>
            Entity.ChildPriceEngineeringTasks
                .Select(x => x.ProductBlock)
                .Distinct()
                .OrderBy(x => x.Designation)
                .ToStringEnum();

        [Designation("Исполнители"), OrderStatus(3000)]
        public string Users =>
            this.ChildPriceEngineeringTasks
                .Select(x => x.Entity.UserConstructor?.Employee.Person)
                .Where(x => x != null)
                .Distinct()
                .OrderBy(x => x.ToString())
                .ToStringEnum();

        public string StatusString =>
            this.ChildPriceEngineeringTasks
                .Select(x => x.StatusString)
                .Distinct()
                .OrderBy(x => x)
                .ToStringEnum();

        public string Numbers { get; }

        public bool ToShow => ChildPriceEngineeringTasks.Any(x => x.ToShow);

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
        public System.DateTime? StartMoment => Entity.StartMoment;

        #endregion

        #region ComplexProperties

        [OrderStatus(1800)]
        public UserLookup BackManager { get { return GetLookup<UserLookup>(); } }

        #endregion

        public IEnumerable<TChildTask> ChildPriceEngineeringTasks { get; }

        #endregion

        protected PriceEngineeringTasksListItemBase(PriceEngineeringTasks entity) : base(entity)
        {
            ChildPriceEngineeringTasks = GetChildTasks().OrderBy(x => x.Entity.Number).ToList();
            Numbers = $"{entity.NumberFull} ({ChildPriceEngineeringTasks.Select(x => x.Entity.Number).ToStringEnum()})";
        }

        protected abstract IEnumerable<TChildTask> GetChildTasks();

    }
}
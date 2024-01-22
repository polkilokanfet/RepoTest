using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.PriceEngineering.Items
{
    public abstract class PriceEngineeringTasksListItemBase<TChildTask> : LookupItem<PriceEngineeringTasks>
        where TChildTask: PriceEngineeringTaskListItemBase
    {
        private bool _toShowFilt = true;

        /// <summary>
        /// Фильтрационный флаг
        /// </summary>
        public bool ToShowFilt
        {
            get => _toShowFilt;
            set => this.SetProperty(ref _toShowFilt, value);
        }

        public DateTime TermPriority => this
            .ChildPriceEngineeringTasks
            .Select(childTask => childTask.Entity.TermPriority ?? this.Entity.WorkUpTo)
            .Min();

        [Designation("Объекты"), OrderStatus(5000)]
        public string Facilities =>
            GetSalesUnits()
                .Where(salesUnit => salesUnit != null)
                .Select(salesUnit => salesUnit.Facility)
                .Distinct()
                .OrderBy(facility => facility.Name)
                .ToStringEnum();

        [Designation("Заказы"), OrderStatus(2000)]
        public string Orders =>
            GetSalesUnits()
                .Where(salesUnit => salesUnit != null)
                .Select(salesUnit => salesUnit.Order)
                .Where(order => order != null)
                .Distinct()
                .OrderBy(order => order.Number)
                .ToStringEnum();

        protected abstract IEnumerable<SalesUnit> GetSalesUnits();

        [Designation("Блоки"), OrderStatus(4000)]
        public string ProductBlocks
        {
            get
            {
                var childPriceEngineeringTasks = this.ChildPriceEngineeringTasks;
                if (GlobalAppProperties.User.RoleCurrent != Role.Constructor &&
                    GlobalAppProperties.User.RoleCurrent != Role.DesignDepartmentHead)
                {
                    childPriceEngineeringTasks = childPriceEngineeringTasks.Where(task => task.Entity.ParentPriceEngineeringTaskId.HasValue == false);
                }

                return
                    childPriceEngineeringTasks
                        .Select(childTask => childTask.Entity.ProductBlock.Designation)
                        .Distinct()
                        .OrderBy(x => x)
                        .ToStringEnum();
            }
        }

        [Designation("Исполнители"), OrderStatus(3000)]
        public string Users =>
            this.ChildPriceEngineeringTasks
                .Select(childTask => childTask.Entity.UserConstructor?.Employee.Person)
                .Where(person => person != null)
                .Distinct()
                .OrderBy(person => person.ToString())
                .ToStringEnum();

        public string StatusString =>
            this.ChildPriceEngineeringTasks
                .Select(childTask => childTask.StatusString)
                .Distinct()
                .OrderBy(x => x)
                .ToStringEnum();

        public string Numbers { get; }

        public virtual bool ToShow => ChildPriceEngineeringTasks.Any(childTask => childTask.ToShow);

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

        public string BackManagerString => Entity.BackManager?.Employee.Person.ToString();

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
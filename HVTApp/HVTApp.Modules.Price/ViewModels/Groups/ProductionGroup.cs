using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class ProductionGroup : GroupBase<ProductionGroup>
    {
        public DateTime? EndProductionPlanDate
        {
            get { return Unit.EndProductionPlanDate; }
            set { SetValue(value); }
        }

        public OrderWrapper Order
        {
            get { return Unit.Order; }
            set { SetValue(value); }
        }

        public DateTime? SignalToStartProductionDone
        {
            get { return Unit.SignalToStartProductionDone; }
            set { SetValue(value); }
        }

        public string OrderPosition
        {
            get { return Groups != null ? "..." : Unit.OrderPosition; }
            set
            {
                if (Groups != null) return;
                SetValue(value);
            }
        }

        public ProductionGroup(IEnumerable<SalesUnitWrapper> salesUnitWrappers) : base(salesUnitWrappers)
        {
        }

        /// <summary>
        /// Простановка позиций в заказе
        /// </summary>
        public void FillPositions()
        {
            //ставим позиции заказа
            int pos = 1;
            if (Groups != null && Groups.Any())
            {
                foreach (var @group in Groups)
                {
                    @group.OrderPosition = (pos++).ToString();
                }
            }
            else
            {
                OrderPosition = pos.ToString();
            }
        }

        public static IEnumerable<ProductionGroup> Grouping(IEnumerable<SalesUnitWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Facility.Id,
                Product = x.Product.Id,
                Order = x.Order?.Id,
                Project = x.Project.Id,
                Specification = x.Specification?.Id,
                x.EndProductionPlanDate
            }).OrderBy(x => x.Key.EndProductionPlanDate);

            return groups.Select(x => new ProductionGroup(x));
        }

        protected override IEnumerable<ProductionGroup> CreateGroups(IEnumerable<SalesUnitWrapper> salesUnitWrappers)
        {
            return salesUnitWrappers.Select(x => new ProductionGroup(new[] {x}));
        }
    }
}
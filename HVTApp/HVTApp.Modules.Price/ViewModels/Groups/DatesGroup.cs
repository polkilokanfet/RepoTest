using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class DatesGroup : GroupBase<DatesGroup>
    {

        public string SerialNumber
        {
            get { return Groups != null ? "..." : Unit.SerialNumber; }
            set
            {
                if (Groups != null) return;
                SetValue(value);
            }
        }

        public string OrderPosition => Groups != null ? $"{Amount} רע." : Unit.OrderPosition;

        public DateTime? DeliveryDate
        {
            get { return Unit.DeliveryDate; }
            set { SetValue(value); }
        }

        public DateTime? EndProductionDate
        {
            get { return Unit.EndProductionDate; }
            set { SetValue(value); }
        }

        public DateTime? PickingDate
        {
            get { return Unit.PickingDate; }
            set { SetValue(value); }
        }

        public DateTime? RealizationDate
        {
            get { return Unit.RealizationDate; }
            set { SetValue(value); }
        }

        public DateTime? ShipmentDate
        {
            get { return Unit.ShipmentDate; }
            set { SetValue(value); }
        }

        public DatesGroup(IEnumerable<SalesUnitWrapper> salesUnitWrappers) : base(salesUnitWrappers: salesUnitWrappers)
        {
        }

        protected override IEnumerable<DatesGroup> CreateGroups(IEnumerable<SalesUnitWrapper> salesUnitWrappers)
        {
            return salesUnitWrappers.Select(salesUnitWrapper => new DatesGroup(new[] {salesUnitWrapper}));
        }
    }
}
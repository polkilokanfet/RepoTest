using System;

namespace HVTApp.Model.Wrappers
{
    public partial class ShipmentUnitWrapper
    {
        public DateTime ShipmentDateCalculated => DateTime.Today;
        public DateTime DeliveryDateCalculated => DateTime.Today;
    }
}
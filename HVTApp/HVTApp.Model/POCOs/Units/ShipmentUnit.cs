﻿using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ShipmentUnit : BaseEntity
    {
        public virtual SalesUnit SalesUnit { get; set; }

        public int? ExpectedDeliveryPeriod { get; set; }
        public virtual Address Address { get; set; }
        public double Cost { get; set; }

        public virtual DateTime? ShipmentDate { get; set; } //дата отгрузки
        public virtual DateTime? ShipmentPlanDate { get; set; } //плановая дата отгрузки
        public virtual DateTime? RequiredDeliveryDate { get; set; } //желаемая дата поставки
        public virtual DateTime? DeliveryDate { get; set; } //дата поставки
    }
}
﻿using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }

        /// <summary>
        /// Дата открытия заказа.
        /// </summary>
        public virtual DateTime? OpenOrderDate { get; set; }

        public virtual List<ProductionUnit> ProductionProductUnits { get; set; }
    }
}
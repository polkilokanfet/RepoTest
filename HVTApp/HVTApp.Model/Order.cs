using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }

        /// <summary>
        /// Дата открытия заказа.
        /// </summary>
        public virtual DateTime? OpenOrderDate { get; set; }

        public virtual List<ProductMain> Products { get; set; }
    }
}
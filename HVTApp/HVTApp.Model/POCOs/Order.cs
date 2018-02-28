using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Order : BaseEntity
    {
        public string Number { get; set; }
        public DateTime OpenOrderDate { get; set; }

        public override string ToString()
        {
            return $"з/з {Number}";
        }
    }
}
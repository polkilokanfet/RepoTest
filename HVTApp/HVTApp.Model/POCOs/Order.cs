using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Order : BaseEntity
    {
        public string Number { get; set; }
        public DateTime DateOpen { get; set; }

        public override string ToString()
        {
            return $"{Number}";
        }
    }
}
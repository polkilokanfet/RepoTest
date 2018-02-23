using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Contract : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual Company Contragent { get; set; }

        public override string ToString()
        {
            return $"Contract with {Contragent} №{Number} of {Date}";
        }
    }
}
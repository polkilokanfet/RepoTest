using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Контракт")]
    public partial class Contract : BaseEntity
    {
        [Designation("№")]
        public string Number { get; set; }

        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("Контрагент")]
        public virtual Company Contragent { get; set; }

        public override string ToString()
        {
            return $"Contract with {Contragent} №{Number} of {Date}";
        }
    }
}
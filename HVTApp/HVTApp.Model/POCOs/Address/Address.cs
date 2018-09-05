using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Адрес")]
    [DesignationPlural("Адреса")]
    public partial class Address : BaseEntity
    {
        [Designation("Описание")]
        public string Description { get; set; }

        public virtual Locality Locality { get; set; }

        public override string ToString()
        {
            return $"{Locality}, {Description}";
        }

    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("�������� ������")]
    public partial class ProductBlockIsService : BaseEntity
    {
        [Designation("���������"), Required]
        public virtual List<Parameter> Parameters { get; set; }

        [Designation("��� ������"), Required]
        public ProductBlockServiceType Type { get; set; }
    }
}
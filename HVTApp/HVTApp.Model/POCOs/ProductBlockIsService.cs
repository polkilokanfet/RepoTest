using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("�������� ������")]
    public class ProductBlockIsService : BaseEntity
    {
        public virtual List<Parameter> Parameters { get; set; }
    }
}
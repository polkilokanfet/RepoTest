using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.POCOs
{
    [Designation("������������ - ��������� (������)")]
    public class ConstructorParametersList : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        [Designation("���������"), Required]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public override string ToString()
        {
            return $"{Name} ({Parameters.ToStringEnum()})";
        }
    }
}
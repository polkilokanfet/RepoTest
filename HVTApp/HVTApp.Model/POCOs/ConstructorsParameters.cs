using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.POCOs
{
    [Designation("������������ - ���������")]
    public class ConstructorsParameters : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        [Designation("������������"), Required]
        public virtual List<User> Constructors { get; set; } = new List<User>();

        [Designation("������ ����������"), Required]
        public virtual List<ConstructorParametersList> PatametersLists { get; set; } = new List<ConstructorParametersList>();

        public override string ToString()
        {
            return $"{Name} ({PatametersLists.ToStringEnum()})";
        }
    }
}
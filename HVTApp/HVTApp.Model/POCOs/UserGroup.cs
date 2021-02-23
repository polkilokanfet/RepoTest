using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ �������������")]
    [DesignationPlural("������ �������������")]
    public class UserGroup : BaseEntity
    {
        [Designation("��� ������"), Required, MaxLength(256)]
        public string Name { get; set; }

        [Designation("������������"), Required]
        public virtual List<User> Users { get; set; } = new List<User>();
    }
}
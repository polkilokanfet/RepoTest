using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������������")]
    [DesignationPlural("������������")]
    public partial class User : BaseEntity
    {
        [Designation("�����"), Required, MaxLength(20), OrderStatus(20)]
        public string Login { get; set; }

        [Designation("������"), Required, OrderStatus(2)]
        public Guid Password { get; set; } = StringToGuid.GetHashString("1");

        [Designation("������� ����"), NotMapped]
        public Role RoleCurrent { get; set; }

        [Designation("����"), Required]
        public virtual List<UserRole> Roles { get; set; } = new List<UserRole>();

        [Designation("���������"), Required, OrderStatus(25)]
        public virtual Employee Employee { get; set; }

        [Designation("��������"), Required, OrderStatus(5)]
        public bool IsActual { get; set; } = true;

        public override string ToString()
        {
            return $"{Employee.Person} ({Employee.Position})";
        }
    }
}
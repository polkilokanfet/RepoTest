using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("������������")]
    [DesignationPlural("������������")]
    public class User : BaseEntity
    {
        [Designation("�����")]
        public string Login { get; set; }

        [Designation("������")]
        public Guid Password { get; set; }

        [Designation("����")]
        public string PersonalNumber { get; set; }

        [Designation("������� ����")]
        public Role RoleCurrent { get; set; }

        [Designation("����")]
        public virtual List<UserRole> Roles { get; set; } = new List<UserRole>();

        [Designation("���������")]
        public virtual Employee Employee { get; set; }

        public override string ToString()
        {
            return $"{Employee.Person} ({Employee.Position})";
        }
    }
}
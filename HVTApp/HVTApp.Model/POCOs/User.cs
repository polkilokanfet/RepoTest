using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Пользователь")]
    [DesignationPlural("Пользователи")]
    public partial class User : BaseEntity
    {
        [Designation("Логин"), Required, MaxLength(20), OrderStatus(20)]
        public string Login { get; set; }

        [Designation("Пароль"), Required, OrderStatus(2)]
        public Guid Password { get; set; }

        [Designation("Шифр"), Required, MaxLength(10), OrderStatus(15)]
        public string PersonalNumber { get; set; }

        [Designation("Текущая роль"), NotMapped]
        public Role RoleCurrent { get; set; }

        [Designation("Роли"), Required]
        public virtual List<UserRole> Roles { get; set; } = new List<UserRole>();

        [Designation("Сотрудник"), Required, OrderStatus(25)]
        public virtual Employee Employee { get; set; }

        public override string ToString()
        {
            return $"{Employee.Person} ({Employee.Position})";
        }
    }
}
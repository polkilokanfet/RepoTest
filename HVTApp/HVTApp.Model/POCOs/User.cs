using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Пользователь")]
    [DesignationPlural("Пользователи")]
    public class User : BaseEntity
    {
        [Designation("Логин")]
        public string Login { get; set; }

        [Designation("Пароль")]
        public Guid Password { get; set; }

        [Designation("Шифр")]
        public string PersonalNumber { get; set; }

        [Designation("Текущая роль")]
        public Role RoleCurrent { get; set; }

        [Designation("Роли")]
        public virtual List<UserRole> Roles { get; set; }

        [Designation("Сотрудник")]
        public virtual Employee Employee { get; set; }
    }
}
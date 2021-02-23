using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Группа пользователей")]
    [DesignationPlural("Группы пользователей")]
    public class UserGroup : BaseEntity
    {
        [Designation("Имя группы"), Required, MaxLength(256)]
        public string Name { get; set; }

        [Designation("Пользователи"), Required]
        public virtual List<User> Users { get; set; } = new List<User>();
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Департамент ОГК")]
    [DesignationPlural("Департаменты ОГК")]
    public class DesignDepartment : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(120), OrderStatus(110)]
        public string Name { get; set; }

        [Designation("Руководитель"), OrderStatus(100)]
        public virtual User Head { get; set; }

        [Designation("Сотрудники"), Required, OrderStatus(90)]
        public virtual List<User> Staff { get; set; } = new List<User>();

        [Designation("Наборы параметров"), OrderStatus(50)]
        public virtual List<DesignDepartmentParameters> ParameterSets { get; set; } = new List<DesignDepartmentParameters>();
    }
}
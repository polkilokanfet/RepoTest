﻿using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Employee : BaseEntity
    {
        public Person Person { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Company Company { get; set; }
        public virtual EmployeesPosition Position { get; set; }
    }

    /// <summary>
    /// Должность сотрудника.
    /// </summary>
    public class EmployeesPosition : BaseEntity
    {
        public string Name { get; set; }
    }

}
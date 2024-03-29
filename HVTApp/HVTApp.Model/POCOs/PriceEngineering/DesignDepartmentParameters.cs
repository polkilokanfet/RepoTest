﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.POCOs
{
    [Designation("Параметры департамента ОГК")]
    [DesignationPlural("Параметры департамента ОГК")]
    public class DesignDepartmentParameters : BaseEntity, IParametersContainer
    {
        [Designation("Id департамента"), Required, OrderStatus(-10)]
        public Guid DesignDepartmentId { get; set; }

        [Designation("Название набора параметров"), MaxLength(100), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("Параметры"), Required]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name)
                ? Parameters.ToStringEnum()
                : Name;
        }
    }
}
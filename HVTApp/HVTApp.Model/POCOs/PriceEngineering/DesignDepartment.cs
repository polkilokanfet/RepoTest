﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

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

        /// <summary>
        /// Блок продукта подходит этому департаменту
        /// </summary>
        /// <param name="productBlock"></param>
        /// <returns></returns>
        public bool ProductBlockIsSuitable(ProductBlock productBlock)
        {
            foreach (var parameterSet in ParameterSets)
            {
                if (parameterSet.Parameters.AllContainsInById(productBlock.Parameters))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
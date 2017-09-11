using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Параметры обязательных дочерних продуктов.
    /// </summary>
    public class RequiredDependentProductsParameters : BaseEntity
    {
        public virtual List<Parameter> MainProductParameters { get; set; } = new List<Parameter>();
        public virtual List<Parameter> ChildProductParameters { get; set; } = new List<Parameter>();
        public int Count { get; set; } = 1;
    }
}
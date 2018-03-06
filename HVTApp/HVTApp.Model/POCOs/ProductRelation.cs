using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Параметры обязательных дочерних продуктов.
    /// </summary>
    public partial class ProductRelation : BaseEntity
    {
        public virtual List<Parameter> ParentProductParameters { get; set; } = new List<Parameter>();
        public virtual List<Parameter> ChildProductParameters { get; set; } = new List<Parameter>();
        public int ChildProductsAmount { get; set; } = 1;
        public bool IsUnique { get; set; } = true;
    }
}
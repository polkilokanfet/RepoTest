using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Equipment : BaseEntity
    {
        public string Designation { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<Equipment> DependentEquipments { get; set; } = new List<Equipment>();
    }

    public class Product : BaseEntity
    {
        public string Designation { get; set; }
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public virtual List<CostOnDate> Prices { get; set; } = new List<CostOnDate>(); //себестоимости по датам

        public string StructureCostNumber { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Designation))
                return Designation;

            string result = "";
            result = Parameters.Aggregate(result, (current, parameter) => current + (parameter.Value + " "));
            return result;
        }
    }

    /// <summary>
    /// Параметры обязательных дочерних продуктов.
    /// </summary>
    public class RequiredDependentEquipmentsParameters : BaseEntity
    {
        public virtual List<Parameter> MainProductParameters { get; set; } = new List<Parameter>();
        public virtual List<Parameter> ChildProductParameters { get; set; } = new List<Parameter>();
        public int Count { get; set; } = 1;
    }
}

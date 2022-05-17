using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Lookup
{
    public partial class ProductRelationLookup
    {
        [Designation("Родительские параметры")]
        public string ParentProductParametersString =>
            this.Entity.ParentProductParameters
                .Select(x => $"[({x.ParameterGroup}) : ({x.Value})]")
                .OrderBy(x => x)
                .ToStringEnum();

        [Designation("Дочерние параметры")]
        public string ChildProductParametersString => 
            this.Entity.ChildProductParameters
                .Select(x => $"[({x.ParameterGroup}) : ({x.Value})]")
                .OrderBy(x => x)
                .ToStringEnum();
    }
}
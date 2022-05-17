using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Lookup
{
    public partial class ProductRelationLookup
    {
        [Designation("������������ ���������")]
        public string ParentProductParametersString =>
            this.Entity.ParentProductParameters
                .Select(x => $"[({x.ParameterGroup}) : ({x.Value})]")
                .OrderBy(x => x)
                .ToStringEnum();

        [Designation("�������� ���������")]
        public string ChildProductParametersString => 
            this.Entity.ChildProductParameters
                .Select(x => $"[({x.ParameterGroup}) : ({x.Value})]")
                .OrderBy(x => x)
                .ToStringEnum();
    }
}
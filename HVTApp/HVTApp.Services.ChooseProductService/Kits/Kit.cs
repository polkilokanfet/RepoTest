using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService.Kits
{
    public class Kit
    {
        public Product Product { get; }

        public string ComplectType { get; }
        public string ComplectDesignation { get; }
        public string StructureCost { get; }
        public string DesignDepartments { get; }

        public Kit(Product product)
        {
            Product = product;
            ComplectType = product.ProductBlock.Parameters.Single(parameter => parameter.ParameterGroup.IsComplectsGroup()).Value;
            ComplectDesignation = product.ProductBlock.Parameters.Single(parameter => parameter.ParameterGroup.IsComplectDesignationGroup()).Value;
            StructureCost = product.ProductBlock.StructureCostNumber;
            if (product.DesignDepartmentsKits.Any())
                DesignDepartments = product.DesignDepartmentsKits.ToStringEnum();
        }

        public override string ToString()
        {
            return $"{ComplectType} {ComplectDesignation}";
        }
    }
}
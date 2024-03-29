using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService.Complects
{
    public class Complect
    {
        public Product Product { get; }

        public string ComplectType { get; }
        public string ComplectDesignation{ get; }

        public Complect(Product product)
        {
            Product = product;
            ComplectType = product.ProductBlock.Parameters.Single(parameter => parameter.ParameterGroup.IsComplectsGroup()).Value;
            ComplectDesignation = product.ProductBlock.Parameters.Single(parameter => parameter.ParameterGroup.IsComplectDesignationGroup()).Value;
        }

        public override string ToString()
        {
            return $"{ComplectType} {ComplectDesignation}";
        }
    }
}
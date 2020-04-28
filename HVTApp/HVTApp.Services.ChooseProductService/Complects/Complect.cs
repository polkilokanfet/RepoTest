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
            ComplectType = product.ProductBlock.Parameters.Single(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id).Value;
            ComplectDesignation = product.ProductBlock.Parameters.Single(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectDesignationGroup.Id).Value;
        }
    }
}
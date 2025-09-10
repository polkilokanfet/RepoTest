using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IProductDesignationService
    {
        string GetDesignation(ProductBlock block);
        string GetDesignation(Product product);

        ProductCategory GetProductCategory(Product product);
        ProductCategory GetProductCategory(ProductBlock block);

        ProductType GetProductType(ProductBlock block);
        ProductType GetProductType(Product product);
    }
}
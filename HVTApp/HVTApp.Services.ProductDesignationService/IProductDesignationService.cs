using HVTApp.Model.POCOs;

namespace HVTApp.Services.ProductDesignationService
{
    public interface IProductDesignationService
    {
        string GetDesignation(ProductBlock block);
        ProductType GetProductType(ProductBlock block);

        string GetDesignation(Product product);
        ProductType GetProductType(Product product);

        bool IsService(ProductBlock block);
    }
}
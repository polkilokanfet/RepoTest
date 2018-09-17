using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
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
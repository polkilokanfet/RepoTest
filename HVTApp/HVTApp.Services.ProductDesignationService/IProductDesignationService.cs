using HVTApp.Model.POCOs;

namespace HVTApp.Services.ProductDesignationService
{
    public interface IProductDesignationService
    {
        string GetDesignation(Product product);
    }
}
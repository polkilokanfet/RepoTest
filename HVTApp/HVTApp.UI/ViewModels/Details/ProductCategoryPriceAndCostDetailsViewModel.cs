using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductCategoryPriceAndCostDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForSelectCategoryCommand =
                () =>
                {
                    var result = UnitOfWork.Repository<ProductCategory>().GetAll();
                    var exists = UnitOfWork.Repository<ProductCategoryPriceAndCost>().GetAll().Select(x => x.Category).ToList();
                    return result.Except(exists).ToList();
                };
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IGetProductService
    {
        //Task<Product> GetProductAsync(Product originProduct = null);

        /// <summary>
        /// Выбор продукта
        /// </summary>
        /// <param name="originProduct"></param>
        /// <returns></returns>
        Product GetProduct(Product originProduct = null);

        Product GetProduct(IEnumerable<Parameter> requiredParameters);

        Product GetProduct(IUnitOfWork unitOfWork, Product product);

        /// <summary>
        /// Выбор ремонтного комплекта
        /// </summary>
        /// <param name="originProduct"></param>
        /// <returns></returns>
        Product GetComplect(Product originProduct = null);

        /// <summary>
        /// Выбор блока продукта
        /// </summary>
        /// <param name="originProductBlock">Предварительно выбранный блок продукта</param>
        /// <param name="requiredParameters">Обязательные параметры выбираемого блока продукта</param>
        /// <returns></returns>
        ProductBlock GetProductBlock(ProductBlock originProductBlock = null, IEnumerable<Parameter> requiredParameters = null);

        /// <summary>
        /// Выбор блока продукта
        /// </summary>
        /// <param name="parametersContainers"></param>
        /// <param name="originProductBlock"></param>
        /// <returns></returns>
        ProductBlock GetProductBlock(IEnumerable<IParametersContainer> parametersContainers, ProductBlock originProductBlock = null);
    }
}

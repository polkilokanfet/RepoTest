using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    class BankFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Формирование банка для выбора продукта.
        /// </summary>
        /// <param name="originProduct"></param>
        /// <returns></returns>
        public Bank CreateBank(Product originProduct = null)
        {
            var parameters = GetParameters(originProduct);
            var productRelations = _unitOfWork.Repository<ProductRelation>().GetAll();
            var productBlocks = _unitOfWork.Repository<ProductBlock>().GetAll();

            return new Bank(productBlocks, parameters, productRelations);
        }

        /// <summary>
        /// Формирование банка для выбора блока продукта.
        /// </summary>
        /// <param name="requiredParameters">Обязательные параметры в селекторе</param>
        /// <returns></returns>
        public Bank CreateBank(IEnumerable<Parameter> requiredParameters)
        {
            var parameters = GetParameters();
            var productRelations = _unitOfWork.Repository<ProductRelation>().GetAll();
            var productBlocks = _unitOfWork.Repository<ProductBlock>().GetAll();

            if (requiredParameters != null)
            {
                //находим максимальное количество пересечений путей параметров
                List<Parameter> requiredPathParameters = null;
                var requiredParametersArray = requiredParameters as Parameter[] ?? requiredParameters.ToArray();
                foreach (var requiredParameter in requiredParametersArray)
                {
                    var pathsParameters = requiredParameter.Paths()
                        .SelectMany(path => path.Parameters)
                        .Distinct()
                        .ToList();

                    requiredPathParameters = requiredPathParameters == null 
                        ? pathsParameters 
                        : pathsParameters.Intersect(requiredPathParameters).ToList();
                }

                //оставляем обязательные параметры "одинокими"
                foreach (var parameter in requiredPathParameters.Union(requiredParametersArray).Distinct())
                {
                    parameters = parameters.LeaveParameterAloneInGroup(parameter);
                }
            }

            return new Bank(productBlocks, parameters, productRelations);
        }

        private IEnumerable<Parameter> GetParameters(Product originProduct = null)
        {
            return _unitOfWork.Repository<Parameter>()
                .GetAll()
                .WithoutComplects(originProduct)
                .WithoutNew(originProduct);
        }
    }
}
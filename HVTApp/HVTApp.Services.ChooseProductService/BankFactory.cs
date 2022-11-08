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
        /// ������������ ����� ��� ������ ��������.
        /// </summary>
        /// <param name="originProduct"></param>
        /// <returns></returns>
        public Bank CreateBank(Product originProduct)
        {
            var parameters = _unitOfWork.Repository<Parameter>()
                .GetAll()
                .WithoutComplects(originProduct)
                .WithoutNew(originProduct);
            var products = _unitOfWork.Repository<Product>().GetAll();
            var productRelations = _unitOfWork.Repository<ProductRelation>().GetAll();
            var productBlocks = _unitOfWork.Repository<ProductBlock>().GetAll();

            return new Bank(products, productBlocks, parameters, productRelations);
        }

        /// <summary>
        /// ������������ ����� ��� ������ ����� ��������.
        /// </summary>
        /// <param name="requiredParameters">������������ ��������� � ���������</param>
        /// <returns></returns>
        public Bank CreateBank(IEnumerable<Parameter> requiredParameters)
        {
            var parameters = _unitOfWork.Repository<Parameter>()
                .GetAll()
                .WithoutComplects()
                .WithoutNew();
            var products = _unitOfWork.Repository<Product>().GetAll();
            var productRelations = _unitOfWork.Repository<ProductRelation>().GetAll();
            var productBlocks = _unitOfWork.Repository<ProductBlock>().GetAll();

            if (requiredParameters != null)
            {
                //������� ������������ ���������� ����������� ����� ����������
                List<Parameter> requiredPathParameters = null;
                foreach (var requiredParameter in requiredParameters)
                {
                    var pathsParameters = requiredParameter.Paths()
                        .SelectMany(path => path.Parameters)
                        .Distinct()
                        .ToList();

                    requiredPathParameters = requiredPathParameters == null 
                        ? pathsParameters 
                        : pathsParameters.Intersect(requiredPathParameters).ToList();
                }

                //��������� ������������ ��������� "���������"
                foreach (var parameter in requiredPathParameters.Union(requiredParameters).Distinct())
                {
                    parameters = parameters.LeaveParameterAloneInGroup(parameter).ToList();
                }
            }

            return new Bank(products, productBlocks, parameters, productRelations);
        }
    }
}
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
        public Bank CreateBank(Product originProduct = null)
        {
            return this.GetBank(GetParameters(originProduct));
        }

        /// <summary>
        /// ������������ ����� ��� ������ ����� ��������.
        /// </summary>
        /// <param name="requiredParameters">������������ ��������� � ���������</param>
        /// <returns></returns>
        public Bank CreateBank(IEnumerable<Parameter> requiredParameters)
        {
            var parameters = GetParameters();

            if (requiredParameters != null)
            {
                //������� ������������ ���������� ����������� ����� ����������
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

                //��������� ������������ ��������� "���������"
                foreach (var parameter in requiredPathParameters.Union(requiredParametersArray).Distinct())
                {
                    parameters = parameters.LeaveParameterAloneInGroup(parameter);
                }
            }

            return this.GetBank(parameters);
        }


        /// <summary>
        /// ������������ ����� ��� ������ ����� ��������.
        /// </summary>
        /// <returns></returns>
        public Bank CreateBankP(IEnumerable<Parameter> parameters)
        {
            return this.GetBank(parameters);
        }

        private Bank GetBank(IEnumerable<Parameter> parameters)
        {
            var relations = _unitOfWork.Repository<ProductRelation>().GetAll();
            var blocks = _unitOfWork.Repository<ProductBlock>()
                .Find(block => block.DesignationSpecial != null);
            var specialDesignationsDictionary = blocks
                .ToDictionary(block => block.GetHashCode(), block => block.DesignationSpecial);

            return new Bank(parameters, specialDesignationsDictionary, relations);
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
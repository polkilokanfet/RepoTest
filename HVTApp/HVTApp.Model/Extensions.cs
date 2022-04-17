using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;

namespace HVTApp.Model
{
    public static class Extensions
    {
        public static Parameter AddRequiredPreviousParameters(this Parameter parameter, params Parameter[] requiredPreviousParameters)
        {
            parameter.ParameterRelations.Add(new ParameterRelation
            {
                RequiredParameters = new List<Parameter>(requiredPreviousParameters)
            });
            return parameter;
        }

        public static double GetWeight(this Parameter parameter, ProductBlock block)
        {
            if (parameter.IsOrigin) return 2;
            return 1.0 / parameter.StepsToOrigin(block);
        }

        public static int StepsToOrigin(this Parameter parameter, ProductBlock block)
        {
            if (parameter.IsOrigin) return 0;

            var relations = parameter.ParameterRelations.Where(x => x.RequiredParameters.AllContainsIn(block.Parameters)).ToList();
            if (!relations.Any()) throw new ArgumentException("������� ��������, ������� �� ������ ���� � �����.");

            var relation = relations.OrderBy(x => x.RequiredParameters.Count).Last();

            int result = 1;

            foreach (var requiredParameter in relation.RequiredParameters)
            {
                result += requiredParameter.StepsToOrigin(block);
            }

            return result;
        }

        public static bool IsComplectsGroup(this ParameterGroup parameterGroup)
        {
            return GlobalAppProperties.Actual.ComplectsGroup.Id == parameterGroup.Id;
        }

        public static bool IsComplectDesignationGroup(this ParameterGroup parameterGroup)
        {
            return GlobalAppProperties.Actual.ComplectDesignationGroup.Id == parameterGroup.Id;
        }

        /// <summary>
        /// ������������ �������� ������� ������������� ����������
        /// </summary>
        /// <param name="user">����������� ������������</param>
        /// <returns></returns>
        public static bool IsAppCurrentUser(this User user)
        {
            return GlobalAppProperties.User.Id == user.Id;
        }

        /// <summary>
        /// ������������ �������� ������� ������������� ����������
        /// </summary>
        /// <param name="userWrapper">����������� ������������</param>
        /// <returns></returns>
        public static bool IsAppCurrentUser(this UserWrapper userWrapper)
        {
            return userWrapper.Model.IsAppCurrentUser();
        }

        public static Region GetRegion(this Facility facility)
        {
            //�� ������ �������
            return facility.Address.Locality.Region;
        }

        /// <summary>
        /// ���������� ����� �������� ��� ����� ������������ ��������.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Address GetCompanyOrParentAddress(this Company company)
        {
            //�� ����������� ������ ��������� ������� (��� ��� �������� �����������)
            Address result = company.AddressLegal;
            while (company != null && result == null)
            {
                result = company.AddressLegal;
                company = company.ParentCompany;
            }

            return result;
        }

        public static string GetDeliveryAddressString(this SalesUnit salesUnit)
        {
            Address address = salesUnit.AddressDeliveryCalculated;
            return address == null ? "����� �� ���������." : address.ToString();
        }

        /// <summary>
        /// ���������� ��������� � ���� �����.
        /// </summary>
        /// <param name="sumsOnDates">�����</param>
        /// <param name="date">����</param>
        /// <returns></returns>
        public static ISumOnDate GetClosedSumOnDate(this IEnumerable<ISumOnDate> sumsOnDates, DateTime date)
        {
            var sumOnDates = sumsOnDates as ISumOnDate[] ?? sumsOnDates.ToArray();
            var dif = sumOnDates.Select(sumOnDate => Math.Abs((sumOnDate.Date - date).Days)).Min();
            return sumOnDates.First(sumOnDate => sumOnDate.Date == date.AddDays(-dif) || sumOnDate.Date == date.AddDays(dif));
            //SumOnDate result = null;
            //double? currentDif = null;
            //foreach (var sumOnDate in sumsOnDates)
            //{
            //    var dif = Math.Abs((sumOnDate.Date - date).TotalDays);
            //    if (currentDif == null || dif < currentDif)
            //    {
            //        currentDif = dif;
            //        result = sumOnDate;
            //    }
            //}
            //return result;
        }

        /// <summary>
        /// ������� �������� ������������ ���������� �������� (���� ��� ��� - null)
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Parameter GetVoltageParameter(this Product product)
        {
            return product.ProductBlock.Parameters.FirstOrDefault(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.VoltageGroup.Id);
        }

        /// <summary>
        /// �������� �������� ������������ � ������
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Parameter> LeaveParameterAsTheOnlyOneInTheGroup(this IEnumerable<Parameter> parameters, Parameter parameter)
        {
            var result = parameters.ToList();

            List<Parameter> parametersToRemove = new List<Parameter>();
            foreach (var parameter1 in result)
            {
                if (parameter1.Id == parameter.Id)
                    continue;

                if (parameter1.ParameterGroup.Id == parameter.ParameterGroup.Id)
                    parametersToRemove.Add(parameter1);
            }

            return result.Except(parametersToRemove);
        }

        public static PriceEngineeringTasks GetPriceEngineeringTasks(this PriceEngineeringTask task, IUnitOfWork unitOfWork)
        {
            while (task.ParentPriceEngineeringTasksId.HasValue == false)
            {
                if (task.ParentPriceEngineeringTaskId.HasValue)
                {
                    task = unitOfWork.Repository<PriceEngineeringTask>().GetById(task.ParentPriceEngineeringTaskId.Value);
                }
            }

            return unitOfWork.Repository<PriceEngineeringTasks>().GetById(task.ParentPriceEngineeringTasksId.Value);
        }
    }
}
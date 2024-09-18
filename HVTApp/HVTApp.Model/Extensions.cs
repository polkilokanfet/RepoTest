using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;

namespace HVTApp.Model
{
    public static class Extensions
    {
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
            if (sumsOnDates == null)
                throw new ArgumentNullException(nameof(sumsOnDates));

            return
                sumsOnDates
                    .OrderBy(sumOnDate => Math.Abs((sumOnDate.Date - date).Ticks))
                    .First();

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
        public static IEnumerable<Parameter> LeaveParameterAloneInGroup(this IEnumerable<Parameter> parameters, Parameter parameterRequired)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.Id == parameterRequired.Id)
                {
                    yield return parameter;
                }
                else
                {
                    if (parameter.ParameterGroup.Id != parameterRequired.ParameterGroup.Id)
                    {
                        yield return parameter;
                    }
                }
            }
        }

        /// <summary>
        /// �������� ��������� ������������� � ����� �������
        /// </summary>
        /// <param name="parameters">��� ���������</param>
        /// <param name="parametersRequired">������������ ��������� � ����� �������</param>
        /// <returns></returns>
        public static IEnumerable<Parameter> LeaveParametersAloneInGroup(this IEnumerable<Parameter> parameters, IEnumerable<Parameter> parametersRequired)
        {
            var requiredParameters = parametersRequired as Parameter[] ?? parametersRequired.ToArray();
            foreach (var parameter in parameters)
            {
                if (requiredParameters.ContainsById(parameter))
                {
                    yield return parameter;
                }
                else
                {
                    var groups = requiredParameters
                        .Select(x => x.ParameterGroup)
                        .Distinct();
                    if (groups.ContainsById(parameter.ParameterGroup) == false)
                    {
                        yield return parameter;
                    }
                }
            }
        }

        public static IEnumerable<Parameter> RemoveUnreachable(this IEnumerable<Parameter> parameters)
        {
            var parametersAll = parameters as Parameter[] ?? parameters.ToArray();

            foreach (var parameter in parametersAll)
            {
                if (parameter.IsOrigin)
                    yield return parameter;

                if (parameter.Paths().Any(x => x.Parameters.AllContainsInById(parametersAll)))
                    yield return parameter;
            }
        }

        /// <summary>
        /// ������� �������� ������ ����� ���
        /// </summary>
        /// <param name="task"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static PriceEngineeringTasks GetPriceEngineeringTasks(this PriceEngineeringTask task, IUnitOfWork unitOfWork)
        {
            if (task.ParentPriceEngineeringTasksId.HasValue == false) 
                task = task.GetTopPriceEngineeringTask(unitOfWork);

            if (task.ParentPriceEngineeringTasksId.HasValue == false)
                throw new ArgumentException($"������ ��� �� ������� �� � ����� ������� �����. {nameof(GetPriceEngineeringTasks)} failed");

            return unitOfWork.Repository<PriceEngineeringTasks>().GetById(task.ParentPriceEngineeringTasksId.Value);
        }

        /// <summary>
        /// ������� ������� ������ ���
        /// </summary>
        /// <param name="task"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static PriceEngineeringTask GetTopPriceEngineeringTask(this PriceEngineeringTask task, IUnitOfWork unitOfWork)
        {
            task = unitOfWork.Repository<PriceEngineeringTask>().GetById(task.Id);

            while (task.ParentPriceEngineeringTaskId.HasValue)
            {
                task = unitOfWork.Repository<PriceEngineeringTask>().GetById(task.ParentPriceEngineeringTaskId.Value);
            }

            return task;
        }
    }
}
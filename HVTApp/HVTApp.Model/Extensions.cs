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
        /// <summary>
        /// Пользователь является текущим пользователем приложения
        /// </summary>
        /// <param name="user">Проверяемый пользователь</param>
        /// <returns></returns>
        public static bool IsAppCurrentUser(this User user)
        {
            return GlobalAppProperties.User.Id == user.Id;
        }

        /// <summary>
        /// Пользователь является текущим пользователем приложения
        /// </summary>
        /// <param name="userWrapper">Проверяемый пользователь</param>
        /// <returns></returns>
        public static bool IsAppCurrentUser(this UserWrapper userWrapper)
        {
            return userWrapper.Model.IsAppCurrentUser();
        }

        public static Region GetRegion(this Facility facility)
        {
            //по адресу объекта
            return facility.Address.Locality.Region;
        }

        /// <summary>
        /// Возвращает адрес компании или адрес родительской компании.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Address GetCompanyOrParentAddress(this Company company)
        {
            //по населенному пункту владельца объекта (или его головных организаций)
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
            return address == null ? "Адрес не определен." : address.ToString();
        }

        /// <summary>
        /// Возвращает ближайшую к дате сумму.
        /// </summary>
        /// <param name="sumsOnDates">Суммы</param>
        /// <param name="date">Дата</param>
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
        /// Вернуть параметр номинального напряжения продукта (если его нет - null)
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Parameter GetVoltageParameter(this Product product)
        {
            return product.ProductBlock.Parameters.FirstOrDefault(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.VoltageGroup.Id);
        }

        /// <summary>
        /// Оставить параметр единственным в группе
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
        /// Оставить параметры единственным в группе
        /// </summary>
        /// <param name="parameters">Все параметры</param>
        /// <param name="parametersRequired">Единственные параметры в своих группах</param>
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

        public static string StatusToString(this PriceEngineeringTaskStatusEnum status)
        {
            switch (status)
            {
                case PriceEngineeringTaskStatusEnum.Created:
                    return "Создано";
                case PriceEngineeringTaskStatusEnum.Started:
                    return "Запущено на проработку";
                case PriceEngineeringTaskStatusEnum.Stopped:
                    return "Остановлено менеджером";
                case PriceEngineeringTaskStatusEnum.RejectedByManager:
                    return "Проработка отклонена менеджером";
                case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                    return "Проработка отклонена исполнителем";
                case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    return "Проработано исполнителем";
                case PriceEngineeringTaskStatusEnum.Accepted:
                    return "Принято менеджером";
                case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                    return "На проверке у руководителя КБ";
                case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                    return "Согласовано руководителем КБ";
                case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                    return "Проработка отклонена руководителем КБ";
                default:
                    return "Статус вышел за пределы";
            }
        }
    }
}
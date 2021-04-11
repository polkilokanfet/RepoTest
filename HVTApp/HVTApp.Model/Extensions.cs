using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!relations.Any()) throw new ArgumentException("Передан параметр, который не должен быть в блоке.");

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
            var region = facility.Address?.Locality.Region;

            //по владельцу объекта
            var company = facility.OwnerCompany;
            while (company != null && region == null)
            {
                region = company.AddressLegal?.Locality.Region;
                company = company.ParentCompany;
            }

            return region;
        }

        public static Address GetDeliveryAddress(this SalesUnit salesUnit)
        {
            //по адресу доставки
            if (salesUnit.AddressDelivery != null)
                return salesUnit.AddressDelivery;

            //по адресу объекта
            if (salesUnit.Facility.Address != null)
                return salesUnit.Facility.Address;

            ////по адресу владельца объекта
            //if (salesUnit.Facility.OwnerCompany.AddressLegal != null)
            //    return salesUnit.Facility.OwnerCompany.AddressLegal;

            //по населенному пункту владельца объекта (или его головных организаций)
            var company = salesUnit.Facility.OwnerCompany;
            Locality locality = company.AddressLegal?.Locality;
            while (company != null && locality == null)
            {
                locality = company.AddressLegal?.Locality;
                company = company.ParentCompany;
            }

            if (locality != null)
            {
                return new Address
                {
                    Locality = locality,
                    Description = $"{salesUnit.Facility} (вычислено)"
                };
            }

            return null;
        }

        public static string GetDeliveryAddressString(this SalesUnit salesUnit)
        {
            Address address = salesUnit.GetDeliveryAddress();
            return address == null ? "Адрес не определен." : address.ToString();
        }

        /// <summary>
        /// Возвращает ближайшую к дате сумму.
        /// </summary>
        /// <param name="sumsOnDates">Суммы</param>
        /// <param name="date">Дата</param>
        /// <returns></returns>
        public static SumOnDate GetClosedSumOnDate(this IEnumerable<SumOnDate> sumsOnDates, DateTime date)
        {
            var sumOnDates = sumsOnDates as SumOnDate[] ?? sumsOnDates.ToArray();
            var dif = sumOnDates.Select(x => Math.Abs((x.Date - date).Days)).Min();
            return sumOnDates.First(x => x.Date == date.AddDays(-dif) || x.Date == date.AddDays(dif));
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

        public static User GetFrontManager(this TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (!technicalRequrementsTask.Requrements.Any()) return null;
            if (!technicalRequrementsTask.Requrements.SelectMany(technicalRequrements => technicalRequrements.SalesUnits).Any()) return null;
            return technicalRequrementsTask.Requrements.SelectMany(technicalRequrements => technicalRequrements.SalesUnits).First().Project.Manager;
        }

        public static User GetFrontManager(this PriceCalculation priceCalculation)
        {
            if (!priceCalculation.PriceCalculationItems.Any()) return null;
            if (!priceCalculation.PriceCalculationItems.SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits).Any()) return null;
            return priceCalculation.PriceCalculationItems.SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits).First().Project.Manager;
        }
    }
}
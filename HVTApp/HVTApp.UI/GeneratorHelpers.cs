﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI
{
    public static class GeneratorHelpers
    {

        #region IsType

        public static bool IsType<T>(this PropertyInfo property)
        {
            return typeof(T) == property.PropertyType;
        }

        /// <summary>
        /// Простой ли тип
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimple(this Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        public static bool IsCollection(this Type type)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        public static bool IsCollection(this PropertyInfo property)
        {
            return property.PropertyType.IsCollection();
        }

        public static bool IsComplex(this PropertyInfo property)
        {
            return property.PropertyType.IsComplex();
        }

        public static bool IsComplex(this Type type)
        {
            return !type.IsSimple() && !type.IsCollection();
        }

        //коллекция простых типов?
        private static bool CollectionMemberTypeIsSimple(Type genericCollectionType)
        {
            var t = genericCollectionType.GetInterfaces()
                .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>))
                .GetGenericArguments()[0];

            return IsSimple(t);
        }

        #endregion

        /// <summary>
        /// Все типы для генерации окон с деталями.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetModelTypesPocos()
        {
            var ns = typeof(Address).Namespace;
            //return typeof(Address).Assembly.GetTypes().Where(x => !x.IsAbstract && !x.IsEnum && x.Namespace == ns && !x.Name.Contains("<"));
            return typeof(Address).Assembly.GetTypes().Where(x => x.Namespace == ns && x.GetBaseTypes().Contains(typeof(BaseEntity)));
        }

        public static IEnumerable<Type> GetModelTypesLookups()
        {
            return typeof(AddressLookup).Assembly.GetTypes().
                Where(p => !p.IsAbstract && p.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ILookupItemNavigation<>))).
                OrderBy(x => x.Name);
        }

        public static IEnumerable<Type> GetModelTypesForListViewsGeneration()
        {
            foreach (var type in GetModelTypesLookups())
            {
                var a = type.GetCustomAttribute<NotForListViewGenerationAttribute>();
                if (a == null) yield return type;
            }
        }


        public static IEnumerable<PropertyInfo> GetPropertiesForListViews(this Type typeLookup)
        {
            //свойства со спец.атрибутом
            var entityType = typeLookup.GetProperty(nameof(ILookupItemNavigation<IBaseEntity>.Entity)).PropertyType;
            var dontShowPropNames = entityType.GetProperties()
                    .Where(x => x.GetCustomAttribute<NotForListViewAttribute>() != null)
                    .Select(x => x.Name);

            return typeLookup.GetProperties().Where(x => !dontShowPropNames.Contains(x.Name) 
                                                        && x.Name != nameof(ILookupItemNavigation<IBaseEntity>.Entity) 
                                                        && x.Name != nameof(ILookupItemNavigation<IBaseEntity>.DisplayMember) 
                                                        //&& x.Name != nameof(ILookupItemNavigation<IBaseEntity>.Id)
                                                         ).
                                                         OrderByDescending(x => x, new PropOrderComparer());
        }

        public static IEnumerable<PropertyInfo> GetPropertiesForSalesUnitReport(this Type type)
        {
            return type.GetProperties()
                .Where(propertyInfo => propertyInfo.Name != nameof(ILookupItemNavigation<IBaseEntity>.Id))
                .OrderByDescending(propertyInfo => propertyInfo, new PropOrderComparer());
        }


        public static IEnumerable<PropertyInfo> GetPropertiesForDetailView(this Type type)
        {
            return type.GetProperties()
                .Where(propertyInfo => propertyInfo.CanWrite)
                .Where(propertyInfo => propertyInfo.GetCustomAttribute<NotForDetailsViewAttribute>() == null)
                .OrderByDescending(propertyInfo => propertyInfo, new PropOrderComparer());
        }

        /// <summary>
        /// Собирает все базовые типы, от которого наследуется этот
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            var result = new List<Type>();
            while (type.BaseType != null)
            {
                result.Add(type.BaseType);
                type = type.BaseType;
            }
            return result;
        }

        /// <summary>
        /// возвращаем имя типа
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeName(this Type type)
        {
            if (!type.IsGenericType) return type.FullName;

            var genericArguments = type.GetGenericArguments().Select(GetTypeName).ToArray();
            var typeDefinition = type.GetGenericTypeDefinition().FullName;
            typeDefinition = typeDefinition.Substring(0, typeDefinition.IndexOf('`'));
            return $"{typeDefinition}<{string.Join(",", genericArguments)}>";
        }


        #region Prop Types

        private static IEnumerable<PropertyInfo> GetProps(Type type)
        {
            //return type.GetProperties().Where(x => !Equals(x.Name, "Id"));
            return type.GetProperties();
        }

        /// <summary>
        /// Получить свойства string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> StringProperties(this Type type)
        {
            return GetProps(type).Where(p => p.PropertyType == typeof(string));
        }

        /// <summary>
        /// Получить свойства string
        /// </summary>
        /// <param name="propertyInfos"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> StringProperties(this IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Where(p => p.PropertyType == typeof(string));
        }



        /// <summary>
        /// Получить числовые свойства
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> DigitProperties(this Type type)
        {
            var propsInt = type.SimpleProperties<int>();
            var propsDouble = type.SimpleProperties<double>();
            return propsInt.Union(propsDouble);
        }

        public static IEnumerable<PropertyInfo> DigitProperties(this IEnumerable<PropertyInfo> propertyInfos)
        {
            var propsInt = propertyInfos.SimpleProperties<int>();
            var propsDouble = propertyInfos.SimpleProperties<double>();
            return propsInt.Union(propsDouble);
        }


        /// <summary>
        /// Получить простые свойства (int, double, DateTime)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> SimpleProperties<T>(this Type type)
            where T : struct
        {
            return GetProps(type).Where(p => p.PropertyType == typeof(T) || p.PropertyType == typeof(T?));
        }

        public static IEnumerable<PropertyInfo> SimpleProperties<T>(this IEnumerable<PropertyInfo> propertyInfos)
            where T : struct
        {
            return propertyInfos.Where(p => p.PropertyType == typeof(T) || p.PropertyType == typeof(T?));
        }

        public static IEnumerable<PropertyInfo> AllSimpleProperties(this Type type)
        {
            //return GetProps(type).Where(p => p.PropertyType.IsSimple()).Except(type.SimpleProperties<double>()).Except(type.SimpleProperties<DateTime>());
            return GetProps(type).Where(p => p.PropertyType.IsSimple());
        }

        public static IEnumerable<PropertyInfo> AllSimpleProperties(this IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Where(p => p.PropertyType.IsSimple());
        }


        public static IEnumerable<PropertyInfo> AllCollectionProperties(this Type type)
        {
            return GetProps(type).Except(type.AllSimpleProperties())
                .Where(p => p.PropertyType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)));
        }

        public static IEnumerable<PropertyInfo> AllCollectionPropertiesForDetailsGeneration(this Type type)
        {
            foreach (var propertyInfo in AllCollectionProperties(type))
            {
                var attribute1 = propertyInfo.GetCustomAttribute<NotForDetailsViewAttribute>();
                var attribute2 = propertyInfo.GetCustomAttribute<NotForWrapperAttribute>();
                if (attribute1 == null && attribute2 == null) yield return propertyInfo;
            }
        }



        public static IEnumerable<PropertyInfo> AllCollectionPropertiesForLookupGenerator(this Type type)
        {
            foreach (var propertyInfo in AllCollectionProperties(type))
            {
                var a = propertyInfo.GetCustomAttribute<NotForListViewAttribute>();
                if (a == null) yield return propertyInfo;
            }
        }

        public static IEnumerable<PropertyInfo> AllCollectionProperties(this IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Except(propertyInfos.AllSimpleProperties())
                    .Where(p => p.PropertyType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)));
        }

        public static IEnumerable<PropertyInfo> CollectionComplexProperties(this Type type)
        {
            return type.AllCollectionProperties().Where(x => !CollectionMemberTypeIsSimple(x.PropertyType));
        }

        public static IEnumerable<PropertyInfo> AllComplexProperties(this Type type)
        {
            //var allComplexProperties = allProperties.Except(simpleProperties).Where(p => p.PropertyType.IsClass && !typeof(IEnumerable).IsAssignableFrom(p.PropertyType));
            return GetProps(type).Except(type.AllSimpleProperties()).Except(type.AllCollectionProperties()).Except(type.SimpleProperties<double>()).Except(type.SimpleProperties<DateTime>());
        }

        public static IEnumerable<PropertyInfo> AllComplexPropertiesForDetailsGeneration(this Type type)
        {
            foreach (var propertyInfo in AllComplexProperties(type))
            {
                var attribute1 = propertyInfo.GetCustomAttribute<NotForDetailsViewAttribute>();
                var attribute2 = propertyInfo.GetCustomAttribute<NotForWrapperAttribute>();
                if (attribute1 == null && attribute2 == null) yield return propertyInfo;
            }
        }

        public static IEnumerable<PropertyInfo> AllComplexPropertiesForLookupGeneration(this Type type)
        {
            foreach (var propertyInfo in AllComplexProperties(type))
            {
                var a = propertyInfo.GetCustomAttribute<NotForListViewAttribute>();
                if (a == null) yield return propertyInfo;
            }
        }

        public static IEnumerable<PropertyInfo> AllComplexProperties(this IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos
                .Except(propertyInfos.AllSimpleProperties())
                .Except(propertyInfos.AllCollectionProperties())
                .Except(propertyInfos.SimpleProperties<double>())
                .Except(propertyInfos.SimpleProperties<DateTime>());
        }

        #endregion

        public static int OrderStatus(this PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<OrderStatusAttribute>();
            return attr?.OrderStatus ?? 1;
        }


        private static IEnumerable<AllowEditAttribute> GetAllowEditAttributes(this Type type)
        {
            var atrs = type.GetCustomAttributes<AllowEditAttribute>().ToList();
            foreach (var atr in atrs)
            {
                yield return atr;
            }

            if(!atrs.SelectMany(x => x.Roles).Contains(Role.Admin))
                yield return new AllowEditAttribute(Role.Admin);
        }

        public static IEnumerable<Role> GetAllowEditRoles(this Type type)
        {
            return type.GetAllowEditAttributes().SelectMany(x => x.Roles);
        }


        public static string GetAllowEdit(this Type type)
        {
            List<AllowEditAttribute> attributes = type.GetAllowEditAttributes().ToList();
            return attributes.Any() 
                ? attributes.Select(x => x.ToString()).ToStringEnum(" ")
                : string.Empty;
        }

    }

    class PropOrderComparer : IComparer<PropertyInfo>
    {
        public int Compare(PropertyInfo x, PropertyInfo y)
        {
            if (x == null) throw new ArgumentNullException();
            if (y == null) throw new ArgumentNullException();

            int result = (int)x.OrderStatus() - (int)y.OrderStatus();
            return result != 0 
                ? result 
                : x.Designation().CompareTo(y.Designation());
        }
    }

}

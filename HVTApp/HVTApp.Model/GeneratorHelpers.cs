using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.Model
{
    public static class GeneratorHelpers
    {
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

        public static IEnumerable<PropertyInfo> GetPropertiesForListViews(this Type typeLookup)
        {
            //свойства со спец.атрибутом
            Type entityType = typeLookup.GetProperty(nameof(ILookupItemNavigation<IBaseEntity>.Entity)).PropertyType;
            var names = entityType.GetProperties()
                    .Where(x => x.GetCustomAttribute<NotForListViewAttribute>() != null)
                    .Select(x => x.Name);

            return typeLookup.GetProperties().Where(x => !names.Contains(x.Name) &&
                                                         x.Name != nameof(ILookupItemNavigation<IBaseEntity>.Entity) &&
                                                         x.Name != nameof(ILookupItemNavigation<IBaseEntity>.DisplayMember) &&
                                                         x.Name != nameof(ILookupItemNavigation<IBaseEntity>.Id));
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

        public static IEnumerable<PropertyInfo> AllComplexProperties(this IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos
                .Except(propertyInfos.AllSimpleProperties())
                .Except(propertyInfos.AllCollectionProperties())
                .Except(propertyInfos.SimpleProperties<double>())
                .Except(propertyInfos.SimpleProperties<DateTime>());
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

        //коллекция простых типов?
        private static bool CollectionMemberTypeIsSimple(Type genericCollectionType)
        {
            var t = genericCollectionType.GetInterfaces()
                .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>))
                .GetGenericArguments()[0];

            return IsSimple(t);
        }


        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI
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
            return typeof(Address).Assembly.GetTypes().Where(x => x.GetBaseTypes().Contains(typeof(BaseEntity)));
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

        //возвращаем имя типа
        public static string GetTypeName(Type type)
        {
            if (!type.IsGenericType) return type.FullName;

            var genericArguments = type.GetGenericArguments().Select(GetTypeName).ToArray();
            var typeDefinition = type.GetGenericTypeDefinition().FullName;
            typeDefinition = typeDefinition.Substring(0, typeDefinition.IndexOf('`'));
            return $"{typeDefinition}<{string.Join(",", genericArguments)}>";
        }


        #region Prop Types

        private static IEnumerable<PropertyInfo> GetPropsWithoutId(Type type)
        {
            return type.GetProperties().Where(x => !Equals(x.Name, "Id"));
        }

        /// <summary>
        /// Получить свойства string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> StringProperties(this Type type)
        {
            return GetPropsWithoutId(type).Where(p => p.PropertyType == typeof(string));
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
            return GetPropsWithoutId(type).Where(p => p.PropertyType == typeof(T) || p.PropertyType == typeof(T?));
        }

        public static IEnumerable<PropertyInfo> AllSimpleProperties(this Type type)
        {
            //return GetPropsWithoutId(type).Where(p => p.PropertyType.IsSimple()).Except(type.SimpleProperties<double>()).Except(type.SimpleProperties<DateTime>());
            return GetPropsWithoutId(type).Where(p => p.PropertyType.IsSimple());
        }


        public static IEnumerable<PropertyInfo> AllCollectionProperties(this Type type)
        {
            return GetPropsWithoutId(type).Except(type.AllSimpleProperties())
                    .Where(p => p.PropertyType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)));
        }

        public static IEnumerable<PropertyInfo> CollectionComplexProperties(this Type type)
        {
            return type.AllCollectionProperties().Where(x => !CollectionMemberTypeIsSimple(x.PropertyType));
        }

        public static IEnumerable<PropertyInfo> AllComplexProperties(this Type type)
        {
            //var allComplexProperties = allProperties.Except(simpleProperties).Where(p => p.PropertyType.IsClass && !typeof(IEnumerable).IsAssignableFrom(p.PropertyType));
            return GetPropsWithoutId(type).Except(type.AllSimpleProperties()).Except(type.AllCollectionProperties()).Except(type.SimpleProperties<double>()).Except(type.SimpleProperties<DateTime>());
        }

        //public static IEnumerable<PropertyInfo> MyClass(this Type type)
        //{
        //    var properties = type.GetProperties().Where(x => !Equals(x.Name, "Id"));
        //    var complexSetProperties = allComplexProperties.Where(p => p.CanWrite).ToList();
        //    var complexGetProperties = allComplexProperties.Where(p => !p.CanWrite).ToList();
        //}

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

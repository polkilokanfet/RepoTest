using System;
using System.Reflection;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model
{
    public static class DesignationExt
    {
        //public static string DesignationPlural(this Type type)
        //{
        //    var attr = type.GetCustomAttribute<DesignationPluralAttribute>();
        //    return attr != null ? attr.Designation : type.Name;
        //}
        //public static string DesignationSingle(this Type type)
        //{
        //    var attr = type.GetCustomAttribute<DesignationAttribute>();
        //    return attr != null ? attr.Designation : type.Name;
        //}

        public static string Designation(this PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<DesignationAttribute>(true);
            if (attr != null)
                return attr.Designation;

            var pi = propertyInfo.PropertyType;
            attr = pi.GetCustomAttribute<DesignationAttribute>(true);
            if (attr != null)
                return attr.Designation;

            return propertyInfo.Name;
        }

    }
}

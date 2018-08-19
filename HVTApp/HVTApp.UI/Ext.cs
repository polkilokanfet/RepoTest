using System.Reflection;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.UI
{
    public static class Ext
    {
        public static string Designation(this PropertyInfo propertyInfo)
        {
            var attr = (DesignationAttribute)propertyInfo.GetCustomAttribute(typeof(DesignationAttribute), true);
            if (attr != null)
                return attr.Designation;

            var pi = propertyInfo.PropertyType;
            attr = (DesignationAttribute)pi.GetCustomAttribute(typeof(DesignationAttribute), true);
            if (attr != null)
                return attr.Designation;

            return propertyInfo.Name;
        }

    }
}

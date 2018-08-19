using System;
using System.Reflection;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.UI.Lookup;

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

        public static string DesignationLookup(this PropertyInfo lookupPropertyInfo)
        {
            var propertyInfo = lookupPropertyInfo.DeclaringType.GetProperty(nameof(AddressLookup.Entity)).PropertyType.GetProperty(lookupPropertyInfo.Name);
            if (propertyInfo != null)
                return propertyInfo.Designation();
            return lookupPropertyInfo.Designation();
        }
    }
}

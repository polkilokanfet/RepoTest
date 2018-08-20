using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.UI.Lookup;

namespace HVTApp.UI
{
    public static class DesignationExt
    {
        public static string DesignationPlural(this Type type)
        {
            var attr = type.GetCustomAttribute<DesignationPluralAttribute>();
            return attr != null ? attr.Designation : type.Name;
        }

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

        public static string DesignationLookup(this PropertyInfo lookupPropertyInfo)
        {
            var propertyInfo = lookupPropertyInfo.DeclaringType.GetProperty(nameof(AddressLookup.Entity)).PropertyType.GetProperty(lookupPropertyInfo.Name);
            if (propertyInfo != null)
                return propertyInfo.Designation();
            return lookupPropertyInfo.Designation();
        }

        public static void AddToNavigate(this ObservableCollection<NavigationItem> collection, Type typeView)
        {
            var design = typeView.Name;
            var attr = typeView.GetCustomAttribute<DesignationPluralAttribute>();
            if (attr != null)
                design = attr.Designation;


            var item = new NavigationItem(design, typeView);
            collection.Add(item);
        }
    }
}

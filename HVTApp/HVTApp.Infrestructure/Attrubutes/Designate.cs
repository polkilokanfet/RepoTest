using System;
using System.Reflection;

namespace HVTApp.Infrastructure.Attrubutes
{
    public static class Designate
    {
        public static string GetDesignation(Type type)
        {
            var attr = (DesignationAttribute)type.GetCustomAttribute(typeof(DesignationAttribute), true);
            if (attr != null)
                return attr.Designation;
            return type.Name;
        }
    }
}
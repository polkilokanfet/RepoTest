//using System;
//using System.Reflection;
//using HVTApp.Infrastructure.Attrubutes;

//namespace HVTApp.Model.POCOs
//{
//    public static class Designate
//    {
//        public static string GetTypeDesignation(Type type)
//        {
//            var attr = (DesignationAttribute)type.GetCustomAttribute(typeof(DesignationAttribute), true);
//            return attr != null ? attr.Designation : type.Name;
//        }

//        public static string GetPropDesignation(PropertyInfo propertyInfo)
//        {
//            var attr = (DesignationAttribute)propertyInfo.GetCustomAttribute(typeof(DesignationAttribute), true);
//            if (attr != null)
//                return attr.Designation;
//            return propertyInfo.Name;
//        }
//    }
//}
using System;

namespace HVTApp.Infrastructure.Attrubutes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DesignationAttribute : Attribute
    {
        public string Designation { get; }

        public DesignationAttribute(string designation)
        {
            Designation = designation;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DesignationPluralAttribute : Attribute
    {
        public string Designation { get; }

        public DesignationPluralAttribute(string designation)
        {
            Designation = designation;
        }
    }

    /// <summary>
    /// Атрибут для маркировки свойств, которые не нужно отображать в списках
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotForListViewAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotForDetailsViewAttribute : Attribute { }
}

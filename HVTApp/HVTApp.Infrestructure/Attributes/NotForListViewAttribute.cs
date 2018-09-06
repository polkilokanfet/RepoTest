using System;

namespace HVTApp.Infrastructure.Attributes
{
    /// <summary>
    /// Атрибут для маркировки свойств, которые не нужно отображать в списках
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotForListViewAttribute : Attribute { }
}
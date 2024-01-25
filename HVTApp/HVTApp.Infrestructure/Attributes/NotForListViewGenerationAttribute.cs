using System;

namespace HVTApp.Infrastructure.Attributes
{
    /// <summary>
    /// Атрибут, которым помечаются классы не предназначенные для генерируемых ListViews
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NotForListViewGenerationAttribute : Attribute
    {
    }
}
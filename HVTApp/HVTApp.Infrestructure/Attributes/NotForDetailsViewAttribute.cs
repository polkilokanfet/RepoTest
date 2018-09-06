using System;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotForDetailsViewAttribute : Attribute { }
}
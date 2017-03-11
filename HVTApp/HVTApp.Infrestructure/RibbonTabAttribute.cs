using System;

namespace HVTApp.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RibbonTabAttribute : Attribute
    {
        public Type RibbonTabType { get; }

        public RibbonTabAttribute(Type ribbonTabType)
        {
            if(ribbonTabType == null) throw new ArgumentNullException(nameof(ribbonTabType));
            if(ribbonTabType.BaseType != typeof(RibbonTabItemWithViewModel)) throw new ArgumentOutOfRangeException(nameof(ribbonTabType));

            RibbonTabType = ribbonTabType;
        }
    }
}

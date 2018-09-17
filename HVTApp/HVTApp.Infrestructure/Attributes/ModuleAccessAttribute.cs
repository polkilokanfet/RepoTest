using System;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModuleAccessAttribute : Attribute
    {
        public Role[] Roles { get; }

        public ModuleAccessAttribute(params Role[] roles)
        {
            Roles = roles;
        }
    }
}
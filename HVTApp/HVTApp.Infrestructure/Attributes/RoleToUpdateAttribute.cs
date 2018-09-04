using System;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RoleToUpdateAttribute : Attribute
    {
        public Role[] Roles { get; }

        public RoleToUpdateAttribute(params Role[] roles)
        {
            Roles = roles;
        }
    }
}
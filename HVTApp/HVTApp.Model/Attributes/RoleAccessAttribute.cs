using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RoleAccessAttribute : Attribute
    {
        public List<Role> Roles { get; }

        public RoleAccessAttribute(List<Role> roles)
        {
            Roles = roles;
        }
    }
}

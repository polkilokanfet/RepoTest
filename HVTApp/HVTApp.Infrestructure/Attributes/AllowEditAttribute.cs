using System;
using System.Linq;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AllowEditAttribute : Attribute
    {
        public Role[] Roles { get; }

        public AllowEditAttribute(params Role[] roles)
        {
            Roles = roles;
        }

        public override string ToString()
        {
            return Roles.Any()
                ? Roles.OrderBy(role => role).Select(role => $"[{this.GetType().Name}(Infrastructure.Role.{role.ToString()})]").ToStringEnum(" ")
                : string.Empty;
        }
    }
}
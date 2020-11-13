using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure.Extansions;

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
                ? Roles.OrderBy(x => x).Select(role => $"[{this.GetType().Name}(Role.{role.ToString()})]").ToStringEnum(" ")
                : string.Empty;
            var sb = new StringBuilder();
            foreach (var role in Roles)
            {
                sb.AppendLine($"[{this.GetType().Name}(Role.{role.ToString()})]");
            }
            return sb.ToString();
        }
    }
}
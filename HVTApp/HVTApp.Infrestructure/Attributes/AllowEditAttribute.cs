using System;
using System.Text;

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
            var sb = new StringBuilder();
            foreach (var role in Roles)
            {
                sb.AppendLine($"[{this.GetType().Name}(Role.{role.ToString()})]");
            }
            return sb.ToString();
        }
    }
}
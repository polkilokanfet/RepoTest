using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Model
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

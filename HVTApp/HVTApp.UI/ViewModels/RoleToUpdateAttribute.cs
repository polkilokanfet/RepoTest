using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
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
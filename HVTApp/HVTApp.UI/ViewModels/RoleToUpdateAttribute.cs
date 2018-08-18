using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RoleToUpdateAttribute : Attribute
    {
        public Role[] Roles { get; }

        public RoleToUpdateAttribute(params Role[] roleses)
        {
            Roles = roleses;
        }
    }
}
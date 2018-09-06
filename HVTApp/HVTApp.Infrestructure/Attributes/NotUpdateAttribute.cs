using System;
using System.Collections.Generic;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotUpdateAttribute : Attribute
    {
        public IEnumerable<Role> RolesCantUpdate { get; }

        public NotUpdateAttribute(params Role[] rolesCantUpdate)
        {
            RolesCantUpdate = rolesCantUpdate;
        }
    }
}
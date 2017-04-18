using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public Guid Password { get; set; }
        public string PersonalNumber { get; set; }
        public virtual Role RoleCurrent { get; set; }
        public virtual List<UserRole> Roles { get; set; }
        public virtual Employee Employee { get; set; }
    }

    public class UserRole : BaseEntity
    {
        public Role Role { get; set; }
    }

    public enum Role
    {
        Admin,
        SalesManager,
        Economist,
        DataBaseFiller,
        Director
    }

}
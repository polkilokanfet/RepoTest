using System;

namespace HVTApp.Infrastructure
{
    [Flags]
    public enum Role
    {
        Admin,
        SalesManager,
        Economist,
        DataBaseFiller,
        Director
    }
}

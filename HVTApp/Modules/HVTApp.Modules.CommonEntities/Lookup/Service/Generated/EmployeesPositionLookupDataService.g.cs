using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class EmployeesPositionLookupDataService : LookupDataService<EmployeesPositionLookup, EmployeesPosition>, IEmployeesPositionLookupDataService
    {
        public EmployeesPositionLookupDataService(HvtAppContext context) : base(context) { }
    }
}

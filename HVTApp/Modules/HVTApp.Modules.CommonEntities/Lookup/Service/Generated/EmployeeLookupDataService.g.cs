using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class EmployeeLookupDataService : LookupDataService<EmployeeLookup, Employee>, IEmployeeLookupDataService
    {
        public EmployeeLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}

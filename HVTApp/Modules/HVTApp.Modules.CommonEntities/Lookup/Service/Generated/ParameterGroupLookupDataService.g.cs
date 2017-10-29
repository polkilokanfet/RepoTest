using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ParameterGroupLookupDataService : LookupDataService<ParameterGroupLookup, ParameterGroup>, IParameterGroupLookupDataService
    {
        public ParameterGroupLookupDataService(HvtAppContext context) : base(context) { }
    }
}

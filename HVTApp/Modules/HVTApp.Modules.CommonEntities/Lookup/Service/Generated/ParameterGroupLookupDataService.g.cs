using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ParameterGroupLookupDataService : LookupDataService<ParameterGroupLookup, ParameterGroup>, IParameterGroupLookupDataService
    {
        public ParameterGroupLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
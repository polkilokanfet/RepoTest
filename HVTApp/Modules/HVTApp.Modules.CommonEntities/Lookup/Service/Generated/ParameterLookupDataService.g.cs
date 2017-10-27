using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ParameterLookupDataService : LookupDataService<ParameterLookup, Parameter>, IParameterLookupDataService
    {
        public ParameterLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}

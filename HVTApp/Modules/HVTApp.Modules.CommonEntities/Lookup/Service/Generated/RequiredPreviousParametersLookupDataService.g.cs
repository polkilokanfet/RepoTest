using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class RequiredPreviousParametersLookupDataService : LookupDataService<RequiredPreviousParametersLookup, RequiredPreviousParameters>, IRequiredPreviousParametersLookupDataService
    {
        public RequiredPreviousParametersLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
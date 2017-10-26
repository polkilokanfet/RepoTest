using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class SpecificationLookupDataService : LookupDataService<SpecificationLookup, Specification>, ISpecificationLookupDataService
    {
        public SpecificationLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}

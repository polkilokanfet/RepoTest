using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class MeasureLookupDataService : LookupDataService<MeasureLookup, Measure>, IMeasureLookupDataService
    {
        public MeasureLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
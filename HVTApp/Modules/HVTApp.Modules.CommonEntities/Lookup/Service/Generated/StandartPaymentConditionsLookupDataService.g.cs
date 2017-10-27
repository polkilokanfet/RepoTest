using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class StandartPaymentConditionsLookupDataService : LookupDataService<StandartPaymentConditionsLookup, StandartPaymentConditions>, IStandartPaymentConditionsLookupDataService
    {
        public StandartPaymentConditionsLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}

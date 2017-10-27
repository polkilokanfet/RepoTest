using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ContractLookupDataService : LookupDataService<ContractLookup, Contract>, IContractLookupDataService
    {
        public ContractLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}

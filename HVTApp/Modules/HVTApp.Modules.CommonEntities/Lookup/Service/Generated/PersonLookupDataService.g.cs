using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PersonLookupDataService : LookupDataService<PersonLookup, Person>, IPersonLookupDataService
    {
        public PersonLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
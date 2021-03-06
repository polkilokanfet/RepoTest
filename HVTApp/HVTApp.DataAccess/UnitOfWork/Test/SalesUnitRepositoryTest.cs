using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitRepositoryTest : ISalesUnitRepository
    {
        public IEnumerable<SalesUnit> GetCurrentUserSalesUnits()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetByProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetBySpecification(Guid specificationId)
        {
            throw new NotImplementedException();
        }
    }
}

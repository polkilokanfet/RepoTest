using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitRepositoryTest : ISalesUnitRepository
    {
        public IEnumerable<SalesUnit> GetAllOfCurrentUser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetAllOfCurrentUserForMarketView()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetByProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetByOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetBySpecification(Guid specificationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetAllWithActualPayments()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetAllWithActualPaymentsOfCurrentUser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetAllIncludeActualPayments()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesUnit> GetAllNotRemovedNotLoosen()
        {
            throw new NotImplementedException();
        }
    }
}

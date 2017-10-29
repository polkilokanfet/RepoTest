using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class BankDetailsRepository : BaseRepository<BankDetails>, IBankDetailsRepository
    {
        public BankDetailsRepository(DbContext context) : base(context)
        {
        }
    }
}

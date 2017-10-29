using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class StandartPaymentConditionsRepository : BaseRepository<StandartPaymentConditions>, IStandartPaymentConditionsRepository
    {
        public StandartPaymentConditionsRepository(DbContext context) : base(context)
        {
        }
    }
}

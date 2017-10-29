using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ExchangeCurrencyRateRepository : BaseRepository<ExchangeCurrencyRate>, IExchangeCurrencyRateRepository
    {
        public ExchangeCurrencyRateRepository(DbContext context) : base(context)
        {
        }
    }
}

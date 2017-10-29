using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class RequiredPreviousParametersRepository : BaseRepository<RequiredPreviousParameters>, IRequiredPreviousParametersRepository
    {
        public RequiredPreviousParametersRepository(DbContext context) : base(context)
        {
        }
    }
}

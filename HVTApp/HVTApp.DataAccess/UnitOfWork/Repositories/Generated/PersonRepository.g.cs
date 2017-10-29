using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}

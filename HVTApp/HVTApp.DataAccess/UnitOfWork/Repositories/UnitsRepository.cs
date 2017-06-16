using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class UnitsRepository : BaseRepository<Unit, UnitWrapper>, IUnitsRepository
    {
        public UnitsRepository(DbContext context) : base(context)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class PartsRepository : BaseRepository<Part>, IPartsRepository
    {
        public PartsRepository(DbContext context) : base(context)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterRepository
    {
        public override List<Parameter> Find(Func<Parameter, bool> predicate)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return Context.Set<Parameter>().AsQueryable()
                .Include(x => x.ParameterGroup)
                .Include(x => x.ParameterRelations.Select(r => r.RequiredParameters))
                .Where(predicate).ToList();
        }
    }
}
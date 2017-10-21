using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class PartsRepository : BaseRepository<Part>, IPartsRepository
    {
        public PartsRepository(DbContext context) : base(context)
        {
        }

        public PartWrapper GetProductItem(IEnumerable<ParameterWrapper> parameters)
        {
            //var prmtrs = parameters.ToList();
            //var productItems = this.GetAll();
            //var result = productItems.FirstOrDefault(x => !x.Parameters.Except(prmtrs).Any() &&
            //                                              !prmtrs.Except(x.Parameters).Any());
            //if (result != null) return result;

            //result = GetWrapper(new Part { Parameters = new List<Parameter>(prmtrs.Select(x => x.Model)) });
            //result.Designation = result.ToString();
            //return result;
            throw new NotImplementedException();
        }
    }
}
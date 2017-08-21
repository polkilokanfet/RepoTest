using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class PartsRepository : BaseRepository<Part, PartWrapper>, IPartsRepository
    {
        public PartsRepository(DbContext context, IGetWrapper wrappersGetter) : base(context, wrappersGetter)
        {
        }

        public PartWrapper GetProductItem(IEnumerable<ParameterWrapper> parameters)
        {
            var prmtrs = parameters.ToList();
            var productItems = this.GetAll();
            var result = productItems.FirstOrDefault(x => !x.Parameters.Except(prmtrs).Any() &&
                                                          !prmtrs.Except(x.Parameters).Any());
            if (result != null) return result;

            result = GetWrapper(new Part { Parameters = new List<Parameter>(prmtrs.Select(x => x.Model)) });
            result.Designation = result.ParametersToString;
            return result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParametersEnumerableComparer : IComparer<IEnumerable<Parameter>>
    {
        public int Compare(IEnumerable<Parameter> x, IEnumerable<Parameter> y)
        {
            //var cloudX = x.SelectMany(p => p.Cloud()).Distinct();
            //if (cloudX.Intersect(y).Any()) return 1;

            //var cloudY = y.SelectMany(p => p.Cloud()).Distinct();
            //if (cloudY.Intersect(x).Any()) return -1;

            //есть ли в 2ом списке параметры, зависимые от параметров 1ого
            bool xIsMain = x.Any(paramY => GetAllRequiredParameters(y).Contains(paramY));
            //есть ли в 1ом списке параметры, зависимые от параметров 2ого
            bool yIsMain = y.Any(paramY => GetAllRequiredParameters(x).Contains(paramY));

            if (xIsMain && !yIsMain) return -1;
            if (yIsMain && !xIsMain) return 1;

            int xMin = x.Select(parameter => parameter.ParameterRelations.Select(rp => rp.RequiredParameters).Count()).Min();
            int yMin = y.Select(parameter => parameter.ParameterRelations.Select(rp => rp.RequiredParameters).Count()).Min();

            if (xMin > yMin) return 1;
            if (xMin < yMin) return -1;

            return 0;
        }

        private IEnumerable<Parameter> GetAllRequiredParameters(Parameter parameter)
        {
            return parameter.ParameterRelations.SelectMany(x => x.RequiredParameters);
        }

        private IEnumerable<Parameter> GetAllRequiredParameters(IEnumerable<Parameter> parameters)
        {
            return parameters.SelectMany(GetAllRequiredParameters);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService.Comparers
{
    /// <summary>
    /// Сравнение групп параметров по длинам путей к началу
    /// </summary>
    public class ParametersEnumerableComparerByPaths : IComparer<IEnumerable<Parameter>>
    {
        public int Compare(IEnumerable<Parameter> parameterX, IEnumerable<Parameter> parameterY)
        {
            if (parameterX == null) throw new ArgumentNullException(nameof(parameterX));
            if (parameterY == null) throw new ArgumentNullException(nameof(parameterY));

            var parameterXPaths = parameterX.SelectMany(parameter => parameter.Paths()).ToList();
            double kx = (double)parameterXPaths.Sum(path => path.Parameters.Count) / parameterXPaths.Count;

            var parameterYPaths = parameterY.SelectMany(n => n.Paths()).ToList();
            double ky = (double)parameterYPaths.Sum(path => path.Parameters.Count) / parameterYPaths.Count;

            if (kx < ky) return -1;
            if (kx > ky) return 1;
            return 0;

        }
    }
}
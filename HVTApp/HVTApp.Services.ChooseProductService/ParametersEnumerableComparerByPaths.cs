using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Сравнение групп параметров по длинам путей к началу
    /// </summary>
    public class ParametersEnumerableComparerByPaths : IComparer<IEnumerable<Parameter>>
    {
        public int Compare(IEnumerable<Parameter> x, IEnumerable<Parameter> y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            var xPaths = x.SelectMany(n => n.Paths()).ToList();
            double kx = (double)xPaths.Sum(n => n.Parameters.Count) / xPaths.Count;

            var yPaths = y.SelectMany(n => n.Paths()).ToList();
            double ky = (double)yPaths.Sum(n => n.Parameters.Count) / yPaths.Count;

            if (kx < ky) return -1;
            if (kx > ky) return 1;
            return 0;

        }
    }
}
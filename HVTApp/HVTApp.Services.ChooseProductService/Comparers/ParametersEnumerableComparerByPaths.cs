using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService.Comparers
{
    /// <summary>
    /// Сравнение групп параметров по длинам путей к началу
    /// </summary>
    public class ParametersEnumerableComparerByPaths : IComparer<IEnumerable<Parameter>>
    {
        public int Compare(IEnumerable<Parameter> parametersX, IEnumerable<Parameter> parametersY)
        {
            if (parametersX == null) throw new ArgumentNullException(nameof(parametersX));
            if (parametersY == null) throw new ArgumentNullException(nameof(parametersY));

            if (parametersX.Any() == false) throw new ArgumentNullException(nameof(parametersX));
            if (parametersY.Any() == false) throw new ArgumentNullException(nameof(parametersY));

            if (parametersX.AllContainsInById(parametersY))
            {
                return 0;
            }

            foreach (var parameterX in parametersX)
            {
                foreach (var parameterY in parametersY)
                {
                    if (parameterX.ContainsParameterInPath(parameterY))
                    {
                        return 1;
                    }
                }
            }

            foreach (var parameterY in parametersY)
            {
                foreach (var parameterX in parametersX)
                {
                    if (parameterY.ContainsParameterInPath(parameterX))
                    {
                        return -1;
                    }
                }
            }

            return parametersX.First().ParameterGroup.CompareTo(parametersY.First().ParameterGroup);
        }

        //public int Compare(IEnumerable<Parameter> parameterX, IEnumerable<Parameter> parameterY)
        //{
        //    if (parameterX == null) throw new ArgumentNullException(nameof(parameterX));
        //    if (parameterY == null) throw new ArgumentNullException(nameof(parameterY));

        //    var parameterXPaths = parameterX.SelectMany(parameter => parameter.Paths()).ToList();
        //    double kx = (double)parameterXPaths.Sum(path => path.Parameters.Count) / parameterXPaths.Count;

        //    var parameterYPaths = parameterY.SelectMany(parameter => parameter.Paths()).ToList();
        //    double ky = (double)parameterYPaths.Sum(path => path.Parameters.Count) / parameterYPaths.Count;

        //    if (kx < ky) return -1;
        //    if (kx > ky) return 1;
        //    return 0;
        //}

    }
}
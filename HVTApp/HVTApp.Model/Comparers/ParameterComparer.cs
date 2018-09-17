using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Comparers
{
    public class ParameterComparer : IEqualityComparer<Parameter>
    {
        public bool Equals(Parameter x, Parameter y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Parameter obj)
        {
            return 0;
        }
    }
}
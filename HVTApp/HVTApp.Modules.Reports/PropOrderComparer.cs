using System;
using System.Collections.Generic;
using System.Reflection;

namespace HVTApp.Modules.Reports
{
    class PropOrderComparer : IComparer<PropertyInfo>
    {
        public int Compare(PropertyInfo x, PropertyInfo y)
        {
            if (x == null) throw new ArgumentNullException();
            if (y == null) throw new ArgumentNullException();

            int result = (int)x.OrderStatus() - (int)y.OrderStatus();
            if (result != 0) return result;

            return x.Designation().CompareTo(y.Designation());
        }
    }
}
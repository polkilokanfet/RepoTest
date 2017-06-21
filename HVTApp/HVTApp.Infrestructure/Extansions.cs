using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Infrastructure
{
    public static class Extansions
    {
        public static bool HasSameMembers<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return !first.Except(second).Any() && !second.Except(first).Any();
        }
    }
}

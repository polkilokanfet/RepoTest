using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Infrastructure
{
    public static class Extansions
    {
        public static bool AllMembersAreSame<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstList = first as IList<T> ?? first.ToList();
            var secondList = second as T[] ?? second.ToArray();

            return !firstList.Except(secondList).Any() && !secondList.Except(firstList).Any();
        }
    }
}

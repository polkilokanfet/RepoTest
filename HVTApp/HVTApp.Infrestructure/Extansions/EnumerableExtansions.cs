using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Infrastructure.Extansions
{
    public static class EnumerableExtansions
    {
        public static bool AllMembersAreSame<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstList = first as IList<T> ?? first.ToList();
            var secondList = second as T[] ?? second.ToArray();

            return !firstList.Except(secondList).Any() && !secondList.Except(firstList).Any();
        }

        public static bool AllContainsIn<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (second == null) throw new ArgumentNullException(nameof(second));
            var firstArray = first as T[] ?? first.ToArray();
            var secondArray = second as T[] ?? second.ToArray();
            if (!secondArray.Any()) throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(second));
            if (!firstArray.Any()) throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(first));

            return firstArray.All(secondArray.Contains);
        }
    }
}

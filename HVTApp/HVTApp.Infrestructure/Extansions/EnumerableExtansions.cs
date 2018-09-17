using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Infrastructure.Extansions
{
    public static class EnumerableExtansions
    {
        /// <summary>
        /// Члены коллекций совпадают.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool MembersAreSame<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer = null)
        {
            var firstArray = first as T[] ?? first.ToArray();
            var secondArray = second as T[] ?? second.ToArray();

            if (firstArray.Length != secondArray.Length) return false;

            if (comparer == null)
                return !firstArray.Except(secondArray).Any() && !secondArray.Except(firstArray).Any();

            return !firstArray.Except(secondArray, comparer).Any() && !secondArray.Except(firstArray, comparer).Any();
        }

        public static bool AllContainsIn<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer = null)
        {
            if (second == null) throw new ArgumentNullException(nameof(second));

            var firstArray = first as T[] ?? first.ToArray();
            var secondArray = second as T[] ?? second.ToArray();

            if (!secondArray.Any()) throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(second));
            if (!firstArray.Any()) throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(first));

            return comparer == null ? firstArray.All(x => secondArray.Contains(x)) : firstArray.All(x => secondArray.Contains(x, comparer));
        }
    }
}

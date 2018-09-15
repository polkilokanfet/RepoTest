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
        /// <returns></returns>
        public static bool MembersAreSame<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstArray = first as T[] ?? first.ToArray();
            var secondArray = second as T[] ?? second.ToArray();

            return firstArray.Length == secondArray.Length &&  !firstArray.Except(secondArray).Any();
        }

        public static bool AllContainsIn<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            var firstArray = first as T[] ?? first.ToArray();
            var secondArray = second as T[] ?? second.ToArray();
            if (!secondArray.Any())
                throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(second));
            if (!firstArray.Any())
                throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(first));

            return firstArray.All(secondArray.Contains);
        }
    }
}

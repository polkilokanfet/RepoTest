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
        /// <typeparam name="T">Тип данных в перечислениях</typeparam>
        /// <param name="first">Первое перечисление</param>
        /// <param name="second">Второе перечисление</param>
        /// <param name="comparer">Спопсоб сравнения</param>
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

        public static bool ContainsById<T>(this IEnumerable<T> enumerable, IContainsId objContainsId)
            where T : IContainsId
        {
            return enumerable.Select(x => x.Id).Contains(objContainsId.Id);
        }

        public static T GetById<T>(this IEnumerable<T> enumerable, IContainsId objContainsId)
            where T : IContainsId
        {
            return enumerable.SingleOrDefault(x => x.Id == objContainsId.Id);
        }
    }
}

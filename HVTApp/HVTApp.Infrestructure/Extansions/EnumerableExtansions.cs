using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// Последовательность полностью содержит другую последовательность.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"> Последовательновсть, которая содержится в другой </param>
        /// <param name="second"> Последовательность в которой содержится другая </param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool AllContainsIn<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer = null)
        {
            if (second == null) throw new ArgumentNullException(nameof(second));

            var firstArray = first as T[] ?? first.ToArray();
            var secondArray = second as T[] ?? second.ToArray();

            if (!secondArray.Any()) throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(second));
            if (!firstArray.Any()) throw new ArgumentException("Передано перечисление не содержащее членов.", nameof(first));

            return comparer == null 
                ? firstArray.All(x => secondArray.Contains(x)) 
                : firstArray.All(x => secondArray.Contains(x, comparer));
        }

        public static bool ContainsById<T>(this IEnumerable<T> enumerable, IId objId)
            where T : IId
        {
            if(objId == null)
                throw new ArgumentNullException(nameof(objId));

            return enumerable.Select(x => x.Id).Contains(objId.Id);
        }

        public static T GetById<T>(this IEnumerable<T> enumerable, IId objId)
            where T : IId
        {
            return enumerable.SingleOrDefault(x => x.Id == objId.Id);
        }

        public static void RemoveById<T>(this ICollection<T> collection, IId objId)
            where T : IId
        {
            var item = collection.Single(x => x.Id == objId.Id);
            collection.Remove(item);
        }


        public static void RemoveIfContainsById<T>(this ICollection<T> collection, IId objId)
            where T : IId
        {
            if(collection.ContainsById(objId))
                collection.RemoveById(objId);
        }

        /// <summary>
        /// Сначала удаляет элемент из коллекции по Id (если он есть),
        /// потом добавляет элемент.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="objId"></param>
        public static void ReAddById<T>(this ICollection<T> collection, T objId)
            where T : IId
        {
            collection.RemoveIfContainsById(objId);
            collection.Add(objId);
        }

        public static string ToStringEnum<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.Distinct().ToList();
            var builder = new StringBuilder();
            list.ForEach(x => builder.Append("; ").Append($"{x}"));
            return builder.Remove(0, 2).ToString();
        }
    }
}

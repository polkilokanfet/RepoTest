using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HVTApp.Infrastructure
{
    public abstract class SmartEnumeration<TEnum> : IEquatable<SmartEnumeration<TEnum>>
    where TEnum : SmartEnumeration<TEnum>
    {
        private static readonly Dictionary<int, TEnum> Enumerations = GetEnumerations();

        /// <summary>
        /// Вернуть все члены перечисления
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetMembers()
        {
            return Enumerations.Select(x => x.Value);
        }

        private static Dictionary<int, TEnum> GetEnumerations()
        {
            var enumerationType = typeof(TEnum);

            var fieldsForType = enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default));

            return fieldsForType.ToDictionary(x => x.Value);
        }

        public int Value { get; }

        protected SmartEnumeration(int value)
        {
            Value = value;
        }

        public static TEnum FromValue(int value)
        {
            return Enumerations[value];
        }

        public bool Equals(SmartEnumeration<TEnum> other)
        {
            if (other is null) return false;
            return this.GetType() == other.GetType() &&
                   this.Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is SmartEnumeration<TEnum> other &&
                   this.Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        //public override string ToString()
        //{
        //    return Name;
        //}
    }
}
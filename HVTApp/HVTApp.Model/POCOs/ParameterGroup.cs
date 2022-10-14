using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Группа параметров")]
    public partial class ParameterGroup : BaseEntity, IComparable<ParameterGroup>
    {
        [Designation("Название"), Required, MaxLength(150), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("Единица измерения")]
        public virtual Measure Measure { get; set; }

        [Designation("Комментарий"), MaxLength(75)]
        public string Comment { get; set; }

        /// <summary>
        /// Сила группы относительно других групп. Чем больше, тем сильнее.
        /// </summary>
        [Designation("Сила")]
        public int Powerful { get; set; } = 0;


        public override string ToString()
        {
            return Name;
        }

        public override int CompareTo(object other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            var first = this;

            if (!(other is ParameterGroup second))
                throw new ArgumentException();

            return first.Powerful == second.Powerful 
                ? string.Compare(first.Name, second.Name, StringComparison.Ordinal)
                : second.Powerful - first.Powerful;
        }

        public int CompareTo(ParameterGroup other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return this.CompareTo((object) other);
        }
    }
}
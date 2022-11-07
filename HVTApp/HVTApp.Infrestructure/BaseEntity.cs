using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Infrastructure
{
    [Serializable]
    public abstract class BaseEntity : IBaseEntity, IComparable
    {
        public Guid Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            var other = obj as BaseEntity;
            if (other == null) return false;
            if (Equals(this.Id, other.Id)) return true;
            return base.Equals(obj);

            //var allProperties = GetType().GetProperties().Where(x => !Equals(x.Name, nameof(Id))).ToList();
            //var collectionProperties = allProperties.Where(p => p.PropertyType.GetInterfaces()
            //    .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>))).ToList();
            //var otherProperties = allProperties.Except(collectionProperties).ToList();

            //if (otherProperties.Any(prop => !Equals(prop.GetValue(this), prop.GetValue(other)))) return false;
            //if (collectionProperties.Any(collProp => !((ICollection<object>)collProp.GetValue(this)).AllMembersAreSame((ICollection<object>)collProp.GetValue(other)))) return false;

            //return true;
        }

        protected bool Equals(BaseEntity other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Id.Equals(other.Id);
        }

        public virtual int CompareTo(object other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            return string.Compare(this.ToString(), other.ToString(), StringComparison.Ordinal);
        }

        //public override int GetHashCode()
        //{
        //    var allProperties = GetType().GetProperties().Where(x => !Equals(x.Name, nameof(Id))).ToList();
        //    var collectionProperties = allProperties.Where(p => p.PropertyType.GetInterfaces()
        //        .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>))).ToList();
        //    var otherProperties = allProperties.Except(collectionProperties).ToList();
        //    int result = otherProperties.Sum(prop => prop.GetValue(this).GetHashCode());
        //    foreach (var collectionProperty in collectionProperties)
        //    {
        //        var members = collectionProperty.GetValue(this) as ICollection<object>;
        //        result += members.Sum(x => x.GetHashCode());
        //    }
        //    return result;
        //}
    }
}
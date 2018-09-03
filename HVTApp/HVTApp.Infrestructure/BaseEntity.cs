using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Infrastructure
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
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
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
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
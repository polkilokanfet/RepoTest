using System;

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
            BaseEntity other = obj as BaseEntity;
            return other != null && this.Id.Equals(other.Id);
        }
    }

    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}
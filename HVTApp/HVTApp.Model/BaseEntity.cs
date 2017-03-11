namespace HVTApp.Model
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            BaseEntity otherEntity = obj as BaseEntity;
            if (otherEntity == null)
                return false;

            if (this.Id > 0 && otherEntity.Id > 0)
                return this.Id == otherEntity.Id;

            return EqualsProperties(obj);
        }

        public virtual bool EqualsProperties(object obj)
        {
            return base.Equals(obj);
        }
    }
}
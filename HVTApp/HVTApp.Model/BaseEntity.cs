namespace HVTApp.Model
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            //должны совпадать типы сравниваемых сущностей.
            if (this.GetType() != obj.GetType())
                return false;

            BaseEntity otherEntity = obj as BaseEntity;
            if (otherEntity == null)
                return false;

            if (this.Id > 0 && otherEntity.Id > 0)
                return this.Id == otherEntity.Id;

            return EqualsProperties(obj);
        }

        /// <summary>
        /// ¬се свойства совпадают.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool EqualsProperties(object obj)
        {
            return base.Equals(obj);
        }
    }

    public interface IBaseEntity
    {
        int Id { get; set; }
    }
}
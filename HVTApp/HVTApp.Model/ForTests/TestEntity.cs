namespace HVTApp.Model
{
    public class Parent : BaseEntity
    {
        public virtual Child Child { get; set; }
    }

    public class Child : BaseEntity
    {
        public int N { get; set; }
        public virtual Parent Parent { get; set; }
    }
}

namespace HVTApp.Model
{
    public class TestEntity1 : BaseEntity
    {
        public virtual TestEntity2 TestEntity2 { get; set; }
    }

    public class TestEntity2 : BaseEntity
    {
        public virtual TestEntity1 TestEntity1 { get; set; }
    }
}

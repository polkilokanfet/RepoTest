using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class TestEntity : BaseEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Name))
                return $"{Name} {Id}";
            return base.ToString();
        }
    }

    public class TestHusband : TestEntity
    {
        public TestHusband()
        {
            Name = "husband";
        }
        public virtual TestWife Wife { get; set; }
        public List<TestChild> Children { get; set; } = new List<TestChild>();
    }

    public class TestWife : TestEntity
    {
        public TestWife()
        {
            Name = "wife";
        }
        public int N { get; set; }
        public virtual TestHusband Husband { get; set; }
    }

    public class TestChild : TestEntity
    {
        public TestChild()
        {
            Name = "child";
        }
        public TestHusband Husband { get; set; }
        public TestWife Wife { get; set; }
    }
}

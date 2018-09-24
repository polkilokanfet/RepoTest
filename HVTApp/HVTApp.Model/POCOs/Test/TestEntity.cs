using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs.Test
{
    public partial class TestEntity : BaseEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Name))
                return $"{Name} {Id}";
            return base.ToString();
        }
    }

    public partial class TestHusband : TestEntity
    {
        public TestHusband()
        {
            Name = "husband";
        }
        public virtual TestWife Wife { get; set; }
        public List<TestChild> Children { get; set; } = new List<TestChild>();
    }

    public partial class TestWife : TestEntity
    {
        public TestWife()
        {
            Name = "wife";
        }
        public int N { get; set; }
        public virtual TestHusband Husband { get; set; }
    }

    public partial class TestChild : TestEntity
    {
        public TestChild()
        {
            Name = "child";
        }
        public TestHusband Husband { get; set; }
        public TestWife Wife { get; set; }
    }
}

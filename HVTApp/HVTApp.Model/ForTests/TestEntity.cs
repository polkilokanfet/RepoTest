using System.Collections.Generic;

namespace HVTApp.Model
{
    public class TestHusband : BaseEntity
    {
        public virtual TestWife Wife { get; set; }
        public List<TestChild> Children { get; set; } = new List<TestChild>();
    }

    public class TestWife : BaseEntity
    {
        public int N { get; set; }
        public virtual TestHusband Husband { get; set; }
    }

    public class TestChild : BaseEntity
    {
        public TestHusband Husband { get; set; }
        public TestWife Wife { get; set; }
    }
}

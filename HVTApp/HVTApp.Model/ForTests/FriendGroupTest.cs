using System.Collections.Generic;

namespace HVTApp.Model
{
    public class FriendGroupTest : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<FriendTest> FriendTests { get; set; }
    }
}
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class TestFriendGroup : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<TestFriend> FriendTests { get; set; }
    }
}
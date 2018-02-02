using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class TestFriendGroup : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<TestFriend> FriendTests { get; set; } = new List<TestFriend>();
    }
}
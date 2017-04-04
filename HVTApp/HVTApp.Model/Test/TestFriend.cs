using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class TestFriend : BaseEntity
    {
        public int FriendGroupId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsDeveloper { get; set; }

        public TestFriendAddress TestFriendAddress { get; set; }
        public virtual TestFriendGroup TestFriendGroup { get; set; }

        public List<TestFriendEmail> Emails { get; set; }


        public int IdGet => 5;

        public TestFriendEmail TestFriendEmailGet => new TestFriendEmail();

    }
}

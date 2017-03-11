using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class FriendTest : BaseEntity
    {
        public int FriendGroupId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsDeveloper { get; set; }

        public FriendAddressTest FriendAddressTest { get; set; }
        public virtual FriendGroupTest FriendGroupTest { get; set; }

        public List<FriendEmailTest> Emails { get; set; }


        public int IdGet => 5;
        public virtual List<int> IntList { get; set; } = new List<int>();
        public virtual List<int> IntListGet => new List<int>();


        public FriendEmailTest FriendEmailTestGet => new FriendEmailTest();

    }
}

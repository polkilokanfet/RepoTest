using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTestsTests
{
    [TestClass]
    public class ChangeNotificationCollectionProperty
    {
        private TestFriend _testFriend;
        private TestFriendEmail _testFriendEmail;

        [TestInitialize]
        public void Initialize()
        {
            _testFriendEmail = new TestFriendEmail {Email = "thomas@thomasclaudiushuber.com"};
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress(),
                TestFriendGroup = new TestFriendGroup() {FriendTests = new List<TestFriend>()},
                Emails = new List<TestFriendEmail>
                {
                    new TestFriendEmail {Email = "julia@juhu-design.com"},
                    _testFriendEmail,
                }
            };
        }

        [TestMethod]
        public void ShouldInitializeEmailsProperty()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsNotNull(wrapper.Emails);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterRemovingEmail()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            var emailToRemove = wrapper.Emails.Single(ew => Equals(ew.Model, _testFriendEmail));
            wrapper.Emails.Remove(emailToRemove);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterAddingEmail()
        {
            _testFriend.Emails.Remove(_testFriendEmail);
            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.Emails.Add(new TestFriendEmailWrapper(_testFriendEmail));
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterClearingEmails()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.Emails.Clear();
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        private void CheckIfModelEmailsCollectionIsInSync(TestFriendWrapper wrapper)
        {
            Assert.AreEqual(_testFriend.Emails.Count, wrapper.Emails.Count);
            Assert.IsTrue(_testFriend.Emails.All(e =>
                        wrapper.Emails.Any(we => Equals(we.Model, e))));
        }
    }
}

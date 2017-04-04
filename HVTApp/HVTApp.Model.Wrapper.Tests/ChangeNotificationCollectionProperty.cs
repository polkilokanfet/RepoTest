using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
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
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            Assert.IsNotNull(wrapper.Emails);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterRemovingEmail()
        {
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            var emailToRemove = wrapper.Emails.Single(ew => ew.Model == _testFriendEmail);
            wrapper.Emails.Remove(emailToRemove);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterAddingEmail()
        {
            _testFriend.Emails.Remove(_testFriendEmail);
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.Emails.Add(TestFriendEmailWrapper.GetWrapper(_testFriendEmail));
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterClearingEmails()
        {
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.Emails.Clear();
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        private void CheckIfModelEmailsCollectionIsInSync(TestFriendWrapper wrapper)
        {
            Assert.AreEqual(_testFriend.Emails.Count, wrapper.Emails.Count);
            Assert.IsTrue(_testFriend.Emails.All(e =>
                        wrapper.Emails.Any(we => we.Model == e)));
        }
    }
}

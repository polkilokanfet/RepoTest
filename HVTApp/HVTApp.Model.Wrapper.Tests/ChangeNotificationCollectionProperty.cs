using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ChangeNotificationCollectionProperty
    {
        private FriendTest _friendTest;
        private FriendEmailTest _friendEmailTest;

        [TestInitialize]
        public void Initialize()
        {
            _friendEmailTest = new FriendEmailTest {Email = "thomas@thomasclaudiushuber.com"};
            _friendTest = new FriendTest
            {
                FirstName = "Thomas",
                FriendAddressTest = new FriendAddressTest(),
                FriendGroupTest = new FriendGroupTest() {FriendTests = new List<FriendTest>()},
                Emails = new List<FriendEmailTest>
                {
                    new FriendEmailTest {Email = "julia@juhu-design.com"},
                    _friendEmailTest,
                }
            };
        }

        [TestMethod]
        public void ShouldInitializeEmailsProperty()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsNotNull(wrapper.Emails);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterRemovingEmail()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            var emailToRemove = wrapper.Emails.Single(ew => ew.Model == _friendEmailTest);
            wrapper.Emails.Remove(emailToRemove);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterAddingEmail()
        {
            _friendTest.Emails.Remove(_friendEmailTest);
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.Emails.Add(new FriendEmailTestWrapper(_friendEmailTest));
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterClearingEmails()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.Emails.Clear();
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        private void CheckIfModelEmailsCollectionIsInSync(FriendTestWrapper wrapper)
        {
            Assert.AreEqual(_friendTest.Emails.Count, wrapper.Emails.Count);
            Assert.IsTrue(_friendTest.Emails.All(e =>
                        wrapper.Emails.Any(we => we.Model == e)));
        }
    }
}

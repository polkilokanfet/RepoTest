using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ValidationClassLevel
    {
        private TestFriend _testFriend;

        [TestInitialize]
        public void Initialize()
        {
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress { City = "Müllheim" },
                TestFriendGroup = new TestFriendGroup() { FriendTests = new List<TestFriend>() },
                Emails = new List<TestFriendEmail>
        {
          new TestFriendEmail { Email="thomas@thomasclaudiushuber.com" },
          new TestFriendEmail {Email="julia@juhu-design.com" }
        }
            };
        }

        [TestMethod]
        public void ShouldHaveErrorsAndNotBeValidWhenIsDeveloperIsTrueAndNoEmailExists()
        {
            var expectedError = "A developer must have an email-address";

            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.Emails.Clear();
            Assert.IsFalse(wrapper.IsDeveloper);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.IsFalse(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.AreEqual(1, emailsErrors.Count);
            Assert.AreEqual(expectedError, emailsErrors.Single());

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.AreEqual(1, isDeveloperErrors.Count);
            Assert.AreEqual(expectedError, isDeveloperErrors.Single());
        }

        [TestMethod]
        public void ShouldBeValidAgainWhenIsDeveloperIsSetBackToFalse()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.Emails.Clear();
            Assert.IsFalse(wrapper.IsDeveloper);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.IsFalse(wrapper.IsValid);


            wrapper.IsDeveloper = false;
            Assert.IsTrue(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.AreEqual(0, emailsErrors.Count);

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.AreEqual(0, isDeveloperErrors.Count);
        }

        [TestMethod]
        public void ShouldBeValidAgainWhenEmailIsAdded()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.Emails.Clear();
            Assert.IsFalse(wrapper.IsDeveloper);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.Add(new TestFriendEmailWrapper(new TestFriendEmail { Email = "thomas@thomasclaudiushuber.com" }));
            Assert.IsTrue(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.AreEqual(0, emailsErrors.Count);

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.AreEqual(0, isDeveloperErrors.Count);
        }

        [TestMethod]
        public void ShouldIntializeWithoutProblems()
        {
            _testFriend.IsDeveloper = true;
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}

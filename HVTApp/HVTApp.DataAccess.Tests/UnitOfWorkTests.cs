using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.DataAccess.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void CanAddSaveRemoveEntity()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new HVTAppContext());
            //очищаем все записи
            unitOfWork.FriendGroups.DeleteRange(unitOfWork.FriendGroups.GetAll());
            unitOfWork.Complete();


            TestFriendGroup testFriendGroup = new TestFriendGroup { Name = "Тестовая группа" };
            var testFriendGroupWrapper = unitOfWork.FriendGroups.GetWrapper(testFriendGroup);
            unitOfWork.FriendGroups.Add(testFriendGroupWrapper);
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HVTAppContext());
            Assert.IsTrue(unitOfWork.FriendGroups.Find(x => x.Name == testFriendGroupWrapper.Name).Count() == 1);
            
            //очищаем все записи
            unitOfWork.FriendGroups.DeleteRange(unitOfWork.FriendGroups.GetAll());
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HVTAppContext());
            Assert.IsFalse(unitOfWork.FriendGroups.GetAll().Any());
        }
    }
}

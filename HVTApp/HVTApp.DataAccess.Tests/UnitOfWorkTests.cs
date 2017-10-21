using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.DataAccess.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void CanCreateDataBase()
        {
            var hvtAppContext = new HVTAppContext();
            var unitOfWork = new UnitOfWork(hvtAppContext);
            unitOfWork.ActivityFields.GetAll();
        }

        [TestMethod]
        public void CanAddSaveRemoveEntity()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new HVTAppContext());
            //очищаем все записи
            unitOfWork.FriendGroups.DeleteRange(unitOfWork.FriendGroups.GetAll());
            unitOfWork.Complete();


            var testFriendGroup = new TestFriendGroup { Name = "Тестовая группа" };
            unitOfWork.FriendGroups.Add(testFriendGroup);
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HVTAppContext());
            Assert.IsTrue(unitOfWork.FriendGroups.Find(x => x.Name == testFriendGroup.Name).Count() == 1);
            
            //очищаем все записи
            unitOfWork.FriendGroups.DeleteRange(unitOfWork.FriendGroups.GetAll());
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HVTAppContext());
            Assert.IsFalse(unitOfWork.FriendGroups.GetAll().Any());
        }
    }
}

using System;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
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


            FriendGroupTest friendGroupTest = new FriendGroupTest {Name = "Тестовая группа"};
            unitOfWork.FriendGroups.Add(friendGroupTest);
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HVTAppContext());
            Assert.IsTrue(unitOfWork.FriendGroups.Find(x => x.Name == friendGroupTest.Name).Count() == 1);
            
            //очищаем все записи
            unitOfWork.FriendGroups.DeleteRange(unitOfWork.FriendGroups.GetAll());
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HVTAppContext());
            Assert.IsFalse(unitOfWork.FriendGroups.GetAll().Any());
        }
    }
}

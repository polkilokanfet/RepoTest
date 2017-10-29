using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.DataAccess.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void ShouldReturnRepository()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new HvtAppContext());
            var repo = unitOfWork.GetRepository<Company>();
            Assert.IsNotNull(repo as CompaniesRepository);
        }

        [TestMethod]
        public void CanCreateDataBase()
        {
            var hvtAppContext = new HvtAppContext();
            var unitOfWork = new UnitOfWork(hvtAppContext);
            unitOfWork.ActivityFields.GetAllAsync();
        }

        [TestMethod]
        public async Task CanAddSaveRemoveEntity()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new HvtAppContext());
            //очищаем все записи
            unitOfWork.FriendGroups.DeleteRange(await unitOfWork.FriendGroups.GetAllAsync());
            unitOfWork.Complete();


            var testFriendGroup = new TestFriendGroup { Name = "Тестовая группа" };
            unitOfWork.FriendGroups.Add(testFriendGroup);
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HvtAppContext());
            Assert.IsTrue(unitOfWork.FriendGroups.Find(x => x.Name == testFriendGroup.Name).Count() == 1);
            
            //очищаем все записи
            unitOfWork.FriendGroups.DeleteRange(await unitOfWork.FriendGroups.GetAllAsync());
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HvtAppContext());
            Assert.IsFalse((await unitOfWork.FriendGroups.GetAllAsync()).Any());
        }
    }
}

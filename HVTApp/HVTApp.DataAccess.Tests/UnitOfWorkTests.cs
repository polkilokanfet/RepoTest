using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
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
            Assert.IsNotNull(repo as CompanyRepository);
        }

        [TestMethod]
        public async void CanCreateDataBase()
        {
            var hvtAppContext = new HvtAppContext();
            var unitOfWork = new UnitOfWork(hvtAppContext);
            await unitOfWork.GetRepository<ActivityField>().GetAllAsync();
        }

        [TestMethod]
        public async Task CanAddSaveRemoveEntity()
        {
            var unitOfWork = new UnitOfWork(new HvtAppContext());
            //очищаем все записи
            unitOfWork.GetRepository<TestFriendGroup>().DeleteRange(await unitOfWork.GetRepository<TestFriendGroup>().GetAllAsync());
            await unitOfWork.SaveChangesAsync();


            var testFriendGroup = new TestFriendGroup { Name = "Тестовая группа" };
            unitOfWork.GetRepository<TestFriendGroup>().Add(testFriendGroup);
            await unitOfWork.SaveChangesAsync();

            unitOfWork = new UnitOfWork(new HvtAppContext());
            Assert.IsTrue(unitOfWork.GetRepository<TestFriendGroup>().Find(x => x.Name == testFriendGroup.Name).Count == 1);

            //очищаем все записи
            unitOfWork.GetRepository<TestFriendGroup>().DeleteRange(await unitOfWork.GetRepository<TestFriendGroup>().GetAllAsync());
            await unitOfWork.SaveChangesAsync();

            unitOfWork = new UnitOfWork(new HvtAppContext());
            Assert.IsFalse((await unitOfWork.GetRepository<TestFriendGroup>().GetAllAsync()).Any());
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
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
        public void CanCreateDataBase()
        {
            var hvtAppContext = new HvtAppContext();
            var unitOfWork = new UnitOfWork(hvtAppContext);
            unitOfWork.GetRepository<ActivityField>().GetAllAsync();
        }

        [TestMethod]
        public async Task CanAddSaveRemoveEntity()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new HvtAppContext());
            //очищаем все записи
            unitOfWork.GetRepository<TestFriendGroup>().DeleteRange(await unitOfWork.GetRepository<TestFriendGroup>().GetAllAsync());
            unitOfWork.Complete();


            var testFriendGroup = new TestFriendGroup { Name = "Тестовая группа" };
            unitOfWork.GetRepository<TestFriendGroup>().Add(testFriendGroup);
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HvtAppContext());
            Assert.IsTrue(unitOfWork.GetRepository<TestFriendGroup>().Find(x => x.Name == testFriendGroup.Name).Count() == 1);
            
            //очищаем все записи
            unitOfWork.GetRepository<TestFriendGroup>().DeleteRange(await unitOfWork.GetRepository<TestFriendGroup>().GetAllAsync());
            unitOfWork.Complete();

            unitOfWork = new UnitOfWork(new HvtAppContext());
            Assert.IsFalse((await unitOfWork.GetRepository<TestFriendGroup>().GetAllAsync()).Any());
        }
    }
}

using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.TestDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.DataAccess.Tests
{
    [TestClass]
    public class UnitOfWorkTestTests
    {
        private UnitOfWorkTest _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = new UnitOfWorkTest(new TestData());
        }

        [TestMethod]
        public void TestDataTest()
        {
            var testData = new TestData();
            Assert.IsTrue(testData.GetAll<Company>().Contains(testData.CompanyEnel));
        }

        [TestMethod]
        public void GetRepositoryTest()
        {
            var repo = _unitOfWork.GetRepository<Company>();
            Assert.AreEqual(repo.GetType(), typeof(CompanyRepositoryTest));
        }

        [TestMethod]
        public async Task RepositoryTestGetAll()
        {
            var testData = new TestData();
            var repository = new CompanyRepositoryTest(testData);
            var companies = await repository.GetAllAsync();
            Assert.IsTrue(companies.Contains(testData.CompanyFsk));
        }

        [TestMethod]
        public async Task Test1()
        {
            var wife = new TestWife();
            var unitOfWork1 = new UnitOfWork(new HvtAppContext());
            unitOfWork1.GetRepository<TestWife>().Add(wife);
            unitOfWork1.SaveChanges();
            wife = await unitOfWork1.GetRepository<TestWife>().GetByIdAsync(wife.Id);

            var husband = new TestHusband();
            var unitOfWork2 = new UnitOfWork(new HvtAppContext());
            unitOfWork2.GetRepository<TestHusband>().Add(husband);
            unitOfWork2.SaveChanges();
            husband = await unitOfWork2.GetRepository<TestHusband>().GetByIdAsync(husband.Id);

            husband.Wife = wife;
            unitOfWork2.SaveChanges();
        }

    }
}
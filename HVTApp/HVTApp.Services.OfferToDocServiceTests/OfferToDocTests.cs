using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Services.OfferToDocService;
using HVTApp.TestDataGenerator;
using HVTApp.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.OfferToDocServiceTests
{
    [TestClass()]
    public class OfferToDocTests
    {
        [TestMethod()]
        public void GenerateOfferDocTest()
        {
            var offerWrapper = new OfferWrapper(new TestData().OfferMrsk); 
            var offerToDoc = new OfferToDoc(new UnitOfWorkTest(new TestData()));
            offerToDoc.GenerateOfferDocAsync(offerWrapper);
        }
    }
}
using HVTApp.TestDataGenerator;
using HVTApp.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.OfferToDocService.Tests
{
    [TestClass()]
    public class OfferToDocTests
    {
        [TestMethod()]
        public void GenerateOfferDocTest()
        {
            OfferWrapper offerWrapper = new OfferWrapper(new TestData().OfferMrsk); 
            OfferToDoc offerToDoc = new OfferToDoc();
            offerToDoc.GenerateOfferDoc(offerWrapper);
        }
    }
}
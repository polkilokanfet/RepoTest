using Microsoft.VisualStudio.TestTools.UnitTesting;
using HVTApp.Services.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HVTApp.Services.PrintService.Tests
{
    [TestClass()]
    public class PrintOfferServiceTests
    {
        [TestMethod()]
        public void GetImageTest()
        {           
            var rr = GetImage("header.jpg");
        }

        public static BitmapSource GetImage(string resourceName)
        {
            return new BitmapImage(new Uri(@"pack://application:,,,/HVTApp.Services.PrintService;component/Images/" + resourceName));
        }

    }
}
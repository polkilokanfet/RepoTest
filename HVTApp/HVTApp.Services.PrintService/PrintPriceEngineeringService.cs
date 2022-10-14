using HVTApp.Model.POCOs;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintPriceEngineeringService : PrintServiceBase
    {
        public PrintPriceEngineeringService(IUnityContainer container) : base(container)
        {
        }

        public void PrintPriceEngineeringTaskInformation()
        {

        }

        protected override string GetFullPath(Document document, string path)
        {
            throw new System.NotImplementedException();
        }

        protected override void PrintBody(Document document, WordDocumentWriter docWriter)
        {
            throw new System.NotImplementedException();
        }
    }
}
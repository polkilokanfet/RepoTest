using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintPriceEngineeringService : PrintServiceBase, IPrintPriceEngineering
    {
        public PrintPriceEngineeringService(IUnityContainer container) : base(container)
        {
        }


        public void PrintPriceEngineeringTasks(Guid id)
        {
            this.PrintPriceEngineeringTasksInformation(Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(id));
        }

        private void PrintPriceEngineeringTasksInformation(PriceEngineeringTasks priceEngineeringTasks)
        {
            //string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestPriceEngineeringTasks.docx";
            string offerDocumentPath = @"C:\Users\kosol\Desktop\Новая папка\TestPriceEngineeringTasks1.docx";
            
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();

            docWriter.PrintParagraph("Информация о технической проработке задач в УП ВВА.");
            foreach (var childTask in priceEngineeringTasks.ChildPriceEngineeringTasks)
            {
                this.PrintPriceEngineeringTaskInformation(childTask, docWriter);
            }

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);
        }

        private void PrintPriceEngineeringTaskInformation(PriceEngineeringTask priceEngineeringTask, WordDocumentWriter docWriter)
        {
            docWriter.PrintParagraph($"Блок {priceEngineeringTask.ProductBlock}");
            docWriter.PrintParagraph("Техническое задание от ОП ВВА:");
            foreach (var file in priceEngineeringTask.FilesTechnicalRequirements)
            {
                //docWriter.StartParagraph();
                //Uri uri = new Uri("files/1.txt", UriKind.Relative);
                ////var rr = TextHyperlink.Create(docWriter, @"file://1.txt", "111");
                ////docWriter.AddHyperlink(rr);
                //docWriter.AddHyperlink(uri.AbsolutePath, $" - [{file.CreationMoment.ToShortDateString()} {file.CreationMoment.ToShortTimeString()}] {file.Name}");
                //docWriter.EndParagraph();
            }

            foreach (var childTask in priceEngineeringTask.ChildPriceEngineeringTasks)
            {
                this.PrintPriceEngineeringTaskInformation(childTask, docWriter);
            }
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
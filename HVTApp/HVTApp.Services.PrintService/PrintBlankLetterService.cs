using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintBlankLetterService : PrintServiceBase, IPrintBlankLetterService
    {
        public PrintBlankLetterService(IUnityContainer container) : base(container) { }

        public void PrintBlankLetter(Document letter, string path)
        {
            this.PrintOnLetterhead(letter, path);
        }

        protected override string GetFullPath(Document document, string path)
        {
            return document.GetPath(path);
        }

        protected override void PrintBody(Document document, WordDocumentWriter docWriter)
        {
            var paraFormat1 = docWriter.CreateParagraphProperties();
            //paraFormat1.LeftIndent = 12;
            paraFormat1.Alignment = ParagraphAlignment.Both;
            docWriter.PrintParagraph("Some text...", paraFormat1);
        }
    }

}
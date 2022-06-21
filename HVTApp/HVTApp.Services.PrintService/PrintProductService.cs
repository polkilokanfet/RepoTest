using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.PrintService.Extansions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;
using Padding = Infragistics.Documents.Word.Padding;

namespace HVTApp.Services.PrintService
{
    public class PrintProductService : IPrintProductService
    {
        private readonly IUnityContainer _container;

        public PrintProductService(IUnityContainer container)
        {
            _container = container;
        }

        public void PrintProduct(Product product)
        {
            string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestProductDocument.docx";
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();
            docWriter.PrintParagraph($"{product}");

            Print(docWriter, product);

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);
        }

        public void PrintProducts(IEnumerable<Product> products, ProductBlock block = null)
        {
            try
            {
                var pathDocx = AppDomain.CurrentDomain.BaseDirectory + $"\\{Guid.NewGuid()}.docx";
                WordDocumentWriter docWriter = WordDocumentWriter.Create(pathDocx);
                docWriter.StartDocument();

                foreach (var product in products)
                {
                    docWriter.PrintParagraph($"{product}");
                    Print(docWriter, product, null, block);

                    var paragraphProperties = docWriter.CreateParagraphProperties();
                    paragraphProperties.PageBreakBefore = true;
                    docWriter.PrintParagraph(string.Empty, paragraphProperties);
                }


                docWriter.EndDocument();
                docWriter.Close();

                //var pathPdf = AppDomain.CurrentDomain.BaseDirectory + @"\ProductsDocument.pdf";
                //var documentCore = DocumentCore.Load(pathDocx);
                //documentCore.Save(pathPdf, SaveOptions.PdfDefault);

                System.Diagnostics.Process.Start(pathDocx);
                //System.Diagnostics.Process.Start(pathPdf);
            }
            catch (IOException ioException)
            {
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка", ioException.PrintAllExceptions());
            }
        }

        public void Print(WordDocumentWriter docWriter, Product product, int? amount = null, ProductBlock block = null)
        {
            var tableProperties = docWriter.CreateTableProperties();
            tableProperties.Alignment = ParagraphAlignment.Left;
            tableProperties.PreferredWidthAsPercentage = 100;            
            //выделяем необходимый блок
            if (block != null && product.ProductBlock.Id == block.Id)
            {
                tableProperties.BorderProperties.Color = Colors.Red;
                tableProperties.BorderProperties.Width = 3;
            }
            docWriter.StartTable(2, tableProperties);

            //Заголовок
            docWriter.StartTableRow();
            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;
            var header = amount == null ? $"{product}" : $"{product} x {amount} шт.";
            docWriter.PrintTableCell(header, docWriter.CellProps(2, null, Colors.AliceBlue), null, fontBold);
            docWriter.EndTableRow();

            //строки параметров
            foreach (var parameter in product.ProductBlock.ParametersOrdered)
            {
                docWriter.StartTableRow();
                docWriter.PrintTableCell($"{parameter.ParameterGroup}");
                docWriter.PrintTableCell($"{parameter}");
                docWriter.EndTableRow();
            }

            //печать зависимого оборудования
            foreach (var dependent in product.DependentProducts)
            {
                docWriter.StartTableRow();
                docWriter.StartTableCell(docWriter.CellProps(2, Padding.PadAll(0.25f)));
                Print(docWriter, dependent.Product, dependent.Amount, block);
                docWriter.EndTableCell();
                docWriter.EndTableRow();
            }

            docWriter.EndTable();
        }
    }
}

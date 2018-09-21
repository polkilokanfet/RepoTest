using System;
using System.Collections.Generic;
using System.Windows.Media;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Infragistics.Documents.Word;

namespace HVTApp.Services.PrintService
{
    public class PrintProductService : IPrintProductService
    {
        public void PrintProduct(Product product)
        {
            string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestProductDocument.docx";
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();
            docWriter.Paragraph($"{product}");

            Print(docWriter, product);

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);
        }

        public void PrintProducts(IEnumerable<Product> products, ProductBlock block = null)
        {
            string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestProductDocument.docx";
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();

            foreach (var product in products)
            {
                docWriter.Paragraph($"{product}");
                Print(docWriter, product, null, block);

                var paragraphProperties = docWriter.CreateParagraphProperties();
                paragraphProperties.PageBreakBefore = true;
                docWriter.Paragraph(string.Empty, paragraphProperties);
            }


            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);
        }

        public void Print(WordDocumentWriter docWriter, Product product, int? Amount = null, ProductBlock block = null)
        {
            var tableProps = docWriter.CreateTableProperties();
            tableProps.Alignment = ParagraphAlignment.Both;            
            //выделяем необходимый блок
            if (block != null && product.ProductBlock.Id == block.Id)
                tableProps.BorderProperties.Color = Colors.Red;
            docWriter.StartTable(2, tableProps);

            //Заголовок
            docWriter.StartTableRow();
            string header = Amount == null ? $"{product}" : $"{product} x {Amount} шт.";
            docWriter.Cell(header, docWriter.CellProps(2, null, Colors.AliceBlue));
            docWriter.EndTableRow();

            foreach (var parameter in product.ProductBlock.GetOrderedParameters())
            {
                docWriter.StartTableRow();
                docWriter.Cell($"{parameter.ParameterGroup}");
                docWriter.Cell($"{parameter}");
                docWriter.EndTableRow();
            }

            foreach (var dependent in product.DependentProducts)
            {
                docWriter.StartTableRow();
                docWriter.StartTableCell(docWriter.CellProps(2, Padding.PadAll(5)));
                Print(docWriter, dependent.Product, dependent.Amount, block);
                docWriter.EndTableCell();
                docWriter.EndTableRow();
            }

            docWriter.EndTable();
        }



    }
}

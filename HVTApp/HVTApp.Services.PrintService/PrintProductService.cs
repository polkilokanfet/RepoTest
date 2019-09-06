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
            string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestProductsDocument.docx";
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

        public void Print(WordDocumentWriter docWriter, Product product, int? amount = null, ProductBlock block = null)
        {
            var tableProperties = docWriter.CreateTableProperties();
            tableProperties.Alignment = ParagraphAlignment.Both;            
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
            docWriter.Cell(header, docWriter.CellProps(2, null, Colors.AliceBlue), null, fontBold);
            docWriter.EndTableRow();

            //строки параметров
            foreach (var parameter in product.ProductBlock.GetOrderedParameters())
            {
                docWriter.StartTableRow();
                docWriter.Cell($"{parameter.ParameterGroup}");
                docWriter.Cell($"{parameter}");
                docWriter.EndTableRow();
            }

            //печать зависимого оборудования
            foreach (var dependent in product.DependentProducts)
            {
                docWriter.StartTableRow();
                docWriter.StartTableCell(docWriter.CellProps(2, Padding.PadAll(7)));
                Print(docWriter, dependent.Product, dependent.Amount, block);
                docWriter.EndTableCell();
                docWriter.EndTableRow();
            }

            docWriter.EndTable();
        }
    }
}

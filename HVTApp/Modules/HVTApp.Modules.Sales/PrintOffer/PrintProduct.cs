using System;
using System.Windows.Media;
using HVTApp.Model.POCOs;
using Infragistics.Documents.Word;

namespace HVTApp.Modules.Sales.PrintOffer
{
    public class PrintProduct
    {
        public void Print(WordDocumentWriter docWriter, Product product, int? Amount = null)
        {
            var tableProps = docWriter.CreateTableProperties();
            tableProps.Alignment = ParagraphAlignment.Both;
            docWriter.StartTable(2, tableProps);

            //Заголовок
            docWriter.StartTableRow();
            string header = Amount == null ? $"{product}" : $"{product} x {Amount} шт.";
            Cell(docWriter, header, CellProps(docWriter, 2, null, Colors.AliceBlue));
            docWriter.EndTableRow();

            foreach (var parameter in product.ProductBlock.GetOrderedParameters())
            {
                docWriter.StartTableRow();
                Cell(docWriter, $"{parameter.ParameterGroup}");
                Cell(docWriter, $"{parameter}");
                docWriter.EndTableRow();
            }

            foreach (var dependent in product.DependentProducts)
            {
                docWriter.StartTableRow();
                docWriter.StartTableCell(CellProps(docWriter, 2, Padding.PadAll(5)));
                Print(docWriter, dependent.Product, dependent.Amount);
                docWriter.EndTableCell();
                docWriter.EndTableRow();
            }

            docWriter.EndTable();
        }

        private static void Cell(WordDocumentWriter docWriter, string text, TableCellProperties cellProperties = null, ParagraphProperties paragraphProperties = null)
        {
            cellProperties = cellProperties ?? docWriter.CreateTableCellProperties();
            docWriter.StartTableCell(cellProperties);
            docWriter.Paragraph(text, paragraphProperties);
            docWriter.EndTableCell();
        }

        private TableCellProperties CellProps(WordDocumentWriter docWriter, int? span = null, Padding? margin = null, Color? color = null)
        {            
            var cellProps = docWriter.CreateTableCellProperties();
            if (span.HasValue) cellProps.ColumnSpan = span.Value;
            if (margin.HasValue) cellProps.Margins =  margin.Value;
            if (color.HasValue) cellProps.BackColor = color.Value;
            return cellProps;
        }
    }
}
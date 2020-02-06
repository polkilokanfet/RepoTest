using System.Windows.Media;
using Infragistics.Documents.Word;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Services.PrintService
{
    public static class WordDocumentWriterExt
    {
        public static void PrintParagraph(this WordDocumentWriter docWriter, string text, ParagraphProperties paragraphProperties = null, Font font = null)
        {
            text = text ?? string.Empty;

            if (paragraphProperties == null) docWriter.StartParagraph();
            else docWriter.StartParagraph(paragraphProperties);

            if(font == null)
                docWriter.AddTextRun(text);
            else
                docWriter.AddTextRun(text, font);

            docWriter.EndParagraph();
        }

        public static WordDocumentWriter PrintTableCell(this WordDocumentWriter docWriter, string text, 
            TableCellProperties cellProperties = null, ParagraphProperties paragraphProperties = null, Font font = null)
        {
            cellProperties = cellProperties ?? docWriter.CreateTableCellProperties();
            docWriter.StartTableCell(cellProperties);
            docWriter.PrintParagraph(text, paragraphProperties, font);
            docWriter.EndTableCell();
            return docWriter;
        }

        public static WordDocumentWriter PrintTableRow(this WordDocumentWriter docWriter, TableCellProperties cellProps, 
            TableRowProperties tableRowProperties = null, ParagraphProperties paragraphProperties = null, Font font = null, 
            params string[] text)
        {
            if(tableRowProperties == null) docWriter.StartTableRow();
            else docWriter.StartTableRow(tableRowProperties);

            text.ForEach(x => docWriter.PrintTableCell(x, cellProps, paragraphProperties, font));

            docWriter.EndTableRow();
            return docWriter;
        }

        public static TableCellProperties CellProps(this WordDocumentWriter docWriter, int? span = null, Padding? margin = null, Color? color = null)
        {
            var tableCellProperties = docWriter.CreateTableCellProperties();
            if (span.HasValue) tableCellProperties.ColumnSpan = span.Value;
            if (margin.HasValue) tableCellProperties.Margins = margin.Value;
            if (color.HasValue) tableCellProperties.BackColor = color.Value;
            return tableCellProperties;
        }

    }
}
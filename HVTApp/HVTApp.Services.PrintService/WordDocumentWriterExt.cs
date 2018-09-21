using System.Windows.Media;
using Infragistics.Documents.Word;

namespace HVTApp.Services.PrintService
{
    public static class WordDocumentWriterExt
    {
        public static void Paragraph(this WordDocumentWriter docWriter, string text, ParagraphProperties paragraphProperties = null, Font font = null)
        {
            text = text ?? string.Empty;
            if (paragraphProperties == null)
                docWriter.StartParagraph();
            else
                docWriter.StartParagraph(paragraphProperties);

            if(font == null)
                docWriter.AddTextRun(text);
            else
                docWriter.AddTextRun(text, font);

            docWriter.EndParagraph();
        }

        public static WordDocumentWriter TableCell(this WordDocumentWriter docWriter, string text, TableCellProperties cellProps, ParagraphProperties paragraphProperties = null, Font font = null)
        {
            docWriter.StartTableCell(cellProps);
            if(font == null)
                docWriter.Paragraph(text, paragraphProperties);
            else
                docWriter.Paragraph(text, paragraphProperties, font);

            docWriter.EndTableCell();
            return docWriter;
        }

        public static WordDocumentWriter TableRow(this WordDocumentWriter docWriter, TableCellProperties cellProps, 
            TableRowProperties tableRowProperties = null, ParagraphProperties paragraphProperties = null, Font font = null, params string[] text)
        {
            if(tableRowProperties == null)
                docWriter.StartTableRow();
            else
                docWriter.StartTableRow(tableRowProperties);
            foreach (var txt in text)
            {
                docWriter.TableCell(txt, cellProps, paragraphProperties, font);
            }
            docWriter.EndTableRow();
            return docWriter;
        }


        public static void Cell(this WordDocumentWriter docWriter, string text, TableCellProperties cellProperties = null, ParagraphProperties paragraphProperties = null)
        {
            cellProperties = cellProperties ?? docWriter.CreateTableCellProperties();
            docWriter.StartTableCell(cellProperties);
            docWriter.Paragraph(text, paragraphProperties);
            docWriter.EndTableCell();
        }

        public static TableCellProperties CellProps(this WordDocumentWriter docWriter, int? span = null, Padding? margin = null, Color? color = null)
        {
            var cellProps = docWriter.CreateTableCellProperties();
            if (span.HasValue) cellProps.ColumnSpan = span.Value;
            if (margin.HasValue) cellProps.Margins = margin.Value;
            if (color.HasValue) cellProps.BackColor = color.Value;
            return cellProps;
        }

    }
}
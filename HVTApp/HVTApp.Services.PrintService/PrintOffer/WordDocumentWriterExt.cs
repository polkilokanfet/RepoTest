using Infragistics.Documents.Word;

namespace HVTApp.Services.PrintService.PrintOffer
{
    public static class WordDocumentWriterExt
    {
        public static void PrintParagraph(this WordDocumentWriter docWriter, string text, ParagraphProperties paragraphProperties = null, Font font = null)
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
                docWriter.PrintParagraph(text, paragraphProperties);
            else
                docWriter.PrintParagraph(text, paragraphProperties, font);

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

    }
}
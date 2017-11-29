using Infragistics.Documents.Word;

namespace HVTApp.Services.OfferToDocService
{
    public static class WordDocumentWriterExt
    {
        public static void Paragraph(this WordDocumentWriter docWriter, string text, ParagraphProperties paragraphProperties = null)
        {
            if (paragraphProperties == null) docWriter.StartParagraph();
            else docWriter.StartParagraph(paragraphProperties);
            docWriter.AddTextRun(text);
            docWriter.EndParagraph();
        }

        public static void TableCell(this WordDocumentWriter docWriter, string text, TableCellProperties cellProps, ParagraphProperties paragraphProperties = null)
        {
            docWriter.StartTableCell(cellProps);
            docWriter.Paragraph(text, paragraphProperties);
            docWriter.EndTableCell();
        }
    }
}
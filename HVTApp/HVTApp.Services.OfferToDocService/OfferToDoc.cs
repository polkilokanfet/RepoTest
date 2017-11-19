using System;
using System.Threading.Tasks;
using System.Windows.Media;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Infragistics.Documents.Word;

namespace HVTApp.Services.OfferToDocService
{
    public static class WordDocumentWriterExt
    {
        public static void Paragraph(this WordDocumentWriter docWriter, string text)
        {
            docWriter.StartParagraph();
            docWriter.AddTextRun(text);
            docWriter.EndParagraph();
        }
    }

    public class OfferToDoc : IOfferToDoc
    {
        private readonly IUnitOfWork _unitOfWork;

        public OfferToDoc(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateOfferDocAsync(OfferWrapper offer)
        {
            string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestOfferDocument.docx";
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();
            docWriter.Paragraph($"Получатель");
            docWriter.Paragraph($"должность: {offer.RecipientEmployee.Position.Name}");
            docWriter.Paragraph($"компания: {offer.RecipientEmployee.Company}");
            Person person = await _unitOfWork.GetRepository<Person>().GetByIdAsync(offer.RecipientEmployee.PersonId);
            docWriter.Paragraph($"Ф.И.О.: {person.Surname} {person.Name} {person.Patronymic}");

            docWriter.Paragraph($"Validity Date: {offer.ValidityDate.ToShortDateString()}");


            //Table
            // Create border properties for Table
            TableBorderProperties borderProps = docWriter.CreateTableBorderProperties();
            borderProps.Color = Colors.Black;
            borderProps.Style = TableBorderStyle.Double;

            // Create table properties
            TableProperties tableProps = docWriter.CreateTableProperties();
            tableProps.Alignment = ParagraphAlignment.Left;
            tableProps.BorderProperties.Color = borderProps.Color;
            tableProps.BorderProperties.Style = borderProps.Style;

            // Create table row properties
            TableRowProperties rowProps = docWriter.CreateTableRowProperties();

            // Create table cell properties
            TableCellProperties cellProps = docWriter.CreateTableCellProperties();
            cellProps.BorderProperties = borderProps;

            // Begin a table with 2 columns, and apply the table properties
            docWriter.StartTable(2, tableProps);

            // Begin a Row and apply table row properties
            // This row will be set as the Header row by the row properties
            //Make the row a Header
            rowProps.IsHeaderRow = true;
            docWriter.StartTableRow(rowProps);
            // Define back color for header cell
            cellProps.BackColor = Colors.Aquamarine;
            cellProps.ColumnSpan = 2;
            // Cell Value for 1st row 1st column
            // Start a Paragraph and add a text run to the cell
            docWriter.StartTableCell(cellProps);
            // Set the alignment of the cell text using ParagraphProperties
            ParagraphProperties paraProps = docWriter.CreateParagraphProperties();
            paraProps.Alignment = ParagraphAlignment.Center;
            docWriter.StartParagraph(paraProps);
            docWriter.AddTextRun("test table header");
            docWriter.EndParagraph();
            docWriter.EndTableCell();
            // End the Table Row
            docWriter.EndTableRow();

            // Reset the cell properties, so that the 
            // cell properties are different from the header cells.
            cellProps.Reset();
            cellProps.BackColor = Colors.Azure;
            cellProps.VerticalAlignment = TableCellVerticalAlignment.Center;
            // Reset the row properties
            rowProps.Reset();

            // DATA ROW
            docWriter.StartTableRow();
            // Cell Value for 2nd row 1st column
            docWriter.StartTableCell(cellProps);
            docWriter.StartParagraph();
            docWriter.AddTextRun("test1");
            docWriter.EndParagraph();
            docWriter.EndTableCell();
            // Cell Value for 2nd row 2nd column
            docWriter.StartTableCell(cellProps);
            docWriter.StartParagraph();
            docWriter.AddTextRun("test2");
            docWriter.EndParagraph();
            docWriter.EndTableCell();
            docWriter.EndTableRow();


            // DATA ROW
            docWriter.StartTableRow();
            // Cell Value for 2nd row 1st column
            docWriter.StartTableCell(cellProps);
            docWriter.StartParagraph();
            docWriter.AddTextRun("test11");
            docWriter.EndParagraph();
            docWriter.EndTableCell();
            // Cell Value for 2nd row 2nd column
            docWriter.StartTableCell(cellProps);
            docWriter.StartParagraph();
            docWriter.AddTextRun("test22");
            docWriter.EndParagraph();
            docWriter.EndTableCell();
            docWriter.EndTableRow();


            // DATA ROW
            docWriter.StartTableRow();
            // Cell Value for 3rd row 1st column
            docWriter.StartTableCell(cellProps);
            docWriter.StartParagraph();
            docWriter.AddTextRun("test3");
            docWriter.AddNewLine();
            docWriter.EndParagraph();
            docWriter.StartParagraph();
            docWriter.AddTextRun("test4");
            docWriter.EndParagraph();
            docWriter.EndTableCell();
            // Cell Value for 3rd row 2nd column
            docWriter.StartTableCell(cellProps);
            docWriter.StartParagraph();
            docWriter.AddTextRun("test5");
            docWriter.EndParagraph();
            docWriter.EndTableCell();
            docWriter.EndTableRow();

            docWriter.EndTable();



            //docWriter.StartTable(2, tableProps);
            //docWriter.StartTableRow(rowProps);
            //docWriter.StartTableCell(cellProps);
            //docWriter.StartParagraph();
            //docWriter.AddTextRun("test cell text");
            //docWriter.EndParagraph();
            //docWriter.EndTableCell();
            //docWriter.EndTableRow();
            //docWriter.EndTable();

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);



            ////  Create a new instance of the WordDocumentWriter     
            ////  class using the static 'Create' method.      
            ////  This instance must be closed once content is written into Word.  
            //WordDocumentWriter docWriter = WordDocumentWriter.Create(documentName);
            ////  Set the document properties, such as title, author, etc.          
            //string strDocTitle = "testTitle";
            //docWriter.DocumentProperties.Title = strDocTitle;
            //docWriter.DocumentProperties.Author = string.Format("Infragistics.{0}", "TestName");
            ////  Start the document...note that each call to StartDocument must   
            ////  be balanced with a corresponding call to EndDocument.     
            //docWriter.StartDocument();
            ////  Create a font, which we can reuse in content creation          
            //Infragistics.Documents.Word.Font font = docWriter.CreateFont();
            //font.Reset();
            //font.Bold = true;
            //font.Size = 15;
            //font.Underline = Infragistics.Documents.Word.Underline.Double;
            ////  Start a paragraph           
            //docWriter.StartParagraph();
            ////  Add a text run for the title   
            //string strSimpleWordHeading = "testHeader";
            //docWriter.AddTextRun(strSimpleWordHeading, font);
            //// Add a new line         
            //docWriter.AddNewLine();
            ////End the paragraph      
            //docWriter.EndParagraph();
            ////  Start a paragraph        
            //docWriter.StartParagraph();
            //font.Reset();
            ////  Add a text run for the title   
            //string strSimpleWordDocSampleText = "testText";
            //docWriter.AddTextRun(strSimpleWordDocSampleText);
            ////End the paragraph    
            //docWriter.EndParagraph();
            //docWriter.EndDocument();
            //// Close the writer     
            //docWriter.Close();
            //System.Diagnostics.Process.Start(documentName);

        }
    }
}
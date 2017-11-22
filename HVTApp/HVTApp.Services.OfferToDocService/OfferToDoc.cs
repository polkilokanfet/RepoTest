using System;
using System.Threading.Tasks;
using System.Windows.Media;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;
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

            docWriter.StartTable(4, tableProps);

            rowProps.IsHeaderRow = true;
            docWriter.StartTableRow(rowProps);
            cellProps.BackColor = Colors.Aquamarine;
            ParagraphProperties paraProps = docWriter.CreateParagraphProperties();
            paraProps.Alignment = ParagraphAlignment.Left;

            docWriter.TableCell("Оборудование", cellProps, paraProps);
            docWriter.TableCell("Кол.", cellProps, paraProps);
            docWriter.TableCell("Стоимость, руб.", cellProps, paraProps);
            docWriter.TableCell("Сумма, руб.", cellProps, paraProps);

            // End the Table Row
            docWriter.EndTableRow();

            // Reset the cell properties, so that the 
            // cell properties are different from the header cells.
            cellProps.Reset();
            cellProps.BackColor = Colors.Azure;
            cellProps.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            rowProps.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;
            foreach (var groupUnit in offer.OfferUnits.ToGroupUnits())
            {
                docWriter.StartTableRow();

                docWriter.TableCell(groupUnit.Product.DisplayMember, cellProps);
                docWriter.TableCell($"{groupUnit.Amount:D}", cellProps, parPropRight);
                docWriter.TableCell($"{groupUnit.Cost:C}", cellProps, parPropRight);
                docWriter.TableCell($"{groupUnit.Amount * groupUnit.Cost:C}", cellProps, parPropRight);

                docWriter.EndTableRow();
            }

            docWriter.StartTableRow();
            cellProps.ColumnSpan = 3;
            docWriter.TableCell("Итого без НДС:", cellProps);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.TotalCost:C}", cellProps, parPropRight);
            docWriter.EndTableRow();

            docWriter.StartTableRow();
            cellProps.ColumnSpan = 3;
            docWriter.TableCell($"НДС ({offer.VatProc} %):", cellProps);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.TotalCost * offer.Vat:C}", cellProps, parPropRight);
            docWriter.EndTableRow();


            docWriter.StartTableRow();
            cellProps.ColumnSpan = 3;
            docWriter.TableCell($"Итого с НДС:", cellProps);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.TotalCostWithVat:C}", cellProps, parPropRight);
            docWriter.EndTableRow();

            docWriter.EndTable();


            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);

        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;
using Infragistics.Documents.Word;

namespace HVTApp.Services.OfferToDocService
{
    public class OfferToDoc : IOfferToDoc
    {
        private readonly IUnitOfWork _unitOfWork;

        public OfferToDoc(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateOfferDocAsync(Guid offerId)
        {
            var offer = new OfferWrapper(await _unitOfWork.GetRepository<Offer>().GetByIdAsync(offerId));
            Person person = await _unitOfWork.GetRepository<Person>().GetByIdAsync(offer.RecipientEmployee.PersonId);

            string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestOfferDocument.docx";
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();
            docWriter.Paragraph("Получатель");
            docWriter.Paragraph($"должность: {offer.RecipientEmployee.Position.Name}");
            docWriter.Paragraph($"компания: {offer.RecipientEmployee.Company}");
            docWriter.Paragraph($"Ф.И.О.: {person.Surname} {person.Name} {person.Patronymic}");

            docWriter.Paragraph($"Предложение на поставку оборудования по проекту: ''{offer.Project.Name}''");
            docWriter.Paragraph($"Срок действия ТКП: {offer.ValidityDate.ToShortDateString()}");


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

            docWriter.StartTable(5, tableProps);

            rowProps.IsHeaderRow = true;
            docWriter.StartTableRow(rowProps);
            cellProps.BackColor = Colors.Azure;
            ParagraphProperties paraProps = docWriter.CreateParagraphProperties();
            paraProps.Alignment = ParagraphAlignment.Left;

            docWriter.TableCell("№", cellProps, paraProps);
            docWriter.TableCell("Оборудование", cellProps, paraProps);
            docWriter.TableCell("Кол.", cellProps, paraProps);
            docWriter.TableCell("Стоимость, руб.", cellProps, paraProps);
            docWriter.TableCell("Сумма, руб.", cellProps, paraProps);

            // End the Table Row
            docWriter.EndTableRow();

            // Reset the cell properties, so that the 
            // cell properties are different from the header cells.
            cellProps.Reset();
            cellProps.BackColor = Colors.White;
            cellProps.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            rowProps.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            string productsDetails = string.Empty;
            var rowNum = 0;
            var groupedSalesUnits = offer.SalesUnits.ToUnitGroups().GroupBy(x => x.Facility);
            foreach (var groupedSalesUnit in groupedSalesUnits)
            {
                docWriter.StartTableRow();
                cellProps.ColumnSpan = 5;
                docWriter.TableCell(groupedSalesUnit.Key.DisplayMember, cellProps);
                docWriter.EndTableRow();

                cellProps.ColumnSpan = 1;
                foreach (var groupUnit in groupedSalesUnit)
                {
                    rowNum++;
                    docWriter.StartTableRow();
                    docWriter.TableCell(rowNum.ToString(), cellProps);
                    docWriter.TableCell(groupUnit.Product.DisplayMember, cellProps);
                    docWriter.TableCell($"{groupUnit.Amount:D}", cellProps, parPropRight);
                    docWriter.TableCell($"{groupUnit.Cost:C}", cellProps, parPropRight);
                    docWriter.TableCell($"{groupUnit.Total:C}", cellProps, parPropRight);
                    docWriter.EndTableRow();

                    productsDetails += $"Позиция {rowNum}. {groupUnit.Product.Model.Designation}:" + Environment.NewLine;
                    productsDetails += $"{groupUnit.Product.Model.GetFullDescription()}" + Environment.NewLine;
                }
            }

            cellProps.BackColor = Colors.Azure;
            docWriter.StartTableRow();
            cellProps.ColumnSpan = 4;
            docWriter.TableCell("Итого без НДС:", cellProps);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.TotalCost:C}", cellProps, parPropRight);
            docWriter.EndTableRow();

            docWriter.StartTableRow();
            cellProps.ColumnSpan = 4;
            docWriter.TableCell($"НДС ({offer.VatProc} %):", cellProps);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.TotalCost * offer.Vat:C}", cellProps, parPropRight);
            docWriter.EndTableRow();


            docWriter.StartTableRow();
            cellProps.ColumnSpan = 4;
            docWriter.TableCell($"Итого с НДС:", cellProps);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.TotalCostWithVat:C}", cellProps, parPropRight);
            docWriter.EndTableRow();

            docWriter.EndTable();

            docWriter.Paragraph("Параметры оборудования (по табличным позициям):");
            docWriter.Paragraph(productsDetails);

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);

        }
    }
}
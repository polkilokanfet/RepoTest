using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using HVTApp.UI.ViewModels;
using Infragistics.Documents.Word;

namespace HVTApp.Services.OfferToDocService
{
    public class OfferToDoc : IOfferToDoc
    {
        private readonly OfferDetailsViewModel _offerViewModel;

        public OfferToDoc(OfferDetailsViewModel offerViewModel)
        {
            _offerViewModel = offerViewModel;
        }

        public async Task PrintOfferAsync(Guid offerId)
        {
            await _offerViewModel.LoadAsync(offerId);
            var offer = _offerViewModel.Item;

            string offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestOfferDocument.docx";
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();
            docWriter.Paragraph("Получатель");
            docWriter.Paragraph($"должность: {offer.RecipientEmployee.Position.Name}");
            docWriter.Paragraph($"компания: {offer.RecipientEmployee.Company}");
            docWriter.Paragraph($"Ф.И.О.: {offer.RecipientEmployee.Person.Surname} {offer.RecipientEmployee.Person.Name} {offer.RecipientEmployee.Person.Patronymic}");

            docWriter.Paragraph($"Предложение на поставку оборудования по проекту: ''{offer.Project.Name}''");
            docWriter.Paragraph($"Срок действия ТКП: {offer.ValidityDate.ToShortDateString()}");


            //Table
            // Create border properties for Table
            TableBorderProperties borderProps = GetTableBorderProperties(docWriter);
            // Create table properties
            TableProperties tableProps = GetTableProperties(docWriter, borderProps);
            // Create table row properties
            TableRowProperties rowProps = docWriter.CreateTableRowProperties();

            // Create table cell properties
            TableCellProperties cellProps = docWriter.CreateTableCellProperties();
            cellProps.BorderProperties = borderProps;

            docWriter.StartTable(6, tableProps);

            rowProps.IsHeaderRow = true;
            cellProps.BackColor = Colors.AliceBlue;
            ParagraphProperties paragraphProps = docWriter.CreateParagraphProperties();
            paragraphProps.Alignment = ParagraphAlignment.Left;

            Font fontHeader = docWriter.CreateFont();
            fontHeader.Bold = true;

            docWriter.TableRow(cellProps, rowProps, paragraphProps, fontHeader, "№", "Тип оборудования", "Обозначение", "Кол.", "Стоимость, руб.", "Сумма, руб.");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            cellProps.Reset();
            cellProps.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            rowProps.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            string productsDetails = string.Empty;
            var rowNum = 0;
            var offerUnitsGroupsOrdered = _offerViewModel.Groups.GroupBy(x => x.Facility.Model);
            foreach (var offerUnitsGroups in offerUnitsGroupsOrdered)
            {
                docWriter.StartTableRow();
                cellProps.ColumnSpan = 6;
                docWriter.TableCell(offerUnitsGroups.Key.ToString(), cellProps); //объект
                docWriter.EndTableRow();

                cellProps.ColumnSpan = 1;
                foreach (var offerUnitsGroup in offerUnitsGroups)
                {
                    rowNum++;
                    docWriter.StartTableRow();
                    docWriter.TableCell(rowNum.ToString(), cellProps);                              //номер строки
                    docWriter.TableCell(offerUnitsGroup.Product.ProductType?.Name, cellProps);      //тип оборудования
                    docWriter.TableCell(offerUnitsGroup.Product.ToString(), cellProps);             //обозначение
                    docWriter.TableCell($"{offerUnitsGroup.Amount:D}", cellProps, parPropRight);    //колличество
                    docWriter.TableCell($"{offerUnitsGroup.Cost:C}", cellProps, parPropRight);      //стоимость
                    docWriter.TableCell($"{offerUnitsGroup.Total:C}", cellProps, parPropRight);     //сумма
                    docWriter.EndTableRow();

                    productsDetails += $"Позиция {rowNum}. {offerUnitsGroup.Product}:" + Environment.NewLine;
                    productsDetails += $"Условия оплаты: {offerUnitsGroup.PaymentConditionSet}" + Environment.NewLine;
                    productsDetails += $"Технические харрактеристики: {offerUnitsGroup.Product.Model.GetFullDescription()}" + Environment.NewLine + Environment.NewLine;

                    //дополнительное оборудование
                    if (!offerUnitsGroup.ProductsIncluded.Any()) continue;

                    Font fontSmall = docWriter.CreateFont();
                    fontSmall.Size = 10;
                    docWriter.TableRow(cellProps, null, null, fontSmall, "-", "в составе каждого изделия:", "-", "-", "-", "-");

                    var rn = 0;
                    foreach (var dependentProduct in offerUnitsGroup.ProductsIncluded)
                    {
                        rn++;
                        docWriter.StartTableRow();
                        docWriter.TableCell($"{rowNum}.{rn}.", cellProps, null, fontSmall);
                        docWriter.TableCell(dependentProduct.Product.ProductType?.Name, cellProps, null, fontSmall);
                        docWriter.TableCell(dependentProduct.Product.ToString(), cellProps, null, fontSmall);
                        docWriter.TableCell(dependentProduct.Amount.ToString(), cellProps, parPropRight, fontSmall);
                        docWriter.TableCell("-", cellProps, parPropRight, fontSmall);
                        docWriter.TableCell("-", cellProps, parPropRight, fontSmall);
                        docWriter.EndTableRow();
                    }
                }
            }

            cellProps.BackColor = Colors.AliceBlue;
            docWriter.StartTableRow();
            cellProps.ColumnSpan = 5;
            docWriter.TableCell("Итого без НДС:", cellProps, null, fontHeader);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.Sum:C}", cellProps, parPropRight, fontHeader);
            docWriter.EndTableRow();

            docWriter.StartTableRow();
            cellProps.ColumnSpan = 5;
            docWriter.TableCell($"НДС ({offer.Vat * 100} %):", cellProps, null, fontHeader);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.Sum * offer.Vat:C}", cellProps, parPropRight, fontHeader);
            docWriter.EndTableRow();


            docWriter.StartTableRow();
            cellProps.ColumnSpan = 5;
            docWriter.TableCell($"Итого с НДС:", cellProps, null, fontHeader);
            cellProps.ColumnSpan = 1;
            docWriter.TableCell($"{offer.Sum * (1 + offer.Vat):C}", cellProps, parPropRight, fontHeader);
            docWriter.EndTableRow();

            docWriter.EndTable();

            docWriter.Paragraph("Деталировка условий поставки оборудования (в соответствии с позициями таблицы):", null, fontHeader);
            docWriter.Paragraph(productsDetails);

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);

        }

        private TableBorderProperties GetTableBorderProperties(WordDocumentWriter docWriter)
        {
            var borderProps = docWriter.CreateTableBorderProperties();
            borderProps.Color = Colors.Black;
            borderProps.Style = TableBorderStyle.Single;
            return borderProps;
        }

        private TableProperties GetTableProperties(WordDocumentWriter docWriter, TableBorderProperties borderProps)
        {
            var tableProps = docWriter.CreateTableProperties();
            tableProps.Alignment = ParagraphAlignment.Left;
            tableProps.BorderProperties.Color = borderProps.Color;
            tableProps.BorderProperties.Style = borderProps.Style;
            return tableProps;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Comparers;
using HVTApp.UI.Groups;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.PrintOffer
{
    public class PrintOfferService : IPrintOfferService
    {
        private readonly IUnityContainer _container;

        public PrintOfferService(IUnityContainer container)
        {
            _container = container;
        }

        public async Task PrintOfferAsync(Guid offerId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var offerUnits = (await unitOfWork.Repository<OfferUnit>().GetAllAsync()).Where(x => x.Offer.Id == offerId).ToList();
            var offer = offerUnits.First().Offer;

            //разбиваем на группы, а их делим по объектам
            var offerUnitsGroupsByFacilities = offerUnits.GroupBy(x => x, new OfferUnitsGroupsComparer())
                                                         .Select(x => new OfferUnitsGroup(x))
                                                         .GroupBy(x => x.Facility.Model)
                                                         .ToList();

            var offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestOfferDocument.docx";
            var docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();

            //печать заголовка
            PrintHeader(docWriter, offer);

            var tableBorderProperties = GetTableBorderProperties(docWriter);
            var tableRowProperties = docWriter.CreateTableRowProperties();
            var tableCellProperties = docWriter.CreateTableCellProperties();

            tableCellProperties.BorderProperties = tableBorderProperties;

            docWriter.StartTable(6, GetTableProperties(docWriter, tableBorderProperties));
            
            tableRowProperties.IsHeaderRow = true;
            tableCellProperties.BackColor = Colors.AliceBlue;
            var paragraphProps = docWriter.CreateParagraphProperties();
            paragraphProps.Alignment = ParagraphAlignment.Left;

            Font fontHeader = docWriter.CreateFont();
            fontHeader.Bold = true;

            docWriter.TableRow(tableCellProperties, tableRowProperties, paragraphProps, fontHeader, "№", "Тип оборудования", "Обозначение", "Кол.", "Стоимость, руб.", "Сумма, руб.");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            string productsDetails = string.Empty;
            var num = 0;
            foreach (var offerUnitsGroupsByFacility in offerUnitsGroupsByFacilities)
            {
                docWriter.StartTableRow();
                tableCellProperties.ColumnSpan = 6;
                docWriter.TableCell($"{offerUnitsGroupsByFacility.Key}", tableCellProperties); //объект
                docWriter.EndTableRow();

                tableCellProperties.ColumnSpan = 1;
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    num++;
                    docWriter.StartTableRow();
                    docWriter.TableCell(num.ToString(), tableCellProperties);                                 //номер строки
                    docWriter.TableCell(offerUnitsGroup.Product.ProductType?.Name, tableCellProperties);      //тип оборудования
                    docWriter.TableCell(offerUnitsGroup.Product.Designation, tableCellProperties);            //обозначение
                    docWriter.TableCell($"{offerUnitsGroup.Amount:D}", tableCellProperties, parPropRight);    //колличество
                    docWriter.TableCell($"{offerUnitsGroup.Cost:N}", tableCellProperties, parPropRight);      //стоимость
                    docWriter.TableCell($"{offerUnitsGroup.Total:N}", tableCellProperties, parPropRight);     //сумма
                    docWriter.EndTableRow();

                    productsDetails += $"Позиция {num}. {offerUnitsGroup.Product}:" + Environment.NewLine;
                    productsDetails += $"Условия оплаты: {offerUnitsGroup.PaymentConditionSet}" + Environment.NewLine;
                    //productsDetails += $"Технические харрактеристики: {offerUnitsGroup.Product.Model.GetFullDescription()}" + Environment.NewLine + Environment.NewLine;

                    ////дополнительное оборудование
                    //if (!offerUnitsGroup.ProductsIncluded.Any()) continue;

                    //Font fontSmall = docWriter.CreateFont();
                    //fontSmall.Size = 10;
                    //docWriter.TableRow(cellProps, null, null, fontSmall, "-", "в составе:", "-", "-", "-", "-");

                    //var rn = 0;
                    //foreach (var dependentProduct in offerUnitsGroup.ProductsIncluded)
                    //{
                    //    rn++;
                    //    docWriter.StartTableRow();
                    //    docWriter.TableCell($"{num}.{rn}.", cellProps, null, fontSmall);
                    //    docWriter.TableCell(dependentProduct.Product.ProductType?.Name, cellProps, null, fontSmall);
                    //    docWriter.TableCell(dependentProduct.Product.ToString(), cellProps, null, fontSmall);
                    //    docWriter.TableCell(dependentProduct.Amount.ToString(), cellProps, parPropRight, fontSmall);
                    //    docWriter.TableCell("-", cellProps, parPropRight, fontSmall);
                    //    docWriter.TableCell("-", cellProps, parPropRight, fontSmall);
                    //    docWriter.EndTableRow();
                    //}
                }
            }

            var sum = offerUnits.Sum(x => x.Cost);

            tableCellProperties.BackColor = Colors.AliceBlue;
            docWriter.StartTableRow();
            tableCellProperties.ColumnSpan = 5;
            docWriter.TableCell("Итого без НДС:", tableCellProperties, null, fontHeader);
            tableCellProperties.ColumnSpan = 1;
            docWriter.TableCell($"{sum:N}", tableCellProperties, parPropRight, fontHeader);
            docWriter.EndTableRow();

            docWriter.StartTableRow();
            tableCellProperties.ColumnSpan = 5;
            docWriter.TableCell($"НДС ({offer.Vat} %):", tableCellProperties, null, fontHeader);
            tableCellProperties.ColumnSpan = 1;
            docWriter.TableCell($"{sum * offer.Vat / 100:N}", tableCellProperties, parPropRight, fontHeader);
            docWriter.EndTableRow();


            docWriter.StartTableRow();
            tableCellProperties.ColumnSpan = 5;
            docWriter.TableCell($"Итого с НДС:", tableCellProperties, null, fontHeader);
            tableCellProperties.ColumnSpan = 1;
            docWriter.TableCell($"{sum * (1 + offer.Vat / 100):N}", tableCellProperties, parPropRight, fontHeader);
            docWriter.EndTableRow();

            docWriter.EndTable();

            var paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;
            docWriter.PrintParagraph("Деталировка условий поставки оборудования (в соответствии с позициями таблицы):", paragraphProperties, fontHeader);
            docWriter.PrintParagraph(productsDetails);

            // Печать технических деталей ТКП
            PrintTechnicalDetails(docWriter, offerUnitsGroupsByFacilities);

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);

        }

        /// <summary>
        /// Печать технических деталей ТКП
        /// </summary>
        /// <param name="docWriter"></param>
        /// <param name="offerUnitsGroupsByFacilities"></param>
        private static void PrintTechnicalDetails(WordDocumentWriter docWriter, List<IGrouping<Facility, OfferUnitsGroup>> offerUnitsGroupsByFacilities)
        {
            var paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;

            Font fontHeader = docWriter.CreateFont();
            fontHeader.Bold = true;

            docWriter.PrintParagraph("Технические характеристики оборудования(в соответствии с позициями таблицы):", paragraphProperties, fontHeader);
            int positionNumber = 1;
            foreach (var offerUnitsGroupsByFacility in offerUnitsGroupsByFacilities)
            {
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    docWriter.PrintParagraph($"{positionNumber++}. {offerUnitsGroup.Product} = {offerUnitsGroupsByFacility.Count()} шт.:");
                    new PrintProduct().Print(docWriter, offerUnitsGroup.Product.Model);

                    // включенное в состав оборудование
                    if (offerUnitsGroup.ProductsIncluded.Any())
                    {
                        docWriter.PrintParagraph("Дополнительное оборудование, включенное в состав:");
                        foreach (var productIncluded in offerUnitsGroup.ProductsIncluded)
                        {
                            docWriter.PrintParagraph($"{productIncluded.Product} = {productIncluded.Amount} шт.:");
                            new PrintProduct().Print(docWriter, productIncluded.Product.Model);
                        }
                    }

                    docWriter.PrintParagraph(Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Печать заголовка ТКП
        /// </summary>
        /// <param name="docWriter"></param>
        /// <param name="offer"></param>
        private static void PrintHeader(WordDocumentWriter docWriter, Offer offer)
        {
            docWriter.PrintParagraph("Получатель");
            docWriter.PrintParagraph($"должность: {offer.RecipientEmployee.Position.Name}");
            docWriter.PrintParagraph($"компания: {offer.RecipientEmployee.Company}");
            docWriter.PrintParagraph($"Ф.И.О.: {offer.RecipientEmployee.Person.Surname} {offer.RecipientEmployee.Person.Name} {offer.RecipientEmployee.Person.Patronymic}");

            docWriter.PrintParagraph($"Проект: \"{offer.Project.Name}\"");
            docWriter.PrintParagraph($"Срок действия ТКП: {offer.ValidityDate.ToShortDateString()}");
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
            tableProps.Alignment = ParagraphAlignment.Both;
            tableProps.BorderProperties.Color = borderProps.Color;
            tableProps.BorderProperties.Style = borderProps.Style;
            return tableProps;
        }
    }
}
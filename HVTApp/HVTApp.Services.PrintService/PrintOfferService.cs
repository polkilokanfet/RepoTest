using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Groups;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintOfferService : IPrintOfferService
    {
        private readonly IUnityContainer _container;
        private readonly IMessageService _messageService;
        private readonly PrintProductService _printProductService;

        public PrintOfferService(IUnityContainer container, IMessageService messageService)
        {
            _container = container;
            _messageService = messageService;
            _printProductService = container.Resolve<IPrintProductService>() as PrintProductService;
        }

        public void PrintOffer(Guid offerId, string path = "")
        {
            var offerUnitsGroups = GetOfferUnitsGroups(offerId);
            var offerUnitsGroupsByFacilities = offerUnitsGroups.GroupBy(x => x.Model.Facility).ToList();
            var offer = offerUnitsGroups.First().Offer.Model;

            var fileName = $"\\{offer.RegNumber} от {offer.Date.ToShortDateString()} ({offer.RecipientEmployee.Company.ShortName.ReplaceUncorrectSimbols()}) {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString().ReplaceUncorrectSimbols("-")}";
            fileName = fileName.ReplaceUncorrectSimbols("-").Replace('.', '-').Replace(' ', '_') + ".docx";
            var offerDocumentPath = path == "" ? AppDomain.CurrentDomain.BaseDirectory + $"\\{fileName}" : path + $"\\{fileName}";
            WordDocumentWriter docWriter;
            try
            {
                docWriter = WordDocumentWriter.Create(offerDocumentPath);
            }
            catch (IOException e)
            {
                _messageService.ShowOkMessageDialog(e.GetType().Name, e.Message);
                return;
            }
            docWriter.DefaultParagraphProperties.Alignment = ParagraphAlignment.Left;
            docWriter.StartDocument();

            #region Print Header

            BitmapSource headerBitmapSource = GetImage("header.jpg");
            AnchoredPicture headerPicture = docWriter.CreateAnchoredPicture(headerBitmapSource);
            docWriter.StartParagraph();
            docWriter.AddAnchoredPicture(headerPicture);
            docWriter.EndParagraph();

            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            var noneTableBorderProperties = docWriter.CreateTableBorderProperties();
            noneTableBorderProperties.Style = TableBorderStyle.None;
            noneTableBorderProperties.Sides = TableBorderSides.None;

            var headTableProperties = GetTableProperties(docWriter, noneTableBorderProperties);
            headTableProperties.Alignment = ParagraphAlignment.Left;
            docWriter.StartTable(2, headTableProperties);

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "Тема: ", $"ТКП на поставку оборудования для нужд объектов: {offerUnitsGroups.Select(x => x.Model.Facility).ToStringEnum(", ")}");

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "исх.№ ", $"{offer.RegNumber} от {offer.Date.ToShortDateString()} г.");

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "на № ", offer.RequestDocument == null ? string.Empty : $"{offer.RequestDocument.RegNumber} от {offer.RequestDocument.Date.ToShortDateString()} г.");

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                string.Empty, string.Empty);

            var recipient = offer.RecipientEmployee;
            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "Получатель: ", $"{recipient.Position} {recipient.Company.Form.ShortName} \"{recipient.Company.ShortName}\" {recipient.Person}");

            docWriter.EndTable();


            docWriter.PrintParagraph(string.Empty);
            var prefix = offer.RecipientEmployee.Person.IsMan ? "Уважаемый" : "Уважаемая";
            docWriter.PrintParagraph($"{prefix} {offer.RecipientEmployee.Person.Name} {offer.RecipientEmployee.Person.Patronymic}!", font: fontBold);
            docWriter.PrintParagraph($"В ответ на Ваш запрос, предоставляем технико-коммерческое предложение на поставку оборудования по проекту \"{offer.Project.Name}\".");

            docWriter.PrintParagraph("Стоимость оборудования/услуг (в рублях) приведена в таблице:", font: fontBold);

            #endregion

            #region Print Main Table

            var colorTableHeader = Colors.AliceBlue;

            var tableBorderProperties = GetTableBorderProperties(docWriter);
            var tableRowProperties = docWriter.CreateTableRowProperties();
            var tableCellProperties = docWriter.CreateTableCellProperties();

            tableCellProperties.BorderProperties = tableBorderProperties;

            var tableProperties = GetTableProperties(docWriter, tableBorderProperties);
            tableProperties.Alignment = ParagraphAlignment.Left;
            docWriter.StartTable(6, tableProperties);

            tableRowProperties.IsHeaderRow = true;
            tableCellProperties.BackColor = colorTableHeader;
            var paragraphProps = docWriter.CreateParagraphProperties();
            paragraphProps.Alignment = ParagraphAlignment.Left;

            docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold, 
                "№", "Тип оборудования", "Обозначение оборудования", "Кол.", "Стоимость", "Сумма");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            foreach (var offerUnitsGroupsByFacility in offerUnitsGroupsByFacilities)
            {
                //Название объекта
                docWriter.StartTableRow();

                tableCellProperties.ColumnSpan = 6;
                docWriter.PrintTableCell($"{offerUnitsGroupsByFacility.Key}", tableCellProperties, font: fontBold); //объект

                docWriter.EndTableRow();

                tableCellProperties.ColumnSpan = 1;
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    docWriter.StartTableRow();

                    docWriter.PrintTableCell(offerUnitsGroup.Position.ToString(), tableCellProperties); //номер строки ТКП
                    docWriter.PrintTableCell(offerUnitsGroup.Product.ProductType?.Name, tableCellProperties);
                    //тип оборудования
                    docWriter.PrintTableCell(offerUnitsGroup.Product.Designation, tableCellProperties); //обозначение
                    docWriter.PrintTableCell($"{offerUnitsGroup.Amount:D}", tableCellProperties, parPropRight); //колличество
                    docWriter.PrintTableCell($"{offerUnitsGroup.Cost:N}", tableCellProperties, parPropRight); //стоимость
                    docWriter.PrintTableCell($"{offerUnitsGroup.Total:N}", tableCellProperties, parPropRight); //сумма

                    docWriter.EndTableRow();
                }
            }

            //сумма ТКП
            var sum = offerUnitsGroups.Sum(x => x.Amount * x.Cost);

            tableCellProperties.BackColor = colorTableHeader;

            PrintSumTableString("Итого без НДС:", sum, docWriter, tableCellProperties, fontBold, parPropRight);
            PrintSumTableString($"НДС ({offer.Vat} %):", sum * offer.Vat / 100, docWriter, tableCellProperties, fontBold, parPropRight);
            PrintSumTableString("Итого с НДС:", sum * (1 + offer.Vat / 100), docWriter, tableCellProperties, fontBold, parPropRight);

            docWriter.EndTable();

            #endregion

            #region Print Conditions

            var paragraphProperties = docWriter.CreateParagraphProperties();
            docWriter.PrintParagraph("Условия поставки и оплаты оборудования:", paragraphProperties, fontBold);

            var conditions = new List<string>
            {
                "Комплектация и технические характеристики оборудования в соответствии с приложением к настоящему предложению.",
                PrintConditions("Условия оплаты:", offerUnitsGroups.GroupBy(x => x.PaymentConditionSet.Model)),
                PrintConditions("Срок производства (календарных дней, с правом досрочной поставки): ", offerUnitsGroups.GroupBy(x => x.ProductionTerm)),
                "Точный срок поставки оборудования уточняется при заключении договора.",
                "Цена и сроки поставки могут быть пересмотрены после окончательного согласования опросных листов на оборудование.",
                $"Настоящее предложение действительно до {offer.ValidityDate.ToShortDateString()} г.",
                "Заводская гарантия на оборудование: 5 лет."
            };

            docWriter.StartTable(2, GetTableProperties(docWriter, noneTableBorderProperties));

            var nn = 1;
            foreach (var condition in conditions)
            {
                docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                    $"{nn++}.", condition);
            }

            docWriter.EndTable();

            #endregion

            #region Sender

            docWriter.PrintParagraph(string.Empty);

            var bordProps = docWriter.CreateTableBorderProperties();
            bordProps.Style = TableBorderStyle.None;
            bordProps.Sides = TableBorderSides.None;

            docWriter.StartTable(3, GetTableProperties(docWriter, bordProps));

            docWriter.PrintTableRow(
                docWriter.CreateTableCellProperties(),
                docWriter.CreateTableRowProperties(),
                docWriter.CreateParagraphProperties(),
                docWriter.CreateFont(),
                $"С уважением, {offer.SenderEmployee.Position}",
                string.Empty,
                $"{offer.SenderEmployee.Person}");

            docWriter.EndTable();

            #endregion

            #region Author Footer

            var parts = SectionHeaderFooterParts.FooterAllPages;
            var writerSet = docWriter.AddSectionHeaderFooter(parts);
            writerSet.FooterWriterAllPages.Open();
            writerSet.FooterWriterAllPages.StartParagraph();
            writerSet.FooterWriterAllPages.AddTextRun($"Исполнитель: {offer.Author}");
            writerSet.FooterWriterAllPages.EndParagraph();
            //writerSet.FooterWriterAllPages.StartParagraph();
            //writerSet.FooterWriterAllPages.AddTextRun($"{offer.Author.Person.Surname} {offer.Author.Person.Name} {offer.Author.Person.Patronymic}");
            //writerSet.FooterWriterAllPages.EndParagraph();
            writerSet.FooterWriterAllPages.StartParagraph();
            writerSet.FooterWriterAllPages.AddTextRun($"тел.: {offer.Author.PhoneNumber}; e-mail: {offer.Author.Email}; uetm.ru");
            writerSet.FooterWriterAllPages.EndParagraph();
            writerSet.FooterWriterAllPages.Close();

            #endregion

            #region Print Technical Details

            paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;

            docWriter.PrintParagraph($"Техническое приложение к предложению исх.№ {offer.RegNumber} от {offer.Date.ToShortDateString()} г.", paragraphProperties, fontBold);
            docWriter.PrintParagraph("Технические характеристики оборудования:");
            foreach (var offerUnitsGroupsByFacility in offerUnitsGroupsByFacilities)
            {
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    docWriter.PrintParagraph(Environment.NewLine + $"{offerUnitsGroup.Position}. {offerUnitsGroup.Product} - {offerUnitsGroup.Amount} шт.:");
                    _printProductService.Print(docWriter, offerUnitsGroup.Product.Model);

                    // включенное в состав оборудование
                    if (offerUnitsGroup.ProductsIncluded.Any())
                    {
                        docWriter.PrintParagraph(Environment.NewLine + "Дополнительное оборудование и услуги, включенные в состав:");

                        int n = 1;
                        var productsIncluded = offerUnitsGroup.ProductsIncluded.GroupBy(x => new { x.Product.Model });
                        foreach (var productIncluded in productsIncluded)
                        {
                            docWriter.PrintParagraph(Environment.NewLine + $"{offerUnitsGroup.Position}.{n++} {productIncluded.Key.Model} - {productIncluded.Count()} шт.:");
                            _printProductService.Print(docWriter, productIncluded.Key.Model);
                        }
                    }
                }
            }

            #endregion

            docWriter.EndDocument();
            docWriter.Close();

            var dr = _messageService.ShowYesNoMessageDialog("Процесс завершен", "Формирование ТКП завершено. Открыть результат?");
            if (dr == MessageDialogResult.Yes)
                System.Diagnostics.Process.Start(offerDocumentPath);

        }

        private List<OfferUnitsGroup> GetOfferUnitsGroups(Guid offerId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var offerUnits = unitOfWork.Repository<OfferUnit>().GetAll().Where(x => x.Offer.Id == offerId).ToList();

            //разбиваем на группы, а их делим по объектам
            var offerUnitsGroupsByFacilities = offerUnits
                .GroupBy(x => x, new OfferUnitsGroupsComparer())
                .Select(x => new OfferUnitsGroup(x))
                .OrderByDescending(x => x.Cost)
                .GroupBy(x => x.Facility.Model)
                .ToList();

            var offerUnitsGroups = offerUnitsGroupsByFacilities.SelectMany(x => x.ToList()).ToList();

            //назначаем позиции ТКП
            var i = 1;
            offerUnitsGroups.ForEach(x => x.Position = i++);

            return offerUnitsGroups;
        }

        private static string PrintConditions<T>(string text, IEnumerable<IGrouping<T, OfferUnitsGroup>> offerUnitsGroupsGrouped)
        {
            if (offerUnitsGroupsGrouped.Count() == 1)
            {
                return $"{text} {offerUnitsGroupsGrouped.First().Key}.";
            }

            var result = string.Empty;
            foreach (var unitsGroups in offerUnitsGroupsGrouped)
            {
                if (!string.IsNullOrEmpty(result))
                    result += Environment.NewLine;
                var positions = unitsGroups.Select(x => x.Position).ToStringEnum(", ");
                var prefix = unitsGroups.Count() == 1 ? "позиции" : "позиций";
                result += $"{text} {prefix} {positions}: {unitsGroups.Key}.";
            }
            return result;
        }

        private static void PrintSumTableString(string text, double sum, WordDocumentWriter docWriter, TableCellProperties tableCellProperties,
            Font font, ParagraphProperties parProp)
        {
            docWriter.StartTableRow();
            tableCellProperties.ColumnSpan = 5;
            docWriter.PrintTableCell(text, tableCellProperties, null, font);
            tableCellProperties.ColumnSpan = 1;
            docWriter.PrintTableCell($"{sum:N}", tableCellProperties, parProp, font);
            docWriter.EndTableRow();
        }

        /// <summary>
        /// Печать технических деталей ТКП
        /// </summary>
        /// <param name="docWriter"></param>
        /// <param name="offerUnitsGroupsByFacilities"></param>
        private void PrintTechnicalDetails(WordDocumentWriter docWriter, List<IGrouping<Facility, OfferUnitsGroup>> offerUnitsGroupsByFacilities)
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
                    _printProductService.Print(docWriter, offerUnitsGroup.Product.Model);

                    // включенное в состав оборудование
                    if (offerUnitsGroup.ProductsIncluded.Any())
                    {
                        docWriter.PrintParagraph("Дополнительное оборудование, включенное в состав:");
                        foreach (var productIncluded in offerUnitsGroup.ProductsIncluded)
                        {
                            docWriter.PrintParagraph($"{productIncluded.Product} {productIncluded.Amount} шт.:");
                            _printProductService.Print(docWriter, productIncluded.Product.Model);
                        }
                    }

                    docWriter.PrintParagraph(Environment.NewLine);
                }
            }
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

        #region GetImage
        private BitmapSource GetImage(string resourceName)
        {
            return new BitmapImage(new Uri(@"..\..\Images\" + resourceName, UriKind.RelativeOrAbsolute));
            //return new BitmapImage(new Uri(@"pack://application:,,,/HVTApp.Services.PrintService;component/Images/" + resourceName));
        }
        #endregion GetImage
    }

}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintOfferService : PrintServiceBase, IPrintOfferService
    {
        private readonly PrintProductService _printProductService;

        public PrintOfferService(IUnityContainer container) : base(container)
        {
            _printProductService = container.Resolve<IPrintProductService>() as PrintProductService;
        }

        public void PrintOffer(Guid offerId, string path = "")
        {
            var offerUnitsGroups = GetOfferUnitsGroups(offerId);
            var offerUnitsGroupsByFacilities = offerUnitsGroups.GroupBy(offerUnitsGroup => offerUnitsGroup.Model.Facility).ToList();
            var offer = offerUnitsGroups.First().Offer.Model;
            
            //полный путь к файлу (с именем файла)
            var fullPath = offer.GetPath(path);

            if (File.Exists(fullPath))
            {
                var dr = MessageService.ShowYesNoMessageDialog("Внимание", "Это ТКП уже напечатано. Заменить его?", defaultNo: true);
                if(dr != MessageDialogResult.Yes)
                    return;
            }

            var docWriter = GetWordDocumentWriter(fullPath);
            if (docWriter == null) return;
            docWriter.StartDocument();

            #region Print Header

            docWriter.StartParagraph();
            docWriter.AddInlinePicture(GetImage("header.jpg"));
            docWriter.EndParagraph();

            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            var noBordersTableBorderProperties = docWriter.CreateTableBorderProperties();
            noBordersTableBorderProperties.Style = TableBorderStyle.None;
            noBordersTableBorderProperties.Sides = TableBorderSides.None;

            var headTableProperties = GetTableProperties(docWriter, noBordersTableBorderProperties);
            headTableProperties.Alignment = ParagraphAlignment.Left;
            headTableProperties.PreferredWidthAsPercentage = 100;
            docWriter.StartTable(2, headTableProperties);

            //таблица с номером ТКП
            docWriter.StartTableRow();
            TableCellProperties tableCellProperties1 = docWriter.CreateTableCellProperties();
            tableCellProperties1.PreferredWidthAsPercentage = 50;
            docWriter.StartTableCell(tableCellProperties1);
            docWriter.StartTable(4, headTableProperties);

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "исх.№ ", 
                $"{offer.RegNumber}", 
                "от", 
                $"{offer.Date.ToShortDateString()} г.");

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "на № ", 
                offer.RequestDocument == null ? string.Empty : $"{offer.RequestDocument.RegNumber}", 
                "от", 
                offer.RequestDocument == null ? string.Empty : $"{offer.RequestDocument.Date.ToShortDateString()} г.");

            docWriter.EndTable();
            docWriter.EndTableCell();

            var recipient = offer.RecipientEmployee;
            docWriter.PrintTableCell("Получатель: " + Environment.NewLine + $"{recipient.Position} {recipient.Company.Form.ShortName} \"{recipient.Company.ShortName}\"" + Environment.NewLine + $"{recipient.Person}");

            docWriter.EndTableRow();

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                $"О предложении поставки оборудования для нужд объектов: {offerUnitsGroups.Select(x => x.Model.Facility).ToStringEnum(", ")}", string.Empty);

            docWriter.EndTable();

            #endregion

            #region Print Text Above Table

            docWriter.PrintParagraph(string.Empty);

            var paraFormat = docWriter.CreateParagraphProperties();
            paraFormat.Alignment = ParagraphAlignment.Center;
            var prefix = offer.RecipientEmployee.Person.IsMan ? "Уважаемый" : "Уважаемая";
            docWriter.PrintParagraph($"{prefix} {offer.RecipientEmployee.Person.Name} {offer.RecipientEmployee.Person.Patronymic}!", paraFormat, fontBold);

            var paraFormat1 = docWriter.CreateParagraphProperties();
            //paraFormat1.LeftIndent = 12;
            paraFormat1.Alignment = ParagraphAlignment.Both;
            docWriter.PrintParagraph($"В ответ на Ваш запрос, предоставляем технико-коммерческое предложение на поставку оборудования по проекту \"{offer.Project.Name}\".", paraFormat1);

            docWriter.PrintParagraph("Стоимость оборудования/услуг (в рублях) приведена в таблице:", paraFormat1, font: fontBold);         

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

            var dr2 = MessageService.ShowYesNoMessageDialog("Обозначение", "Использовать полное обозначение оборудования?", defaultYes: true);
            bool printFullDesignation = dr2 == MessageDialogResult.Yes;

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
                    docWriter.PrintTableCell(offerUnitsGroup.Model.Product.ProductType?.Name, tableCellProperties); //тип оборудования
                    var des = printFullDesignation ? offerUnitsGroup.Model.Product.Designation : offerUnitsGroup.Model.Product.Category.NameShort;
                    docWriter.PrintTableCell(des, tableCellProperties); //обозначение
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
            paragraphProperties.Alignment = ParagraphAlignment.Both;
            docWriter.PrintParagraph("Условия поставки и оплаты оборудования:", paragraphProperties, fontBold);

            var conditions = new List<string>
            {
                GetShipmentConditions(offerUnitsGroups),
                PrintPaymentConditions("Условия оплаты:", offerUnitsGroups.GroupBy(x => x.Model.PaymentConditionSet), offer.ValidityDate),
                PrintConditions("Срок производства (календарных дней, с правом досрочной поставки):", offerUnitsGroups.GroupBy(offerUnitsGroup => offerUnitsGroup.ProductionTerm)),
                $"Настоящее предложение действительно при заключении контракта до {offer.ValidityDate.ToShortDateString()} года.",
                "Комплектация и характеристики оборудования в соответствии с техническим приложением к настоящему предложению.",
                "В случае изменения технических характеристик оборудования, объёма поставки или сроков заключения контракта условия предложения могут быть пересмотрены.",
            };

            docWriter.StartTable(2, GetTableProperties(docWriter, noBordersTableBorderProperties));

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

            TableProperties tableProperties2 = GetTableProperties(docWriter, bordProps);
            tableProperties2.PreferredWidthAsPercentage = 100;
            docWriter.StartTable(3, tableProperties2);

            TableCellProperties tableCellProperties2 = docWriter.CreateTableCellProperties();
            tableCellProperties2.PreferredWidthAsPercentage = 33;

            docWriter.StartTableRow();

            var part1 = "С уважением," + Environment.NewLine + $"{offer.SenderEmployee.Position}";
            docWriter.PrintTableCell(part1, tableCellProperties2);

            //подпись
            if (GlobalAppProperties.User.Id == GlobalAppProperties.Actual.Developer?.Id)
            {
                var drt = MessageService.ShowYesNoMessageDialog("Подпись", "Печать с подписью?", defaultNo:true);
                if (drt == MessageDialogResult.Yes)
                {
                    try
                    {
                        docWriter.StartTableCell(tableCellProperties2);

                        var prpr = docWriter.CreateParagraphProperties();
                        prpr.Alignment = ParagraphAlignment.Center;
                        docWriter.StartParagraph(prpr);
                        docWriter.AddInlinePicture(GetImage("sign_deev.png"));
                        docWriter.EndParagraph();

                        docWriter.EndTableCell();

                    }
                    catch (Exception e)
                    {
                        MessageService.ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
                        docWriter.PrintTableCell(string.Empty);
                    }
                }
                else
                {
                    docWriter.PrintTableCell(string.Empty);
                }
            }
            else
            {
                docWriter.PrintTableCell(string.Empty);                
            }

            //ФИО подписывающего лица
            var part3 = Environment.NewLine + $"{offer.SenderEmployee.Person.Name[0]}.{offer.SenderEmployee.Person.Patronymic?[0]}.{offer.SenderEmployee.Person.Surname}";
            var pp = docWriter.CreateParagraphProperties();
            pp.Alignment = ParagraphAlignment.Right;
            docWriter.PrintTableCell(part3, tableCellProperties2, pp);

            docWriter.EndTableRow();

            //docWriter.PrintTableRow(
            //    tableCellProperties2,
            //    docWriter.CreateTableRowProperties(),
            //    docWriter.CreateParagraphProperties(),
            //    docWriter.CreateFont(),
            //    "С уважением," + Environment.NewLine + $"{offer.SenderEmployee.Position}",
            //    string.Empty,
            //    Environment.NewLine + $"{offer.SenderEmployee.Person.Name[0]}.{offer.SenderEmployee.Person.Patronymic?[0]}.{offer.SenderEmployee.Person.Surname}");

            docWriter.EndTable();

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
                        var productsIncluded = offerUnitsGroup.ProductsIncluded.GroupBy(x => new
                        {
                            x.Model.Product,
                            x.Model.Amount
                        });
                        foreach (var productIncluded in productsIncluded)
                        {
                            docWriter.PrintParagraph(Environment.NewLine + $"{offerUnitsGroup.Position}.{n++} {productIncluded.Key.Product} - {productIncluded.Count() * productIncluded.Key.Amount} шт.:");
                            _printProductService.Print(docWriter, productIncluded.Key.Product);
                        }
                    }
                }
            }

            #endregion

            #region Author Footer

            var parts = SectionHeaderFooterParts.FooterAllPages;
            var writerSet = docWriter.AddSectionHeaderFooter(parts);
            writerSet.FooterWriterAllPages.Open();

            writerSet.FooterWriterAllPages.StartParagraph();
            writerSet.FooterWriterAllPages.AddTextRun("Исполнитель:" + Environment.NewLine + $"{offer.Author}" + Environment.NewLine + $"тел.: {offer.Author.PhoneNumber}; e-mail: {offer.Author.Email}; ");
            writerSet.FooterWriterAllPages.AddHyperlink(@"http://www.uetm.ru", "uetm.ru");
            writerSet.FooterWriterAllPages.AddTextRun(Environment.NewLine + $"{offer.RegNumber} от {offer.Date.ToShortDateString()} г. - стр. ");
            writerSet.FooterWriterAllPages.AddPageNumberField(PageNumberFieldFormat.Decimal);
            writerSet.FooterWriterAllPages.EndParagraph();

            writerSet.FooterWriterAllPages.Close();

            #endregion

            docWriter.EndDocument();
            docWriter.Close();

            OpenDocument(fullPath);
        }

        private string GetShipmentConditions(List<OfferUnitsGroup> offerUnitsGroups)
        {
            if (offerUnitsGroups.Any(x => x.CostDelivery.HasValue && x.CostDelivery > 0))
            {
                if (offerUnitsGroups.All(x => x.CostDelivery.HasValue && x.CostDelivery > 0))
                    return "В стоимости оборудования учтены расходы, связанные с его доставкой на объект.";

                var positions = offerUnitsGroups
                    .Where(x => x.CostDelivery.HasValue && x.CostDelivery > 0)
                    .Select(x => x.Position).ToList();
                var end = positions.Count == 1 ? "и" : "й";
                    
                return $"В стоимости позици{end} {positions.ToStringEnum(", ")} учтены расходы, связанные с его доставкой на объект.";
            }
            return "В стоимости оборудования не учтены расходы, связанные с его доставкой на объект.";
        }

        private List<OfferUnitsGroup> GetOfferUnitsGroups(Guid offerId)
        {
            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var offerUnits = unitOfWork.Repository<OfferUnit>().Find(offerUnit => offerUnit.Offer.Id == offerId).ToList();

            //разбиваем на группы, а их делим по объектам
            var offerUnitsGroupsByFacilities = offerUnits
                .GroupBy(offerUnit => offerUnit, new OfferUnitsGroupsComparer())
                .Select(x => new OfferUnitsGroup(x))
                .OrderBy(offerUnitsGroup => offerUnitsGroup.Model, new ProductCostComparer())
                .GroupBy(offerUnitsGroup => offerUnitsGroup.Facility.Model)
                .ToList();

            var offerUnitsGroups = offerUnitsGroupsByFacilities.SelectMany(x => x.ToList()).ToList();

            //назначаем позиции ТКП
            var i = 1;
            offerUnitsGroups.ForEach(offerUnitsGroup => offerUnitsGroup.Position = i++);

            return offerUnitsGroups;
        }

        private static string PrintPaymentConditions(string text, IEnumerable<IGrouping<PaymentConditionSet, OfferUnitsGroup>> offerUnitsGroupsGrouped, DateTime date)
        {
            var result = text;
            var g = offerUnitsGroupsGrouped as IGrouping<PaymentConditionSet, OfferUnitsGroup>[] ?? offerUnitsGroupsGrouped.ToArray();
            if (g.Length == 1)
            {
                var paymentConditionSet = g.First().Key;
                return result + Environment.NewLine + paymentConditionSet.GetStringForOffer(date);
            }

            foreach (var unitsGroups in g)
            {
                result += Environment.NewLine + "- ";
                var positions = unitsGroups.Select(x => x.Position).ToStringEnum(", ");
                var prefix = unitsGroups.Count() == 1 ? "позиции" : "позиций";
                result += PrintPaymentConditions($"{prefix} {positions}:", unitsGroups.GroupBy(x => x.Model.PaymentConditionSet), date);
            }
            return result;
        }


        private static string PrintConditions<T>(string text, IEnumerable<IGrouping<T, OfferUnitsGroup>> offerUnitsGroupsGrouped)
        {
            var result = text;
            if (offerUnitsGroupsGrouped.Count() == 1)
            {
                return $"{text} {offerUnitsGroupsGrouped.First().Key}.";
            }

            foreach (var unitsGroups in offerUnitsGroupsGrouped)
            {
                result += Environment.NewLine + "- ";
                var positions = unitsGroups.Select(x => x.Position).ToStringEnum(", ");
                var prefix = unitsGroups.Count() == 1 ? "позиции" : "позиций";
                result += $"{prefix} {positions}: {unitsGroups.Key}.";
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
                            docWriter.PrintParagraph($"{productIncluded.Model.Product} {productIncluded.Model.Amount} шт.:");
                            _printProductService.Print(docWriter, productIncluded.Model.Product);
                        }
                    }

                    docWriter.PrintParagraph(Environment.NewLine);
                }
            }
        }

        protected override string GetFullPath(Document document, string path)
        {
            throw new NotImplementedException();
        }

        protected override void PrintBody(Document document, WordDocumentWriter docWriter)
        {
            throw new NotImplementedException();
        }
    }

}
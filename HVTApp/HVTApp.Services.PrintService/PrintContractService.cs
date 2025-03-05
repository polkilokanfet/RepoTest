using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintContractService : PrintUnitsServiceBase, IPrintContract
    {
        public PrintContractService(IUnityContainer container) : base(container)
        {
        }

        public void PrintContract(Guid contractId)
        {

        }

        public void PrintSpecification(Guid specificationId)
        {
            var specification = Container.Resolve<IUnitOfWork>().Repository<Specification>().GetById(specificationId);
            var unitsGroups = GetUnitsGroups(specification.SalesUnits);
            var unitsGroupsByFacilities = unitsGroups.GroupBy(unitsGroup => unitsGroup.Facility).ToList();

            //полный путь к файлу (с именем файла)
            var fullPath = GetPath($"{specification.Contract.Number}_{specification.Number}");

            var docWriter = GetWordDocumentWriter(fullPath);
            if (docWriter == null) return;
            docWriter.StartDocument();

            #region Print Text Above Table

            docWriter.PrintParagraph(string.Empty);

            var paraFormat = docWriter.CreateParagraphProperties();
            paraFormat.Alignment = ParagraphAlignment.Center;
            docWriter.PrintParagraph($"Спецификация №{specification.Number} от {specification.Date.ToShortDateString()} г.", paraFormat);

            var paraFormat1 = docWriter.CreateParagraphProperties();
            paraFormat1.Alignment = ParagraphAlignment.Both;
            Company c1 = GlobalAppProperties.Actual.OurCompany;
            Company c2 = specification.Contract.Contragent;
            var contragentEmployee = specification.Contract.ContragentEmployee;
            docWriter.PrintParagraph($"{c1.Form.FullName} «{c1.FullName}» (сокращенное наименование {c1.Form.ShortName} «{c1.ShortName}»), именуемое в дальнейшем Поставщик, в лице Генерального директора Калаущенко Владимира Николаевича, действующего на основании устава, с одной стороны, и {c1.Form.FullName} «{c1.FullName}» (сокращенное наименование {c2.Form.ShortName} «{c2.ShortName}»), именуемое в дальнейшем Покупатель, в лице {contragentEmployee?.Position} {contragentEmployee?.Person}, действующего на основании устава, с другой стороны, вместе именуемые Стороны, по отдельности Сторона, заключили настоящую спецификацию к договору поставки от {specification.Contract.Date.ToLongDateString()} {specification.Contract.Number} (далее - спецификация) о нижеследующем:", paraFormat1);

            #endregion

            #region Print Main Table

            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            PrintUnitsTable(unitsGroups, docWriter, fontBold, unitsGroupsByFacilities, specification);

            //Сумма прописью
            var sum = unitsGroups.Sum(x => x.Total);
            var vatSum = sum * specification.Vat / 100;
            var totalSum = sum + vatSum;
            var paragraphPropertiesCenter = docWriter.CreateParagraphProperties();
            paragraphPropertiesCenter.Alignment = ParagraphAlignment.Center;
            docWriter.PrintParagraph($"Всего по настоящей спецификации: {totalSum.ToSumWordCurrency()}, в том числе НДС {vatSum.ToSumWordCurrency()}", paragraphPropertiesCenter);

            #endregion

            #region Print Conditions

            var c = new Dictionary<string, string>
            {
                { "Упаковка, маркировка и консервация", "согласно ГОСТ Р 52565-2006, ГОСТ 14192-96, ГОСТ 23216-78, ГОСТ 15150-69" },
                { "Технические и иные требования", "согласно технической спецификации, являющейся приложением №1 к настоящей спецификации" },
                { "Перечень документов, относящихся к товару, передаваемых Поставщиком", "нет" },
                { "Условия хранения", "согласно статье 3(17) договора поставки" },
                { "Условия отгрузки", GetShipmentConditions(unitsGroups) },
                { "Грузополучатель", $"{specification.Contract.Contragent}, ИНН {specification.Contract.Contragent.Inn}. {specification.Contract.Contragent.AddressLegal}" },
                { "Срок поставки", PrintConditions("Срок поставки (календарных дней при соблюдении Покупателем условий оплаты):", unitsGroups.GroupBy(unitsGroup => unitsGroup.ProductionTerm)) },
                { "Момент поставки, перехода права собственности, рисков случайной гибели или случайного повреждения", "согласно статье 3(4), 3(5) договора поставки" },
                { "Оплата", PrintPaymentConditions("Условия оплаты:", unitsGroups.GroupBy(x => x.PaymentConditionSet), specification.Date.AddDays(14)) },
                { "Гарантийный срок", "составляет 60 (шестьдесят) месяцев с даты ввода в эксплуатацию, но не более 66 (шестидесяти нести) месяцев с момента поставки" },
                { "Шеф-монтаж", GetSupervisionConditions(unitsGroups) },
                { "Условия шефмонтажа", "согласно приложениям №2, 3 к настоящей спецификации" },
                { "Изготовитель", "ООО \"Эльмаш (УЭТМ)\" (ИНН 6686007865)" },
                { "Адреса электронной почты", $"согласно статье 12(2) договора поставки для направления уведомлений, сообщений, писем Стороны определяют следующие адреса: {c1.Email} (Поставщик), {c2.Email} (Покупатель)," },
                { "Иные условия", "нет" },
            };

            docWriter.StartTable(2, GetTableProperties(docWriter, docWriter.CreateTableBorderProperties()));

            var nn = 1;
            foreach (var condition in c)
            {
                docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                    $"{nn++}.", condition.Key, condition.Value);
            }

            docWriter.EndTable();

            #endregion

            #region Sender

            docWriter.PrintParagraph(string.Empty);

            var noBordersTableBorderProperties = docWriter.CreateTableBorderProperties();
            noBordersTableBorderProperties.Style = TableBorderStyle.None;
            noBordersTableBorderProperties.Sides = TableBorderSides.None;

            TableProperties tableProperties2 = GetTableProperties(docWriter, noBordersTableBorderProperties);
            tableProperties2.PreferredWidthAsPercentage = 100;
            docWriter.StartTable(3, tableProperties2);

            TableCellProperties tableCellProperties2 = docWriter.CreateTableCellProperties();
            tableCellProperties2.PreferredWidthAsPercentage = 33;

            docWriter.StartTableRow();

            var part1 = "Поставщик" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "____________ В.Н.Калаущенко";
            docWriter.PrintTableCell(part1, tableCellProperties2);

            docWriter.PrintTableCell(string.Empty);

            //ФИО подписывающего лица
            var part3 = "Покупатель" + Environment.NewLine + Environment.NewLine + Environment.NewLine + $"{contragentEmployee?.Person.Name[0]}.{contragentEmployee?.Person.Patronymic?[0]}.{contragentEmployee?.Person.Surname}";
            docWriter.PrintTableCell(part3, tableCellProperties2);

            docWriter.EndTableRow();

            docWriter.EndTable();

            #endregion

            #region Print Technical Details

            var paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;
            docWriter.PrintParagraph($"Приложение №1 к спецификации №{specification.Number} от {specification.Date.ToShortDateString()} г.", paragraphProperties, fontBold);

            PrintTechnicalDetails(docWriter, unitsGroupsByFacilities);

            #endregion

            #region Author Footer

            var writerSet = docWriter.AddSectionHeaderFooter(SectionHeaderFooterParts.FooterAllPages);
            writerSet.FooterWriterAllPages.Open();

            writerSet.FooterWriterAllPages.StartParagraph();
            writerSet.FooterWriterAllPages.AddTextRun($"Спецификация №{specification.Number} к договору {specification.Contract.Number}" + Environment.NewLine + "стр. ");
            writerSet.FooterWriterAllPages.AddPageNumberField(PageNumberFieldFormat.Decimal);
            writerSet.FooterWriterAllPages.EndParagraph();

            writerSet.FooterWriterAllPages.Close();

            #endregion

            docWriter.EndDocument();
            docWriter.Close();

            OpenDocument(fullPath);
        }

        private static string GetPath(string fileName)
        {
            //удаляем некорректные символы
            fileName = fileName.ReplaceUncorrectSimbols("-").Replace('.', '-').Replace(' ', '_') + ".docx";

            //возвращаем путь
            return Path.GetTempPath() + $"\\{fileName}";
        }
    }
}
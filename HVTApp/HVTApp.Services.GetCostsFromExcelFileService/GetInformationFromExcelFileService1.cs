using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using ExcelDataReader;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Services.GetCostsFromExcelFileService
{
    public class GetInformationFromExcelFileService1 : IGetInformationFromExcelFileService
    {
        public Dictionary<string, double> GetCostsDictionaryFromR3File(string path)
        {
            var result = new Dictionary<string, double>();

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow.ItemArray[0] is DBNull == false) continue;
                    if (dataRow.ItemArray[2] is DBNull) continue;
                    if (dataRow[26] is double == false) continue;

                    var key = dataRow[2].ToString();
                    if (result.ContainsKey(key)) continue;
                    result.Add(key, (double)dataRow[26]);
                }
            }

            stream?.Close();

            return result;
        }

        public Dictionary<string, double> GetCostsDictionaryFromCalculationFile(string path)
        {
            var result = new Dictionary<string, double>();

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet();
                var index = dataSet.Tables.IndexOf("расчет МД");
                var table = dataSet.Tables[index];
                foreach (DataRow dataRow in table.Rows)
                {
                    if (dataRow.ItemArray[0] is DBNull == false) continue;
                    if (dataRow.ItemArray[1] is DBNull) continue;
                    if (dataRow.ItemArray[2] is DBNull) continue;
                    if (dataRow.ItemArray[3] is DBNull) continue;
                    if (dataRow[5] is double == false) continue;

                    var key = dataRow[3].ToString().Trim();
                    if (result.ContainsKey(key)) continue;
                    result.Add(key, (double)dataRow[5]);
                }
            }

            stream?.Close();

            return result;
        }

        public Dictionary<string, Dictionary<int, DateTime>> GetPickingDatesFromFile(string path)
        {
            var result = new Dictionary<string, Dictionary<int, DateTime>>();

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet();
                var index = dataSet.Tables.IndexOf("Таблица всех СЗК");
                var table = dataSet.Tables[index];
                foreach (DataRow dataRow in table.Rows)
                {
                    if (dataRow[0] is DBNull) continue;
                    if (dataRow[2] is int == false) continue;
                    if (dataRow[3] is int == false) continue;
                    if (dataRow[5] is DBNull) continue;
                    if (dataRow[16] is int == false) continue;
                    if (dataRow[17] is int == false) continue;

                    var order = dataRow.ItemArray[5].ToString().Trim();
                    var week = (int)dataRow[2];
                    var year = (int)dataRow[3];
                    var friday = FridayOfWeekISO8601(year, week);
                    var positionStart = (int)dataRow[16];
                    var positionFinish = (int)dataRow[17];

                    if (result.ContainsKey(order) == false)
                        result.Add(order, new Dictionary<int, DateTime>());

                    for (int i = positionStart; i <= positionFinish; i++)
                    {
                        result[order].Add(i, friday);
                    }
                }
            }

            stream?.Close();

            return result;
        }

        public static DateTime FridayOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // 2 days from Thursday to get Friday, which is the first weekday in ISO8601
            return result.AddDays(2);
        }
    }
}
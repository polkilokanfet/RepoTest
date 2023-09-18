using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Services.GetCostsFromExcelFileService
{
    public class GetCostsFromExcelFileService1 : IGetCostsFromExcelFileService
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

                    result.Add(dataRow[2].ToString(), (double)dataRow[26]);
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

                    result.Add(dataRow[3].ToString().Trim(), (double)dataRow[5]);
                }
            }

            stream?.Close();

            return result;
        }
    }
}
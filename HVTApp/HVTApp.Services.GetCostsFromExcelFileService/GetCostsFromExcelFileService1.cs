using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Services.GetCostsFromExcelFileService
{
    public class GetCostsFromExcelFileService1 : IGetCostsFromExcelFileService
    {
        public Dictionary<string, double> GetCostsDictionary(string path)
        {
            var result = new Dictionary<string, double>();

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
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

            return result;
        }
    }
}
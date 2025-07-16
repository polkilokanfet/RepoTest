using System;
using System.IO;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PrintService.Extensions
{
    public static class OtherExt
    {
        public static string GetPath(this Document document, string path)
        {
            //var fileName = $"{document.RegNumber} {document.Date.ToShortDateString()} {DateTime.Today.ToShortDateString()}";
            return GetPath(path, document.Id.ToString());
        }

        public static string GetPath(this Offer offer, string path)
        {
            var fileName = $"{offer.RegNumber}_{offer.Date.ToShortDateString()}_{DateTime.Today.ToShortDateString()}";
            return GetPath(path, fileName);
        }


        private static string GetPath(string path, string fileName)
        {
            //удаляем некорректные символы
            fileName = fileName.ReplaceUncorrectSimbols("-").Replace('.', '-').Replace(' ', '_') + ".docx";
            
            //возвращаем путь
            return path == ""
                ? Path.GetTempPath() + $"\\{fileName}"
                : path + $"\\{fileName}";
        }
    }
}
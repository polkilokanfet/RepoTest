using System;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PrintService.Extensions
{
    public static class OtherExt
    {
        public static string GetPath(this Document document, string path)
        {
            var fileName = $"{document.RegNumber} {document.Date.ToShortDateString()} {DateTime.Today.ToShortDateString()}";
            return GetPath(path, fileName);
        }

        public static string GetPath(this Offer offer, string path)
        {
            var fileName = $"{offer.RegNumber} {offer.Date.ToShortDateString()} ({offer.RecipientEmployee.Company.ShortName.ReplaceUncorrectSimbols()}) {DateTime.Today.ToShortDateString()}";
            return GetPath(path, fileName);
        }


        private static string GetPath(string path, string fileName)
        {
            //удаляем некорректные символы
            fileName = fileName.ReplaceUncorrectSimbols("-").Replace('.', '-').Replace(' ', '_') + ".docx";
            
            //возвращаем путь
            return path == ""
                ? AppDomain.CurrentDomain.BaseDirectory + $"\\{fileName}"
                : path + $"\\{fileName}";
        }
    }
}
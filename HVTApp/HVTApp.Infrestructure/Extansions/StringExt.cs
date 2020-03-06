using System.Collections.Generic;

namespace HVTApp.Infrastructure.Extansions
{
    public static class StringExt
    {
        public static string ReplaceUncorrectSimbols(this string text, string stringToReplace = "")
        {
            var simbols = new List<string> {"/", "\\", ":", "*", "?", "\"", "<", ">", "|"};
            var result = text;
            foreach (var simbol in simbols)
            {
                result = result.Replace(simbol, stringToReplace);
            }
            return result;
        }
    }
}
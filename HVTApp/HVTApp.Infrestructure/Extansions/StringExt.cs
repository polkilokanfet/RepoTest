using System.Collections.Generic;

namespace HVTApp.Infrastructure.Extansions
{
    public static class StringExt
    {
        public static string ReplaceUncorrectSimbols(this string text, string stringToReplace = "")
        {
            var simbols = new List<string> {"/", "\\", ":", "*", "?", "\"", "<", ">", "|"};
            var result = text;
            simbols.ForEach(x => result = result.Replace(x, stringToReplace));
            return result;
        }
    }
}
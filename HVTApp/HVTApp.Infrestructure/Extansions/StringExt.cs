using System.Collections.Generic;

namespace HVTApp.Infrastructure.Extansions
{
    public static class StringExt
    {
        /// <summary>
        /// �������� ��������, ������� �� ����� ���� � ����
        /// </summary>
        /// <param name="text"></param>
        /// <param name="stringToReplace"></param>
        /// <returns></returns>
        public static string ReplaceUncorrectSimbols(this string text, string stringToReplace = "")
        {
            var simbols = new List<string> {"/", "\\", ":", "*", "?", "\"", "<", ">", "|"};
            var result = text;
            simbols.ForEach(x => result = result.Replace(x, stringToReplace));
            return result;
        }

        /// <summary>
        /// ���������� ����� ������
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        public static string LimitLengh(this string text, int lenth = 25)
        {
            return text.Length > lenth ? text.Substring(0, lenth - 1) : text;
        }

        public static string TrimAndReplaceDoubleSpaces(this string text)
        {
            if (text == null) return null;
            var result = text.Trim().ToLower();
            while (result.Contains("  "))
            {
                result = result.Replace("  ", " ");
            }
            return result;
        }

        /// <summary>
        /// ������� ������ ������� (���������� ������ ������).
        /// </summary>
        /// <param name="text">�����</param>
        /// <param name="lenght">����� ������</param>
        /// <returns></returns>
        public static string GetFirstSimbols(this string text, int lenght)
        {
            return text.Length > lenght ? text.Substring(0, lenght) : text;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace HVTApp.Infrastructure.Extensions
{
    public static class ExceptionExt
    {
        /// <summary>
        /// Печать ошибки со всеми вложенными ошибками.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string PrintAllExceptions(this Exception exception)
        {
            var messages = new List<string>();
            var stringBuilder = new StringBuilder();
            do
            {
                stringBuilder.AppendLine($"    Type:       {exception.GetType()}");
                stringBuilder.AppendLine($"    Message:    {exception.Message}");
                stringBuilder.AppendLine($"    Source:     {exception.Source}");
                stringBuilder.AppendLine($"    StackTrace: {exception.StackTrace}");
                exception = exception.InnerException;

                messages.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            } while (exception != null);

            var sep = Environment.NewLine + "--------------" + Environment.NewLine + Environment.NewLine;
            messages.Reverse();
            return string.Join(sep, messages);
        }
    }
}

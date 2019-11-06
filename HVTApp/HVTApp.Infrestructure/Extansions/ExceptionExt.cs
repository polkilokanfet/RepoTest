using System;
using System.Text;

namespace HVTApp.Infrastructure.Extansions
{
    public static class ExceptionExt
    {
        public static string GetAllExceptions(this Exception exception)
        {
            var stringBuilder = new StringBuilder();
            do
            {
                stringBuilder.AppendLine($"    Type:       {exception.GetType()}");
                stringBuilder.AppendLine($"    Message:    {exception.Message}");
                stringBuilder.AppendLine($"    Source:     {exception.Source}");
                stringBuilder.AppendLine($"    StackTrace: {exception.StackTrace}");
                exception = exception.InnerException;
                if (exception != null)
                {
                    stringBuilder.AppendLine(Environment.NewLine + "--------------" + Environment.NewLine);
                }
            } while (exception != null);

            return stringBuilder.ToString();
        }
    }
}

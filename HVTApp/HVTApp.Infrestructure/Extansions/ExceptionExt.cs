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
                stringBuilder.AppendLine($"    Source: {exception.Source}");
                stringBuilder.AppendLine($"    Type: {exception.GetType()}");
                //stringBuilder.AppendLine($"    StackTrace: {exception.StackTrace}");
                stringBuilder.AppendLine($"    Message: {exception.Message}");
                exception = exception.InnerException;
                if(exception != null)
                    stringBuilder.AppendLine(Environment.NewLine + "------" + Environment.NewLine);
            } while (exception != null);

            return stringBuilder.ToString();
        }
    }
}

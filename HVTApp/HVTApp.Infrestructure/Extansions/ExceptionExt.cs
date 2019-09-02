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
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine();
                exception = exception.InnerException;
            } while (exception != null);

            return stringBuilder.ToString();
        }
    }
}

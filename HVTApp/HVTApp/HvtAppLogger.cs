using System;
using HVTApp.Infrastructure;
using log4net;
using log4net.Config;

namespace HVTApp
{
    public class HvtAppLogger : IHvtAppLogger
    {
        static HvtAppLogger()
        {
            XmlConfigurator.Configure();
        }

        public void LogError(string message, Exception exception = null, string fileName = "")
        {
            if (exception == null)
            {
                LogManager.GetLogger(fileName).Error(message);
            }
            else
            {
                LogManager.GetLogger(fileName).Error(message, exception);
            }
        }
    }
}
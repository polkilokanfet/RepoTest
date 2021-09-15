using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IOutlookMessagesService
    {
        IEnumerable<OutlookMessage> GetOutlookMessages(string path);
    }

    public class OutlookMessage
    {

    }
}
using System;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public static class EventServiceAddresses
    {
#if DEBUG
        public static string Address => "localhost";
#else
        public static string Address => "EKB1461";
#endif
        public static Uri TcpBaseAddress => new Uri($"net.tcp://{Address}:8302/EventService.EventService");
        public static Uri HttpBaseAddress => new Uri($"http://{Address}:8301/EventService.EventService");

    }
}
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using EventService;
using EventService1 = EventService.EventService;

namespace EventServiceHost
{
    class Program
    {
        private static readonly EventService1 EventService = new EventService1();
        #if DEBUG
        private static string Address = "localhost";
        #else
        private static string Address = "EKB1461";
        #endif

        static void Main(string[] args)
        {

            var tcpBaseAddress = new Uri($"net.tcp://{Address}:8302/EventService.EventService");
            var httpBaseAddress = new Uri($"http://{Address}:8301/EventService.EventService");

            var binding = new NetTcpBinding(SecurityMode.None, true);

            using (var host = new ServiceHost(EventService, tcpBaseAddress, httpBaseAddress))
            {
                host.AddServiceEndpoint(typeof(IEventService), binding, "");
                host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
                host.AddServiceEndpoint(new UdpDiscoveryEndpoint());

                host.Closing += (sender, eventArgs) => { EventService.Close(); };
                host.Opening += (sender, eventArgs) => { Console.WriteLine($"Opening service on {Address}..."); };
                host.Opened += (sender, eventArgs) => { Console.WriteLine("Service is ready...\nPress <enter> to terminate service."); };
                host.Open();

                Console.ReadLine();
            }
        }
    }
}

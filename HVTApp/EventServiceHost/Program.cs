using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using EventService;

namespace EventServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            var tcpBaseAddress = new Uri("net.tcp://localhost:8302/EventService.EventService");
            var httpBaseAddress = new Uri("http://localhost:8301/EventService.EventService");
#else
            var tcpBaseAddress = new Uri("net.tcp://EKB1461:8302/EventService.EventService");
            var httpBaseAddress = new Uri("http://EKB1461:8301/EventService.EventService");
#endif
            var binding = new NetTcpBinding(SecurityMode.None, true);
            using (var host = new ServiceHost(typeof(EventService.EventService), tcpBaseAddress, httpBaseAddress))
            {
                host.AddServiceEndpoint(typeof(IEventService), binding, "");
                host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
                host.AddServiceEndpoint(new UdpDiscoveryEndpoint());

                host.Opening += host_Opening;
                host.Opened += host_Opened;
                host.Open();
                Console.ReadLine();
            }
        }

        private static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Service is ready...");
            Console.WriteLine("Press <enter> to terminate service.");
        }

        private static void host_Opening(object sender, EventArgs e)
        {
            Console.WriteLine("Opening service...");
        }
    }
}

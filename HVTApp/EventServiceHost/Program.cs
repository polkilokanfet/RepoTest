using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using EventService1 = EventService.EventService;

namespace EventServiceHost
{
    class Program
    {
        private static readonly EventService1 EventService = new EventService1();

        static void Main(string[] args)
        {
            var tcpBaseAddress = EventServiceAddresses.TcpBaseAddress;
            var httpBaseAddress = EventServiceAddresses.HttpBaseAddress;

            var binding = new NetTcpBinding(SecurityMode.None, true);
            //увеличиваем таймаут бездействия
            binding.SendTimeout = new TimeSpan(7, 0, 0, 0);
            binding.ReceiveTimeout = new TimeSpan(7, 0, 0, 0);
            binding.OpenTimeout = new TimeSpan(7, 0, 0, 0);
            binding.CloseTimeout = new TimeSpan(7, 0, 0, 0);

            using (var host = new ServiceHost(EventService, tcpBaseAddress, httpBaseAddress))
            {
                host.AddServiceEndpoint(typeof(IEventService), binding, "");
                host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
                host.AddServiceEndpoint(new UdpDiscoveryEndpoint());

                EventService.PrintMessageEvent += s => {Console.WriteLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {s}");};

                host.Closing += (sender, eventArgs) => { EventService.Close(); };
                host.Opening += (sender, eventArgs) => { Console.WriteLine($"Opening service on {EventServiceAddresses.Address}..."); };
                host.Opened += (sender, eventArgs) => { Console.WriteLine("Service is ready...\nPress <enter> to terminate service."); };
                host.Open();

                Console.ReadLine();
            }
        }
    }
}

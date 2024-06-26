﻿using System;
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

            //увеличиваем таймаут бездействия
            var binding = new NetTcpBinding(SecurityMode.None, true)
            {
                //SendTimeout = new TimeSpan(7, 0, 0, 0),
                ReceiveTimeout = new TimeSpan(7, 0, 0, 0),
                //OpenTimeout = new TimeSpan(7, 0, 0, 0),
                //CloseTimeout = new TimeSpan(7, 0, 0, 0)
            };

            using (var host = new ServiceHost(EventService, tcpBaseAddress, httpBaseAddress))
            {
                host.AddServiceEndpoint(typeof(IEventService), binding, "");
                host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
                host.AddServiceEndpoint(new UdpDiscoveryEndpoint());

                EventService.PrintMessageEvent += s => {Console.WriteLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {s}");};

                host.Closing += (sender, eventArgs) => { EventService.Close(); };
                host.Opening += (sender, eventArgs) => { Console.WriteLine($"Opening service...\nTCP: {tcpBaseAddress}\nHTTP: {httpBaseAddress}"); };
                host.Opened += (sender, eventArgs) => { Console.WriteLine("Service is ready...\nPress <enter> to terminate service."); };
                host.Open();

                while (true)
                {
                    var ss = Console.ReadLine();

                    if (ss == "q")
                        break;

                    if (ss == "x")
                    {
                        EventService.ApplicationsShutdown();
                        continue;
                    }

                    Console.WriteLine("q - Stop service; x - Users Applications Shutdown");
                }
                
            }
        }
    }
}

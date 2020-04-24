using System;
using System.ServiceModel;

namespace EventServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(EventService.EventService)))
            {
                host.Open();
                Console.WriteLine("Host started...");
                Console.ReadLine();
            }
        }
    }
}

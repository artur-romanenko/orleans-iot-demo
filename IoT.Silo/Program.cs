using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using Microsoft.Extensions.Logging;

namespace IoT.Silo.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = BuildSilo())
            {
                host.StartAsync().Wait();

                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();
            }
        }

        private static ISiloHost BuildSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "Demo.IoT.Cluster";
                    options.ServiceId = "IoT.Silo";
                })
                .Configure<EndpointOptions>(
                    options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureLogging(logging => logging.AddConsole());

            return builder.Build();
        }
    }
}

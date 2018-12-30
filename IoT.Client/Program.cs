using IoT.Grains.Contracts;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;

namespace IoT.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var clusterClient = BuildOrleansClient();
            clusterClient.Connect().Wait();

            var grain = clusterClient.GetGrain<ITemperatureSensorGrain>(Guid.NewGuid());

            Console.WriteLine("Client started");
            Console.WriteLine("Start typing temperature (number). Press Enter to terminate.");

            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                if (!double.TryParse(input, out double temperature))
                {
                    Console.WriteLine("Not a valid number");
                    continue;
                }

                var response = grain.SetTemperature(temperature).Result;
                Console.WriteLine(response);
            }

            clusterClient.Close().Wait();

            Console.WriteLine("Client closed");
        }

        private static IClusterClient BuildOrleansClient()
        {
            var clientBuilder = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "Demo.IoT.Cluster";
                    options.ServiceId = "IoT.Client";
                })
            .ConfigureLogging(logging => logging.AddConsole());

            return clientBuilder.Build();
        }
    }
}

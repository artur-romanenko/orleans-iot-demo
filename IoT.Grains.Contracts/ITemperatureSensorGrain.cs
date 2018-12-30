using System.Threading.Tasks;
using Orleans;

namespace IoT.Grains.Contracts
{
    public interface ITemperatureSensorGrain : IGrainWithGuidKey
    {
        Task<string> SetTemperature(double value); 
    }
}

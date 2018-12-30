using Orleans;
using System.Threading.Tasks;

namespace IoT.Grains.Contracts
{
    public interface IHelloDeviceGrain : IGrainWithIntegerKey
    {
        Task<string> SayHello(string msg);
    }
}

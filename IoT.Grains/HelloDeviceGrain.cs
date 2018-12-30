using IoT.Grains.Contracts;
using Orleans;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IoT.Grains
{
    public class HelloDeviceGrain : Grain, IHelloDeviceGrain
    {
        public override Task OnActivateAsync()
        {
            var key = this.GetPrimaryKeyLong();
            Debug.WriteLine($"Activated: {key}.");

            return base.OnActivateAsync();
        }

        public Task<string> SayHello(string msg)
        {
            return Task.FromResult(string.Format("You said {0}, I say: Hello!", msg));
        }
    }
}

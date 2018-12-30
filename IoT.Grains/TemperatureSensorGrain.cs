using IoT.Grains.Contracts;
using Orleans;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IoT.Grains
{
    public class TemperatureSensorGrain : Grain, ITemperatureSensorGrain
    {
        public override Task OnActivateAsync()
        {
            var key = this.GetPrimaryKey();
            Debug.WriteLine($"Activated: {key}.");

            return base.OnActivateAsync();
        }

        private double _lastValue;

        public Task<string> SetTemperature(double value)
        {
            bool warning = _lastValue < 100 && value >= 100;

            _lastValue = value;
            return warning ?
                Task.FromResult($"High temperature recorded: {value}.") 
                : Task.FromResult($"Current: {_lastValue}");
        }
    }
}

using IoT.Grains.Contracts;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Threading.Tasks;

namespace IoT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IClusterClient _grainsClient;

        public HelloController(IClusterClient grainsClient)
        {
            _grainsClient = grainsClient;
        }

        [HttpGet("{to}")]
        public async Task<ActionResult<string>> Get(string to)
        {
            var grain = _grainsClient.GetGrain<IHelloDeviceGrain>(33);
            return await grain.SayHello(to);
        }
    }
}

using NexusNetworkCloud.CsharpPowerview;
using NexusNetworkCloud.CsharpPowerView.Api.Shades;

namespace NexusNetworkCloud.CsharpPowerView.Api
{
    public interface IPowerViewApi
    {
        IShadeApi Shades { get; }
    }

    internal class PowerViewApi : IPowerViewApi
    {
        public IShadeApi Shades { get; private set; }

        internal PowerViewApi(PowerViewApiClient powerViewApiClient)
        {
            Shades = new ShadeApi(powerViewApiClient);
        }
    }
}

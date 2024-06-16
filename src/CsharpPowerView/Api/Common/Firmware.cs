using Newtonsoft.Json;

namespace NexusNetworkCloud.CsharpPowerView.Api.Common
{
    public class Firmware
    {
        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("subRevision")]
        public int SubRevision { get; set; }

        [JsonProperty("build")]
        public int Build { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }
    }
}

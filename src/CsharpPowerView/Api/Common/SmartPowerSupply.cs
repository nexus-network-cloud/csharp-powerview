using Newtonsoft.Json;

namespace NexusNetworkCloud.CsharpPowerView.Api.Common
{
    public class SmartPowerSupply
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }
    }
}

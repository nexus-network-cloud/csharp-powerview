using Newtonsoft.Json;
using NexusNetworkCloud.CsharpPowerView.Api.Common;
using System.Text;

namespace NexusNetworkCloud.CsharpPowerView.Api.Shades
{
    public class ShadeStatusResponse
    {
        [JsonProperty("shade")]
        public Shade Shade { get; set; } = new();
    }

    public class ListShadesResponse
    {
        [JsonProperty("shadeIds")]
        public List<int> ShadeIds { get; set; } = new();

        [JsonProperty("shadeData")]
        public List<Shade> Shades { get; set; } = new();
    }

    public class Shade
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("capabilities")]
        public int Capabilities { get; set; }

        [JsonProperty("batteryKind")]
        public int BatteryKind { get; set; }

        [JsonProperty("smartPowerSupply")]
        public SmartPowerSupply SmartPowerSupply { get; set; } = new();

        [JsonProperty("batteryStatus")]
        public BatteryStatus BatteryStatus { get; set; }

        [JsonProperty("batteryStrength")]
        public int BatteryStrength { get; set; }

        [JsonProperty("roomId")]
        public int RoomId { get; set; }

        [JsonProperty("firmware")]
        public Firmware Firmware { get; set; } = new();

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonIgnore]
        public string? DecodedName { get { return Name is null ? null : Encoding.UTF8.GetString(Convert.FromBase64String(Name)); } }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("positions")]
        public ShadePosition Positions { get; set; } = new();

        [JsonIgnore]
        public bool IsDuolite { get { return Type == 0 ? false : Type == 9; } }

        [JsonProperty("timedOut")]
        public bool TimedOut { get; set; }
    }

    public class ShadePosition
    {
        [JsonProperty("posKind1")]
        public PositionKind PosKind1 { get; set; }

        [JsonProperty("position1")]
        public int PositionOne { get; set; }

        [JsonProperty("posKind2")]
        public PositionKind PosKind2 { get; set; }

        [JsonProperty("position2")]
        public int PositionTwo { get; set; }
    }

    public enum BatteryStatus
    {
        NoStatus,
        Low,
        Medium,
        High,
        PluggedIn
    }

    public enum PositionKind
    {
        None,
        PrimaryRail,
        SecondaryRail,
        VaneTilt,
        Error
    }
}

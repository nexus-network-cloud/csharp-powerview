using Newtonsoft.Json;
using System.Text;


namespace NexusNetworkCloud.CsharpPowerview.Models
{
    public class PowerViewShade
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("capabilities")]
        public int Capabilities { get; set; }

        [JsonProperty("batteryKind")]
        public int BatteryKind { get; set; }

        [JsonProperty("smartPowerSupply")]
        public PowerViewSmartPowerSupply SmartPowerSupply { get; set; } = new();

        [JsonProperty("batteryStatus")]
        public int BatteryStatus { get; set; }

        [JsonProperty("batteryStrength")]
        public int BatteryStrength { get; set; }

        [JsonProperty("roomId")]
        public int RoomID { get; set; }

        [JsonProperty("firmware")]
        public PowerViewFirmware Firmware { get; set; } = new();

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonIgnore]
        public string DecodedName { get { return Encoding.UTF8.GetString(Convert.FromBase64String(Name)); } }

        [JsonProperty("groupId")]
        public int GroupID { get; set; }

        [JsonProperty("positions")]
        public PowerViewShadePosition Positions { get; set; } = new();

        [JsonIgnore]
        public bool IsDuolite { get { return Type == 0 ? false : Type == 9; } }
    }

    public class PowerViewSmartPowerSupply
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }
    }

    public class PowerViewFirmware
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

    public class PowerViewShadePosition
    {
        [JsonProperty("posKind1")]
        public int PosKind1 { get; set; }

        [JsonProperty("position1")]
        public int PositionOne { get; set; }

        [JsonProperty("posKind2")]
        public int PosKind2 { get; set; }

        [JsonProperty("position2")]
        public int PositionTwo { get; set; }

        public bool ShadeAlreadyInPosition(ShadePosition position)
        {
            if (position == ShadePosition.OpenTop)
            {
                return PositionOne == 65535 && PositionTwo == 0;
            }
            else if (position == ShadePosition.OpenBottom)
            {
                return PositionOne == 0 && PositionTwo == 65535;
            }
            else if (position == ShadePosition.Closed)
            {
                return PositionOne == 0 && PositionTwo == 0;
            }
            else
            {
                return false;
            }
        }
    }

    public enum ShadePosition
    {
        OpenTop,
        OpenBottom,
        Closed,
        TopPartial,
        BottomPartial
    }
}

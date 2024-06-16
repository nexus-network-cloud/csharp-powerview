using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusNetworkCloud.CsharpPowerview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusNetworkCloud.CsharpPowerView.Api.Shades
{
    public class ListShadesRequest : PowerViewApiRequest
    {
        public ListShadesRequest()
            : base(HttpMethod.Get, "shades") { }
    }

    public class GetShadeStatusRequest : PowerViewApiRequest
    {
        public GetShadeStatusRequest(int shadeId)
            : base(HttpMethod.Get, $"shades/{shadeId}") { }
    }

    public class UpdatePositionAndGetShadeStatusRequest : PowerViewApiRequest
    {
        public UpdatePositionAndGetShadeStatusRequest(int shadeId)
            : base(HttpMethod.Get, $"shades/{shadeId}?refresh=true") { }
    }

    public class UpdateBatteryLevelAndGetShadeStatusRequest : PowerViewApiRequest
    {
        public UpdateBatteryLevelAndGetShadeStatusRequest(int shadeId)
            : base(HttpMethod.Get, $"shades/{shadeId}?updateBatteryLevel=true") { }
    }

    public class SetShadePositionRequest : PowerViewApiRequest, IRequestPayload
    {
        public object RequestPayload { get; private set; }

        public SetShadePositionRequest(int shadeId, ShadePosition position)
            : base(HttpMethod.Put, $"shades/{shadeId}")
        {
            RequestPayload = new JObject(
                new JProperty("shade", new JObject(
                    new JProperty("positions", JObject.FromObject(position))
                ))
            );
        }
    }
}

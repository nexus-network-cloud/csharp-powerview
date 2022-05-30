using Newtonsoft.Json.Linq;
using NexusNetworkCloud.CsharpPowerview.Models;
using System.Net;
using System.Text;

namespace NexusNetworkCloud.CsharpPowerview
{
    public interface IPowerViewAPIClient
    {
        Task<List<PowerViewShade>?> GetAllShadesAsync();
        Task<PowerViewShade?> GetShadeAsync(int shadeID);
        Task<bool> ActivatePowerViewSceneAsync(int sceneID);
        Task<bool> SetShadePositionAsync(int shadeID, ShadePosition position);
    }

    public class PowerViewAPIClient : IPowerViewAPIClient, IDisposable
    {
        private readonly IPAddress _powerViewAddress;
        private readonly HttpClient _httpClient;

        public PowerViewAPIClient(IPAddress powerViewAddress)
        {
            _powerViewAddress = powerViewAddress;
            _httpClient = new();
        }

        public async Task<List<PowerViewShade>?> GetAllShadesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://{_powerViewAddress}/api/shades");

            if (!response.IsSuccessStatusCode)
                return null;
            else
                return JObject.Parse(await response.Content.ReadAsStringAsync())?.Value<JArray>("shadeData")?.ToObject<List<PowerViewShade>>();
        }

        public async Task<PowerViewShade?> GetShadeAsync(int shadeID)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://{_powerViewAddress}/api/shades/{shadeID}");

            if (!response.IsSuccessStatusCode)
                return null;
            else
                return JObject.Parse(await response.Content.ReadAsStringAsync())?.Value<JObject>("shade")?.ToObject<PowerViewShade>();
        }

        public async Task<bool> ActivatePowerViewSceneAsync(int sceneID)
        {
            return (await _httpClient.GetAsync($"http://{_powerViewAddress}/api/scenes?sceneId={sceneID}")).IsSuccessStatusCode;
        }

        public async Task<bool> SetShadePositionAsync(int shadeID, ShadePosition position)
        {
            if (position == ShadePosition.Closed)
                return await SetShadePositionAsync(shadeID, new PowerViewShadePosition { PosKind1 = 1, PositionOne = 0, PosKind2 = 2, PositionTwo = 0 });
            else if (position == ShadePosition.OpenTop)
                return await SetShadePositionAsync(shadeID, new PowerViewShadePosition { PosKind1 = 1, PositionOne = 65535, PosKind2 = 2, PositionTwo = 0 });
            else if (position == ShadePosition.OpenBottom)
                return await SetShadePositionAsync(shadeID, new PowerViewShadePosition { PosKind1 = 1, PositionOne = 0, PosKind2 = 2, PositionTwo = 65535 });
            else
                return false;
        }

        public async Task<bool> SetShadePositionAsync(int shadeID, PowerViewShadePosition position)
        {
            JObject positionData = JObject.FromObject(position);

            JObject positionsData = new JObject { { "positions", positionData } };

            JObject putData = new JObject { { "shade", positionsData } };

            return (await _httpClient.PutAsync($"http://{_powerViewAddress}/api/shades/{shadeID}", new StringContent(putData.ToString(), Encoding.UTF8, "application/json"))).IsSuccessStatusCode;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            _httpClient.Dispose();
        }
    }
}
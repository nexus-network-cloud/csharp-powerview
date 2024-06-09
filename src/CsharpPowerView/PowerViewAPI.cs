using Newtonsoft.Json.Linq;
using NexusNetworkCloud.CsharpPowerview.Models;
using System.Net;
using System.Text;

namespace NexusNetworkCloud.CsharpPowerview
{
    public interface IPowerViewApiClient
    {
        Task<List<PowerViewShade>?> GetAllShadesAsync();
        Task<PowerViewShade?> GetShadeAsync(int shadeID);
        Task<bool> ActivatePowerViewSceneAsync(int sceneID);
        Task<bool> SetShadePositionAsync(int shadeID, ShadePosition position);
        Task<bool> SetShadePositionAsync(int shadeID, PowerViewShadePosition position);
    }

    public class PowerViewApiClient : IPowerViewApiClient, IDisposable
    {
        private readonly IPAddress _powerViewAddress;
        private readonly HttpClient _httpClient;

        public PowerViewApiClient(IPAddress powerViewAddress)
        {
            _powerViewAddress = powerViewAddress;
            _httpClient = new();
        }

        public PowerViewApiClient(IPAddress powerViewAddress, HttpClient httpClient)
        {
            _powerViewAddress = powerViewAddress;
            _httpClient = httpClient;
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
            if (position.PositionOne < 0 || position.PositionOne > 65535)
                return false; // Has to be between those two values
            else if (position.PositionTwo < 0 || position.PositionTwo > 65535)
                return false;

            JObject positionData = JObject.FromObject(position);

            JObject positionsData = new JObject { { "positions", positionData } };

            JObject putData = new JObject { { "shade", positionsData } };

            return (await _httpClient.PutAsync($"http://{_powerViewAddress}/api/shades/{shadeID}", new StringContent(putData.ToString(), Encoding.UTF8, "application/json"))).IsSuccessStatusCode;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
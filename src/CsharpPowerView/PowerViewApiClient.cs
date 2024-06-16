using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusNetworkCloud.CsharpPowerView.Api;
using System.Net;
using System.Text;

namespace NexusNetworkCloud.CsharpPowerview
{
    public interface IPowerViewApiClient
    {
        IPowerViewApi Api { get; }

        //Task<List<PowerViewShade>?> GetAllShadesAsync();
        //Task<PowerViewShade?> GetShadeAsync(int shadeID);
        //Task<bool> ActivatePowerViewSceneAsync(int sceneID);
        //Task<bool> SetShadePositionAsync(int shadeID, ShadePosition position);
        //Task<bool> SetShadePositionAsync(int shadeID, PowerViewShadePosition position);
    }

    public class PowerViewApiClient : IPowerViewApiClient, IDisposable
    {
        private readonly IPAddress _powerViewAddress;
        private readonly HttpClient _httpClient;

        public IPowerViewApi Api { get; private set; }

        public PowerViewApiClient(IPAddress powerViewAddress)
        {
            _powerViewAddress = powerViewAddress;
            _httpClient = new();

            Api = new PowerViewApi(this);
        }

        public PowerViewApiClient(IPAddress powerViewAddress, HttpClient httpClient)
        {
            _powerViewAddress = powerViewAddress;
            _httpClient = httpClient;

            Api = new PowerViewApi(this);
        }

        internal async Task<PowerViewApiResponse<T>> SendPowerViewApiRequestAsync<T>(PowerViewApiRequest request)
        {
            HttpRequestMessage powerViewHttpRequest = new HttpRequestMessage(request.HttpMethod, $"http://{_powerViewAddress}/api/{request.RelativePath}");

            if (request is IRequestPayload)
                powerViewHttpRequest.Content = new StringContent(JObject.FromObject((request as IRequestPayload)!.RequestPayload).ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(powerViewHttpRequest);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return new PowerViewApiResponse<T> { Successful = false, Error = $"An error occured when making PowerView Api request. Error: {responseContent ?? ""}" };
            else
                return new PowerViewApiResponse<T> { Successful = true, Data = JObject.Parse(responseContent).ToObject<T>() };
        }

        internal async Task<PowerViewApiResponse> SendPowerViewApiRequestAsync(PowerViewApiRequest request)
        {
            HttpRequestMessage powerViewHttpRequest = new HttpRequestMessage(request.HttpMethod, $"http://{_powerViewAddress}/api/{request.RelativePath}");

            if (request is IRequestPayload)
                powerViewHttpRequest.Content = new StringContent(JObject.FromObject((request as IRequestPayload)!.RequestPayload).ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(powerViewHttpRequest);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return new PowerViewApiResponse { Successful = false, Error = $"An error occured when making PowerView Api request. Error: {responseContent ?? ""}" };
            else
                return new PowerViewApiResponse { Successful = true };
        }

        public async Task<bool> ActivatePowerViewSceneAsync(int sceneID)
        {
            return (await _httpClient.GetAsync($"http://{_powerViewAddress}/api/scenes?sceneId={sceneID}")).IsSuccessStatusCode;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }

    public class PowerViewApiResponse<T> : PowerViewApiResponse
    {
        public T? Data { get; set; }
    }

    public class PowerViewApiResponse
    {
        public bool Successful { get; set; }

        public string? Error { get; set; }
    }

    public class PowerViewApiRequest
    {
        public HttpMethod HttpMethod { get; set; }
        public string RelativePath { get; set; }

        public PowerViewApiRequest(HttpMethod httpMethod, string relativePath)
        {
            HttpMethod = httpMethod;
            RelativePath = relativePath.Trim('/');
        }
    }

    internal interface IRequestPayload
    {
        internal object RequestPayload { get; }
    }
}
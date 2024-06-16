using NexusNetworkCloud.CsharpPowerview;

namespace NexusNetworkCloud.CsharpPowerView.Api.Shades
{
    public interface IShadeApi
    {
        Task<PowerViewApiResponse<ListShadesResponse>> ListAsync(ListShadesRequest request);
        Task<PowerViewApiResponse<ShadeStatusResponse>> GetStatusAsync(GetShadeStatusRequest request);
        Task<PowerViewApiResponse<ShadeStatusResponse>> UpdatePositionAndGetStatusAsync(UpdatePositionAndGetShadeStatusRequest request);
        Task<PowerViewApiResponse<ShadeStatusResponse>> UpdateBatteryLevelAndGetStatusAsync(UpdateBatteryLevelAndGetShadeStatusRequest request);
        Task<PowerViewApiResponse<ShadeStatusResponse>> SetPositionAsync(SetShadePositionRequest request);
    }

    internal class ShadeApi : IShadeApi
    {
        private readonly PowerViewApiClient _powerViewApiClient;

        internal ShadeApi(PowerViewApiClient powerViewApiClient)
        {
            _powerViewApiClient = powerViewApiClient;
        }

        public async Task<PowerViewApiResponse<ListShadesResponse>> ListAsync(ListShadesRequest request)
            => await _powerViewApiClient.SendPowerViewApiRequestAsync<ListShadesResponse>(request);

        public async Task<PowerViewApiResponse<ShadeStatusResponse>> GetStatusAsync(GetShadeStatusRequest request)
            => await _powerViewApiClient.SendPowerViewApiRequestAsync<ShadeStatusResponse>(request);

        public async Task<PowerViewApiResponse<ShadeStatusResponse>> UpdatePositionAndGetStatusAsync(UpdatePositionAndGetShadeStatusRequest request)
            => await _powerViewApiClient.SendPowerViewApiRequestAsync<ShadeStatusResponse>(request);

        public async Task<PowerViewApiResponse<ShadeStatusResponse>> UpdateBatteryLevelAndGetStatusAsync(UpdateBatteryLevelAndGetShadeStatusRequest request)
            => await _powerViewApiClient.SendPowerViewApiRequestAsync<ShadeStatusResponse>(request);

        public async Task<PowerViewApiResponse<ShadeStatusResponse>> SetPositionAsync(SetShadePositionRequest request)
            => await _powerViewApiClient.SendPowerViewApiRequestAsync<ShadeStatusResponse>(request);
    }
}

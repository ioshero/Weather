using System.Threading.Tasks;
using TinyIoC;
using Weather.Common.Interfaces;
using Weather.Data.DTO;

namespace Weather.Data.Services
{
    public class WeatherProvider : IWeatherProvider
    {
        private ApiCallService apiCallService;

        public WeatherProvider()
        {
            apiCallService = new ApiCallService();
        }

        public async Task<IWeatherInfo> GetWeatherInfoAsync(double longtitude, double lattitude)
        {
            var result = await apiCallService.CallApiForCoordinatesAsync(longtitude, lattitude);
            return await WeatherInfo.FromJsonAsync(result);
        }
    }
}

using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using static Weather.Data.Consts.ApiRequestStrings;

namespace Weather.Data.Services
{
    class ApiCallService
    {
        public async Task<string> CallApiForCoordinatesAsync(double longtitude, double latitude)
        {
            using (var webClient = new WebClient
            {
                BaseAddress = BaseApiCallUrl,
                QueryString = CreateParameters(longtitude, latitude)
            })
            {
                return await webClient.DownloadStringTaskAsync(string.Empty);
            }
        }

        private static NameValueCollection CreateParameters(double longtitude, double latitude)
        {
            return new NameValueCollection
            {
                { ApiKeyParameter, ApiKey },
                { LatitudeParameter, latitude.ToString() },
                { LongtitudeParameter, longtitude.ToString() },
                { UnitsParameter, UnitsMetricValue }
            };
        }
    }
}

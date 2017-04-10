using System;
using System.Threading.Tasks;
using Weather.Common.Enums;
using Weather.Common.Interfaces;
using Weather.Data.Consts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Weather.Data.DTO
{
    public struct WeatherInfo : IWeatherInfo
    {
        #region Properties

        public double MaxTemperature
        {
            get;
            set;
        }

        public double MinTemperature
        {
            get;
            set;
        }

        public string WeatherDescription
        {
            get;
            set;
        }

        public string WeatherImageUrl
        {
            get;
            set;
        }

        #endregion

        #region Static methods

        public static WeatherInfo FromJson(string json)
        {

            var obj = JObject.Parse(json);
            var jsonWeather = obj.Value<JArray>(ApiResponseStrings.WeatherKey)[0];
            var mainInfo = obj.Value<JObject>(ApiResponseStrings.MainKey);

            return new WeatherInfo
                {
                    MaxTemperature = mainInfo.Value<double>(ApiResponseStrings.TemperatureMaxKey),
                    MinTemperature = mainInfo.Value<double>(ApiResponseStrings.TemperatureMinKey),
                    WeatherDescription = jsonWeather.Value<string>(ApiResponseStrings.WeatherDescriptionKey),
                    WeatherImageUrl = string.Format(ApiResponseStrings.IconPathFormat, jsonWeather.Value<string>(ApiResponseStrings.IconKey))
                };
        }

        public static Task<WeatherInfo> FromJsonAsync(string json)
        {
            return Task.Run(() => FromJson(json));
        }

        #endregion
    }
}

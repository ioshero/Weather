using Weather.Common.Enums;

namespace Weather.Common.Interfaces
{
    public interface IWeatherInfo
    {
        string WeatherDescription { get; }
        string WeatherImageUrl { get; }
        double MinTemperature { get; }
        double MaxTemperature { get; }
    }
}

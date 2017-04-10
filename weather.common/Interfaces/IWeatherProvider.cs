using System.Threading.Tasks;

namespace Weather.Common.Interfaces
{
    public interface IWeatherProvider
    {
        Task<IWeatherInfo> GetWeatherInfoAsync(double longtitude, double attitude);
    }
}

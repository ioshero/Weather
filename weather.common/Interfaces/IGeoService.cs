using System.Threading.Tasks;

namespace Weather.Common.Interfaces
{
    public interface IGeoService
    {
        DoubleLocation ResolveLocation();
    }
}

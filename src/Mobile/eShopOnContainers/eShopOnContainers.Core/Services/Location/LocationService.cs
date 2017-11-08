using System.Threading.Tasks;

namespace eShopOnContainers.Core.Services.Location
{
    using Models.Location;

    public class LocationService : ILocationService
    {
        public Task UpdateUserLocation(Location newLocReq, string token)
        {
            return Task.FromResult(0);
        }
    }
}
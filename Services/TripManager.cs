using Roamy.Models;

namespace Roamy.Services
{
    public class TripManager
    {
        public Trip CurrentTrip { get; set; }

        public void StartTrip(TripLocation location, DateTime startDate, DateTime endDate)
        {
            CurrentTrip = new Trip(location, startDate, endDate);
             
        }
        public TripManager()
        {

        }
    }
}

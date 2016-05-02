namespace FlightManager.Models
{
    using System.Collections.Generic;

    public class Gate
    {
        public int Id { get; set; }
        public List<Flight> Flights { get; set; }
    }
}

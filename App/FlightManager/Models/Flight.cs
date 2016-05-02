using System;

namespace FlightManager.Models
{
    public class Flight
    {
        public string FlightNo { get; set; }       
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public bool Active { get; set; }
    }
}

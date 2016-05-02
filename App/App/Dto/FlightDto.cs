namespace App.Dto
{
    using System;

    public class FlightDto
    {
        public string FlightNo { get; set; }       
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public bool Active { get; set; }

        public FlightDto()
        {
            Active = true;
        }
    }
}

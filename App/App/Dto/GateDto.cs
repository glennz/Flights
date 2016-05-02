namespace App.Dto
{
    using System.Collections.Generic;

    public class GateDto
    {
        public int Id { get; set; }
        public List<FlightDto> FlightDtos { get; set; }

        public string GateName
        {
            get { return string.Format("Gate {0}", Id.ToString()); }
        }

        public GateDto()
        {
            FlightDtos = new List<FlightDto>();            
        }
    }
}

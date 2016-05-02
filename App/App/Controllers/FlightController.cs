namespace App.Controllers
{
    using System.Web.Http;
    using App.Dto;
    using App.Filters;
    using App.Services;

    [RoutePrefix("api/gate")]
    [CustomExceptionFilter]
    public class FlightController : ApiController
    {
        private readonly IFlightService _service;

        public FlightController(IFlightService service)
        {
            _service = service;
        }

        /// <summary>
        /// view and query all my daily flights within the specific gate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetGate(int id)
        {
            var gate = _service.GetGate(id);
            
            if (gate == null)
            {
                return NotFound();
            }

            return Ok(gate);
        }

        /// <summary>
        /// view all gates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IHttpActionResult ListGates()
        {
            var gates = _service.GetGateIdList();
            return Ok(gates);
        }

        /// <summary>
        /// add new flight to a specific gate
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flightDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("flight/add/{gateId}")]
        public IHttpActionResult AddFlight(int gateId, [FromBody] FlightDto flightDto)
        {
            var f = _service.AddFlight(gateId, flightDto);
            return Ok(f);
        }

        /// <summary>
        /// update arrival and/or departure time for specific flight.
        /// </summary>
        /// <param name="flightDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("flight/update")]
        public IHttpActionResult UpdateFlight([FromBody] FlightDto flightDto)
        {
            var f = _service.UpdateFlight(flightDto);
            return Ok(f);
        }

        /// <summary>
        /// cancel a flight.
        /// </summary>
        /// <param name="flightDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("flight/cancel")]
        public IHttpActionResult CancelFlight([FromBody] FlightDto flightDto)
        {
            var f = _service.CancelFlight(flightDto);
            return Ok(f);
        }

        /// <summary>
        /// assign a flight to another gate.
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flightDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("flight/change-gate/{gateId}")]
        public IHttpActionResult ChangeFlightToAntoherGate(int gateId, [FromBody] FlightDto flightDto)
        {
            var f = _service.AssignFlightToGate(gateId, flightDto);
            return Ok(f);
        }
    }
}

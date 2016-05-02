namespace FlightManager
{
    using System;
    using System.Collections.Generic;

    using FlightManager.Models;

    public interface IRepo
    {
        /// <summary>
        /// List all gates
        /// </summary>
        /// <returns></returns>
        IEnumerable<Gate> GetGates();

        /// <summary>
        /// Add a flight to a gate
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        Flight AddFlight(int gateId, Flight flight);

        /// <summary>
        /// Update arrival time and/or departure time
        /// </summary>
        /// <param name="flightNo"></param>
        /// <param name="arrivalTime"></param>
        /// <param name="departureTime"></param>
        Flight UpdateFlightTime(string flightNo, DateTime? arrivalTime, DateTime? departureTime);

        /// <summary>
        /// Set flight inactive
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flightNo"></param>
        Flight CancelFlight(string flightNo);

        /// <summary>
        /// Remove flight from gate
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        bool RemoveFlight(int gateId, Flight flight);
    }
}

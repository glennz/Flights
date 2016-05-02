namespace App.Services
{
    using System.Collections.Generic;

    using App.Dto;

    using FlightManager.Models;

    public interface IFlightService
    {
        /// <summary>
        /// get all gate ids
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> GetGateIdList();

        /// <summary>
        /// get all gates
        /// </summary>
        /// <returns></returns>
        IEnumerable<GateDto> GetGates();

        /// <summary>
        /// get a gate by id
        /// </summary>
        /// <param name="gateId"></param>
        /// <returns></returns>
        GateDto GetGate(int gateId);

        /// <summary>
        /// Get flights by gate id
        /// </summary>
        /// <param name="gateId"></param>
        /// <returns></returns>
        IEnumerable<FlightDto> GetFlights(int gateId);

        /// <summary>
        /// Get a flight by flight no
        /// </summary>
        /// <param name="flightNo"></param>
        /// <returns></returns>
        FlightDto GetFlight(string flightNo);

        /// <summary>
        /// Get a flight by gateId and Flight no
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flightNo"></param>
        /// <returns></returns>
        FlightDto GetFlight(int gateId, string flightNo);

        /// <summary>
        /// Get gateId by flight no
        /// </summary>
        /// <param name="flightNo"></param>
        /// <returns></returns>
        int GetGateId(string flightNo);

        /// <summary>
        /// Check if flight exist
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        bool IsFlightExist(FlightDto flight);

        /// <summary>
        /// Check if flight exist
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        bool IsFlightExist(int gateId, FlightDto flight);

        /// <summary>
        /// Add a flight to a gate
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        FlightDto AddFlight(int gateId, FlightDto flight);

        /// <summary>
        /// Update a flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        FlightDto UpdateFlight(FlightDto flight);

        /// <summary>
        /// Cancel a flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        FlightDto CancelFlight(FlightDto flight);

        FlightDto AssignFlightToGate(int gateId, FlightDto flight);

        /// <summary>
        /// Is new flight overlap with existing flights
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        bool IsOverlap(int gateId, FlightDto flight);
    }
}

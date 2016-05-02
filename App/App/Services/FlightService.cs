namespace App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using App.Dto;

    using AutoMapper;

    using FlightManager;
    using FlightManager.Models;

    public class FlightService : IFlightService
    {
        private readonly IRepo _repo;
        private readonly IMappingEngine _mappingEngine;

        public FlightService(IRepo repo, IMappingEngine mappingEngine)
        {
            _repo = repo;
            _mappingEngine = mappingEngine;
        }

        /// <summary>
        /// get all gate ids
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetGateIdList()
        {
            var gates = _repo.GetGates();
            return gates.Select(x => x.Id);
        }

        /// <summary>
        /// get all gates
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GateDto> GetGates()
        {
            var gates = _repo.GetGates();
            return _mappingEngine.Map<IEnumerable<GateDto>>(gates);
        }

        /// <summary>
        /// get a gate by id
        /// </summary>
        /// <param name="gateId"></param>
        /// <returns></returns>
        public GateDto GetGate(int gateId)
        {
            var gate = _repo.GetGates().FirstOrDefault(o => o.Id == gateId);
            return _mappingEngine.Map<GateDto>(gate);
        }

        /// <summary>
        /// Get flights by gate id
        /// </summary>
        /// <param name="gateId"></param>
        /// <returns></returns>
        public IEnumerable<FlightDto> GetFlights(int gateId)
        {
            var gate = GetGate(gateId);
            if (gate == null)
            {
                throw new InvalidOperationException("Gate is not found");
            }

            //The terminal only shows today schedules from 12:00AM to 11:59PM.
            return gate.FlightDtos.Where(o => o.DepartureDateTime != null && 
                (o.ArrivalDateTime != null && (o.ArrivalDateTime.Value.Hour >= 12 || o.DepartureDateTime.Value.Hour >= 12)));
        }

        /// <summary>
        /// Get a flight by flight no
        /// </summary>
        /// <param name="flightNo"></param>
        /// <returns></returns>
        public FlightDto GetFlight(string flightNo)
        {
            return GetGates().Select(gate => gate.FlightDtos.SingleOrDefault(o => o.FlightNo == flightNo)).FirstOrDefault(flight => flight != null);
        }

        /// <summary>
        /// Get a flight by gateId and Flight no
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flightNo"></param>
        /// <returns></returns>
        public FlightDto GetFlight(int gateId, string flightNo)
        {
            var gate = GetGate(gateId);

            if (gate == null)
            {
                throw new InvalidOperationException("Gate is not found");
            }

            return gate.FlightDtos.SingleOrDefault(o => o.FlightNo == flightNo);
        }

        /// <summary>
        /// Get gateId by flight no
        /// </summary>
        /// <param name="flightNo"></param>
        /// <returns></returns>
        public int GetGateId(string flightNo)
        {
            var gates = GetGates();

            return (from gate in gates 
                    where gate.FlightDtos.Any(x => x.FlightNo == flightNo) 
                    select gate.Id).FirstOrDefault();
        }

        /// <summary>
        /// Check if flight exist
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public bool IsFlightExist(FlightDto flight)
        {
            var f = GetFlight(flight.FlightNo);

            return f != null;
        }

        /// <summary>
        /// Check if flight exist
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public bool IsFlightExist(int gateId, FlightDto flight)
        {
            var f = GetFlight(gateId, flight.FlightNo);

            return f != null;
        }

        /// <summary>
        /// Add a flight to a gate
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public FlightDto AddFlight(int gateId, FlightDto flight)
        {
            if (!flight.ArrivalDateTime.HasValue)
            {
                throw new InvalidOperationException("Flight arrival time is invalid");
            }

            if (!flight.DepartureDateTime.HasValue)
            {
                throw new InvalidOperationException("Flight departure time is invalid");
            }

            if (IsFlightExist(flight))
            {
                throw new InvalidOperationException("Flight is found");
            }

            if (IsOverlap(gateId, flight))
            {
                throw new InvalidOperationException("Flight overlaps with others");
            }

            var f = _mappingEngine.Map<Flight>(flight);
            _repo.AddFlight(gateId, f);
            return flight;
        }

        /// <summary>
        /// Update a flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public FlightDto UpdateFlight(FlightDto flight)
        {
            var gateId = GetGateId(flight.FlightNo);

            if (IsOverlap(gateId, flight))
            {
                throw new InvalidOperationException("Flight overlaps with others");
            }

            var f = _repo.UpdateFlightTime(flight.FlightNo, flight.ArrivalDateTime, flight.DepartureDateTime);
            return _mappingEngine.Map<FlightDto>(f);
        }

        /// <summary>
        /// Cancel a flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public FlightDto CancelFlight(FlightDto flight)
        {
            var f = _repo.CancelFlight(flight.FlightNo);
            return _mappingEngine.Map<FlightDto>(f);
        }

        public FlightDto AssignFlightToGate(int gateId, FlightDto flight)
        {
            var oldGateId = GetGateId(flight.FlightNo);
            if (oldGateId == 0)
            {
                throw new InvalidOperationException(string.Format("Cannot find flight {0}", flight.FlightNo));
            }
            //remove flight from old gate
            var oldGate = GetGate(gateId);

            var currentFlight = _mappingEngine.Map<Flight>(flight);

            _repo.RemoveFlight(oldGateId, currentFlight);
            
            //add to new gate
            var gate = GetGate(gateId);
            if (gate == null)
            {
                throw new InvalidOperationException(string.Format("Cannot find gate {0}", gateId));
            }

            if (IsFlightExist(gateId, flight))
            {
                throw new InvalidOperationException("Flight is found");
            }

            var newFlight = _repo.AddFlight(gateId, currentFlight);

            return _mappingEngine.Map<FlightDto>(newFlight);
        }

        /// <summary>
        /// Is new flight overlap with existing flights
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public bool IsOverlap(int gateId, FlightDto flight)
        {
            var gate = GetGate(gateId);

            foreach (var f in gate.FlightDtos)
            {
                if (f.FlightNo == flight.FlightNo)
                {
                    continue;                    
                }

                if (f.ArrivalDateTime <= flight.ArrivalDateTime && flight.ArrivalDateTime <= f.DepartureDateTime)
                {
                    return true;
                }

                if (f.ArrivalDateTime <= flight.DepartureDateTime && flight.DepartureDateTime <= f.DepartureDateTime)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightManager
{
    using FlightManager.Data;
    using FlightManager.Models;

    /// <summary>
    /// Gate and flight Repository
    /// </summary>
    public class Repo : IRepo
    {
        /// <summary>
        /// Inline memory
        /// </summary>
        public Repo()
        {
            Init();
        }

        /// <summary>
        /// List all gates
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Gate> GetGates()
        {
            return DataPool.Gates;
        }

        /// <summary>
        /// Add a flight to a gate
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        public Flight AddFlight(int gateId, Flight flight)
        {
            var gate = GetGates().Single(o => o.Id == gateId);
            gate.Flights.Add(flight);
            return flight;
        }

        /// <summary>
        /// Update arrival time and/or departure time
        /// </summary>
        /// <param name="flightNo"></param>
        /// <param name="arrivalTime"></param>
        /// <param name="departureTime"></param>
        public Flight UpdateFlightTime(string flightNo, DateTime? arrivalTime, DateTime? departureTime)
        {
            if (!arrivalTime.HasValue && !departureTime.HasValue)
            {
                throw new InvalidOperationException("Invalid time");
            }

            var flight = GetGates().Select(gate => gate.Flights.SingleOrDefault(o => o.FlightNo == flightNo)).FirstOrDefault(f => f != null);

            if (flight == null)
            {
                throw new InvalidOperationException("Flight is not found");
            }
            
            if (!flight.Active)
            {
                throw new InvalidOperationException("Flight is not active");
            }

            if (arrivalTime.HasValue)
            {
                flight.ArrivalDateTime = arrivalTime;
            }

            if (departureTime.HasValue)
            {
                flight.DepartureDateTime = departureTime;
            }

            return flight;
        }

        /// <summary>
        /// Set flight inactive
        /// </summary>
        /// <param name="flightNo"></param>
        public Flight CancelFlight(string flightNo)
        {
            Flight f = null;
            foreach (var flight in from gate in GetGates() from flight in gate.Flights where flight.FlightNo == flightNo select flight)
            {
                flight.Active = false;
                f = flight;
                break;
            }
            return f;
        }

        /// <summary>
        /// Remove a flight
        /// </summary>
        /// <param name="gateId"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public bool RemoveFlight(int gateId, Flight flight)
        {
            var gate = GetGates().SingleOrDefault(o => o.Id == gateId);
            if (gate != null && gate.Flights.Any(o => o.FlightNo == flight.FlightNo))
            {
                var f = gate.Flights.Single(x => x.FlightNo == flight.FlightNo);
                gate.Flights.Remove(f);
                return true;
            }
            return false;
        }

        //populate data
        private static void Init()
        {
            if (DataPool.Gates != null)
            {
                return;
            }

            DataPool.Gates = new List<Gate>
            {
                new Gate()
                {
                    Id=1,
                    Flights = new List<Flight>
                    {
                        new Flight
                        {
                            FlightNo = "Q1111",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222a",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 12, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 12, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222b",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 13, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 13, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222c",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 14, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 14, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222d",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 15, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 15, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222e",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 16, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 16, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222f",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 17, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 17, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222g",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 18, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 18, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222h",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 19, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 19, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222i",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 20, 0, 0),
                             DepartureDateTime= new DateTime(2016, 5, 10, 20, 29, 0),
                            Active = true
                        }
                    }
                },
                new Gate()
                {
                    Id=2,
                    Flights = new List<Flight>
                    {
                        new Flight
                        {
                            FlightNo = "QS1122",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 9, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 9, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "QS2211a",
                             ArrivalDateTime= new DateTime(2016, 5, 10, 10, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 10, 29, 0),         
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "QS2211b",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 11, 29, 0),    
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "QS2211c",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 12, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 12, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211d",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 13, 0, 0),
                             DepartureDateTime= new DateTime(2016, 5, 10, 13, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211e",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 14, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 14, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211f",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 15, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 15, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211g",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 16, 0, 0),
                             DepartureDateTime= new DateTime(2016, 5, 10, 16, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211h",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 17, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 17, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211i",
                             ArrivalDateTime= new DateTime(2016, 5, 10, 18, 0, 0),
                            DepartureDateTime = new DateTime(2016, 5, 10, 18, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 19, 0, 0),
                             DepartureDateTime= new DateTime(2016, 5, 10, 19, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211j",
                             ArrivalDateTime = new DateTime(2016, 5, 10, 20, 0, 0),
                             DepartureDateTime= new DateTime(2016, 5, 10, 20, 29, 0),    
                            Active = true                            
                        }
                    }
                },
                new Gate()
                {
                    Id=3,
                    Flights = new List<Flight>
                    {
                        new Flight
                        {
                            FlightNo = "VS122",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 21, 0, 0),
                             DepartureDateTime= new DateTime(2016, 5, 10, 21, 29, 0),    
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "VS900",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 22, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 22, 29, 0),    
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "VS888",
                            ArrivalDateTime = new DateTime(2016, 5, 10, 23, 0, 0),
                             DepartureDateTime = new DateTime(2016, 5, 10, 23, 29, 0),    
                            Active = true                            
                        }
                    }
                }
            };            
        }
    }
}

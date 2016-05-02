using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagerTests
{
    using System.Security.Authentication.ExtendedProtection.Configuration;

    using App.Config;
    using App.Dto;
    using App.Services;

    using AutoMapper;
    using AutoMapper.Mappers;

    using FlightManager;
    using FlightManager.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    //Demonstrate of using Moq for repo layer
    [TestClass]
    public class RepoUnitTestWithMock
    {
        private Mock<IRepo> m;
        private FlightService service;

        [TestInitialize]
        public void TestInit()
        {
            //Mock the repo
            m = new Mock<IRepo>();
        }

        private void SetupMock()
        {
            var gates = InitData();
            m.Setup(mock => mock.GetGates()).Returns(gates);

            var repo = m.Object;

            var cfg = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            MappingRelationship.Create(cfg);
            cfg.AssertConfigurationIsValid();

            var mapEngine = new MappingEngine(cfg);

            service = new FlightService(repo, mapEngine);
        }

        private void SetupMockForCancel()
        {
            var f = new Flight
            {
                FlightNo = "Q1111",
                DepartureDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                ArrivalDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                Active = false
            };
            m.Setup(mock => mock.CancelFlight(f.FlightNo)).Returns(f);

            var repo = m.Object;

            var cfg = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            MappingRelationship.Create(cfg);
            cfg.AssertConfigurationIsValid();

            var mapEngine = new MappingEngine(cfg);

            service = new FlightService(repo, mapEngine);
        }

        private void SetupMockForAdd()
        {
            var gates = InitData();
            m.Setup(mock => mock.GetGates()).Returns(gates);

            var f = new Flight
            {
                FlightNo = "Q111T",
                DepartureDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                ArrivalDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                Active = true
            };
            m.Setup(mock => mock.AddFlight(1, f)).Returns(f);

            var repo = m.Object;

            var cfg = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            MappingRelationship.Create(cfg);
            cfg.AssertConfigurationIsValid();

            var mapEngine = new MappingEngine(cfg);

            service = new FlightService(repo, mapEngine);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            m = null;
            service = null;
        }

        [TestMethod]
        public void ShouldGetAllGateIds()
        {
            SetupMock();

            var ids = service.GetGateIdList();
            Assert.IsTrue(ids.Count() == 3);
        }

        [TestMethod]
        public void ShouldGetGates()
        {
            SetupMock();

            //Mock and set output
            var gatesOutput = service.GetGates();
            Assert.IsTrue(gatesOutput.Count() == 3);

            var gate = gatesOutput.First();
            Assert.IsTrue(gate.Id == 1);
        }

        [TestMethod]
        public void ShouldGateOne()
        {
            SetupMock();

            var gate = service.GetGate(1);
            Assert.IsTrue(gate.Id == 1);
            Assert.IsTrue(gate.FlightDtos.Count == 10);
        }

        [TestMethod]
        public void ShouldGetFlight()
        {
            SetupMock();

            var flight = service.GetFlight("VS122");
            Assert.IsTrue(flight.FlightNo == "VS122");

            var flight1 = service.GetFlight(1, "VS122");
            Assert.IsNull(flight1);

            var flight2 = service.GetFlight(3, "VS122");
            Assert.IsTrue(flight2.FlightNo == "VS122");
        }

        [TestMethod]
        public void ShouldGetFlights()
        {
            SetupMock();

            var flights = service.GetFlights(1);
            Assert.IsTrue(flights.Count() == 9);
        }

        [TestMethod]
        public void ShoudFoundFlight()
        {
            SetupMock();

            var f = new FlightDto
            {
                FlightNo = "Q2222f"
            };
            var found = service.IsFlightExist(f);
            Assert.IsTrue(found);

            var found1 = service.IsFlightExist(1, f);
            Assert.IsTrue(found1);
        }

        [TestMethod]
        public void ShoudNotFoundFlight()
        {
            SetupMock();

            var f = new FlightDto
            {
                FlightNo = "Q2222f"
            };
            var found2 = service.IsFlightExist(2, f);
            Assert.IsFalse(found2);
        }

        [TestMethod]
        public void ShoudCancelFlight()
        {
            SetupMockForCancel();

            var f = new FlightDto
            {
                FlightNo = "Q1111",
                DepartureDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                ArrivalDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                Active = true
            };

            var flight = service.CancelFlight(f);
            Assert.IsTrue(flight.Active == false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Flight is found")]
        public void ShouldNotAddFlight()
        {
            SetupMockForAdd();

            var f = new FlightDto
            {
                FlightNo = "Q1111",
                DepartureDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                ArrivalDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                Active = true
            };

            var f1 = service.AddFlight(1, f);

            Assert.IsTrue(f1.FlightNo == "Q1111");
        }

        [TestMethod]
        public void ShouldAddFlight()
        {
            SetupMockForAdd();

            var f = new FlightDto
            {
                FlightNo = "Q111T",
                DepartureDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                ArrivalDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                Active = true
            };

            var f1 = service.AddFlight(1, f);

            Assert.IsTrue(f1.FlightNo == "Q111T");
        }

        //populate data for mock
        private List<Gate> InitData()
        {
            var gates = new List<Gate>
            {
                new Gate()
                {
                    Id=1,
                    Flights = new List<Flight>
                    {
                        new Flight
                        {
                            FlightNo = "Q1111",
                            DepartureDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222a",
                            DepartureDateTime = new DateTime(2016, 5, 10, 12, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 12, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222b",
                            DepartureDateTime = new DateTime(2016, 5, 10, 13, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 13, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222c",
                            DepartureDateTime = new DateTime(2016, 5, 10, 14, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 14, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222d",
                            DepartureDateTime = new DateTime(2016, 5, 10, 15, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 15, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222e",
                            DepartureDateTime = new DateTime(2016, 5, 10, 16, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 16, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222f",
                            DepartureDateTime = new DateTime(2016, 5, 10, 17, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 17, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222g",
                            DepartureDateTime = new DateTime(2016, 5, 10, 18, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 18, 29, 0),
                        },
                        new Flight
                        {
                            FlightNo = "Q2222h",
                            DepartureDateTime = new DateTime(2016, 5, 10, 19, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 19, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "Q2222i",
                            DepartureDateTime = new DateTime(2016, 5, 10, 20, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 20, 29, 0),
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
                            DepartureDateTime = new DateTime(2016, 5, 10, 9, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 9, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "QS2211a",
                            DepartureDateTime = new DateTime(2016, 5, 10, 10, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 10, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "QS2211b",
                            DepartureDateTime = new DateTime(2016, 5, 10, 11, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 11, 29, 0),
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211c",
                            DepartureDateTime = new DateTime(2016, 5, 10, 12, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 12, 29, 0),
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211d",
                            DepartureDateTime = new DateTime(2016, 5, 10, 13, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 13, 29, 0),
                            Active = true                          
                        },
                        new Flight
                        {
                            FlightNo = "QS2211e",
                            DepartureDateTime = new DateTime(2016, 5, 10, 14, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 14, 29, 0),
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211f",
                            DepartureDateTime = new DateTime(2016, 5, 10, 15, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 15, 29, 0),
                            Active = true                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211g",
                            DepartureDateTime = new DateTime(2016, 5, 10, 16, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 16, 29, 0)                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211h",
                            DepartureDateTime = new DateTime(2016, 5, 10, 17, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 17, 29, 0)                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211i",
                            DepartureDateTime = new DateTime(2016, 5, 10, 18, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 18, 29, 0)                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211",
                            DepartureDateTime = new DateTime(2016, 5, 10, 19, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 19, 29, 0)                            
                        },
                        new Flight
                        {
                            FlightNo = "QS2211j",
                            DepartureDateTime = new DateTime(2016, 5, 10, 20, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 20, 29, 0),
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
                            DepartureDateTime = new DateTime(2016, 5, 10, 21, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 21, 29, 0),
                            Active = true
                        },
                        new Flight
                        {
                            FlightNo = "VS900",
                            DepartureDateTime = new DateTime(2016, 5, 10, 22, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 22, 29, 0),
                            Active = true                         
                        },
                        new Flight
                        {
                            FlightNo = "VS888",
                            DepartureDateTime = new DateTime(2016, 5, 10, 23, 0, 0),
                            ArrivalDateTime = new DateTime(2016, 5, 10, 23, 29, 0),
                            Active = true                           
                        }
                    }
                }
            };

            return gates;
        }
    }
}

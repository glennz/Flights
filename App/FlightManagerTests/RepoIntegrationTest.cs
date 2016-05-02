using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightManagerTests
{
    using System.Linq;

    using FlightManager;
    using FlightManager.Models;

    [TestClass]
    public class RepoIntegrationTest
    {
        private IRepo repo;

        [TestInitialize]
        public void Init()
        {
            repo = new Repo();
        }

        [TestCleanup]
        public void Clear()
        {
            repo = null;
        }

        [TestMethod]
        public void ShouldListGates()
        {
            var gates = repo.GetGates();
            Assert.IsTrue(gates.Any());
        }

        [TestMethod]
        public void ShoudCancelFlight()
        {
            var flight = repo.CancelFlight("VS900");
            Assert.IsTrue(flight.Active == false);
        }

        [TestMethod]
        public void ShouldAddFlight()
        {
            var f = new Flight
            {
                FlightNo = "Q111T",
                DepartureDateTime = new DateTime(2016, 5, 10, 1, 0, 0),
                ArrivalDateTime = new DateTime(2016, 5, 10, 1, 29, 0),
                Active = true
            };

            var f1 = repo.AddFlight(1, f);
            var f2 = repo.GetGates().Where(o => o.Id == 1).Single().Flights.SingleOrDefault(o => o.FlightNo == "Q111T");
            Assert.IsNotNull(f2);
            Assert.IsTrue(f2.FlightNo == "Q111T");
        }

        /// <summary>
        /// Exception test. MSTest is not good at handling exception test. NUnit or XUnit is much better
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Departure time and Arrival time are null")]
        public void ShouldFailToUpdateFlight()
        {
            DateTime? arriveTime = null;
            DateTime? departTime = null;
            var flight = repo.UpdateFlightTime("Q2222b", arriveTime, departTime);
        }

        [TestMethod]        
        public void ShouldUpdateFlight()
        {
            DateTime? arriveTime = new DateTime(2016, 5, 10, 13, 28, 0);
            DateTime? departTime = null;
            var flight = repo.UpdateFlightTime("Q2222b", arriveTime, departTime);

            Assert.IsTrue(flight.ArrivalDateTime.Value.Minute == 28);
        }

        [TestMethod]
        public void ShouldRemoveFlight()
        {
            var f = new Flight
            {
                FlightNo = "Q2222a",
                DepartureDateTime = new DateTime(2016, 5, 10, 12, 0, 0),
                ArrivalDateTime = new DateTime(2016, 5, 10, 12, 29, 0),
                Active = true
            };

            var result = repo.RemoveFlight(1, f);
            Assert.IsTrue(result);

            var gate = repo.GetGates().SingleOrDefault(x => x.Id == 1);
            var f1 = gate.Flights.SingleOrDefault(o => o.FlightNo == f.FlightNo);
            Assert.IsNull(f1);
        }
    }
}

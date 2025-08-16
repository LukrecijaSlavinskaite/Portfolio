using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using L3.MyClasses;

namespace L3.Tests
{
    [TestClass]
    public class TripLinkListTests
    {
        [TestMethod]
        public void SetFifo_And_Get_FirstTripIsCorrect()
        {
            var list = new TripLinkList<Trip>();
            var trip = new Trip("Kaunas", "Vilnius", TimeSpan.Parse("08:00"), "Klaipėda", TimeSpan.Parse("12:00"), 1);

            list.SetFifo(trip);
            list.Begin();
            var result = list.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(trip, result);
        }

        [TestMethod]
        public void Remove_Trip_RemovesItFromList()
        {
            var list = new TripLinkList<Trip>();
            var trip1 = new Trip("A", "B", TimeSpan.Parse("08:00"), "C", TimeSpan.Parse("10:00"), 1);
            var trip2 = new Trip("D", "E", TimeSpan.Parse("09:00"), "F", TimeSpan.Parse("11:00"), 2);
            list.SetFifo(trip1);
            list.SetFifo(trip2);

            list.Begin();
            list.Remove(trip1);

            list.Begin();
            Assert.AreEqual(trip2, list.Get());
            list.Next();
            Assert.IsFalse(list.Exist()); 
        }

        [TestMethod]
        public void Sort_Trips_SortsByDepartureCity()
        {
            var list = new TripLinkList<Trip>();
            var tripA = new Trip("X", "A", TimeSpan.Parse("07:00"), "Y", TimeSpan.Parse("09:00"), 1);
            var tripB = new Trip("X", "B", TimeSpan.Parse("06:00"), "Y", TimeSpan.Parse("08:00"), 1);
            list.SetFifo(tripB);
            list.SetFifo(tripA);

            list.Sort();
            list.Begin();
            var first = list.Get();
            list.Next();
            var second = list.Get();

            Assert.AreEqual("A", first.DepartureCity);
            Assert.AreEqual("B", second.DepartureCity);
        }

        [TestMethod]
        public void SetLifo_AddsAtBeginning()
        {
            var list = new TripLinkList<Trip>();
            var trip1 = new Trip("X", "A", TimeSpan.Parse("08:00"), "B", TimeSpan.Parse("10:00"), 1);
            var trip2 = new Trip("X", "C", TimeSpan.Parse("09:00"), "D", TimeSpan.Parse("11:00"), 2);

            list.SetLifo(trip1);
            list.SetLifo(trip2); 

            list.Begin();
            Assert.AreEqual(trip2, list.Get());
        }

        [TestMethod]
        public void Next_MovesToNextNode()
        {
            var list = new TripLinkList<Trip>();
            var trip1 = new Trip("X", "A", TimeSpan.Parse("08:00"), "B", TimeSpan.Parse("10:00"), 1);
            var trip2 = new Trip("X", "C", TimeSpan.Parse("09:00"), "D", TimeSpan.Parse("11:00"), 2);

            list.SetFifo(trip1);
            list.SetFifo(trip2);

            list.Begin();
            Assert.AreEqual(trip1, list.Get()); 
            list.Next();
            Assert.AreEqual(trip2, list.Get());
        }

        [TestMethod]
        public void Exist_ReturnsFalseIfEmpty()
        {
            var list = new TripLinkList<Trip>();
            list.Begin();
            Assert.IsFalse(list.Exist());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2.MyClasses
{
    /// <summary>
    /// Class to store data about a trip
    /// </summary>
    public class Trip
    {
        public string CurrentCity { get; set; }
        public string DepartureCity { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string ArrivalCity { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public bool IsBusTransit { get; set; }
        public int Weekday { get; set; }

        /// <summary>
        /// A constructor that sets initial data to zero
        /// </summary>
        public Trip()
        {
            DepartureCity = "";
            DepartureTime = TimeSpan.Zero;
            ArrivalCity = "";
            ArrivalTime = TimeSpan.Zero; 
            CurrentCity = "";
            Weekday = 0;
            IsBusTransit = false;
        }
        /// <summary>
        /// Bool that marks a bus transit if departure city is not the same as current city
        /// </summary>
        /// <param name="currentCity">current city</param>
        /// <returns>true if departure city is not the same as current city</returns>
        private bool IsTransit(string currentCity)
        {
            return DepartureCity != currentCity;
        }
        /// <summary>
        /// Method that adds data to a trip
        /// </summary>
        /// <param name="CurrentCity"></param>
        /// <param name="DepartureCity"></param>
        /// <param name="DepartureTime"></param>
        /// <param name="ArrivalCity"></param>
        /// <param name="ArrivalTime"></param>
        /// <param name="Weekday"></param>
        public void AddTrip(string CurrentCity, string DepartureCity, TimeSpan DepartureTime, string ArrivalCity, TimeSpan ArrivalTime, int Weekday)
        {
            this.CurrentCity = CurrentCity;
            this.DepartureCity = DepartureCity;
            this.DepartureTime = DepartureTime;
            this.ArrivalCity = ArrivalCity;
            this.ArrivalTime = ArrivalTime;
            this.Weekday = Weekday;
            IsBusTransit = IsTransit(CurrentCity);
        }
        /// <summary>
        /// Override that forms a table with trips data 
        /// </summary>
        /// <returns>a string line with formated data</returns>
        public override string ToString()
        {
            return $"{DepartureCity,-20} {DepartureTime.ToString(@"hh\:mm"),-16} {ArrivalCity,-18} {ArrivalTime.ToString(@"hh\:mm"),-18} {Weekday, -5}";
        }
        /// <summary>
        /// bool operator that compares two trip objects based on their departure city and departure time
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns true if trip1 departs from a greater city or later</returns>
        static public bool operator > (Trip trip1, Trip trip2)
        {
            int DC = String.Compare(trip1.DepartureCity, trip2.DepartureCity, StringComparison.CurrentCulture);

            return DC > 0 || DC==0 & trip1.DepartureTime > trip2.DepartureTime;
        }
        /// <summary>
        /// bool operator that compares two trip objects based on their departure city and departure time
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns true if trip1 departs from a smaller city or earlier</returns>
        static public bool operator <(Trip trip1, Trip trip2)
        {
            int DC = String.Compare(trip1.DepartureCity, trip2.DepartureCity, StringComparison.CurrentCulture);

            return DC < 0 || DC == 0 & trip1.DepartureTime > trip2.DepartureTime;
        }
    }
}
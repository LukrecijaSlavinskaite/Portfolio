using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace L3.MyClasses
{
    /// <summary>
    /// Class to store data about a trip
    /// </summary>
    
    [Serializable]

    public class Trip : IComparable<Trip>, IEquatable<Trip>
    {
        public string CurrentCity { get; set; }
        public string DepartureCity { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string ArrivalCity { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public bool IsBusTransit { get; set; }
        public int Weekday { get; set; }

        /// <summary>
        /// A constructor that sets initial data 
        /// </summary>
        public Trip(string CurrentCity, string DepartureCity, TimeSpan DepartureTime, string ArrivalCity, TimeSpan ArrivalTime, int Weekday)
        {
            this.CurrentCity = CurrentCity;
            this.DepartureCity = DepartureCity;
            this.DepartureTime = DepartureTime;
            this.ArrivalCity = ArrivalCity;
            this.ArrivalTime = ArrivalTime;
            this.IsBusTransit = IsTransit(CurrentCity, DepartureCity);
            this.Weekday = Weekday;
        }

        /// <summary>
        /// Bool that marks a bus transit if departure city is not the same as current city
        /// </summary>
        /// <param name="currentCity">current city</param>
        /// <returns>true if departure city is not the same as current city</returns>
        private bool IsTransit(string currentCity, string departureCity)
        {
            return departureCity != currentCity;
        }
        /// <summary>
        /// Method from Icomparable interface
        /// </summary>
        /// <param name="other">object</param>
        /// <returns>comparision</returns>
        public int CompareTo(Trip other)
        {
            if (other == null) return 1;
            if (DepartureCity.CompareTo(other.DepartureCity) != 0)
            {
                return DepartureCity.CompareTo(other.DepartureCity);
            }
            else
            {
                return DepartureTime.CompareTo(other.DepartureTime);
            }
        }
        /// <summary>
        /// bool operator that compares two trip objects
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns 1 if trip1 is greater</returns>
        static public bool operator >(Trip trip1, Trip trip2)
        {
            return trip1.CompareTo(trip2) == 1;
        }
        /// <summary>
        /// bool operator that compares two trip objects
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns -1 if trip1 is greater</returns>
        static public bool operator <(Trip trip1, Trip trip2)
        {
            return trip1.CompareTo(trip2) == -1;
        }
        /// <summary>
        /// bool operator that compares two trip objects
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns true if trip1 is greater</returns>
        static public bool operator >=(Trip trip1, Trip trip2)
        {
            return !(trip1 < trip2);
        }
        /// <summary>
        /// bool operator that compares two trip objects
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns false if trip1 is greater</returns>
        static public bool operator <=(Trip trip1, Trip trip2)
        {
            return !(trip1 > trip2);
        }
        /// <summary>
        /// Method from Iequatable interface
        /// </summary>
        /// <param name="other">other</param>
        /// <returns>true or false</returns>
        public bool Equals(Trip other)
        {
            if (other == null)
            {
                return false;
            }
            if (DepartureCity == other.DepartureCity && DepartureTime == other.DepartureTime && ArrivalCity == other.ArrivalCity && ArrivalTime == other.ArrivalTime && Weekday == other.Weekday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Override equals method
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Trip tripObj = obj as Trip;
            if (tripObj == null)
            {
                return false;
            }
            else
            {
                return Equals(tripObj);
            }
        }
    
        public override int GetHashCode()
        {
            return this.DepartureCity.GetHashCode();
        }
        /// <summary>
        /// bool operator that compares two trip objects
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns true if trip1 is equal to trip2</returns>
        public static bool operator ==(Trip trip1, Trip trip2)
        {
            if (((object)trip1) == null || ((object)trip2 == null))
            {
                return Object.Equals(trip1, trip2);
            }
            return trip1.Equals(trip2);
        }
        /// <summary>
        /// bool operator that compares two trip objects
        /// </summary>
        /// <param name="trip1">the first trip to compare</param>
        /// <param name="trip2">the second trip to compare</param>
        /// <returns>returns true if trip1 is not equal to trip2</returns>
        public static bool operator !=(Trip trip1, Trip trip2)
        {
            if (((object)trip1) == null || ((object)trip2 == null))
            {
                return !Object.Equals(trip1, trip2);
            }
            return !trip1.Equals(trip2);
        }
        /// <summary>
        /// String to format header
        /// </summary>
        /// <returns>header formatation</returns>
        public static string GetInfoForHeader()
        {
            string[] parts = { "Departure City", "Departure Time", "Arrival City", "Arrival Time", "WeekDay" };
            string template = "| {0,-20} | {1,-16} | {2,-18} | {3,-18} | {4,-9} |";
           return string.Format(template, parts);
        }
        /// <summary>
        /// Override that forms a table with trips data 
        /// </summary>
        /// <returns>a string line with formated data</returns>
        public override string ToString()
        {
            return string.Format("| {0,-20} | {1,-16} | {2,-18} | {3,-18} | {4,-9} |", DepartureCity, DepartureTime.ToString(@"hh\:mm"), ArrivalCity, ArrivalTime.ToString(@"hh\:mm"), Weekday);
        }

        

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L3.MyClasses
{
    /// <summary>
    /// Class to store data about a trip
    /// </summary>
    public class TripInfoForCommon : IComparable<TripInfoForCommon>, IEquatable<TripInfoForCommon>
    {
        public string CurrentCity { get; set; }
        public string DepartureCity { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Weekday { get; set; }
        public bool IsBusTransit { get; set; }

        /// <summary>
        /// A constructor that sets initial data 
        /// </summary>
        public TripInfoForCommon(string currentCity, string departureCity, TimeSpan departureTime, TimeSpan arrivalTime, int weekday)
        {
            CurrentCity = currentCity;
            DepartureCity = departureCity;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Weekday = weekday;
            IsBusTransit = IsTransit(currentCity, departureCity);
        }
        /// <summary>
        /// Bool that marks a bus transit if departure city is not the same as current city
        /// </summary>
        /// <param name="currentCity">current city</param>
        /// <returns>true if departure city is not the same as current city</returns>
        private bool IsTransit(string currentCity, string departureCity)
        {
            return !string.IsNullOrEmpty(currentCity) && departureCity != currentCity;
        }
        /// <summary>
        /// String to format header
        /// </summary>
        /// <returns>header formatation</returns>
        public static string HeaderForCommonTrips()
        {
            string[] parts = { "Departure City", "Departure Time", "Arrival Time", "WeekDay" };
            string template = "| {0,-20} | {1,-16} | {2,-18} | {3,-9} |";
            return string.Format(template, parts);
        }
        /// <summary>
        /// Override that forms a table with trips data 
        /// </summary>
        /// <returns>a string line with formated data</returns>
        public override string ToString()
        {
            return string.Format("| {0,-20} | {1,-16} | {2,-18} | {3,-9} |", DepartureCity, DepartureTime.ToString(@"hh\:mm"), ArrivalTime.ToString(@"hh\:mm"), Weekday);
        }
        /// <summary>
        /// Method from Iequatable interface
        /// </summary>
        /// <param name="other">other</param>
        /// <returns>true or false</returns>
        public bool Equals(TripInfoForCommon other)
        {
            if (other == null)
            {
                return false;
            }
            if (DepartureCity == other.DepartureCity && DepartureTime == other.DepartureTime && ArrivalTime == other.ArrivalTime && Weekday == other.Weekday)
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
        /// <summary>
        /// Method from Icomparable interface
        /// </summary>
        /// <param name="other">object</param>
        /// <returns>comparision</returns>
        public int CompareTo(TripInfoForCommon other)
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
        public override int GetHashCode()
        {
            return this.DepartureCity.GetHashCode();
        }
    }
    }

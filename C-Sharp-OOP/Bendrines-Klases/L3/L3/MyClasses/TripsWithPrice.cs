using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L3.MyClasses
{
	public class TripsWithPrice : IComparable<TripsWithPrice>, IEquatable<TripsWithPrice>
    {
        /// <summary>
        /// Class to store data about a trip
        /// </summary>
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public double Price { get; set; }
        public int Weekday { get; set; }

        /// <summary>
        /// A constructor that sets initial data 
        /// </summary>
        public TripsWithPrice(string departureCity,string arrivalCity, TimeSpan departureTime, TimeSpan arrivalTime, double price)
        {
            DepartureCity = departureCity;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            ArrivalCity = arrivalCity;
            Price = price;
            
        }

        /// <summary>
        /// String to format header
        /// </summary>
        /// <returns>header formatation</returns>
        public static string HeaderForCHeapestTrips()
        {
            string[] parts = { "Departure City", "Arrival City", "Departure Time", "Arrival Time", "Price" };
            string template = "| {0,-20} | {1,-16} | {2,-18} | {3,-18} | {4,-9} |";
            return string.Format(template, parts);
        }
        /// <summary>
        /// Override that forms a table with trips data 
        /// </summary>
        /// <returns>a string line with formated data</returns>
        public override string ToString()
        {
            return String.Format("| {0,-20} | {1,-16} | {2,-18} | {3,-18} | {4,-9:F2} | ", DepartureCity, ArrivalCity, DepartureTime.ToString(@"hh\:mm"), ArrivalTime.ToString(@"hh\:mm"), Price);
        }
        /// <summary>
        /// Method from Iequatable interface
        /// </summary>
        /// <param name="other">other</param>
        /// <returns>true or false</returns>
        public bool Equals(TripsWithPrice other)
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
        /// <summary>
        /// Method from Icomparable interface
        /// </summary>
        /// <param name="other">object</param>
        /// <returns>comparision</returns>
        public int CompareTo(TripsWithPrice other)
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
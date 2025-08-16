using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L3.MyClasses
{
    /// <summary>
    /// Class that stores data about prices
    /// </summary>
    /// 
    [Serializable]
    public class Price : IComparable<Price>, IEquatable<Price>
    {
        public string ArrivalCity { get; set; }
        public double PriceToGo { get; set; }

        /// <summary>
        /// COnstructor with parameters that sets data
        /// </summary>
        /// <param name="ArrivalCity">sets arrival city</param>
        /// <param name="price">sets price</param>
        /// <param name="isTransit">sets true or false if trip is transit</param>
        public Price(string ArrivalCity, double price, bool isTransit)
        {
            this.ArrivalCity = ArrivalCity;
            if (isTransit) //if trip is transit, it gets a discount
            {
                PriceToGo = price * 0.9; //10% discount
            }
            else
            {
                PriceToGo = price; //if trip is not transit, price stays without discount
            }
        }
        /// <summary>
        /// Method for interface IEquatable
        /// </summary>
        /// <param name="other"></param>
        /// <returns>equality</returns>
        public bool Equals(Price other)
        {
            if(other == null)
            {
                return false;
            }
            if (ArrivalCity == other.ArrivalCity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Method for interface IEquatable
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            Price priceObj = obj as Price;
            if(priceObj == null)
            {
                return false;
            }
            else
            {
                return Equals(priceObj);
            }
        }
        public override int GetHashCode()
        {
            return this.ArrivalCity.GetHashCode();
        }
        /// <summary>
        /// bool operator that compares two price objects
        /// </summary>
        /// <param name="first">the first price to compare</param>
        /// <param name="second">the second price to compare</param>
        /// <returns></returns>
        public static bool operator == (Price first, Price second)
        {
            if(((object)first) == null || ((object)second) == null)
            {
                return Object.Equals(first, second);
            }
            return first.Equals(second);           
        }
        /// <summary>
        /// bool operator that compares two price objects
        /// </summary>
        /// <param name="first">the first price to compare</param>
        /// <param name="second">the second price to compare</param>
        /// <returns></returns>
        public static bool operator != (Price first, Price second)
        {
            if(((object)first) == null || ((object)second) == null)
            {
                return !Object.Equals(first, second);
            }
            return !(first.Equals(second));
        }

        /// <summary>
        /// Interface IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Price other)
        {
            if (other == null) return 1;
            return PriceToGo.CompareTo(other.PriceToGo);
        }
        /// <summary>
        /// bool operator that compares two price objects
        /// </summary>
        /// <param name="first">the first price to compare</param>
        /// <param name="second">the second price to compare</param>
        /// <returns></returns>
        public static bool operator > (Price first, Price second)
        {
            return first.CompareTo(second) == 1;
        }
        /// <summary>
        /// bool operator that compares two price objects
        /// </summary>
        /// <param name="first">the first price to compare</param>
        /// <param name="second">the second price to compare</param>
        /// <returns></returns>
        public static bool operator < (Price first, Price second)
        {
            return first.CompareTo(second) == -1;
        }
        /// <summary>
        /// bool operator that compares two price objects
        /// </summary>
        /// <param name="first">the first price to compare</param>
        /// <param name="second">the second price to compare</param>
        /// <returns></returns>
        public static bool operator >= (Price first, Price second)
        {
            return !(first < second);
        }
        /// <summary>
        /// bool operator that compares two price objects
        /// </summary>
        /// <param name="first">the first price to compare</param>
        /// <param name="second">the second price to compare</param>
        /// <returns></returns>
        public static bool operator <= (Price first, Price second)
        {
            return !(first > second);
        }
        /// <summary>
        /// Method for header
        /// </summary>
        /// <returns>header</returns>
        public static string GetInfoForHeader()
        {
            string[] parts = { "Arrival City", "Price" };
            string template = "| {0,-15} | {1,14:F2} |";
            return string.Format(template, parts);
        }
        /// <summary>
        /// Method that sets table data and properties
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("| {0,-15} | {1,14:F2} |", ArrivalCity, PriceToGo);
        }
    }
}
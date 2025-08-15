using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2.MyClasses
{
    /// <summary>
    /// Class that stores data about prices
    /// </summary>
    public class Price
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
        /// Method that sets table data and properties
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ArrivalCity,-15} {PriceToGo,14:F2}";
        }
    }
}
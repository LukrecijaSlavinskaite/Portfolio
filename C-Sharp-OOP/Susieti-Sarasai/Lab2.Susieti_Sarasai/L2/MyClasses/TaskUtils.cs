using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace L2.MyClasses
{
    /// <summary>
    /// A class to calculate and do tasks
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Method that finds if a trip with wanted values is possible
        /// </summary>
        /// <param name="triplinklist">list of trips</param>
        /// <param name="currentCity"></param>
        /// <param name="arrivalCity"></param>
        /// <param name="departuretime"></param>
        /// <param name="arrivalTime"></param>
        /// <param name="weekDay"></param>
        /// <returns>a list with possible trips</returns>
        public static TripLinkList FindPossibleTrip(TripLinkList triplinklist, string currentCity, string arrivalCity, TimeSpan departuretime, TimeSpan arrivalTime, int weekDay)
        {
            TripLinkList possibleTrips = new TripLinkList();
            for (triplinklist.Begin(); triplinklist.Exist(); triplinklist.Next())
            {
                if (weekDay == triplinklist.Get().Weekday && arrivalCity == triplinklist.Get().ArrivalCity && departuretime <= triplinklist.Get().DepartureTime && arrivalTime >= triplinklist.Get().ArrivalTime)
                {
                    possibleTrips.SetFifo(triplinklist.Get());
                }
            }
            return possibleTrips;
        }
        /// <summary>
        /// Method that finds one or more cheapest trips from all possible trips by requested value
        /// </summary>
        /// <param name="possibleTrips">trips that are with requested value</param>
        /// <param name="priceLinkList">a list of prices</param>
        /// <returns>a list with cheapest trips</returns>
        public static TripLinkList FindCheapestTrips(TripLinkList possibleTrips, PriceLinkList priceLinkList)
        {
            TripLinkList cheapestTrips = new TripLinkList();
            double minPrice = double.MaxValue;
            for (possibleTrips.Begin(); possibleTrips.Exist(); possibleTrips.Next())
            {
                double finalPrice = CalculatePrice(possibleTrips.Get(), priceLinkList);
                if (finalPrice < minPrice)
                {
                    minPrice = finalPrice;
                    cheapestTrips = new TripLinkList(); 
                    cheapestTrips.SetFifo(possibleTrips.Get());
                }
                else if (finalPrice == minPrice) //can be a few trips with the same lowest price
                {
                    cheapestTrips.SetFifo(possibleTrips.Get()); 
                }
            }
            return cheapestTrips;
        }
        /// <summary>
        /// Method that calculates a final price for every trip: if a bus is transit, there is a discount
        /// </summary>
        /// <param name="trip">trip</param>
        /// <param name="priceLinkList">a list of prices</param>
        /// <returns>a final price for all trips</returns>
        public static double CalculatePrice(Trip trip, PriceLinkList priceLinkList)
        {
            double finalPrice = 0.0;
            for (priceLinkList.Begin(); priceLinkList.Exist(); priceLinkList.Next())
            {
                Price price = priceLinkList.Get();
                if (price.ArrivalCity == trip.ArrivalCity)
                {
                    if (trip.IsBusTransit) //if bus departure city is not current city
                    {
                        finalPrice = price.PriceToGo * 0.9; //transit bus gets 10% discount
                    }
                    else
                    {
                        finalPrice = price.PriceToGo; //if a bus is not transit, the price is the same without discount 
                    }
                }
            }
            return finalPrice;
        }
        /// <summary>
        /// Method finds the most visited city 
        /// </summary>
        /// <param name="tripLink">a list of trips</param>
        /// <returns>the most visited city</returns>
        public static string FindMostVisitedCityByBus(TripLinkList tripLink)
        {
            string mostVisited = "";
            for (tripLink.Begin(); tripLink.Exist(); tripLink.Next())
            {
                mostVisited = tripLink.Get().ArrivalCity;
                int count = 0;                
                for (TripLinkList tempList = tripLink; tempList.Exist(); tempList.Next())
                {
                    if (tempList.Get().ArrivalCity == mostVisited)
                    {
                        count++;
                    }
                }
            }
            return mostVisited;
        }
        /// <summary>
        /// Method that forms a list of the most visited city trips 
        /// </summary>
        /// <param name="tripLinkList">a list of trips</param>
        /// <param name="city">the most visited city</param>
        /// <returns>a list of the most visited city trips</returns>
        public static TripLinkList TripsToMostVisitedCity(TripLinkList tripLinkList, string city)
        {
            TripLinkList tripLinkList1 = new TripLinkList();
            for(tripLinkList.Begin(); tripLinkList.Exist(); tripLinkList.Next())
            {
                if (tripLinkList.Get().ArrivalCity == city)
                {
                    tripLinkList1.SetFifo(tripLinkList.Get());
                }
            }
            return tripLinkList1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace L3.MyClasses
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
        public static TripLinkList<Trip> FindPossibleTrip(TripLinkList<Trip> triplinklist, string currentCity, string arrivalCity, TimeSpan departuretime, TimeSpan arrivalTime, int weekDay)
        {
            TripLinkList<Trip> possibleTrips = new TripLinkList<Trip>();
            if (triplinklist == null)
                return possibleTrips;
            foreach (var trip in triplinklist)
            {
                if (trip.Weekday.Equals(weekDay) && trip.ArrivalCity.Equals(arrivalCity) &&
            trip.DepartureTime >= departuretime &&
            trip.ArrivalTime <= arrivalTime)
                {
                    possibleTrips.SetFifo(trip);
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
        public static TripLinkList<TripsWithPrice> FindCheapestTrips(TripLinkList<Trip> possibleTrips, TripLinkList<Price> values)
        {
            TripLinkList<TripsWithPrice> cheapestTrips = new TripLinkList<TripsWithPrice>();
            double minPrice = double.MaxValue;
            foreach (var trip in possibleTrips)
            {
                double finalPrice = CalculatePrice(trip, values);
                if (finalPrice < minPrice)
                {
                    minPrice = finalPrice;
                    cheapestTrips = new TripLinkList<TripsWithPrice>();
                    cheapestTrips.SetFifo(new TripsWithPrice(trip.DepartureCity, trip.ArrivalCity, trip.DepartureTime, trip.ArrivalTime, finalPrice));
                   
                }       
                else if (finalPrice == minPrice) //can be a few trips with the same lowest price
                {
                    cheapestTrips.SetFifo(new TripsWithPrice(trip.DepartureCity, trip.ArrivalCity, trip.DepartureTime, trip.ArrivalTime, finalPrice));
                }
            }return cheapestTrips;
            
        }
        /// <summary>
        /// Method that finds one or more cheapest trips from all possible trips by requested value
        /// </summary>
        /// <param name="trip">trips that are with requested value</param>
        /// <param name="priceLinkList">a list of prices</param>
        /// <returns>a list with cheapest trips</returns>
        public static double CalculatePrice(Trip trip, TripLinkList<Price> priceLinkList)
        {
            double finalPrice = 0.0;
            foreach (var item in priceLinkList)
            {
                Price price = item;
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
        public static string FindMostVisitedCityByBus(TripLinkList<Trip> tripLink)
        {
            string mostVisited = "";
            int maxcount = 0;
            int count = 0;
            foreach (var trip in tripLink)
            {
                mostVisited = trip.ArrivalCity;

                foreach (var temptrip in tripLink)
                    if (temptrip.ArrivalCity == mostVisited)
                    {
                        count++;
                    }

                if (count > maxcount)
                {
                    maxcount = count;
                    mostVisited = trip.ArrivalCity;
                }
              
            }  return mostVisited;
        }
        /// <summary>
        /// Method that forms a list of the most visited city trips 
        /// </summary>
        /// <param name="trips">a list of trips</param>
        /// <param name="city">the most visited city</param>
        /// <returns>a list of the most visited city trips</returns>
        public static TripLinkList<TripInfoForCommon> TripsToMostVisitedCity(TripLinkList<Trip> trips, string city)
        {
            TripLinkList<TripInfoForCommon> trips1 = new TripLinkList<TripInfoForCommon>();
            foreach(var trip in trips)
            {
                if(trip.ArrivalCity == city)
                {
                    trips1.SetFifo(new TripInfoForCommon(trip.CurrentCity,trip.DepartureCity, trip.DepartureTime, trip.ArrivalTime, trip.Weekday));

                }
            }
            return trips1;
        }

        
    }
        }


    



    

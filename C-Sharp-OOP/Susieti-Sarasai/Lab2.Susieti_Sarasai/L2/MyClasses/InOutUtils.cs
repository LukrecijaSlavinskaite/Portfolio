using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Web.Configuration;

namespace L2.MyClasses
{
    /// <summary>
    /// A class to store reading and printing methods
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Method that reads data from file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <returns>a list of trips</returns>
        public static TripLinkList ReadFromFile1(string FilePath)
        {
            TripLinkList linkList = new TripLinkList();
            string CurrentCity, DepartureCity, ArrivalCity;
            TimeSpan DepartureTime, ArrivalTime;
            int WeekDay;
            string[] lines = File.ReadAllLines(FilePath, Encoding.UTF8);
            CurrentCity = lines[0];
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                DepartureCity = parts[0].Trim();
                DepartureTime = TimeSpan.Parse(parts[1]);
                ArrivalCity = parts[2].Trim();
                ArrivalTime = TimeSpan.Parse(parts[3]);
                WeekDay = int.Parse(parts[4]);
                Trip trip = new Trip();
                trip.AddTrip(CurrentCity, DepartureCity, DepartureTime, ArrivalCity, ArrivalTime, WeekDay);
                linkList.SetFifo(trip);
            }
            return linkList;
        }
        /// <summary>
        /// Method that reads data from file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <returns>a list of prices</returns>
        public static PriceLinkList ReadFromFile2(string FilePath)
        {
            PriceLinkList linkList = new PriceLinkList();
            string ArrivalCity, line;
            double price;
            using (var file = new System.IO.StreamReader(FilePath, Encoding.UTF8))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] values = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ArrivalCity = values[0].Trim().Replace(",","");
                    price = Convert.ToDouble(values[1]);
                    Price price1 = new Price(ArrivalCity, price, false);
                    linkList.SetFifo(price1);
                }
            }
            return linkList;
        }
        /// <summary>
        /// Method that prints initial trips data to a file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <param name="list">list of trips</param>
        /// <param name="header">comment</param>
        public static void PrintTrips(string FilePath, TripLinkList list, string header)
        {
                string head = "--------------------------------------------------------------------------------\r\n" +
                              "Departure City  |  Departure Time  |  Arrival City  |  Arrival Time  |  WeekDay \r\n" +
                              "--------------------------------------------------------------------------------";
            using (var file = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8))
            {
                file.WriteLine(header);
                list.Begin();
                if (list.Exist())
                {
                    file.WriteLine(list.Get().CurrentCity);
                }
                file.WriteLine(head);
                for (list.Begin(); list.Exist(); list.Next())
                {
                    file.WriteLine(list.Get().ToString());
                }
                file.WriteLine("---------------------------------------------------------------------------------");
            }
        }
        /// <summary>
        /// Method that prints initial prices data to a file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <param name="list">list of prices</param>
        /// <param name="header">comment</param>
        public static void PrintPrices(string FilePath, PriceLinkList list, string header)
        {
            using (var file = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8))
            {
                file.WriteLine(header);
                file.WriteLine("--------------------------------------");
                file.WriteLine("   Arrival City  |  Price For A Trip  ");
                file.WriteLine("--------------------------------------");

                for (list.Begin(); list.Exist(); list.Next())
                {
                    file.WriteLine(list.Get().ToString());
                }
                file.WriteLine("--------------------------------------");
            }
        }
        /// <summary>
        /// Method that prints cheapest trips to a file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <param name="list">list of trips</param>
        /// <param name="header">comment</param>
        /// <param name="priceLinkList">list of prices</param>
        public static void PrintCheapestTrips(string FilePath, TripLinkList list, string header, PriceLinkList priceLinkList)
        {
            string head = "--------------------------------------------------------------------------------\r\n" +
                          "Departure City  |  Arrival City  |  Departure Time  |  Arrival Time  |  Price   \r\n" +
                          "--------------------------------------------------------------------------------";

       
            using (var file = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8))
            {
                file.WriteLine(header);
                file.WriteLine(head);

                for (list.Begin(); list.Exist(); list.Next())
                {
                    double finalPrice = TaskUtils.CalculatePrice(list.Get(), priceLinkList);
                    file.WriteLine("{0,-20}{1,-17}{2,-20}{3,-10}{4,10}",list.Get().DepartureCity,list.Get().ArrivalCity,list.Get().DepartureTime.ToString(@"hh\:mm"), list.Get().ArrivalTime.ToString(@"hh\:mm"), finalPrice);
                }
                file.WriteLine("--------------------------------------------------------------------------------");
            }
        }
        /// <summary>
        /// Method that prints most common trips to a file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <param name="list">list of trips</param>
        /// <param name="header">comment</param>
        public static void PrintMostCommonTrips(string FilePath, TripLinkList list, string header)
        {
            string head = "-----------------------------------------------------------------\r\n" +
                          "Departure City  |  Departure Time  |  Arrival Time  |  Weekday   \r\n" +
                          "-----------------------------------------------------------------";

            using (var file = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8))
            {
                file.WriteLine(header);
                file.WriteLine(head);

                for (list.Begin(); list.Exist(); list.Next())
                {
                    file.WriteLine("{0,-22}{1,-17}{2,-18}{3,-10}", list.Get().DepartureCity, list.Get().DepartureTime.ToString(@"hh\:mm"), list.Get().ArrivalTime.ToString(@"hh\:mm"), list.Get().Weekday);

                }
                file.WriteLine("---------------------------------------------------------------");
            }
        }
    }
}


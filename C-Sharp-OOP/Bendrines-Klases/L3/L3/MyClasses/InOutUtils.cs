using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace L3.MyClasses
{
    /// <summary>
    /// A class to store reading and printing methods
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Generic print method
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="FileName"></param>
        /// <param name="Header"></param>
        /// <param name="data"></param>
        /// <param name="comment"></param>
        public static void Print<T>(string FileName, string Header, IEnumerable<T> data, string comment) where T : IComparable<T>, IEquatable<T>
        {
            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                writer.WriteLine(comment);
                writer.WriteLine(Header);
                string dash = new string('-', Header.Length);
                writer.WriteLine(dash);
                foreach (T item in data)
                {
                    writer.WriteLine(item.ToString());
                }
                writer.WriteLine(dash);
                writer.WriteLine();
            }
        }


        /// <summary>
        /// Method that reads data from file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <returns>a list of trips</returns>
        public static TripLinkList<Trip> ReadFromFile1(Stream FilePath)
        {
            TripLinkList<Trip> trips = new TripLinkList<Trip>();
            string CurrentCity, DepartureCity, ArrivalCity;
            TimeSpan DepartureTime, ArrivalTime;
            int WeekDay;
            using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
            {
                CurrentCity = reader.ReadLine();
                string line;
                while ((line = reader.ReadLine())!= null)
                {
                    string[] parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        DepartureCity = parts[0].Trim();
                        DepartureTime = TimeSpan.Parse(parts[1]);
                        ArrivalCity = parts[2].Trim();
                        ArrivalTime = TimeSpan.Parse(parts[3]);
                        WeekDay = int.Parse(parts[4]);
                        Trip trip = new Trip(CurrentCity, DepartureCity, DepartureTime, ArrivalCity, ArrivalTime, WeekDay);
                        trips.SetFifo(trip);                    
                }           
            }
            return trips;
        }
        /// <summary>
        /// Method that reads data from file
        /// </summary>
        /// <param name="FilePath">filepath</param>
        /// <returns>a list of prices</returns>
        public static TripLinkList<Price> ReadFromFile2(Stream FilePath)
         {
            TripLinkList<Price> prices = new TripLinkList<Price>();
            string ArrivalCity, line;
            double price;
            using (var file = new System.IO.StreamReader(FilePath, Encoding.UTF8))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] values = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    ArrivalCity = values[0].Trim().Replace(";", "");
                    price = Convert.ToDouble(values[1]);
                    Price price1 = new Price(ArrivalCity, price, false);
                    prices.SetFifo(price1);
                }
            }
            return prices;
        }

        

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;

namespace L4.MyClasses
{
    /// <summary>
    /// class which implements Transport class
    /// </summary>
    public class Microbus : Transport, IComparable<Transport>
    {
        private static int TechnicalInspectionDurationMonths = 6;
        public int Seats { get; set; }
        /// <summary>
        /// Initializes a new instance of the Microbus class with specified parameters
        /// </summary>
        /// <param name="plate">The license plate of the microbus</param>
        /// <param name="manufacturer">The manufacturer of the microbus</param>
        /// <param name="model">The model of the microbus</param>
        /// <param name="year">The year the microbus was manufactured</param>
        /// <param name="technicalInspectionDate">The date of the microbus's last technical inspection</param>
        /// <param name="fuel">The type of fuel</param>
        /// <param name="fuelConsumption">The fuel consumption of the microbus</param>
        /// <param name="seats">seats count in microbus</param>
        public Microbus(string plate, string manufacturer, string model, DateTime year, DateTime technicalInspectionDate, string fuel, double fuelConsumption, int seats) : base(plate, manufacturer, model, year, technicalInspectionDate, fuel, fuelConsumption)
        {
            Seats = seats;
        }
        /// <summary>
        /// Initializes a new instance of the Microbus class from a data line
        /// </summary>
        /// <param name="data">Data line with microbus information</param>
        public Microbus(string data) : base(data)
        {
            SetData(data);
        }
        /// <summary>
        /// Sets the microbus data from a given line
        /// </summary>
        /// <param name="line">Data line with microbus details</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Seats = int.Parse(values[8]);
        }
        /// <summary>
        /// Implementation of abstract method of Transport class
        /// </summary>
        /// <returns>true if technical inspection is expired</returns>
        public override bool isTechnicalInspectionExpired()
        {
            return TechnicalInspectionDate.AddMonths(TechnicalInspectionDurationMonths) < DateTime.Now;
        }
        /// <summary>
        /// Returns a string
        /// </summary>
        /// <returns>A string with microbus's data</returns>
        public override string ToString()
        {
            return base.ToString() + String.Format("|{0,-15}|", Seats);
        }
        /// <summary>
        /// Returns a header for a table
        /// </summary>
        /// <returns>Formatted header</returns>
        public static string GetHeader()
        {
            return String.Format("{0,-10}|{1,-10}|{2,-15}|{3,-10}|{4,-20}|{5,-25}|{6,-10}|{7,-18:F2}|{8,-32}", "Type", "Plate", "Manufacturer",
                "Model", "Year of manufacture", "Technical Inspection Date", "Fuel", "Fuel Consumption", "Is Technical Inspection Expired");
        }
        /// <summary>
        /// Determines whether the object is equal to the current microbus
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>True if objects are equal</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Microbus);
        }
        /// <summary>
        /// Determines whether the microbus is equal to the current microbus
        /// </summary>
        /// <param name="microbus">The microbus to compare with</param>
        /// <returns>True if microbusses are equal</returns>
        public bool Equals(Microbus microbus)
        {
            return base.Equals(microbus);
        }
        /// <summary>
        /// Returns a hash code 
        /// </summary>
        /// <returns>Hash code based on the microbusses plate</returns>
        public override int GetHashCode()
        {
            return Plate.GetHashCode();
        }
        /// <summary>
        /// Compares the current microbus with another
        /// </summary>
        /// <param name="other">other microbus object</param>
        /// <returns>0 if microbusses are equal, 1 if greater</returns>
        public override int CompareTo(Transport other)
        {
            if (Seats.CompareTo((other as Microbus).Seats) == 0)
            {
                return 0;
            }
            else if (Seats.CompareTo((other as Microbus).Seats) > 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
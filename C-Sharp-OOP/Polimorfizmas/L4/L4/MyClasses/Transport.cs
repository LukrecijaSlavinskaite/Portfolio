using System;

namespace L4.MyClasses
{
    public abstract class Transport : IComparable<Transport>, IEquatable<Transport>
    {
        public string Plate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public DateTime TechnicalInspectionDate { get; set; }
        public string Fuel { get; set; }
        public double FuelConsumption { get; set; }
        public DateTime NextTechnicalInspectionDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the Transport class with specified parameters
        /// </summary>
        /// <param name="plate">The license plate of the transport</param>
        /// <param name="manufacturer">The manufacturer of the transport</param>
        /// <param name="model">The model of the transport</param>
        /// <param name="year">The year the transport was manufactured</param>
        /// <param name="technicalInspectionDate">The date of the transport's last technical inspection</param>
        /// <param name="fuel">The type of fuel</param>
        /// <param name="fuelConsumption">The fuel consumption of the transport</param>
        public Transport(string plate, string manufacturer, string model, DateTime year, DateTime technicalInspectionDate, string fuel, double fuelConsumption)
        {
            Plate = plate;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            TechnicalInspectionDate = technicalInspectionDate;
            Fuel = fuel;
            FuelConsumption = fuelConsumption;
        }
        /// <summary>
        /// Initializes a new instance of the Transport class from a data line
        /// </summary>
        /// <param name="data">Data line with transport information</param>
        public Transport(string data)
        {
            SetData(data);
        }
        /// <summary>
        /// Sets the transport data from a given line
        /// </summary>
        /// <param name="line">Data line with transport details</param>
        public virtual void SetData(string line)
        {
            string[] values = line.Split(',');
            Plate = values[1];
            Manufacturer = values[2];
            Model = values[3];
            Year = DateTime.Parse(values[4]);
            TechnicalInspectionDate = DateTime.Parse(values[5]);
            Fuel = values[6];
            FuelConsumption = double.Parse(values[7]);
        }
        /// <summary>
        /// Determines whether the transport is equal to the current transport
        /// </summary>
        /// <param name="transport">The ctransportar to compare with</param>
        /// <returns>True if transports are equal</returns>
        public bool Equals(Transport transport)
        {
            if (Object.ReferenceEquals(transport, null))
            {
                return false;
            }
            if (this.GetType() != transport.GetType())
            {
                return false;
            }
            return (Plate == transport.Plate);
        }
        /// <summary>
        /// Returns a hash code 
        /// </summary>
        /// <returns>Hash code based on the transport's plate</returns>
        public override int GetHashCode()
        {
            return Plate.GetHashCode();
        }
        /// <summary>
        /// Returns a header for a table
        /// </summary>
        /// <returns>Formatted header</returns>
        public static string GetHeader()
        {
            return String.Format("{0,-10}|{1,-12}|{2,-7}|{3,-6}|{4,-10}", "Type","Manufacturer","Model", "Plate", "Age");
        }
        /// <summary>
        /// Returns a string
        /// </summary>
        /// <returns>A string with transport's data</returns>
        public virtual string ToString()
        {
            return String.Format("{0,-10}|{1,-10}|{2,-15}|{3,-10}|{4,-20}|{5,-25}|{6,-10}|{7,-18:F2}|{8,-32}",
                GetType().Name,
                Plate, Manufacturer, Model, Year.ToString("yyyy-MM"),
                TechnicalInspectionDate.ToString("yyyy-MM-dd"), Fuel, FuelConsumption, isTechnicalInspectionExpired() ? '+' : ' ');
        }
        // <summary>
        /// Compares the current transport with another
        /// </summary>
        /// <param name="other">other transport object</param>
        /// <returns>0 if transport are equal, 1 if greater</returns>
        public virtual int CompareTo(Transport other)
        {
            if (other == null)
            {
                throw new Exception("Objektas nėra transport");
            }
            if (Manufacturer.CompareTo(other.Manufacturer) == 0)
            {
                return (Model.CompareTo(other.Model));
            }
            else
            {
                return Manufacturer.CompareTo(other.Manufacturer);
            }
        }
        /// <summary>
        /// Abstract method for technical inspection duration
        /// </summary>
        /// <returns>true if technical inspection is expired</returns>
        abstract public bool isTechnicalInspectionExpired();
    }
}
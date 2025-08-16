using System;

namespace L4.MyClasses
{
    /// <summary>
    /// class which implements Transport class
    /// </summary>
	public class Truck : Transport
	{
        private static int TechnicalInspectionDurationMonths = 12;
        public int Capacity { get; set; }
        /// <summary>
        /// Initializes a new instance of the Truck class with specified parameters
        /// </summary>
        /// <param name="plate">The license plate of the truck</param>
        /// <param name="manufacturer">The manufacturer of the truck</param>
        /// <param name="model">The model of the truck</param>
        /// <param name="year">The year the truck was manufactured</param>
        /// <param name="technicalInspectionDate">The date of the truck's last technical inspection</param>
        /// <param name="fuel">The type of fuel</param>
        /// <param name="fuelConsumption">The fuel consumption of the truck</param>
        /// <param name="capacity">capacity of a truck</param>
        public Truck(string plate, string manufacturer, string model, DateTime year, DateTime technicalInspectionDate, string fuel, double fuelConsumption, int capacity) : base(plate, manufacturer, model, year, technicalInspectionDate, fuel, fuelConsumption)
        {
            Capacity = capacity;
        }
        /// <summary>
        /// Initializes a new instance of the Truck class from a data line
        /// </summary>
        /// <param name="data">Data line with trcuk information</param>
        public Truck(string data) : base(data)
        {
            SetData(data);
        }
        /// <summary>
        /// Sets the trcuk data from a given line
        /// </summary>
        /// <param name="line">Data line with trcuk details</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Capacity = int.Parse(values[8]);
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
        /// <returns>A string with truck's data</returns>
        public override string ToString()
        {
            return base.ToString() + String.Format("|{0,-15}|", Capacity);
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
        /// Determines whether the object is equal to the current trcuk
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>True if objects are equal</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Truck);
        }
        /// <summary>
        /// Determines whether the truck is equal to the current truck
        /// </summary>
        /// <param name="truck">The truck to compare with</param>
        /// <returns>True if truck's are equal</returns>
        public bool Equals(Truck truck)
        {
            return base.Equals(truck);
        }
        /// <summary>
        /// Returns a hash code 
        /// </summary>
        /// <returns>Hash code based on the trcuk's plate</returns>
        public override int GetHashCode()
        {
            return Plate.GetHashCode();
        }
        /// <summary>
        /// Compares the current trcuk with another
        /// </summary>
        /// <param name="other">other trcuk object</param>
        /// <returns>0 if trcuks are equal, 1 if greater</returns>
        public override int CompareTo(Transport other)
        {
            if (other == null)
            {
                throw new Exception("Object is not type transport");
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
    }
}
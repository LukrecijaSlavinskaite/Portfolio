using System;

namespace L4.MyClasses
{
    /// <summary>
    /// class which implements Transport class
    /// </summary>
	public class Car : Transport
	{
		private static int TechnicalInspectionDurationMonths = 24;
		public int Mileage { get; set; }

        /// <summary>
        /// Initializes a new instance of the Car class with specified parameters
        /// </summary>
        /// <param name="plate">The license plate of the car</param>
        /// <param name="manufacturer">The manufacturer of the car</param>
        /// <param name="model">The model of the car</param>
        /// <param name="year">The year the car was manufactured</param>
        /// <param name="technicalInspectionDate">The date of the car's last technical inspection</param>
        /// <param name="fuel">The type of fuel</param>
        /// <param name="fuelConsumption">The fuel consumption of the car</param>
        /// <param name="mileage">The mileage of the car</param>
        public Car(string plate, string manufacturer, string model, DateTime year, DateTime technicalInspectionDate, string fuel, double fuelConsumption, int mileage) : base(plate, manufacturer, model, year, technicalInspectionDate, fuel, fuelConsumption)
		{
			Mileage = mileage;
		}
        /// <summary>
        /// Initializes a new instance of the Car class from a data line
        /// </summary>
        /// <param name="data">Data line with car information</param>
		public Car(string data) : base(data)
		{
			SetData(data);
		}
        /// <summary>
        /// Sets the car data from a given line
        /// </summary>
        /// <param name="line">Data line with car details</param>
        public override void SetData(string line)
        {
			base.SetData(line);
			string[] values = line.Split(',');
			Mileage = int.Parse(values[8]);
        }
        /// <summary>
        /// Implementation of abstract method of Transport class
        /// </summary>
        /// <returns>True if technical inspection is expired</returns>
        public override bool isTechnicalInspectionExpired()
        {
			return TechnicalInspectionDate.AddMonths(TechnicalInspectionDurationMonths) < DateTime.Now;
        }
        /// <summary>
        /// Returns a string
        /// </summary>
        /// <returns>A string with car's data</returns>
        public override string ToString()
        {
            return base.ToString() + String.Format("|{0,-15}|", Mileage);
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
        /// Determines whether the object is equal to the current car
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>True if objects are equal</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Car);
        }
        /// <summary>
        /// Determines whether the car is equal to the current car
        /// </summary>
        /// <param name="car">The car to compare with</param>
        /// <returns>True if cars are equal</returns>
        public bool Equals(Car car)
        {
            return base.Equals(car);
        }
        /// <summary>
        /// Returns a hash code 
        /// </summary>
        /// <returns>Hash code based on the car's plate</returns>
        public override int GetHashCode()
        {
            return Plate.GetHashCode();
        }
        /// <summary>
        /// Compares the current car with another
        /// </summary>
        /// <param name="other">other car object</param>
        /// <returns>0 if cars are equal, 1 if greater</returns>
        public override int CompareTo(Transport other)
        {
            if(Mileage.CompareTo((other as Car).Mileage)==0)
            {
                return 0;
            }
            else if(Mileage.CompareTo((other as Car).Mileage)<0)
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
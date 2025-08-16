using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;

namespace L4.MyClasses
{
    /// <summary>
    /// Container class
    /// </summary>
	public class Corporate
	{
        /// <summary>
        /// city ​​where the corporate is located
        /// </summary>
		public string City { get; private set; }
        /// <summary>
        /// address of a corporate
        /// </summary>
		public string Address { get; private set; }
        /// <summary>
        /// email address of a corporate
        /// </summary>
		public string Email { get; private set; }

		private List<Transport> transports = new List<Transport>();
		public int Count { get; private set; }
        /// <summary>
        /// Initializes a new instance of the Corporate
        /// </summary>
        /// <param name="city">city ​​where the corporate is located</param>
        /// <param name="adress">address of a corporate</param>
        /// <param name="email">email address of a corporate</param>
		public Corporate(string city = "", string adress = "", string email ="")
		{
			City = city;
			Address = adress;
			Email = email;
			transports = new List<Transport>();
        }
        /// <summary>
        /// Add transport into collection
        /// </summary>
        /// <param name="a">transport to add</param>
		public void AddTransport(Transport a)
		{
			transports.Add(a);
			Count++;
		}
        /// <summary>
        /// Get transport from a colection by index
        /// </summary>
        /// <param name="index">index of transport object</param>
        /// <returns>transport object</returns>
		public Transport GetTransport(int index)
		{
			return transports[index];
		}
        /// <summary>
        /// Method for setting data into a string
        /// </summary>
        /// <returns>a string line</returns>
        public override string ToString()
        {
			return string.Format("{0,-10}|{1,-10}|{2,-25}|", City, Address, Email);
        }
        /// <summary>
        /// Returns a header for a table
        /// </summary>
        /// <returns>Formatted header</returns>
        public static string GetHeader()
		{ 
            return string.Format("{0,-10}|{1,-10}|{2,-25}|", "City", "Address", "Email");
        }
        /// <summary>
        /// Sort transports according to method CompareTo()
        /// </summary>
        public void SortTransports()
		{
            for (int i = 0; i < Count - 1; i++)
            {
                int m = i;
                for (int j = i + 1; j < Count; j++)
                    if (transports[j].CompareTo(transports[m]) <0)
                        m = j;
                Transport a = transports[i];
                transports[i] = transports[m];
                transports[m] = a;
            }
        }
        /// <summary>
        /// Join to collections of transports without making a data copy
        /// </summary>
        /// <param name="a">>First collection</param>
        /// <param name="b">>Second collection</param>
        /// <returns>collections</returns>
        public static Corporate operator +(Corporate a, Corporate b)
        {
            Corporate c = new Corporate(a.City);
            for (int i = 0; i < a.Count; i++)
                c.AddTransport(a.transports[i]);
            for (int i = 0; i < b.Count; i++)
                c.AddTransport(b.transports[i]);
            return c;
        }
    }
}
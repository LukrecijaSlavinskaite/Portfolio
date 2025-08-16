using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab5.MyClasses
{
    /// <summary>
    /// Class to store visit data
    /// </summary>
	public class Visit
	{
		public TimeSpan VisitTime { get; set; } //time of a visit
		public string PCIpAddress { get; set; } //address of PC IP
		public string PagePath { get; set; } //page path
        /// <summary>
		/// Constructor without parameters
		/// </summary>
		public Visit()
		{

		}
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="visitTime">time of a visit</param>
        /// <param name="pCIpAddress">address of PC IP</param>
        /// <param name="pagePath">page path</param>
        public Visit(TimeSpan visitTime, string pCIpAddress, string pagePath)
        {
            VisitTime = visitTime;
            PCIpAddress = pCIpAddress;
            PagePath = pagePath;
        }
        /// <summary>
        /// Returns a header for a table
        /// </summary>
        /// <returns>a header</returns>
		public static string GetHeader()
		{
			return string.Format("{0,-10}|{1,-15}|{2,-7}", "Visit Time", "PC IP Address", "Page Path");
        }
        /// <summary>
        /// Returns a string
        /// </summary>
        /// <returns>>A string with server's data</returns>
        public override string ToString()
        {
            return string.Format("{0,-10}|{1,-15}|{2,-7}", VisitTime.ToString(@"hh\:mm"), PCIpAddress, PagePath);
        }
    }
}
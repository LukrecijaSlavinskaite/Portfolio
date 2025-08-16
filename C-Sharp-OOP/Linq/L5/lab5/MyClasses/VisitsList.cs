using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace lab5.MyClasses
{
    /// <summary>
    /// class to store lists of visits
    /// </summary>
    public class VisitsList : Visit, IEnumerable<Visit>
    {
        public DateTime VisitDate { get; set; } //date of a visit
        public string ServerIpAddress { get; set; } //IP address of a server

        private List<Visit> visitList; //lit of visists
        /// <summary>
        /// Constructor 
        /// </summary>
        public VisitsList()
        {
            visitList = new List<Visit>();
        }
        /// <summary>
        /// Method to add data
        /// </summary>
        /// <param name="visit"> visit list</param>
        public void Add(Visit visit)
        {
            visitList.Add(visit);
        }
        public IEnumerator<Visit> GetEnumerator()
        {
            return visitList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Returns a string
        /// </summary>
        /// <returns>>A string with visits's data</returns>
        public override string ToString()
        {
            return string.Format("{0,-10}{1,-10}", VisitDate.Date, ServerIpAddress);
        }
    }
}
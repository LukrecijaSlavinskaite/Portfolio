using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2.MyClasses
{
    /// <summary>
    /// A single node class  
    /// </summary>
    sealed class TripNode
    {
        public Trip Data { get; set; } //data of a node
        public TripNode Link { get; set; } //reference to the next node in the list
        /// <summary>
        /// Constructor with parameters to initialize the node
        /// </summary>
        /// <param name="value">adds data to a price object</param>
        /// <param name="address">adds a link to a price object</param>
        public TripNode(Trip value, TripNode address)
        {
            Data = value;
            Link = address;
        }
    }
}
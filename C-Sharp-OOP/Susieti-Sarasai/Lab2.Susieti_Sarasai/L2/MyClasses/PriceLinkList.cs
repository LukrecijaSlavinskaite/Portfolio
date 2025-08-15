using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2.MyClasses
{
    /// <summary>
    /// List class to manage a list of prices
    /// </summary>
    public sealed class PriceLinkList
    {
        private PriceNode tail; //tail of a list
        private PriceNode headFifo; // head of the list
        private PriceNode d; //current node pointer

        /// <summary>
        /// Constructor initializing the list with null values
        /// </summary>
        public PriceLinkList()
        {
            this.tail = null;
            headFifo = null;
            this.d = null;
        }
        /// <summary>
        /// Method that adds a new price to the list
        /// </summary>
        /// <param name="price">price of a trip</param>
        public void SetFifo(Price price)
        {
            var dd = new PriceNode(price, null); //new Node
            if (headFifo == null) //if the list is empty
            {
                headFifo = dd; //first element is a head
                tail = dd; //last element is the samehead element
            }
            else
            {
                tail.Link = dd; //setting current tail's linl to the new node
                tail = dd;
            }
        }
        /// <summary>
        /// connection method to set the variable d to the head of the list
        /// </summary>
        public void Begin()
        {
            d = headFifo;
        }
        /// <summary>
        /// connection method to move the iterator to the next element
        /// </summary>
        public void Next()
        {
            d = d.Link;
        }
        /// <summary>
        /// connection method to check if the are more element in the list
        /// </summary>
        /// <returns></returns>
        public bool Exist()
        {
            return d != null;
        }
        /// <summary>
        /// Method to get the element 
        /// </summary>
        /// <returns>data of a current element</returns>
        public Price Get()
        {
            return d.Data;
        }
    }
}
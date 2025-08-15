using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2.MyClasses
{
    /// <summary>
    /// List class to manage a list of trips
    /// </summary>
    public sealed class TripLinkList
    {
        private TripNode tail; //tail of a list
        private TripNode headFifo; //head of a list
        private TripNode d; //current node pointer
        /// <summary>
        /// Constructor initializing the list with null values
        /// </summary>
        public TripLinkList()
        {
            this.tail = null;
            headFifo = null;
            this.d = null;
        }
        /// <summary>
        /// Method that adds a new trip data to the list
        /// </summary>
        /// <param name="trip"></param>
        public void SetFifo(Trip trip)
        {
            var dd = new TripNode(trip, null);
            if (headFifo == null)
            {
                headFifo = dd;
                tail = dd;
            }
            else
            {
                tail.Link = dd;
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
            if (d != null)
            {
                d = d.Link;
            }
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
        public Trip Get()
        {
            if (d != null)
            {
                return d.Data;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Selection sorting method
        /// </summary>
        public void Sort()
        {
            for (TripNode d1 = headFifo; d1 != null; d1 = d1.Link)
            {
                TripNode minv = d1;
                for (TripNode d2 = d1.Link; d2 != null; d2 = d2.Link)
                {
                    if (d2.Data < minv.Data)
                    {
                        minv = d2;
                    }
                }
                Trip trip = d1.Data;
                d1.Data = minv.Data;
                minv.Data = trip;
            }
        }
        /// <summary>
        /// Method to remove current 'd'
        /// </summary>
        public void Remove()
        {
            if (d == headFifo) //if `d` is the first node, update headFifo
            {
                headFifo = headFifo.Link;
                if (headFifo == null) //if list is empty
                {
                    tail = null; //update tail
                }
                d.Link = null; //remove reference
                d = headFifo; //move 'd' to the new head
                return;
            }
            //finding the node before 'd'
            TripNode current = headFifo;
            while (current.Link != d)
            {
                current = current.Link;
            }
            if (d.Link == null) //if 'd' is in the last node
            {
                current.Link = null; //remove reference
                tail = current; //update tail
                d = null; //remove 'd'
                return;
            }
            TripNode s = d.Link; //copy the next node's data into 'd' and remove the next node
            d.Data = s.Data;
            d.Link = s.Link;
            s = null; //remove reference

            if (d.Link == null) //if 'd' is in the last node
            {
                tail = d; //update tail
            }
        }
    }
}
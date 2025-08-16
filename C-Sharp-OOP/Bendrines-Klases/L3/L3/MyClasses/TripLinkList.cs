using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace L3.MyClasses
{
    /// <summary>
    /// Generic list class to manage a list of trips
    /// </summary>
    [Serializable]
	public sealed class TripLinkList<type> : IEnumerable<type> where type : IComparable<type>, IEquatable<type>
	{
		[Serializable]
		private sealed class TripNode<type> where type : IComparable<type>, IEquatable<type>
		{
			public type Data { get; set; }
			public TripNode<type> Link { get; set; }
			public TripNode(type value, TripNode<type> link)
			{
				Data = value;
				Link = link;
			}
		}
		private TripNode<type> head; //head of a list
        private TripNode<type> tail; //tail of a list
        private TripNode<type> d; //current node pointer
        public TripLinkList()
		{
			this.head = null;
			this.tail = null;
			this.d = null;
		}
        /// <summary>
        /// Method to get the element 
        /// </summary>
        /// <returns>data of a current element</returns>
        public type Get()
		{
			if (d != null)
			{
				return d.Data;
			}
			else
			{
				return default(type);
			}
		}
        /// <summary>
        /// connection method to set the variable d to the head of the list
        /// </summary>
        public void Begin()
		{
			d = head;
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
		public void SetLifo(type novel)
		{
			head = new TripNode<type>(novel, head);
		}
        /// <summary>
        /// Method that adds a new trip data to the list
        /// </summary>
        /// <param name="novel"></param>
        public void SetFifo(type novel)
		{
			var dd = new TripNode<type>(novel, null);
			if (head != null)
			{
				tail.Link = dd;
				tail = dd;
			}
			else
			{
				head = dd;
				tail = dd;
			}
		}
        /// <summary>
        /// generic sorting method
        /// </summary>
        public void Sort()
		{
			for (TripNode<type> d1 = head; d1 != null; d1 = d1.Link)
			{
				TripNode<type> maxv = d1;
				for (TripNode<type> d2 = d1; d2 != null; d2 = d2.Link)
				{
					if (d2.Data.CompareTo(maxv.Data) < 0)
					{
						maxv = d2;
					}
				}
				type trip = d1.Data;
				d1.Data = maxv.Data;
				maxv.Data = trip;
			}
		}
		public IEnumerator<type> GetEnumerator()
		{
			for (TripNode<type> dd = head; dd != null; dd = dd.Link)
			{
				yield return dd.Data;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
        /// <summary>
        /// Generic method to remove 
        /// </summary>

        public void Remove(type item)
        {
            TripNode<type> current = head;
            TripNode<type> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    if (previous == null)
                    {
                        head = current.Link;
                        if (head == null)
                            tail = null;
                    }
                    else
                    {
                        previous.Link = current.Link;
                        if (current.Link == null)
                        {
                            tail = previous;
                        }
                    }
                    if (d == current)
                    {
                        d = current.Link;
                    }
                    break;
                }
                previous = current;
                current = current.Link;
            }
        }

    }
	}

	
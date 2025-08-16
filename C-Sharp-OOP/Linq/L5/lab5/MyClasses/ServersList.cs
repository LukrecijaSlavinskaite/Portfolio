using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab5.MyClasses
{
    /// <summary>
    /// Class to store Servers list
    /// </summary>
    public class ServersList : ServerInfo, IEnumerable<ServerInfo>
    {
        private List<ServerInfo> serverList; //lit of servers
        /// <summary>
        /// Constructor 
        /// </summary>
        public ServersList()
        {
            serverList = new List<ServerInfo>();
        }
        /// <summary>
        /// Method to add data
        /// </summary>
        /// <param name="server"> servers list</param>
        public void Add(ServerInfo server)
        {
            serverList.Add(server);
        }
        /// <summary>
        /// Method to add one server
        /// </summary>
        /// <param name="servers">list of servers</param>
        public void Add(ServersList servers)
        {
            foreach (var server in servers)
            {
                serverList.Add(server);
            }
        }
        public IEnumerator<ServerInfo> GetEnumerator()
        {
            return serverList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
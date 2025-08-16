using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace lab5.MyClasses
{
	/// <summary>
	/// Class to store server info
	/// </summary>
	public class ServerInfo
	{
		public string ServerIpAddress { get; set; } //IP address of a server
		public string ServerName { get; set; } //Name of a server
		/// <summary>
		/// Constructor without parameters
		/// </summary>
		public ServerInfo()
		{

		}
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="serverIpAddress">IP address of a server</param>
        /// <param name="serverName">Name of a server</param>
        public ServerInfo(string serverIpAddress, string serverName)
        {
            ServerIpAddress = serverIpAddress;
            ServerName = serverName;
        }
        /// <summary>
        /// Returns a header for a table
        /// </summary>
        /// <returns>a header</returns>
        public static string GetHeader()
		{
			return string.Format("{0,-15}|{1,-10}", "Server IP Address", "Server Name");
		}
        /// <summary>
        /// Returns a string
        /// </summary>
        /// <returns>>A string with server's data</returns>
        public override string ToString()
        {
            return string.Format("{0,-17}|{1,-10}", ServerIpAddress, ServerName);
        }
    }
}
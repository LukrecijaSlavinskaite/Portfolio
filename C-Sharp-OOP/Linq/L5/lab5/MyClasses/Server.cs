using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace lab5.MyClasses
{
	public class Server
	{
		public string ServerIpAddress { get; set; }
		public string ServerName { get; set; }

		public Server()
		{

		}

        public Server(string serverIpAddress, string serverName)
        {
            ServerIpAddress = serverIpAddress;
            ServerName = serverName;
        }
    }
}
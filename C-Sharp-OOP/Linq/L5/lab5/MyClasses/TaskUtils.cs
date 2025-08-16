using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab5.MyClasses
{
	public class TaskUtils
    {
        public WebForm1 WebForm1
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Method that finds a server IP by website name
        /// </summary>
        /// <param name="webname">website name</param>
        /// <param name="servers">list of servers</param>
        /// <returns>a server IP</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string FindServerIp(string webname, ServersList servers)
		{
			if(servers == null)
			{
				throw new ArgumentNullException("Servers yra NULL");
			}
            return servers.FirstOrDefault(server => server.ServerName == webname)?.ServerIpAddress;
		}
        /// <summary>
        /// Method that finds a page path by web name using server IP
        /// </summary>
        /// <param name="visits">list of visits</param>
        /// <param name="serverioIp">server IP address</param>
        /// <param name="name">name of a website</param>
        /// <returns>a page path</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<string> FindPagePathByWebName(List<VisitsList> visits, string serverioIp, string name)
		{
			if(visits==null || serverioIp == null || name == null)
			{
				throw new ArgumentNullException("Visits yra NULL arba serverio IP arba vardas yra null");
			}
			return  visits.Where(first => first.ServerIpAddress == serverioIp)
				.SelectMany(first => first).OrderBy(v=> v.PCIpAddress).ThenBy(v=>v.VisitTime).Select(page =>name+ page.PagePath).Distinct().ToList();
            var pcipaddress = visits.Where(first => first.ServerIpAddress == serverioIp).Select(pc => pc.PCIpAddress).Distinct().ToList();
            var kreipimosilaikas = visits.Where(first => first.ServerIpAddress == serverioIp).Select(pc => pc.VisitTime).Distinct().ToList();

            
        }
        /// <summary>
        /// Method that finds PC IP who called most at selected date
        /// </summary>
        /// <param name="dateTime">date time of a call</param>
        /// <param name="count">count of call times</param>
        /// <param name="visits">list of visits</param>
        /// <returns>count and PC IP</returns>
		public static List<string> FindPcIpForCallingMostAtThisDay(DateTime dateTime, out int count, List<VisitsList> visits)
		{
            if(visits == null)
            {
                throw new ArgumentNullException("Visits yra NULL");
            }
			var day = visits.Where(v => v.VisitDate.Date == dateTime.Date).SelectMany(v => v).GroupBy(v => v.PCIpAddress).ToList();
            if (day.Count == 0)
            {
                count = 0;
                return new List<string>();
            }
            int max = day.Max(g => g.Count());
			count = max;

			return day.Where(g => g.Count() == max).Select(g => g.Key).ToList();
		}

	}
}
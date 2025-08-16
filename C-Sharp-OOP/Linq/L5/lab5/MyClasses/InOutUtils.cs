using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace lab5.MyClasses
{
	public static class InOutUtils
    {
        public static WebForm1 WebForm1
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Reads visits data
        /// </summary>
        /// <param name="FileName">Name of file</param>
        /// <returns>A list of visits</returns>
        /// <exception cref="Exception"></exception>
        public static VisitsList ReadVisitsData(string FileName)
		{
			VisitsList visits = new VisitsList();
			try
			{
				string[] lines = File.ReadAllLines(FileName);
				if (lines.Length == 0)
				{
					throw new FormatException($"failas {FileName} tuščias.");
				}
				string[] parts = lines[0].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
				try
				{
					DateTime visitDate = DateTime.Parse(parts[0]);
					string serverIp = parts[1];
					visits.VisitDate = visitDate;
					visits.ServerIpAddress = serverIp;
				}
				catch (Exception ex)
				{
					throw new FormatException($"Klaida faile {FileName} pirmoje eilutėje: {ex.Message}");
				}

				int linenumber = 2;
				foreach (string line in lines.Skip(1))
				{
					try
					{
						string[] partsother = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
						TimeSpan visitTime = TimeSpan.Parse(partsother[0]);
						string PcIp = partsother[1];
						string pagePath = partsother[2];
						visits.Add(new Visit(visitTime, PcIp, pagePath));
					}

					catch (Exception ex)
					{
						throw new FormatException($"Faile {FileName} bloga eilutė {linenumber} : {ex.Message}");
					}
					linenumber++;
				}
				return visits;
			}
			catch (Exception)
			{
				throw;
			}
		}
        /// <summary>
        /// Reads servers data
        /// </summary>
        /// <param name="FileName">name of a file</param>
        /// <returns>Servers list</returns>
		/// <exception cref="Exception"></exception>
        public static ServersList ReadServersData(string FileName)
		{
			ServersList servers = new ServersList();
			try
			{
				string[] lines = File.ReadAllLines(FileName);
				if (lines.Length == 0)
				{
					throw new FormatException($"failas {FileName} tuščias.");
				}
				int linenumber = 1;
				foreach (string line in lines)
				{
					try
					{
						string[] parts = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
						string serverIp = parts[0];
						string name = parts[1];
						servers.Add(new ServerInfo(serverIp, name));
					}
					catch (Exception ex)
					{
						throw new FormatException($"Faile {FileName} bloga eilutė {linenumber} : {ex.Message}");
					}
					linenumber++;
				}
				return servers;
			}
			catch (Exception)
			{
				throw;
			}
		}
		/// <summary>
		/// Prints a table to a file
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileName">name of a results file</param>
		/// <param name="header">header of a table</param>
		/// <param name="data">data to print</param>
		/// <param name="comment">table comment</param>
		/// <exception cref="Exception"></exception>
		public static void PrintTableToFile<T>(string fileName, string header, IEnumerable<T> data, string comment)
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(fileName, true))
				{
					if (!string.IsNullOrWhiteSpace(comment))
					{
						writer.WriteLine(comment);
					}
					if (!string.IsNullOrWhiteSpace(header))
					{
						writer.WriteLine(header);
						writer.WriteLine(new string('-', header.Length));
					}
					foreach (var item in data)
					{
						writer.WriteLine(item.ToString());
					}
					if (!string.IsNullOrWhiteSpace(header))
					{
						writer.WriteLine(new string('-', header.Length));
					}
					writer.WriteLine();
				}
			}
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
	}
    }
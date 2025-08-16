using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L4.MyClasses
{
    public static class InOutUtils
    {

        /// <summary>
        /// Read data of branches
        /// </summary>
        /// <param name="file">Directory name<</param>
        /// <param name="corporates">Branches storing data</param>
        /// <param name="number">Number of branches</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static void ReadData(string file, List<Corporate> corporates, ref int number)
        {
            if (corporates == null)
            {
                throw new ArgumentNullException("corporates yra null");
            }
            string[] filePaths = null;
            try
            {
                filePaths = Directory.GetFiles(file, "*.txt");
                if (filePaths.Length == 0)
                {
                    throw new Exception("Error: no .txt files found in folder");
                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Error: Folder not found");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            foreach (string path in filePaths)
            {
                try
                {
                    ReadTransportData(path, corporates, ref number);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// Read data of branch
        /// </summary>
        /// <param name="file">Directory name</param>
        /// <param name="corporates">Branches storing data</param>
        /// <param name="number">Number of branches</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        private static void ReadTransportData(string file, List<Corporate> corporates, ref int number)
        {
            if (corporates == null)
            {
                throw new ArgumentNullException("corporates yra null");
            }
            try
            {
                using (StreamReader reader = new StreamReader(@file, Encoding.GetEncoding(1257)))
                {
                    string line = reader.ReadLine();
                    string address = reader.ReadLine();
                    string email = reader.ReadLine();

                    Corporate corporate = TaskUtils.GetBranchByTown(corporates, ref number, line, address, email);
                    if (corporate == null)
                    {
                        throw new Exception("Nepavyko sukurti įmonės.");
                    }
                    while (null != (line = reader.ReadLine()))
                    {
                        try
                        {
                            switch (line[0])
                            {
                                case 'C':
                                    corporate.AddTransport(new Car(line));
                                    break;
                                case 'T':
                                    corporate.AddTransport(new Truck(line));
                                    break;
                                case 'M':
                                    corporate.AddTransport(new Microbus(line));
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            throw new Exception(ex.Message);
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///  Display data from branches on screen
        /// </summary>
        /// <param name="corporates">Branches</param>
        /// <param name="number">Number of branches</param>
        /// <param name="data">file directory</param>
        /// <param name="header">header of a table</param>
        /// <param name="title">title of a table</param>
        /// <exception cref="Exception"></exception>
        public static void PrintData(List<Corporate> corporates, int number, string data, string header, string title)
        {
            for (int ii = 0; ii < number; ii++)
            {
                try
                {
                    Print(corporates[ii], header, data, title);
                }
                catch (Exception ex)
                {
                    continue;
                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// Display data from branch on screen
        /// </summary>
        /// <param name="corporate">Branch</param>
        /// <param name="data">file directory</param>
        /// <param name="header">header of a table</param>
        /// <param name="title">Title of table</param>
        public static void Print(Corporate corporate, string title, string data, string header)
        {
            try
            {
                string s = new string('-', corporate.GetTransport(0).ToString().Length);
                using (StreamWriter writer = new StreamWriter(data, true, Encoding.GetEncoding(1257)))
                {
                    writer.WriteLine(header.Replace(('|'), ' '));
                    writer.WriteLine(title);
                    writer.WriteLine(s);
                    for (int i = 0; i < corporate.Count; i++)
                    {
                        writer.WriteLine(corporate.GetTransport(i).ToString());
                    }
                    writer.WriteLine(s);
                    writer.WriteLine();
                }
        
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }

        }
        /// <summary>
        /// Display data of tranport
        /// </summary>
        /// <param name="transport">transport</param>
        /// <param name="data">file directory</param>
        /// <param name="header">header of a table</param>
        /// <param name="title">Title of table</param>
        /// <exception cref="Exception"></exception>
        public static void PrintBest(Transport transport, string title, string data, string header)
        {
            try
            {
                int age = DateTime.Now.Year - transport.Year.Year;
                string s = new string('-', 42);
                using (StreamWriter writer = new StreamWriter(data, true, Encoding.GetEncoding(1257)))
                {
                    writer.WriteLine(header.Replace('|', ' '));
                    writer.WriteLine(title);
                    writer.WriteLine(s);
                    writer.WriteLine("{0,-10}|{1,-12}|{2,-7}|{3,-6}|{4,-10}",transport.GetType().Name, transport.Manufacturer, transport.Model, transport.Plate, age);
                    writer.WriteLine(s);
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Display data of branches to a file
        /// </summary>
        /// <param name="corporate">corporate</param>
        /// <param name="data">file directory</param>
        /// <param name="header">header of a table</param>
        /// <param name="title">Title of table</param>
        /// <exception cref="Exception"></exception>
        public static void PrintBranch(Corporate corporate, string title, string data, string header)
        {
            try
            {
                string s = new string('-', 48);
                using (StreamWriter writer = new StreamWriter(data, true, Encoding.GetEncoding(1257)))
                {
                    writer.WriteLine(header.Replace(('|'), ' '));
                    writer.WriteLine(title);
                    writer.WriteLine(s);

                    writer.WriteLine(corporate.ToString());

                    writer.WriteLine(s);
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Prints transport list for inspection file
        /// </summary>
        /// <param name="transports">list of transports</param>
        /// <param name="data">file directory</param>
        /// <param name="header">header of a table</param>
        /// <param name="title">Title of table</param>
        /// <exception cref="Exception"></exception>
        public static void PrintForInspection(List<Transport> transports, string title, string data, string header)
        {
            try
            {
                if (transports != null && transports.Count > 0)
                {
                    string s = new string('-', 80);

                    using (StreamWriter writer = new StreamWriter(data, true, Encoding.GetEncoding(1257)))
                    {
                        writer.WriteLine(header.Replace('|', ' '));
                        writer.WriteLine(title);
                        writer.WriteLine(s);
                        foreach (var transport in transports)
                        {
                            writer.WriteLine("{0,-10}|{1,-15}|{2,-15}|{3,-10}|{4:yyyy-MM-dd}",transport.GetType().Name,transport.Manufacturer, transport.Model,transport.Plate,transport.TechnicalInspectionDate);
                        }
                        writer.WriteLine(s);
                        writer.WriteLine();
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(data, true, Encoding.GetEncoding(1257)))
                    {
                        writer.WriteLine("No transports found for inspection.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
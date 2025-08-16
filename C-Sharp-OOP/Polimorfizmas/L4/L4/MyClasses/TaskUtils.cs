
using System;
using System.Collections.Generic;


namespace L4.MyClasses
{
    public static class TaskUtils
    {

        /// <summary>
        /// Find branch according to the title
        /// </summary>
        /// <param name="corporates">>Collection of branches</param>
        /// <param name="number">Number of branches</param>
        /// <param name="town">City name</param>
        /// <param name="address">Address of a branch</param>
        /// <param name="email">Email address of a branch</param>
        /// <returns>Branch object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Corporate GetBranchByTown(List<Corporate> corporates, ref int number, string town, string address, string email)
        {
            if(corporates==null)
            {
                throw new ArgumentNullException("corporates yra null");
            }
            for (int i = 0; i < number; i++)
            {
                if (corporates[i].City == town)
                {
                    return corporates[i];
                }
            }
            Corporate corporate = new Corporate(town, address, email);
            corporates.Add(corporate);
            number++;
            return corporate;
        }
        /// <summary>
        /// Select the transports from the corporate object
        /// </summary>
        /// <param name="corp">Branch</param>
        /// <returns>transports list</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<Transport> GetTransports(Corporate corp)
        {
            if(corp == null)
            {
                throw new ArgumentNullException("corp yra null");
            }
            List<Transport> list = new List<Transport>();
            if (corp.Count == 0)
            {
                return list;
            }
            for (int i = 0; i < corp.Count; i++)
            {
                list.Add(corp.GetTransport(i));
            }
            return list;
        }
        /// <summary>
        /// Select the transports from the common list
        /// </summary>
        /// <param name="corporates">branches list</param>
        /// <returns>transports list</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<Transport> GetAllTransports(List<Corporate> corporates)
        {
            if (corporates == null)
            {
                throw new ArgumentNullException("corporates yra null");
            }
            List<Transport> transports = new List<Transport>();
            foreach (var corporate in corporates)
            {
                List<Transport> transportList = TaskUtils.GetTransports(corporate);
                foreach (var transport in transportList)
                {
                    transports.Add(transport); 
                }
            }
            return transports;
        }
        /// <summary>
        /// Select transports of given type
        /// </summary>
        /// <param name="corporate">branch</param>
        /// <param name="type">type of object</param>
        /// <returns>transport of a given type</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Corporate FilterByType(Corporate corporate, Type type)
        {
            if(corporate==null || type == null)
            {
                throw new ArgumentNullException("corporate arba tipas yra null");
            }
            Corporate corporate1 = new Corporate();
            for (int i = 0; i < corporate.Count; i++)
            {
                if (corporate.GetTransport(i).GetType() == type)
                {
                    corporate1.AddTransport(corporate.GetTransport(i));
                }
            }
            return corporate1;
        }
        /// <summary>
        /// Finds a best truck by its capacity
        /// </summary>
        /// <param name="corporates">branches</param>
        /// <param name="type">given type</param>
        /// <returns>best truck</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Transport GetBestTruck(List<Corporate> corporates, Type type)
        {
            if (corporates == null || type == null)
            {
                throw new ArgumentNullException("corporates arba tipas yra null");
            }
            Truck truck = null;
            foreach (var corporate in corporates)
            {
                for (int i = 0; i < corporate.Count; i++)
                {
                    if (corporate.GetTransport(i).GetType() == type)
                    {
                        Truck truck1 = corporate.GetTransport(i) as Truck;
                        if(truck == null || truck1.Capacity>truck.Capacity)
                        {
                            truck = truck1;
                        }
                    }
                }
            }
            return truck;
        }
        /// <summary>
        /// Finds a best transport by its compareto() method
        /// </summary>
        /// <param name="corporates">branches list</param>
        /// <param name="type">given type of transport</param>
        /// <returns>best transport of a given type</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static Transport GetBestTransport(List<Corporate> corporates, Type type)
        {
            if (corporates == null || type == null)
            {
                throw new ArgumentNullException("corporates arba tipas yra null");
            }
            Transport transport = null;
            foreach (var corporate in corporates)
            {
                try
                {
                    for (int i = 0; i < corporate.Count; i++)
                    {
                        if (corporate.GetTransport(i).GetType() == type)
                        {
                            if (transport == null || corporate.GetTransport(i).CompareTo(transport) < 0)
                            {
                                transport = corporate.GetTransport(i);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    continue;
                    
                }
            }
            if (transport == null)
            {
              
                throw new Exception("Nepavyko rasti transporto priemonės tipo " + type.Name);
            }
            return transport;
        }
        /// <summary>
        /// Finds a corporate with oldest average age of trucks
        /// </summary>
        /// <param name="corporates">list of corporates</param>
        /// <returns> a corporate with oldest average age of trucks</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Corporate WithOldestMicrobusses(List<Corporate> corporates)
        {
            if (corporates == null)
            {
                throw new ArgumentNullException("corporates yra null");
            }
            Corporate witholdest = null;
           double maxage = double.MinValue;
           foreach(var corporate in corporates)
            {
                double age = 0;
                Corporate microbusses = FilterByType(corporate, typeof(Microbus));
                for(int i = 0;i<microbusses.Count;i++)
                {
                    age += (DateTime.Now.Year - microbusses.GetTransport(i).Year.Year);
                }
                age = age / microbusses.Count;
                if(age>maxage )
                {
                    maxage = age;
                    witholdest = corporate;
                }
            }
            return witholdest;
        }
        /// <summary>
        /// Calculates the next technical inspection date for a transport
        /// </summary>
        /// <param name="corporates">list of corporates</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NextTechnicalInspection(List<Corporate> corporates)
        {
            if (corporates == null)
            {
                throw new ArgumentNullException("transports yra null");
            }
            List<Transport> transports = GetAllTransports(corporates);
            foreach (var transport in transports)
            {
                if(transport is Car)
                {
                    transport.NextTechnicalInspectionDate = transport.TechnicalInspectionDate.AddMonths(24);
                }
                else if(transport is Truck)
                {
                    transport.NextTechnicalInspectionDate = transport.TechnicalInspectionDate.AddMonths(12);
                }
                else if(transport is Microbus)
                {
                    transport.NextTechnicalInspectionDate = transport.TechnicalInspectionDate.AddMonths(6);
                }
            }
        }
        /// <summary>
        /// Finds transports whose next technical inspection is in the next 30 days
        /// </summary>
        /// <param name="corporates"></param>
        /// <returns>list of transports</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<Transport> LessThanMonth(List<Corporate> corporates)
        {
            List<Transport> transports = GetAllTransports(corporates);
            if (transports == null)
            {
                throw new ArgumentNullException("transports are null");
            }
            List<Transport> needsInspection = new List<Transport>();
            foreach(var transport in transports)
            {
                TimeSpan difference = transport.NextTechnicalInspectionDate - DateTime.Now;

                if (difference.TotalDays <= 30 && difference.TotalDays >= 0) 
                {
                    needsInspection.Add(transport);
                }
            }
            return needsInspection;
        }
    }
}
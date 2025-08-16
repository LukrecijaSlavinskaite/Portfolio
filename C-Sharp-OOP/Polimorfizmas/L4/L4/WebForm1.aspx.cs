using L4.MyClasses;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace L4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BulletedList1.Items.Clear();
            List<Corporate> corporates = new List<Corporate>();
            List<Transport> cars = new List<Transport>();
            List<Transport> trucks = new List<Transport>();
            List<Transport> buses = new List<Transport>();
            int NumberOfCorporates = 0;
            string DataDir = Server.MapPath("~/App_Data");
            string fr = Server.MapPath("Rezultatai.txt");
            string frr = Server.MapPath("Apžiūra.txt");
            if (File.Exists(fr))
            {
                File.Delete(fr); //prevents data duplicate in the same file
            }
            if (File.Exists(frr))
            {
                File.Delete(frr); //prevents data duplicate in the same file
            }
            try
            {
                InOutUtils.ReadData(DataDir, corporates, ref NumberOfCorporates);
            }
            catch (Exception ex)
            {
                BulletedList1.Items.Add(ex.Message);    
            }

            try
            {
                InOutUtils.Print(corporates[0], Car.GetHeader(), fr, corporates[0].ToString());
            }
            catch(Exception ex)
            {
                BulletedList1.Items.Add(ex.Message);
                BulletedList1.Items.Add(ex.Message);
                BulletedList1.Items.Add(ex.Message);
            }
            try
            {
                InOutUtils.Print(corporates[1], Car.GetHeader(), fr, corporates[1].ToString());
            }
            catch(Exception ex)
            {
                BulletedList1.Items.Add(ex.Message);
                BulletedList1.Items.Add(ex.Message);
                BulletedList1.Items.Add(ex.Message);
            }

            try
            {
                InOutUtils.Print(corporates[2], Car.GetHeader(), fr, corporates[2].ToString());
            }
            catch (Exception ex)
            {
                BulletedList1.Items.Add(ex.Message);
                BulletedList1.Items.Add(ex.Message);
                BulletedList1.Items.Add(ex.Message);

            } 

            if (corporates != null && corporates.Count > 0)
            {
                try
                {
                    if (NumberOfCorporates > 0)
                    {                       
                        AppendTable(PlaceHolder1, Car.GetHeader() , TaskUtils.GetTransports(corporates[0]), corporates[0].ToString());
                    }

                    if (NumberOfCorporates > 1)
                    {
                        AppendTable(PlaceHolder2, Car.GetHeader(), TaskUtils.GetTransports(corporates[1]), corporates[1].ToString());
                    }

                    if (NumberOfCorporates > 2)
                    {
                        AppendTable(PlaceHolder3, Car.GetHeader(), TaskUtils.GetTransports(corporates[2]), corporates[2].ToString());
                    }
                    
                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add(ex.Message);
                }
                try
                {
                    Transport bestCar = TaskUtils.GetBestTransport(corporates, typeof(Car));
                    Transport bestTruck = TaskUtils.GetBestTruck(corporates, typeof(Truck));
                    Transport bestBus = TaskUtils.GetBestTransport(corporates, typeof(Microbus));

                    if (bestCar != null)
                    {
                        cars.Add(bestCar);
                    }
                    if (bestTruck != null)
                    {
                        trucks.Add(bestTruck);
                    }
                    if (bestBus != null)
                    {
                        buses.Add(bestBus);
                    }
                    string header = string.Format("{0,-10}|{1,-12}|{2,-7}|{3,-6}|{4,-10}", "Type","Manufacturer","Model", "Plate", "Age").ToString();
                    AppendBest(PlaceHolder4, header, bestCar);
                    AppendBest(PlaceHolder5, header, bestTruck);
                    AppendBest(PlaceHolder6, header, bestBus);
                    InOutUtils.PrintBest(bestCar, header, fr, "Geriausias automobilis");
                    InOutUtils.PrintBest(bestTruck, header, fr, "Geriausias krovininis automobilis");
                    InOutUtils.PrintBest(bestBus, header, fr, "Geriausias autobusas");

                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add(ex.Message);
                }
                try
                {
                    Corporate oldest = TaskUtils.WithOldestMicrobusses(corporates);
                    AppendTable(PlaceHolder7, oldest.ToString(), null, Corporate.GetHeader());
                    InOutUtils.PrintBranch(oldest, Corporate.GetHeader(), fr, "Filialas, su seniausiais mikroautobusais:");
                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add(ex.Message);
                }
                try
                {
                    Corporate find = TaskUtils.FilterByType(corporates[0], typeof(Truck));
                    find.SortTransports();
                    AppendTable(PlaceHolder8, Truck.GetHeader(), TaskUtils.GetTransports(find), corporates[0].ToString());
                    InOutUtils.Print(find, Truck.GetHeader(), fr, corporates[0].ToString());
                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add(ex.Message);
                }
                try
                {
                    Corporate find1 = TaskUtils.FilterByType(corporates[1], typeof(Truck));
                    find1.SortTransports();
                    AppendTable(PlaceHolder9, Truck.GetHeader(), TaskUtils.GetTransports(find1), corporates[1].ToString());
                    InOutUtils.Print(find1, Truck.GetHeader(), fr, corporates[1].ToString());
                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add(ex.Message);
                }
                try
                {
                    Corporate find2 = TaskUtils.FilterByType(corporates[2], typeof(Truck));
                    find2.SortTransports();
                    AppendTable(PlaceHolder10, Truck.GetHeader(), TaskUtils.GetTransports(find2), corporates[2].ToString());
                    InOutUtils.Print(find2, Truck.GetHeader(), fr, corporates[2].ToString());
                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add(ex.Message);
                }
                try
                {
                    List<Transport> transports = TaskUtils.GetAllTransports(corporates);
                    string header = string.Format("{0,-10}|{1,-15}|{2,-15}|{3,-10}|{4,-15}", "Type", "Manufacturer", "Model", "Plate", "Inspection end date");
                    TaskUtils.NextTechnicalInspection(corporates);
                    List<Transport> filtered = TaskUtils.LessThanMonth(corporates);
                    AppendBForInspection(PlaceHolder11, header, filtered);
                    InOutUtils.PrintForInspection(filtered, header, frr, "Transporto priemonės, kurioms iki techninės apžiūros galiojimo pabaigos liko mažiau kaip 1 mėnesis:");
                        
                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add(ex.Message);
                }
            }
        }
    }
}

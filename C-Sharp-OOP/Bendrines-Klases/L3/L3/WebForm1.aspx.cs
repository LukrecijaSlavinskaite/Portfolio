using L3.MyClasses;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace L3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        const string fd1 = "App_Data/U2a.bin";
        const string fd2 = "App_Data/U2b.bin";
        const string fr = "App_Data/RESULTS.txt";

        private TripLinkList<Trip> trips;
        private TripLinkList<Price> prices;
        private TripLinkList<TripsWithPrice> tripsWithPrices;
        private TripLinkList<TripInfoForCommon> tripInfoForCommons;

        protected void Page_Load(object sender, EventArgs e)
        {
            trips = (TripLinkList<Trip>)Session["trips"];
            prices = (TripLinkList<Price>)Session["prices"];
            tripsWithPrices = (TripLinkList<TripsWithPrice>)Session["tripsWithPrices"];

            if (!IsPostBack)
            {
                if (File.Exists(Server.MapPath(fr)))
                {
                    File.Delete(Server.MapPath(fr)); //prevents data duplicate in the same file
                }

            }
            if (trips != null)
            {
                AppendTable(PlaceHolder1, Trip.GetInfoForHeader(), trips);
            }
            if (prices != null)
            {
                AppendTable(PlaceHolder2, Price.GetInfoForHeader(), prices);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int WeekDay = DropDownList1.SelectedIndex;
            string City = DropDownList2.SelectedValue;
            TimeSpan DepartureTime = TimeSpan.Parse(DropDownList3.SelectedValue);
            TimeSpan ArrivalTime = TimeSpan.Parse(DropDownList4.SelectedValue);
            string CurrentCity = Label12.Text;


            TripLinkList<Trip> possibleTrips = TaskUtils.FindPossibleTrip(trips, CurrentCity, City, DepartureTime, ArrivalTime, WeekDay);
            TripLinkList<TripsWithPrice> cheapestTrips = TaskUtils.FindCheapestTrips(possibleTrips, prices);
            InOutUtils.Print(Server.MapPath(fr), TripsWithPrice.HeaderForCHeapestTrips(), cheapestTrips, "Pigiausios keliones:");
            AppendTable(PlaceHolder3, TripsWithPrice.HeaderForCHeapestTrips(), cheapestTrips);
            
            string mostVisited = TaskUtils.FindMostVisitedCityByBus(trips);
            Label10.Text = mostVisited;

            TripLinkList<TripInfoForCommon> visitedTrips = TaskUtils.TripsToMostVisitedCity(trips, mostVisited);
            visitedTrips.Sort();
            AppendTable(PlaceHolder4, TripInfoForCommon.HeaderForCommonTrips(), visitedTrips);
            string line = "Autobusų sąrašas (rikiuotas pagal išvykimo miestą ir laiką), kurie daugiausiai važiuoja į:" + mostVisited;
            InOutUtils.Print(Server.MapPath(fr), TripInfoForCommon.HeaderForCommonTrips(),visitedTrips, line);
            RemoveIfTransit(visitedTrips);
            AppendTable(PlaceHolder5, TripInfoForCommon.HeaderForCommonTrips(), visitedTrips);
            InOutUtils.Print(Server.MapPath(fr), TripInfoForCommon.HeaderForCommonTrips(), visitedTrips, "Be tranzitinių:");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && FileUpload1.FileName.EndsWith(".txt"))
            {


                trips = InOutUtils.ReadFromFile1(FileUpload1.FileContent);
                InOutUtils.Print<Trip>(Server.MapPath(fr), Trip.GetInfoForHeader(), trips, "Pradiniai kelionių duomenys");


                BinaryFormatter format = new BinaryFormatter();
                using (FileStream file = new FileStream(Server.MapPath(fd1), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    format.Serialize(file, trips);
                }
                Session["trips"] = trips;
                AppendTable(PlaceHolder1, Trip.GetInfoForHeader(), trips);

            }

            if (FileUpload2.HasFile && FileUpload2.FileName.EndsWith(".txt"))
            {
                prices = InOutUtils.ReadFromFile2(FileUpload2.FileContent);
                InOutUtils.Print<Price>(Server.MapPath(fr), Price.GetInfoForHeader(), prices, "Pradiniai kainų duomenys");
                BinaryFormatter format = new BinaryFormatter();
                using (FileStream file = new FileStream(Server.MapPath(fd2), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    format.Serialize(file, prices);
                }
                Session["prices"] = prices;
                AppendTable(PlaceHolder2, Price.GetInfoForHeader(), prices);
            }
            InsertDataToWeb();

        }
        private void InsertDataToWeb()
        {
            if (DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("-");
                for (int i = 1; i <= 7; i++)
                {
                    DropDownList1.Items.Add(i.ToString()); // adding weekdays to dropdownlist
                }
            }
            if (DropDownList3.Items.Count == 0)
            {
                DropDownList3.Items.Add("-");
                foreach (var trip in trips)
                {
                    DropDownList3.Items.Add(trip.DepartureTime.ToString()); // adding departure time
                }
            }
            if (DropDownList4.Items.Count == 0)
            {
                DropDownList4.Items.Add("-");
                foreach (var trip in trips)
                {
                    DropDownList4.Items.Add(trip.ArrivalTime.ToString()); // adding arrival time
                }
            }
            if (DropDownList2.Items.Count == 0)
            {
                DropDownList2.Items.Add("-");
                foreach (var price in prices)
                {
                    DropDownList2.Items.Add(price.ArrivalCity); // adding arrival city
                }
            }
            trips.Begin();
            if (trips.Exist())
            {
                Trip trip = trips.Get();
                Label12.Text = trip.CurrentCity;
            }

        }
        private static void RemoveIfTransit(TripLinkList<TripInfoForCommon> VisitedTrips)
        {
            VisitedTrips.Begin();
            while (VisitedTrips.Exist())
            {
                var trip = VisitedTrips.Get();
                if (trip.IsBusTransit)
                {
                    VisitedTrips.Remove(trip); 
                }
                else
                {
                    VisitedTrips.Next();
                }
            }
        }

    }
}
    
    
    
        
    

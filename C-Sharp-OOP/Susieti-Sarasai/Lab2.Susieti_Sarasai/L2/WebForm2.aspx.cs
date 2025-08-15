using L2.MyClasses;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace L2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        const string fd1 = @"App_Data/U2a.txt";
        const string fd2 = @"App_Data/U2b.txt";
        const string fr = @"App_Data/RESULTS.txt";

        private TripLinkList triplinklist;
        private PriceLinkList pricelinkList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath(fr)))
            {
                File.Delete(Server.MapPath(fr)); //prevents data duplicate in the same file
            }
            triplinklist = InOutUtils.ReadFromFile1(Server.MapPath(fd1)); //reads and sets initial data of trips from a file
            pricelinkList = InOutUtils.ReadFromFile2(Server.MapPath(fd2)); //reads and sets initial data of prices from a file

            InOutUtils.PrintTrips(Server.MapPath(fr), triplinklist, "Pradiniai kelionių duomenys"); //printing initial trips data
            InOutUtils.PrintPrices(Server.MapPath(fr), pricelinkList, "Pradiniai kainų duomenys"); //printing initial prices data
            
            triplinklist.Begin();
            if (triplinklist.Exist())
            {
                Trip firstTrip = triplinklist.Get();
                TextBox1.Text = firstTrip.CurrentCity; //writing a current city to a textbox
            }
            if (DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("-");
                for (int i = 1; i <= 7; i++)
                {
                    DropDownList1.Items.Add(i.ToString()); //adding weekdays to a dropdownlist
                }
            }
            if (DropDownList2.Items.Count == 0)
            {
                DropDownList2.Items.Add("-");
                for (pricelinkList.Begin(); pricelinkList.Exist(); pricelinkList.Next())
                {
                    DropDownList2.Items.Add(pricelinkList.Get().ArrivalCity); //adding arrival city to a dropdownlist
                }
            }
            if (DropDownList3.Items.Count == 0)
            {
                DropDownList3.Items.Add("-");
                for (triplinklist.Begin(); triplinklist.Exist(); triplinklist.Next())
                {
                    DropDownList3.Items.Add(triplinklist.Get().DepartureTime.ToString()); //adding departure time to a dropdownlist
                }
            }
            if (DropDownList4.Items.Count == 0)
            {
                DropDownList4.Items.Add("-");
                for (triplinklist.Begin(); triplinklist.Exist(); triplinklist.Next())
                {
                    DropDownList4.Items.Add(triplinklist.Get().ArrivalTime.ToString()); //adding arrival time to a dropdownlist
                }
            }

            InsertInitialTrips(Server.MapPath("App_Data/U2a.txt")); //inserting initial trips data to a table
            InsertInitialPrices(Server.MapPath("App_Data/U2b.txt")); //inserting initial prices data to a table

            string mostVisited = TaskUtils.FindMostVisitedCityByBus(triplinklist);
            Label10.Text = mostVisited; //writing the most visited city name to a label

            TripLinkList VisitedTrips = TaskUtils.TripsToMostVisitedCity(triplinklist, mostVisited);
            VisitedTrips.Sort(); //sorting trips      
            InsertMostVisitedTrips(VisitedTrips, Table2); //inserting most visited cities data to a table
            string line = "Autobusų sąrašas (rikiuotas pagal išvykimo miestą ir laiką), kurie daugiausiai važiuoja į:" + mostVisited;
            InOutUtils.PrintMostCommonTrips(Server.MapPath(fr), VisitedTrips, line); //printing most common trips to a file
            
            RemoveIfTransit(VisitedTrips); //romoving trips that are transit from the most visited city trips list
            InsertMostVisitedTrips(VisitedTrips, Table5); //inserting a data of the most visited cities trips to a table

            InOutUtils.PrintMostCommonTrips(Server.MapPath(fr), VisitedTrips, "Pašalinti tranzitiniai autobusai"); //printing most common trips without transit trips to a file
        }
        /// <summary>
        /// method that removes a trip if a bus is transit
        /// </summary>
        /// <param name="VisitedTrips">list of the most visited cities trips</param>
        private void RemoveIfTransit(TripLinkList VisitedTrips)
        {
            for (VisitedTrips.Begin(); VisitedTrips.Exist();)
            {
                if (VisitedTrips.Get().IsBusTransit)
                {
                    VisitedTrips.Remove();
                    if (!VisitedTrips.Exist())
                    {
                        break;
                    }
                }
                else
                {
                    VisitedTrips.Next();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int WeekDay = DropDownList1.SelectedIndex;
            string City = DropDownList2.SelectedValue;
            TimeSpan DepartureTime = TimeSpan.Parse(DropDownList3.SelectedValue);
            TimeSpan ArrivalTime = TimeSpan.Parse(DropDownList4.SelectedValue);
            string CurrentCity = TextBox1.Text;

            triplinklist.Begin();
            if (triplinklist.Exist())
            {
                triplinklist.Get().CurrentCity = CurrentCity;
            }
            TripLinkList possibleTrips = TaskUtils.FindPossibleTrip(triplinklist, CurrentCity, City, DepartureTime, ArrivalTime, WeekDay);
            TripLinkList cheapestTrips = TaskUtils.FindCheapestTrips(possibleTrips, pricelinkList);
            InOutUtils.PrintCheapestTrips(Server.MapPath(fr), cheapestTrips, "Pigiausios kelionės:", pricelinkList); //printing the cheapest trips data to a file
            InsertCheapestTrips(cheapestTrips); //inserting a data of cheapest trips to the table
           }
        /// <summary>
        /// Method that inserts initial trips data to a table
        /// </summary>
        /// <param name="file"></param>
        private void InsertInitialTrips(string file)
        {
            string[] allLines = File.ReadAllLines(file);
            foreach (string line in allLines.Skip(1))
            {
                string[] parts = line.Split(',');
                TableRow row = new TableRow();
                TableCell title = new TableCell();
                title.Text = parts[0];
                TableCell time = new TableCell();
                time.Text = parts[1];
                TableCell city = new TableCell();
                city.Text = parts[2];
                TableCell timetime = new TableCell();
                timetime.Text = parts[3];
                TableCell weekday = new TableCell();
                weekday.Text = parts[4];
                row.Cells.Add(title);
                row.Cells.Add(time);
                row.Cells.Add(city);
                row.Cells.Add(timetime);
                row.Cells.Add(weekday);
                Table3.Rows.Add(row);
            }
        }
        /// <summary>
        ///  Method that inserts initial prices data to a table
        /// </summary>
        /// <param name="file"></param>
        private void InsertInitialPrices(string file)
        {
            string[] allLines1 = File.ReadAllLines(file);
            foreach (string line in allLines1)
            {
                string[] parts = line.Split(',');
                TableRow row1 = new TableRow();
                TableCell title = new TableCell();
                title.Text = parts[0];
                TableCell price = new TableCell();
                price.Text = parts[1];
                row1.Cells.Add(title);
                row1.Cells.Add(price);
                Table4.Rows.Add(row1);
            }
        }
        /// <summary>
        ///  Method that inserts cheapest trips data to a table
        /// </summary>
        /// <param name="cheapestTrips"></param>
        private void InsertCheapestTrips(TripLinkList cheapestTrips)
        {
            Table1.Rows.Clear();
            TableRow row = new TableRow();
            TableCell DepartureCity = new TableCell();
            DepartureCity.Text = "<b>Išvykimo miestas</b>";
            TableCell ArrivalCity = new TableCell();
            ArrivalCity.Text = "<b>Atvykimo miestas</b>";
            TableCell DepartureTime = new TableCell();
            DepartureTime.Text = "<b>Išvykimo laikas</b>";
            TableCell ArrivalTime = new TableCell();
            ArrivalTime.Text = "<b>Atvykimo laikas</b>";
            TableCell price = new TableCell();
            price.Text = "<b>Kaina</b>";
            row.Cells.Add(DepartureCity);
            row.Cells.Add(ArrivalCity);
            row.Cells.Add(DepartureTime);
            row.Cells.Add(ArrivalTime);
            row.Cells.Add(price);
            Table1.Rows.Add(row);

            for (cheapestTrips.Begin(); cheapestTrips.Exist(); cheapestTrips.Next())
            {
                TableRow tableRow = new TableRow();
                TableCell departureCityCell = new TableCell();
                departureCityCell.Text = cheapestTrips.Get().DepartureCity;
                TableCell departureTimeCell = new TableCell();
                departureTimeCell.Text = cheapestTrips.Get().DepartureTime.ToString(@"hh\:mm");
                TableCell arrivalCityCell = new TableCell();
                arrivalCityCell.Text = cheapestTrips.Get().ArrivalCity;
                TableCell arrivalTimeCell = new TableCell();
                arrivalTimeCell.Text = cheapestTrips.Get().ArrivalTime.ToString(@"hh\:mm");
                double finalPrice = TaskUtils.CalculatePrice(cheapestTrips.Get(), pricelinkList);
                TableCell priceCell = new TableCell();
                priceCell.Text = finalPrice.ToString();

                tableRow.Cells.Add(departureCityCell);
                tableRow.Cells.Add(arrivalCityCell);
                tableRow.Cells.Add(departureTimeCell);
                tableRow.Cells.Add(arrivalTimeCell);
                tableRow.Cells.Add(priceCell);
                Table1.Rows.Add(tableRow);
            }
        }
        /// <summary>
        ///  Method that inserts most visited cities trips data to a table
        /// </summary>
        /// <param name="VisitedTrips"></param>
        /// <param name="table"></param>
        private void InsertMostVisitedTrips(TripLinkList VisitedTrips, Table table)
        {
            TableRow tableRow = new TableRow();
            TableCell DepartureCity = new TableCell();
            DepartureCity.Text = "<b>Išvykimo miestas</b>";
            TableCell DepartureTime = new TableCell();
            DepartureTime.Text = "<b>Išvykimo laikas</b>";
            TableCell ArrivalTime = new TableCell();
            ArrivalTime.Text = "<b>Atvykimo laikas</b>";
            TableCell WeekDay = new TableCell();
            WeekDay.Text = "<b>Savaitės diena</b>";
            tableRow.Cells.Add(DepartureCity);
            tableRow.Cells.Add(DepartureTime);
            tableRow.Cells.Add(ArrivalTime);
            tableRow.Cells.Add(WeekDay);
            table.Rows.Add(tableRow);

            for (VisitedTrips.Begin(); VisitedTrips.Exist(); VisitedTrips.Next())
            {
                TableRow tableRowCity = new TableRow();
                TableCell departureCity = new TableCell();
                departureCity.Text = VisitedTrips.Get().DepartureCity;
                TableCell departureTime = new TableCell();
                departureTime.Text = VisitedTrips.Get().DepartureTime.ToString(@"hh\:mm");
                TableCell arrivalTime = new TableCell();
                arrivalTime.Text = VisitedTrips.Get().ArrivalTime.ToString(@"hh\:mm");
                TableCell weekdayofarrival = new TableCell();
                weekdayofarrival.Text = VisitedTrips.Get().Weekday.ToString();

                tableRowCity.Cells.Add(departureCity);
                tableRowCity.Cells.Add(departureTime);
                tableRowCity.Cells.Add(arrivalTime);
                tableRowCity.Cells.Add(weekdayofarrival);
                table.Rows.Add(tableRowCity);
            }
        }
    }
}
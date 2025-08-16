using lab5.MyClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab5
{

	public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            string fr = Server.MapPath("Rezultatai.txt");
            
                
                if (File.Exists(fr))
                {
                    File.Delete(fr); //prevents data duplicate in the same file
                }
                string folder = Server.MapPath("~/Testui");
                var visits = new List<VisitsList>();
                var servers = new ServersList();
                Session["visits"] = visits;
                Session["servers"] = servers;
                try
                {
                    string[] Visitsfiles = Directory.GetFiles(folder, "U2*.txt");
                    if (Visitsfiles.Length == 0)
                    {
                        BulletedList1.Items.Add($"Error: no .txt files found in folder {folder}");
                    }
                    foreach (string file in Visitsfiles)
                    {
                        try
                        {
                            visits.Add(InOutUtils.ReadVisitsData(file));

                        }
                        catch (Exception ex)
                        {
                            BulletedList1.Items.Add(ex.Message);
                        }
                    }
                    InsertDay();
                    foreach (var visitList in visits)
                    {
                        try
                        {
                            string comment = visitList.VisitDate.ToShortDateString() + " " + visitList.ServerIpAddress;
                            InOutUtils.PrintTableToFile(fr, Visit.GetHeader(), visitList, comment);
                            AppendTable(PlaceHolder1, Visit.GetHeader(), visitList, comment);
                        }
                        catch (Exception ex)
                        {
                            BulletedList1.Items.Add(ex.Message);
                        }
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    BulletedList1.Items.Add($"Error: Folder {folder} not found");
                }
                catch (Exception ex)
                {
                    BulletedList1.Items.Add("Error loading visits data: " + ex.Message);
                }
                try
                {
                    string[] ServerFile = Directory.GetFiles(folder, "Serveriai*.txt");
                    foreach (string file in ServerFile)
                    {
                        servers.Add(InOutUtils.ReadServersData(file));
                    }
                    if (ServerFile.Length == 0)
                    {
                        BulletedList1.Items.Add($"Error: no .txt files found in {folder} App_Data");
                    }
                    InsertServerName();
                    InOutUtils.PrintTableToFile(fr, ServerInfo.GetHeader(), servers, "");
                    AppendTable(PlaceHolder2, ServerInfo.GetHeader(), servers, "");
                }
                catch (DirectoryNotFoundException)
                {
                    BulletedList1.Items.Add($"Error: Folder {folder} not found");
                }
            
            if (Session["Button1Results"] is List<string> serverInfos)
            {
                AppendTable(PlaceHolder3, "Server Name With Full Path", serverInfos, "");
            }
           
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = DropDownList1.SelectedValue;
                string header = "Server Name With Full Path";
                string serverIP = TaskUtils.FindServerIp(DropDownList1.SelectedValue, Session["servers"] as ServersList);
                
                List<string> serverInfos = TaskUtils.FindPagePathByWebName(Session["visits"] as List<VisitsList>, serverIP, name);
                
                Session["Button1Results"] = serverInfos;
                AppendTable(PlaceHolder3, header, serverInfos, "");
                using (StreamWriter writer = new StreamWriter(Server.MapPath("Rezultatai.txt"), true, Encoding.GetEncoding(1257)))
                {
                    writer.WriteLine(header);
                    foreach(var server in serverInfos)
                    {
                        writer.WriteLine(server);
                    }
                }
            }
            catch(Exception ex)
            {
                BulletedList1.Items.Add("Klaida vykdant 1 užduotį: " + ex.Message);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                List<string> strings = TaskUtils.FindPcIpForCallingMostAtThisDay(DateTime.Parse(DropDownList2.SelectedValue), out count, Session["visits"] as List<VisitsList>);
                AppendTable(PlaceHolder4, "PC IP Address", strings, "Daugiausiai kartų kreiptasi:" + " " + count.ToString());
                using (StreamWriter writer = new StreamWriter(Server.MapPath("Rezultatai.txt"), true, Encoding.GetEncoding(1257)))
                {
                    writer.WriteLine("Daugiausiai kartų kreiptasi:" + " " + count);
                    foreach (var server in strings)
                    {
                        writer.WriteLine(server);
                    }
                }
            }
            catch(Exception ex)
            {
                BulletedList1.Items.Add("Klaida vykdant 2 užduotį: " + ex.Message);
            }
        }
        private void InsertServerName()
        {
            if (DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("-");
                foreach (var server in (ServersList)Session["servers"])
                {
                    DropDownList1.Items.Add(server.ServerName);
                }
            }
        }
        private void InsertDay()
        {
            if (DropDownList2.Items.Count == 0)
            {
                DropDownList2.Items.Add("-");
                foreach (var server in (List<VisitsList>)Session["visits"])
                {
                    DropDownList2.Items.Add(server.VisitDate.ToShortDateString());
                }
            }
        }
    }
}
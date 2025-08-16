using L4.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace L4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// Create a table from list
        /// </summary>
        /// <param name="placeHolder">Where to insert table</param>
        /// <param name="header">Column names</param>
        /// <param name="transports">List of transports</param>
        /// <param name="title">table title</param>
        private void AppendTable(PlaceHolder placeHolder, string header, List<Transport> transports, string title)
        {
            Table table = new Table();
            table.CssClass = "generated-table";
            TableRow tableRow = new TableRow();
            if (title != null)
                foreach (var item in title.Split('|'))
                {
                    TableCell tableCell = new TableHeaderCell();
                    tableCell.Text = item;
                    tableRow.Cells.Add(tableCell);
                }
            TableRow row = new TableHeaderRow();
            foreach (var item in header.Split('|'))
            {
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = item.Trim();
                row.Cells.Add(cell);
            }
            table.Rows.Add(tableRow);
            table.Rows.Add(row);
            if(transports!=null)
            foreach (var transport in transports)
            {
                row = new TableRow();
                foreach (var cellData in transport.ToString().Split('|'))
                {
                    TableCell cell = new TableCell();
                    cell.Text = cellData;
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }
            placeHolder.Controls.Add(table);
        }
        /// <summary>
        /// Create table for best transport
        /// </summary>
        /// <param name="placeHolder">Where to insert table</param>
        /// <param name="header">Column names</param>
        /// <param name="transport">Transport object</param>
        private void AppendBest(PlaceHolder placeHolder, string header, Transport transport)
        {
            Table table = new Table();
            table.CssClass = "generated-table";
            TableRow tableRow = new TableRow();
            if (header != null)
            {
                foreach (var item in header.Split('|'))
                {
                    TableCell tableCell = new TableHeaderCell();
                    tableCell.Text = item;
                    tableRow.Cells.Add(tableCell);
                }
                table.Rows.Add(tableRow);
            }
            TableRow tableRow1 = new TableRow();
            TableCell typecell = new TableCell();
            typecell.Text = transport.GetType().Name;
            tableRow1.Cells.Add(typecell);
            TableCell manufactorcell = new TableCell();
            manufactorcell.Text = transport.Manufacturer;
            tableRow1.Cells.Add(manufactorcell);
            TableCell modelcell = new TableCell();
            modelcell.Text = transport.Model;
            tableRow1.Cells.Add(modelcell);
            TableCell plate = new TableCell();
            plate.Text = transport.Plate;
            tableRow1.Cells.Add(plate);
            TableCell year = new TableCell();
            year.Text = (DateTime.Now.Year - transport.Year.Year).ToString();
            tableRow1.Cells.Add(year);
            table.Rows.Add(tableRow1);
            placeHolder.Controls.Add(table);
        }
        /// <summary>
        /// Create table for inspection needed
        /// </summary>
        /// <param name="placeHolder">Where to insert table</param>
        /// <param name="header">Column names</param>
        /// <param name="transports">Transport list</param>
        private void AppendBForInspection(PlaceHolder placeHolder, string header, List<Transport> transports)
        {
            Table table = new Table();
            table.CssClass = "generated-table";
            TableRow tableRow = new TableRow();
            if (header != null)
            {
                foreach (var item in header.Split('|'))
                {
                    TableCell tableCell = new TableHeaderCell();
                    tableCell.Text = item;
                    tableRow.Cells.Add(tableCell);
                }
                table.Rows.Add(tableRow);
            }
            foreach (var transport in transports)
            {
                TableRow tableRow1 = new TableRow();
                TableCell typecell = new TableCell();
                typecell.Text = transport.GetType().Name;
                tableRow1.Cells.Add(typecell);
                TableCell manufactorcell = new TableCell();
                manufactorcell.Text = transport.Manufacturer;
                tableRow1.Cells.Add(manufactorcell);
                TableCell modelcell = new TableCell();
                modelcell.Text = transport.Model;
                tableRow1.Cells.Add(modelcell);
                TableCell plate = new TableCell();
                plate.Text = transport.Plate;
                tableRow1.Cells.Add(plate);
                TableCell year = new TableCell();
                year.Text = transport.NextTechnicalInspectionDate.ToString("yyyy-MM-dd");
                tableRow1.Cells.Add(year);
                table.Rows.Add(tableRow1);
            }
            placeHolder.Controls.Add(table);
        }
    }
}

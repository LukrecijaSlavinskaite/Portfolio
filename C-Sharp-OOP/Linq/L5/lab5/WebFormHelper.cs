using lab5.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace lab5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private void AppendTable<T>(PlaceHolder placeHolder, string header, IEnumerable<T> visits, string comment)
        {
            Table table = new Table();
            table.CssClass = "generated-table";
            TableRow tt = new TableRow();
            TableRow tableRow = new TableRow();
            TableCell titleecell = new TableCell();
            titleecell.Text = comment;
            tableRow.Cells.Add(titleecell);
            TableRow row = new TableHeaderRow();
            foreach (var item in header.Split('|'))
            {
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = item.Trim();
                row.Cells.Add(cell);
            }
            table.Rows.Add(tableRow);
            table.Rows.Add(row);
            if (visits != null)
                foreach (var transport in visits)
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

    }
}
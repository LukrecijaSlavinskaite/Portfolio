using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI.WebControls;

namespace L3
{
    public partial class WebForm1 : System.Web.UI.Page

    {
        private void AppendRows<T>(Table table, IEnumerable<T> data)
        {
            foreach (var item in data)
            {
                TableRow row = new TableRow();
                foreach (var cellData in data)
                {
                    TableCell cell = new TableCell();
                    cell.Text = cellData.ToString();
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }
        }

        private void AppendTable<T>(PlaceHolder placeHolder, string header, IEnumerable<T> data)
        {
            Table table = new Table();

            table.CssClass = "niceTable";
            TableRow row = new TableHeaderRow();
            foreach (var item in header)
            {
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = item.ToString();
                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
            foreach (var item in data)
            {
                row = new TableRow();
                foreach (var cellData in item.ToString())
                {
                    TableCell cell = new TableCell();
                    cell.Text = cellData.ToString();
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
                
            }
            placeHolder.Controls.Add(table);
        }
    }


    }


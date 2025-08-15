using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Lab1.Rekursija.MyClasses;

namespace Lab1.Rekursija
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string fd = @"App_Data/Kur3.txt";//Data file path.
        
        const string fr = @"App_Data/Results.txt"; //Results file path.
        /// <summary>
        /// A method that runs when the page is loaded.
        /// </summary>
        /// <param name="sender">What caused the page load</param>
        /// <param name="e">Information about the event</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath(fr)))
            {
                File.Delete(Server.MapPath(fr)); //Prevents data duplicate in the same file.
            }
            CrossWord crossWord = InOutUtils.ReadFromFile(Server.MapPath(fd)); 

            InOutUtils.PrintInitialData(Server.MapPath(fr), crossWord, "Kryžiažodis:");
            FillTable(crossWord);
        }
        /// <summary>
        /// Method that runs when button is clicked.
        /// </summary>
        /// <param name="sender">What caused the page load</param>
        /// <param name="e">Information about the event</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label4.Text = "Rezultatas:";
            CrossWord crossWord = InOutUtils.ReadFromFile(Server.MapPath(fd));
            TaskUtils.Solve(crossWord); //Calling a method to solve the crossword.
            InOutUtils.PrintResults(Server.MapPath(fr), crossWord, "Rezultatas:");
            FillResultsTable(crossWord);
        }
        /// <summary>
        /// Method that fills initial data table.
        /// </summary>
        /// <param name="crossWord">Crossword object</param>
        private void FillTable(CrossWord crossWord)
        {
            Table1.Rows.Clear();
            List<TableCell> wordCells = new List<TableCell>();
            int wordIndex = 0; // Words indicator.
            for (int i = 0; i < crossWord.Rows; i++)
            {
                TableRow row = new TableRow(); //Creating a new row.
                for (int j = 0; j < crossWord.Columns; j++)
                {
                    TableCell letterCell = new TableCell(); //Creating a cell for a letter simbol.
                    letterCell.Text = crossWord.GetLetter(i, j).ToString(); //Adding a letter to letter cell.
                    row.Cells.Add(letterCell); //Adding a letter cell to a row.
                }
                    if (wordIndex < crossWord.WordsCount)
                    {
                        TableCell wordCell = new TableCell();  //Creating a cell for words.
                        wordCell.Text = (wordIndex + 1) + ". " + crossWord.GetWord(wordIndex); //Adding a word number, dot and a word to a wordcell.
                        TaskUtils.GetColor(wordIndex, wordCell); //Adding a color to a word.
                        row.Cells.Add(wordCell); //Adding a word cells to a row.
                        wordIndex++;
                    }
                    else
                    {
                        TableCell emptyCell = new TableCell();
                        emptyCell.Text = ""; // Empty cell.
                        row.Cells.Add(emptyCell);
                    }
                    Table1.Rows.Add(row); //Adding a row to a table.
                }
            }
        
        /// <summary>
        /// Method that fills results data table.
        /// </summary>
        /// <param name="crossWord">Crossword object</param>
        private void FillResultsTable(CrossWord crossWord)
        {
            Table4.Rows.Clear();
            int wordIndex = 0; // Words indicator.
            for (int i = 0; i < crossWord.Rows; i++)
            {
                TableRow resultRow = new TableRow();
                for (int j = 0; j < crossWord.Columns; j++)
                {
                    TableCell LetterCell = new TableCell(); //Creating a cell for result letters.   
                    LetterCell.Text = crossWord.GetLetter(i, j).ToString(); //Adding a letter to letter cell.

                    //Changing letter's (number's) color. 
                    if (crossWord.GetLetter(i, j) == '1')
                    {
                        LetterCell.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (crossWord.GetLetter(i, j) == '2')
                    {
                        LetterCell.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (crossWord.GetLetter(i, j) == '3')
                    {
                        LetterCell.ForeColor = System.Drawing.Color.DeepPink;
                    }
                    else if (crossWord.GetLetter(i, j) == '4')
                    {
                        LetterCell.ForeColor = System.Drawing.Color.Brown;
                    }
                    else if (crossWord.GetLetter(i, j) == '5')
                    {
                        LetterCell.ForeColor = System.Drawing.Color.Orange;
                    }
                    resultRow.Cells.Add(LetterCell);  //Adding a result cell to a row.          
                }
                if (wordIndex < crossWord.WordsCount)
                {
                    TableCell wordcell = new TableCell();
                    wordcell.Text = (wordIndex + 1) + ". " + crossWord.GetWord(wordIndex);
                    TaskUtils.GetColor(wordIndex, wordcell);
                    resultRow.Cells.Add(wordcell);
                    wordIndex++;
                }
                else
                {
                    TableCell emptycell = new TableCell();
                    emptycell.Text = "";
                    resultRow.Cells.Add(emptycell);
                }
                Table4.Rows.Add(resultRow);
            }

                TableRow bottomRow = new TableRow(); //Creating a bottom row for sums.
                int[] columnSums = TaskUtils.CalculateColumnSums(crossWord); //Array for calculated columns' sums.
                for (int col = 0; col < crossWord.Columns; col++)
                {
                    TableCell sumCell = new TableCell();
                    sumCell.Text = columnSums[col].ToString();
                    bottomRow.Cells.Add(sumCell);
                }
                string SumText = "Kontrolinės sumos<br/>teisingumui patikrinti";
                TableCell lastCell = new TableCell();
                lastCell.Text = SumText;
                bottomRow.Cells.Add(lastCell);
                Table4.Rows.Add(bottomRow);
            }
        }
    }
 
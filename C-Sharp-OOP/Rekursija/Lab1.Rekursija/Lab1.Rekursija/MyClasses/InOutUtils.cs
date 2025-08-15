using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Lab1.Rekursija.MyClasses
{
    /// <summary>
    /// A class to store reading and printing to a file, methods
    /// </summary>
    public static class InOutUtils
    {
        public static WebForm1 WebForm1
        {
            get => default;
            set
            {
            }
        }

        public static CrossWord CrossWord
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Method that reads data from the file.
        /// </summary>
        /// <param name="fd">File path</param>
        /// <param name="crossWord">Crossword object</param>
        public static CrossWord ReadFromFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string[] dimensions = reader.ReadLine().Split(); // Read the first line to get crossword dimensions.
                int rows = int.Parse(dimensions[0]);
                int columns = int.Parse(dimensions[1]);
                CrossWord crossword = new CrossWord(rows, columns, 0); // Creating a crossword object with given dimensions.
                for (int i = 0; i < rows; i++)
                {
                    string[] letters = reader.ReadLine().Split();
                    for (int j = 0; j < columns; j++)
                    {
                        crossword.AddLetter(i, j, letters[j][0]); // Adding the first character of each cell to the crossword grid.
                    }
                }
                int wordsCount = int.Parse(reader.ReadLine()); // Reading the number of words.
                crossword.WordsCount = wordsCount;
                for (int i = 0; i < wordsCount; i++)
                {
                    string word = reader.ReadLine(); // Reading the words and adding them to the crossword.
                    crossword.AddWord(word);
                }
                return crossword;
            }
        }
        /// <summary>
        /// Method that prints initial data to a file.
        /// </summary>
        /// <param name = "fv" > File path</param>
        /// <param name = "crossWord" > Crossword object</param>
        /// <param name = "header" > Text or comment on top of a grid</param>
        public static void PrintInitialData(string fv, CrossWord crossWord, string header)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(header);
                fr.WriteLine();
                for (int i = 0; i < crossWord.Rows; i++)
                {
                    for (int j = 0; j < crossWord.Columns; j++)
                    {
                        fr.Write(crossWord.GetLetter(i, j) + " ");// Write each letter with a space separator.
                    }
                    if (i < crossWord.WordsCount)
                    {
                        fr.WriteLine(" " + (i + 1) + ". " + crossWord.GetWord(i)); // If the row index is in the word count, print the word in the last column.
                    }
                    else
                    {
                        fr.WriteLine();
                    }
                }
                fr.WriteLine();
            }
        }
        /// <summary>
        /// Method that prints results data to a file.
        /// </summary>
        /// <param name="fv">FIle path</param>
        /// <param name="crossWord">Crossword object</param>
        /// <param name="header">Text or comment on top of a grid</param>
        public static void PrintResults(string fv, CrossWord crossWord, string header)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine();
                fr.WriteLine(header);
                fr.WriteLine();
                int[] columnSums = TaskUtils.CalculateColumnSums(crossWord);
                for (int i = 0; i < crossWord.Rows; i++)
                {
                    for (int j = 0; j < crossWord.Columns; j++)
                    {
                        fr.Write(crossWord.GetLetter(i, j) + " ");

                    }
                    if (i < crossWord.WordsCount)
                    {
                        fr.WriteLine(" " + (i + 1) + ". " + crossWord.GetWord(i));
                    }
                    else
                    {
                        fr.WriteLine();
                    }
                }
                fr.WriteLine();
                for (int j = 0; j < crossWord.Columns; j++)
                {
                    fr.Write(columnSums[j] + " ");
                }
                fr.WriteLine("Kontrolinės sumos teisingumui patikrinti");
                fr.WriteLine();
            }
        }
    }
}
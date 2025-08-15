using System;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Lab1.Rekursija.MyClasses
{
    /// <summary>
    /// A class to perform recursive function
    /// </summary>
    public static class TaskUtils
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
        /// Method that finds a first letter of a looking word and calls a recursive function.
        /// </summary>
        /// <param name="crossword">Crossword grid</param>
        public static void Solve(CrossWord crossword)
        {
            for (int w = 0; w < crossword.WordsCount; w++) //Iteration through every word that needs to be found.
            {
                string word = crossword.GetWord(w); //A word in words array.
                int wordNumber = w + 1; ; //Adding a number to the word.
                for (int i = 0; i < crossword.Rows; i++) //Iteration through rows.
                {
                    for (int j = 0; j < crossword.Columns; j++) //Iteration through columns.
                    {
                        if (crossword.GetLetter(i, j) == word[0]) //Looking if a tablecell contains word's first letter.
                        {
                            FindWordRecursive(crossword, word, i, j, 0, wordNumber); //Looking for the rest part of the word by calling a recursive method.                        
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Recursive method that tries to find a word in a crossword by searching four directions.
        /// </summary>
        /// <param name="crossword">A crossword object</param>
        /// <param name="word">A word that is being searched</param>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <param name="index">Letters index</param>
        /// <param name="wordNumber">A number which will be writen to a grid</param>
        /// <returns>True, if a searching word is found</returns>
        public static bool FindWordRecursive(CrossWord crossword, string word, int row, int col, int index, int wordNumber)
        {
            if (index == word.Length) //If a words last letter is reached, a word is found.
            {
                return true; 
            }
            if (row < 0 || row >= crossword.Rows || col < 0 || col >= crossword.Columns) //A row can't be below or on top of a grid, also a collumn can't be besides a grid.
            {
                return false;
            }
            if (crossword.GetLetter(row, col) != word[index]) //Checks if a current letter is also a letter of a word.
            {
                return false;
            }
            char originalLetter = crossword.GetLetter(row, col); //Stores an original letter.
            crossword.AddLetter(row, col, (char)(wordNumber + '0')); //Marks a cell by word's number.

            //Recursive looking for a word in every direction.
            bool found = FindWordRecursive(crossword, word, row + 1, col, index + 1, wordNumber) || //Down.
                         FindWordRecursive(crossword, word, row, col + 1, index + 1, wordNumber) || //To the right.
                         FindWordRecursive(crossword, word, row - 1, col, index + 1, wordNumber) || //Up.
                         FindWordRecursive(crossword, word, row, col - 1, index + 1, wordNumber); //To the left.
            if (!found)
            {
                crossword.AddLetter(row, col, originalLetter); ; //If a word is not found, original letter is restored.
            }
            return found;
        }
        /// <summary>
        /// A method to set colours to a text of a crossword by index.
        /// </summary>
        /// <param name="index">index of an object</param>
        /// <param name="tableCell">cell which color is changing</param>
        public static void GetColor(int index, TableCell tableCell)
        {
            if (index == 0)
            {
                tableCell.ForeColor= System.Drawing.Color.Blue;
            }
            else if (index == 1)
            {
                tableCell.ForeColor = System.Drawing.Color.Green;
            }
            else if (index == 2)
            {
                tableCell.ForeColor = System.Drawing.Color.DeepPink;
            }
            else if (index == 3)
            {
                tableCell.ForeColor = System.Drawing.Color.Brown;
            }
            else if (index == 4)
            {
                tableCell.ForeColor = System.Drawing.Color.Orange;
            }
        }
        /// <summary>
        /// Method calculates the sum of each column in the crossword grid by word numbers.
        /// </summary>
        /// <param name="crossword">Crossword object</param>
        /// <returns>An array with every column sum</returns>
        public static int[] CalculateColumnSums(CrossWord crossword)
        {
            int[] columnSums = new int[crossword.Columns]; //An array to store the sums of each column.
            for (int col = 0; col < crossword.Columns; col++) //Iteration through every column.
            {
                int columnSum = 0;
                for (int row = 0; row < crossword.Rows; row++) //Iteration through every row in the current column.
                {
                    int letterValue = 0; //Letter value at the beginning.
                    char letter = crossword.GetLetter(row, col); //Getting the letter from the crossword grid.

                    //Setting a letter value to integer if the letter is a number of a word.
                    if (letter == '1')
                    {
                        letterValue = 1;
                    }
                    else if (letter == '2')
                    {
                        letterValue = 2;
                    }
                    else if (letter == '3')
                    {
                        letterValue = 3;
                    }
                    else if (letter == '4')
                    {
                        letterValue = 4;
                    }
                    else if (letter == '5')
                    {
                        letterValue = 5;
                    }
                    columnSum += letterValue; //Adding letter's value to the current column sum.
                }
                columnSums[col] = columnSum; //Storing sums in an array.
            }
            return columnSums;
        }
    }
}




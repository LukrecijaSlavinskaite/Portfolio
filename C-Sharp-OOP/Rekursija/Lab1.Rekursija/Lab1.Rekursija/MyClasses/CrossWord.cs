using System.Collections.Generic;

namespace Lab1.Rekursija.MyClasses
{
    /// <summary>
    /// A class that represents a crossword grid with letters and words array.
    /// </summary>
    public class CrossWord
    {
        private char[,] Grid; //Crossword grid with letters.
        private List<string> Words; //Words list, that need to be founded in a crossword.
        public int Rows { get; set; } //Crossword rows.
        public int Columns { get; set; } //Crossword columns.
        public int WordsCount { get; set; }

        public WebForm1 WebForm1
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// A constructor for a crossword.
        /// </summary>
        /// <param name="rows">Rows in a crossword </param>
        /// <param name="columns">Columns in a crossword </param>
        /// <param name="wordsCount">Words that need to be founded </param>
        public CrossWord(int rows, int columns, int wordsCount)
        {
            Rows = rows;
            Columns = columns;
            WordsCount = wordsCount;
            Grid = new char[rows, columns];
            Words = new List<string>(wordsCount);
        }
        /// <summary>
        /// Gets a word from an array by index.
        /// </summary>
        /// <param name="i">Word's index in an array</param>
        /// <returns>A word by index</returns>
        public string GetWord(int i) { return Words[i]; }
        /// <summary>
        /// Add a word by index.
        /// </summary>
        /// <param name="word">A word that needs to be added</param>
        public void AddWord(string word)
        {
            Words.Add(word);
        }
        /// <summary>
        /// Gets a simbol from crossword by row and column.
        /// </summary>
        /// <param name="row">A row index in a crossword</param>
        /// <param name="column">A column index in a crossword</param>
        /// <returns>A simbol from asked place in a crossword</returns>
        public char GetLetter(int row, int column) { return Grid[row, column]; }
        /// <summary>
        /// Adds a simbol in a crossword.
        /// </summary>
        /// <param name="row">A row index in a crossword</param>
        /// <param name="column">A column index in a crossword</param>
        /// <param name="letter">A simbol that is added</param>
        public void AddLetter(int row, int column, char letter) { Grid[row, column] = letter; }
        /// <summary>
        /// Creating an override for a method.
        /// </summary>
        /// <returns>Value converted to string</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

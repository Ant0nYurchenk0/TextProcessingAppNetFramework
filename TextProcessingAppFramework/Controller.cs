using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextProcessingApp
{
    internal static class Controller
    {
        /// <summary>
        /// form a dictionary with words and their positions in a text file
        /// </summary>
        internal static void ParseFile(string path)
        {
            var lineList = File.ReadAllLines(path).Where(line => line != string.Empty).ToList();
            for(int line = 0; line < lineList.Count; line++)
            {
                lineList[line] = removeChars(lineList[line]);
                var wordList = lineList[line].Split(' ').Where(word => word != string.Empty).ToList();
                for(int word = 0; word < wordList.Count; word++)
                {
                    insertPosition(wordList[word].ToLower(), line, word);
                }
            }
            Global.WordDictionary = Global.WordDictionary
                .OrderByDescending(word => word.Value.Count)
                .ToDictionary(word => word.Key, word => word.Value);
        }
        /// <summary>
        /// out the dictionary to the table 
        /// </summary>
        internal static void ShowDictionary(DataGridView dictionaryTable)
        {
            // adding two columns for word and count
            dictionaryTable.Columns.Add(new DataGridViewTextBoxColumn{Name = Global.WORD_COLUMN});
            dictionaryTable.Columns.Add(new DataGridViewTextBoxColumn{Name = Global.FREQUENCY_COLUMN});
            
            foreach (var word in Global.WordDictionary)
            {
                dictionaryTable.Rows.Add(new DataGridViewRow());
                dictionaryTable.Rows[dictionaryTable.RowCount - 1].Cells[0].Value = word.Key;  
                dictionaryTable.Rows[dictionaryTable.RowCount - 1].Cells[1].Value = word.Value.Count;  
            }
            numerateRows(dictionaryTable);
        }
        /// <summary>
        ///out list of positions to the table
        /// </summary>
        internal static void ShowRepetitions(string word, DataGridView repetitionsTable)
        {
            if (Global.WordDictionary.TryGetValue(word, out var positionList))
            {
                // adding two columns for line and word order in a line
                repetitionsTable.Columns.Add(new DataGridViewTextBoxColumn{Name = Global.LINE_COLUMN});
                repetitionsTable.Columns.Add(new DataGridViewTextBoxColumn{Name = Global.WORD_NUM_COLUMN});
                foreach (var position in positionList)
                {
                    repetitionsTable.Rows.Add(new DataGridViewRow());
                    repetitionsTable.Rows[repetitionsTable.RowCount - 1].Cells[0].Value = position.Line.ToString();
                    repetitionsTable.Rows[repetitionsTable.RowCount - 1].Cells[1].Value = position.Word.ToString();
                }
                numerateRows(repetitionsTable);
            }
            else
            {
                throw new Exception(Global.SearchError);
            }
        }
        /// <summary> 
        /// clear tables
        /// </summary>
        internal static void ResetTable(DataGridView table)
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.Refresh();
        }
        /// <summary>
        /// remove redundant chars from a string
        /// </summary>
        private static string removeChars(string line)
        {
            foreach (var character in Global.CharsToRemove)
            {
                line = line.Replace(character, string.Empty);
            }
            return line;
        }

        /// <summary>
        /// add new position to the list of positions in the dictionary
        /// </summary>
        private static void insertPosition(string keyWord, int line, int word)
        {
            if (Global.WordDictionary.TryGetValue(keyWord, out var positionList))
            {
                positionList.Add(new Position(line + 1, word + 1));
            }
            else
            {
                Global.WordDictionary.Add(keyWord, new List<Position>());
                Global.WordDictionary[keyWord].Add(new Position(line + 1, word + 1));
            }
        }
        /// <summary>
        /// enumarate each row of a table
        /// </summary>
        private static void numerateRows(DataGridView table)
        {
            foreach(DataGridViewRow row in table.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }
    }
}

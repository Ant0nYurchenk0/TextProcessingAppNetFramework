using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextProcessingApp
{
    internal static class Controller
    {
        internal static void ParseFile(string path)
        {
            var lineList = File.ReadAllLines(path).Where(line => line != string.Empty).ToList();
            for(int line = 0; line < lineList.Count; line++)
            {
                lineList[line] = removeChars(lineList[line]);
                var wordList = lineList[line].Split(' ').Where(word => word != string.Empty).ToList();
                for(int word = 0; word < wordList.Count; word++)
                {
                    var keyWord = wordList[word].ToLower();
                    if (Global.WordDictionary.TryGetValue(keyWord, out var positionList))
                    {
                        positionList.Add(new Position(line+1, word+1));
                    }
                    else
                    {
                        Global.WordDictionary.Add(keyWord, new List<Position>());
                        Global.WordDictionary[keyWord].Add(new Position(line + 1, word + 1));
                    }
                }
            }
            Global.WordDictionary = Global.WordDictionary.OrderByDescending(word => word.Value.Count).ToDictionary(word => word.Key, word => word.Value);
        }

        internal static void ShowDictionary(DataGridView dictionaryTable)
        {
            // adding two columns for word and count
            dictionaryTable.Columns.Add(new DataGridViewTextBoxColumn{Name = "Word"});
            dictionaryTable.Columns.Add(new DataGridViewTextBoxColumn{Name = "Frequency"});
            
            foreach (var word in Global.WordDictionary)
            {
                dictionaryTable.Rows.Add(new DataGridViewRow());
                dictionaryTable.Rows[dictionaryTable.RowCount - 1].Cells[0].Value = word.Key;  
                dictionaryTable.Rows[dictionaryTable.RowCount - 1].Cells[1].Value = word.Value.Count;  
            }
        }
        internal static void ShowRepetitions(string word, DataGridView repetitionsTable)
        {
            if (Global.WordDictionary.TryGetValue(word, out var positionList))
            {
                repetitionsTable.Columns.Add(new DataGridViewTextBoxColumn{Name = "Line"});
                repetitionsTable.Columns.Add(new DataGridViewTextBoxColumn{Name = "Word No."});
                foreach (var position in positionList)
                {
                    repetitionsTable.Rows.Add(new DataGridViewRow());
                    repetitionsTable.Rows[repetitionsTable.RowCount - 1].Cells[0].Value = position.Line.ToString();
                    repetitionsTable.Rows[repetitionsTable.RowCount - 1].Cells[1].Value = position.Word.ToString();
                }
            }
            else
            {
                throw new Exception(Global.SearchError);
            }
        }
        internal static void ResetTable(DataGridView table)
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.Refresh();
        }
        private static string removeChars(string line)
        {
            foreach (var character in Global.CharsToRemove)
            {
                line = line.Replace(character, string.Empty);
            }
            return line;
        }
    }
}

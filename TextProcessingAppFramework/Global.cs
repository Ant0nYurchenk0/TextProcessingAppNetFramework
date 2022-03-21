using System.Collections.Generic;

namespace TextProcessingApp
{
    internal static class Global
    {
        internal static Dictionary<string, List<Position>> WordDictionary =
            new Dictionary<string, List<Position>>();
        internal static string SearchError = "Error: No such word";
        internal static string[] CharsToRemove = { ".", ",", ";", ":", "\'", "\"" };
        internal static string FileFilters = "Text files (*.txt)|*.txt|" + "All files (*.*)|*.*";
        internal static string DoubleClick = "Double-click a word to copy";

        internal const string WORD_COLUMN = "Word";
        internal const string FREQUENCY_COLUMN = "Frequency";
        internal const string LINE_COLUMN = "Line No";
        internal const string WORD_NUM_COLUMN = "Word No";
    }
}

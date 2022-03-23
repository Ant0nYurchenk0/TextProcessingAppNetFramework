using System.Collections.Generic;

namespace TextProcessingAppFramework
{
    internal static class Global
    {
        internal static Dictionary<string, List<Position>> WordDictionary =
            new Dictionary<string, List<Position>>();

        internal static readonly string[] CharsToRemove = { ".", ",", ";", ":", "\'", "\"" };

        internal const string SEARCH_ERROR = "Error: No such word";
        internal const string FILE_FILTERS = "Text files (*.txt)|*.txt|" + "All files (*.*)|*.*";
        internal const string TEXT_BOX_COPY_HINT = "Double-click a word to copy";

        internal const string WORD_COLUMN = "Word";
        internal const string FREQUENCY_COLUMN = "Frequency";
        internal const string LINE_COLUMN = "Line No";
        internal const string WORD_NUM_COLUMN = "Word No";
    }
}


namespace TextProcessingAppFramework
{
    internal struct Position
    {
        internal int Word { get ; private set; }
        internal int Line { get; private set; }


        public Position(int line, int word)
        {
            Line = line;
            Word = word;
        }

    }
}


namespace TextProcessingApp
{
    internal struct Position
    {
        public int Word { get => character; }
        public int Line { get => line; }

        private int line;
        private int character;

        public Position(int line, int character)
        {
            this.line = line;
            this.character = character;
        }

    }
}

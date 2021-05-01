namespace Battleships.Domain
{
    public struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(string column, string row)
        {
            X = column[0] - 'A' + 1;
            Y = int.Parse(row);
        }
    }
}
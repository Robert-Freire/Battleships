namespace Battleships.Domain
{
    public enum Direction
    {
        Horizontal, Vertical,
    }
    public enum Status
    {
        OutOfBoard, Afloat, Sunk
    }

    public enum SalvoResult{
        Miss, Hit, Sunk, GameOver, OutOfBoard
    }
}
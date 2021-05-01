namespace Battleships.Domain
{
    public interface IGameConfiguration
    {
        const int SizeBattleship = 5;
        const int SizeFrigate = 4;
        int XSize { get; }
        int YSize { get; }
        int NumBattleships { get; }
        int NumFrigates { get; }
    }

    public class GameConfiguration : IGameConfiguration
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }
        public int NumBattleships { get; private set; }
        public int NumFrigates { get; private set; }

        public GameConfiguration(int xSize, int ySize, int numBattleships, int numFrigates)
        {
            this.XSize = xSize;
            this.YSize = ySize;
            this.NumBattleships = numBattleships;
            this.NumFrigates = numFrigates;
        }
    }
}
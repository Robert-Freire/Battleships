using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Domain
{
    public class Game
    {
        private readonly IGameConfiguration gameConfiguration;
        public IList<Ship> Ships { get; set; } = new List<Ship>();

        public Game(IGameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }
        
        public void Initialize()
        {
            var ships = new List<Ship>();
            ships.AddRange(Enumerable.Range(0, gameConfiguration.NumBattleships).Select(el => new Ship(IGameConfiguration.SizeBattleship)));
            ships.AddRange(Enumerable.Range(0, gameConfiguration.NumFrigates).Select(el => new Ship(IGameConfiguration.SizeFrigate)));
            ships.ForEach((s) => PlaceShip(s));
        }

        private void PlaceShip(Ship ship)
        {            
            var rand = new Random();
            Direction direction = (Direction) rand.Next(2);
            var cells = GetValidCells(ship.Size, direction);
            Ships.Add(ship.Place(cells.ElementAt(rand.Next(cells.Count())), direction));
        }
        public IEnumerable<Cell> GetValidCells(int shipSize, Direction direction)
        {
            (int, int) GetValidStartBoard(int shipSize, Direction direction) =>
                (direction == Direction.Horizontal) ?
                            (gameConfiguration.XSize - shipSize, gameConfiguration.YSize) :
                            (gameConfiguration.XSize, gameConfiguration.YSize - shipSize);

            IEnumerable<Cell> CreateBoardAvoidingClashShips(int XLength, int YLength, int shipSize, Direction direction) =>
                Enumerable.Range(0, XLength * YLength)
                                .Select(el => new Cell() { X = (el / YLength) + 1, Y = (el % YLength) + 1 })
                                .Where(p => !Ships.Any(s => s.IsInShadow(shipSize, direction, p)));

            var (XLength, YLength) = GetValidStartBoard(shipSize, direction);
            return CreateBoardAvoidingClashShips(XLength, YLength, shipSize, direction);
        }

        private bool CellIsInsideBoard(Cell cell) => (cell.X > 0) && (cell.Y > 0) && (cell.X <= gameConfiguration.XSize) && (cell.Y <= gameConfiguration.YSize);
        private bool IsGameOver() => Ships.All(s => s.Status == Status.Sunk);
        public SalvoResult Shot(Cell cell)
        {
            if (!CellIsInsideBoard(cell))
            {
                return SalvoResult.OutOfBoard;
            }

            var hitShip = Ships.FirstOrDefault(s => s.Shot(cell));
            if (hitShip == null)
            {
                return SalvoResult.Miss;
            }

            return IsGameOver() ? SalvoResult.GameOver : (hitShip.Status == Status.Sunk) ? SalvoResult.Sunk : SalvoResult.Hit;
        }
    }
}
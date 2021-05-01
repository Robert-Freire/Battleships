using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Domain
{
    public class Ship
    {
        private IList<Cell> ShipParts;
        public Status Status { get; private set; }
        public int Size { get; }

        public Ship(int size)
        {
            this.Size = size;
            this.Status = Status.OutOfBoard;
        }

        /// <summary>
        /// This method return true if a cell is in the "shadow" of a ship.
        /// We can understand the shadow as the cells besides the ship in which you cannot place another ship because it will cut the current ship
        /// </summary>
        /// <param name="sizeShadow"> it's the size of the ship that we want to place. </param>
        /// <param name="directionShadow"> It's the direction of the ship that we wwant to place </param>
        /// <param name="cell"> The cell that we want to see if is inside the shadow defined by the two previous parameter</param>
        /// <returns>true if the cell is in the "shadow"</returns>
        public bool IsInShadow(int sizeShadow, Direction directionShadow, Cell cell) =>
            directionShadow == Direction.Horizontal ?
                ShipParts.Any(sp => sp.Y == cell.Y) && (cell.X >= ShipParts.Select(sp => sp.X).Min() - sizeShadow && cell.X <= ShipParts.Select(sp => sp.X).Max() + sizeShadow) :
                ShipParts.Any(sp => sp.X == cell.X) && (cell.Y >= ShipParts.Select(sp => sp.Y).Min() - sizeShadow && cell.X <= ShipParts.Select(sp => sp.Y).Max() + sizeShadow);

        public Ship Place(Cell cell, Direction direction)
        {
            var (xInc, yInc) = direction == Direction.Horizontal ? (1, 0) : (0, 1);
            this.ShipParts = Enumerable.Range(0, this.Size).Select(r => new Cell() { X = cell.X + (r * xInc), Y = cell.Y + (r * yInc) }).ToList();
            this.Status = Status.Afloat;
            return this;
        }

        public bool Shot(Cell cell)
        {
            var result = ShipParts.Remove(cell);
            if (!ShipParts.Any()) this.Status = Status.Sunk;
            return result;
        }
    }
}

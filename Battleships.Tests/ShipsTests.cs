using Battleships.Domain;
using Xunit;

namespace Battleships.Tests
{
    public class ShipsTests
    {
        [Theory]
        [InlineData(2, 0, Direction.Horizontal, true)]
        [InlineData(3, 0, Direction.Horizontal, false)]
        [InlineData(0, 2, Direction.Vertical, true)]
        [InlineData(0, 3, Direction.Vertical, false)]
        public void IsInShadow_ForAShipSize4With2Shadow__returnsIsInShadow(int xShift, int yShift, Direction shadowDirection, bool expectedResult)
        {
            var ship = new Ship(4);
            var shipStartCell = new Cell(){X= 3, Y=1};

            ship.Place(shipStartCell, shadowDirection);
            var cellToTest = new Cell() { X = shipStartCell.X - xShift, Y = shipStartCell.Y - yShift};

            var result = ship.IsInShadow(2, shadowDirection, cellToTest);

            Assert.True(result == expectedResult);
        }
    }
}


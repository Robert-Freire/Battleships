using Battleships.Domain;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Battleships.Tests
{
    public class GameTests
    {
        [Fact]
        public void Place_ForAEmptyBoard4by4AShip3PlacedHorizontal_returns1x4Board()
        {
            var configurationMock = new Mock<IGameConfiguration>();

            configurationMock.SetupGet(c => c.XSize).Returns(4);
            configurationMock.SetupGet(c => c.YSize).Returns(4);

            var game = new Game(configurationMock.Object);

            var result = game.GetValidCells(3, Direction.Horizontal);

            var expectedResult = new[] { new Cell { X = 1, Y = 1 }, new Cell { X = 1, Y = 2 }, new Cell { X = 1, Y = 3 }, new Cell { X = 1, Y = 4 } };
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Place_ForABoard4by4Whitha2SizeShipInB2HorizontalaShip3PlacedVertical_returns1x2Board()
        {
            var configurationMock = new Mock<IGameConfiguration>();

            configurationMock.SetupGet(c => c.XSize).Returns(4);
            configurationMock.SetupGet(c => c.YSize).Returns(4);

            var game = new Game(configurationMock.Object)
            {
                Ships = new List<Ship>() { new Ship(2).Place(new Cell { X = 2, Y = 3 }, Direction.Horizontal) }
            };

            var result = game.GetValidCells(3, Direction.Vertical);

            var expectedResult = new[] { new Cell { X = 1, Y = 1 }, new Cell { X = 4, Y = 1 } };
            Assert.Equal(expectedResult, result);
        }
    }
}

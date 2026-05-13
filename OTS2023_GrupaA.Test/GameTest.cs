using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.NUnit;
using System.Game.cs;

namespace OTS2026_GrupaA.Test
{
    [TestFixture]
    internal class GameTest
    {
        private Game game;
        [Setup]
        public void setup()
        {
            game = new Game(Position(1, 2, 0), Position(1, 24, 0));
        }

        // Valid: Matrix[0,0,0], Matrix[30,30,30], Standard(Empty, RevealHiddenItem, Gold)
        // Chosen: [1,2,0]
        // Invalid: Matrix[5,5,0], [5,5,30], [9,5,30], [9, 24, 30], [25, 5, 0], [25,5,30], any number position below 0, MapBarrier (two cubes on the map), Hidden (unless he got RevealHiddenItem)
        [Test]
        public void MovePlayer_PlayerMovedUp_Success()
        {
            // Assert
            // ovde smo namestili u [Setup] delu odredjivanje klase gde smo stavili poziciju igraca i mete

            // Act
            game.MovePlayer(Up);

            // Assert
            Assert.That(game.Player.Y, Is.EqualTo(3));
        }

        // Valid: [0,0,0], [30,30,30]
        // Chosen: [1,3,0], [1,2,3]
        // Invalid: matrix coordinates < 0, matrix coordinates > 30, Matrix[5,5,0], [5,5,30], [9,5,30], [9, 24, 30], [25, 5, 0], [25,5,30], MapBarrier (two cubes on the map), Hidden (unless he got RevealHiddenItem)
        [TestCase(1, 3, 0, ExpectedResult = true)]
        [TestCase(1, 2, 3, ExpectedResult = true)]
        public void ValidatePosition_PlayerPositionIsValid_Success(int x, int y, int z)
        {
            // Assert
            Position position = new Position(x, y, z);

            // Act
            bool test = game.ValidatePosition(position);

            // Assert
            return test;
        }

        // Valid: [0,0,0], [30,30,30]
        // Chosen: [-29347, -1, 0]
        // Invalid: matrix coordinates < 0, matrix coordinates > 30, Matrix[5,5,0], [5,5,30], [9,5,30], [9, 24, 30], [25, 5, 0], [25,5,30], MapBarrier (two cubes on the map), Hidden (unless he got RevealHiddenItem)
        [Test]
        public void ValidatePosition_PlayerIsOutOfBounds_Exception()
        {
            // Assert
            Position position = new Position(-29347, -1, 0);

            // Act
            PositionOutsideOfMapException exception = Assert.Throw<PositionOutsideOfMapException>((TestDelegate)(() => game.ValidatePosition(position)));

            // Assert
            Assert.That(exception, Is.EqualTo(false));
        }

        // Valid: [0,0,0], [30,30,30]
        // Chosen: [1, 2, 3], [3, 5, 0]
        // Invalid: matrix coordinates < 0, matrix coordinates > 30, Matrix[5,5,0], [5,5,30], [9,5,30], [9, 24, 30], [25, 5, 0], [25,5,30], MapBarrier (two cubes on the map), Hidden (unless he got RevealHiddenItem)
        [TestCase(1, 2, 3, true)]
        [TestCase(3, 5, 0, true)]
        public void ValidatePositionInsideMap_MapPositionIsValid_Success(int x, int y, int z, bool expRes)
        {
            // Assert
            Position position = new Position(x, y, z);

            // Act
            bool test = game.ValidatePositionInsideMap(position);

            // Assert
            Assert.That(test.Is.EqualTo(expRes));
        }

        // Valid: [0,0,0], [30,30,30]
        // Chosen: [-2367, 0, -5]
        // Invalid: matrix coordinates < 0, matrix coordinates > 30, Matrix[5,5,0], [5,5,30], [9,5,30], [9, 24, 30], [25, 5, 0], [25,5,30], MapBarrier (two cubes on the map), Hidden (unless he got RevealHiddenItem)
        [TestCase(-2367, 0, -5, ExpectedResult = false]
        public void ValidatePositionInsideMap_MapPositionIsOutOfBoundsOrBadPlacement_Exception(int x, int y, int z)
        {
            // Assert
            Position position = new Position(x, y, z);

            // Act
            PositionOutsideOfMapException exception = Assert.Throw<PositionOutsideOfMapException>((TestDelegate)(() => game.ValidatePositionInsideMap(position));

            // Assert
            return exception;
        }

        // Valid: [0,0,0], [30,30,30], [4, 2, 0]
        // Chosen: [4, 2, 0]
        // Invalid: matrix coordinates < 0, matrix coordinates > 30, Matrix[5,5,0], [5,5,30], [9,5,30], [9, 24, 30], [25, 5, 0], [25,5,30], MapBarrier (two cubes on the map), Hidden (unless he got RevealHiddenItem)
        [Test]
        public void CollectItems_PlayerCollectedGold_Success()
        {
            // Assert
            game = new Game(Position(4, 2, 0), Position(1, 24, 0))

            // Act
            game.CollectItems();

            // Arrange
            Assert.That(game.Player.AmmountOfGold, Is.EqualTo(1));
        }

        // Valid: [0,0,0], [30,30,30], [13, 3, 0], [1, 24, 0]
        // Chosen: [13, 3, 0]
        // Invalid: matrix coordinates < 0, matrix coordinates > 30, Matrix[5,5,0], [5,5,30], [9,5,30], [9, 24, 30], [25, 5, 0], [25,5,30], MapBarrier (two cubes on the map), Hidden (unless he got RevealHiddenItem)
        [Test]
        public void CollectedItems_PlayerCollectedHiddenGold_Success()
        {
            // Assert
            game = new Game(Position(13, 3, 0), Position(1, 24, 0))
            game.Player.CanRevealHidden(true);

            // Act
            game.CollectItems();

            // Arrange
            Assert.That(game.Player.AmmountOfHiddenGold, Is.EqualTo(1));
        }

        [TestCaseSource(typeof(PICTModel), {DataSource}, object[] obj = {"PICTResult.txt"})]
        public void Test()
        {
            
        }
    }
}

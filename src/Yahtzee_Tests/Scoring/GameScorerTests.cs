using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yahtzee_App;

namespace Yahtzee_Tests
{
    [TestClass]
    public class GameScorerTests
    {
        [TestMethod]
        public void GameScorerTest()
        {
            // Arrange
            var rollNumbers = new int[] { 4, 5, 2, 3, 5 };

            // Act
            var scoreCard = GameScorer.Score(rollNumbers);

            // Assert
            Assert.AreEqual(0, scoreCard.Ones);
            Assert.AreEqual(2, scoreCard.Twos);
            Assert.AreEqual(3, scoreCard.Threes);
            Assert.AreEqual(4, scoreCard.Fours);
            Assert.AreEqual(10, scoreCard.Fives);
            Assert.AreEqual(0, scoreCard.Sixes);
            Assert.AreEqual(0, scoreCard.ThreeOfAKind);
            Assert.AreEqual(0, scoreCard.FourOfAKind);
            Assert.AreEqual(0, scoreCard.FullHouse);
            Assert.AreEqual(30, scoreCard.SmallStraight);
            Assert.AreEqual(0, scoreCard.LargeStraight);
            Assert.AreEqual(0, scoreCard.Yahtzee);
            Assert.AreEqual(19, scoreCard.Chance);

            // I realize this isn't a "unit" test per se, but it effectively
            // tests the scoring algorithm as a whole
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "5 numbers must be provided")]
        public void GameScorer_RollNumbersIsNull()
        {
            GameScorer.Score(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "5 numbers must be provided")]
        public void GameScorer_TooManyRollNumbers()
        {
            // Arrange
            var rollNumbers = new int[] { 4, 5, 2, 3, 5, 1 };

            // Act
            GameScorer.Score(rollNumbers);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "All 5 numbers must be between 1 and 6")]
        public void GameScorer_ContainsTooSmallRollNumber()
        {
            // Arrange
            var rollNumbers = new int[] { 4, 5, 2, 3, 0 };

            // Act
            GameScorer.Score(rollNumbers);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "All 5 numbers must be between 1 and 6")]
        public void GameScorer_ContainsTooLargeRollNumber()
        {
            // Arrange
            var rollNumbers = new int[] { 4, 5, 2, 7, 5 };

            // Act
            GameScorer.Score(rollNumbers);

            // Assert
        }
    }
}

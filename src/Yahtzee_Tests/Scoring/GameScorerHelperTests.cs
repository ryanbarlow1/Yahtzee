using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yahtzee_App;

namespace Yahtzee_Tests
{
    [TestClass]
    public class GameScorerHelperTests
    {
        [TestMethod]
        public void UpperSectionItem_Test()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 3, 4, 2, 3 });

            //Act
            var score = scorer.UpperSectionItem(3);

            //Assert
            Assert.AreEqual(6, score);
        }

        [TestMethod]
        public void xOfAKind_Test()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 3, 3, 2, 3 });

            //Act
            var score = scorer.xOfAKind(3);

            //Assert
            Assert.AreEqual(12, score);
        }

        [TestMethod]
        public void xOfAKind_Test_ReturnsZero()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 3, 4, 2, 3 });

            //Act
            var score = scorer.xOfAKind(3);

            //Assert
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void FullHouse_Test()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 2, 1, 2, 1 });

            //Act
            var score = scorer.FullHouse();

            //Assert
            Assert.AreEqual(25, score);
        }

        [TestMethod]
        public void FullHouse_Test_2()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 1, 2, 2, 2 });

            //Act
            var score = scorer.FullHouse();

            //Assert
            Assert.AreEqual(25, score);
        }

        [TestMethod]
        public void FullHouse_Test_ReturnsZero()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 3, 4, 2, 3 });

            //Act
            var score = scorer.FullHouse();

            //Assert
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void FullHouse_Test_ReturnsZero_2()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 2, 4, 2, 4 });

            //Act
            var score = scorer.FullHouse();

            //Assert
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void LargeStraight_Test()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 3, 5, 4, 2 });

            //Act
            var score = scorer.LargeStraight();

            //Assert
            Assert.AreEqual(40, score);
        }

        [TestMethod]
        public void LargeStraight_Test_ReturnsZero()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 2, 1, 3, 4, 3 });

            //Act
            var score = scorer.LargeStraight();

            //Assert
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void SmallStraight_Test()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 3, 6, 4, 2 });

            //Act
            var score = scorer.SmallStraight();

            //Assert
            Assert.AreEqual(30, score);
        }

        [TestMethod]
        public void SmallStraight_Test_ReturnsZero()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 2, 1, 3, 1, 3 });

            //Act
            var score = scorer.SmallStraight();

            //Assert
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void Yahtzee_Test()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 1, 1, 1, 1, 1 });

            //Act
            var score = scorer.Yahtzee();

            //Assert
            Assert.AreEqual(50, score);
        }

        [TestMethod]
        public void Chance_Test()
        {
            // Arrange
            var scorer = new GameScorerHelper(new[] { 3, 6, 3, 1, 4 });

            //Act
            var score = scorer.Chance();

            //Assert
            Assert.AreEqual(17, score);
        }
    }
}

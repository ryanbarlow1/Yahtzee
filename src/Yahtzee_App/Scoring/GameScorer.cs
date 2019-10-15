using System;
using System.Linq;

namespace Yahtzee_App
{
    public static class GameScorer
    {
        public static ScoreCard<int> Score(int[] rollNumbers)
        {
            if (rollNumbers == null || rollNumbers.Length != 5)
                throw new ArgumentException("5 numbers must be provided");

            if (rollNumbers.Any(n => n < 1) || rollNumbers.Any(n => n > 6))
                throw new ArgumentException("All 5 numbers must be between 1 and 6");

            var scoreCard = new ScoreCard<int>();
            var scorer = new GameScorerHelper(rollNumbers);

            scoreCard.Ones = scorer.UpperSectionItem(1);
            scoreCard.Twos = scorer.UpperSectionItem(2);
            scoreCard.Threes = scorer.UpperSectionItem(3);
            scoreCard.Fours = scorer.UpperSectionItem(4);
            scoreCard.Fives = scorer.UpperSectionItem(5);
            scoreCard.Sixes = scorer.UpperSectionItem(6);
            scoreCard.ThreeOfAKind = scorer.xOfAKind(3);
            scoreCard.FourOfAKind = scorer.xOfAKind(4);
            scoreCard.FullHouse = scorer.FullHouse();
            scoreCard.SmallStraight = scorer.SmallStraight();
            scoreCard.LargeStraight = scorer.LargeStraight();
            scoreCard.Yahtzee = scorer.Yahtzee();
            scoreCard.Chance = scorer.Chance();

            return scoreCard;
        }
    }
}

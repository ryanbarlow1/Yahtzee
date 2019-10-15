using System.Collections.Generic;
using System.Linq;

namespace Yahtzee_App
{
    public class GameScorerHelper
    {
        private int[] rollNumbers;
        private int mode;

        public GameScorerHelper(int[] rollNumbers)
        {
            this.rollNumbers = rollNumbers;
            mode = (from item in rollNumbers // Found on https://stackoverflow.com/questions/2655759/how-to-get-the-most-common-value-in-an-int-array-c
                    group item by item into g
                    orderby g.Count() descending
                    select g.Count()).First();
        }

        public int UpperSectionItem(int numberToCount)
        {
            return rollNumbers.Count(x => x.Equals(numberToCount)) * numberToCount;
        }

        public int xOfAKind(int numberNeeded)
        {
            if (mode >= numberNeeded)
                return rollNumbers.Sum();
            return 0;
        }

        public int FullHouse()
        {
            var groupList = rollNumbers.GroupBy(n => n).OrderByDescending(n => n.Count()).ToArray();

            if (groupList.Count() == 2 && groupList[0].Count() == 3 && groupList[1].Count() == 2)
                return 25;
            return 0;
        }

        public int LargeStraight()
        {
            var one = new int[] { 1, 2, 3, 4, 5 };
            var two = new int[] { 2, 3, 4, 5, 6 };

            var distinctList = rollNumbers.Distinct().OrderBy(s => s).ToArray();

            if (distinctList.SequenceEqual(one) || distinctList.SequenceEqual(two))
                return 40;
            return 0;
        }

        public int SmallStraight()
        {
            var distinctList = rollNumbers.Distinct().OrderBy(s => s).ToArray();

            var hashset = new HashSet<int[]>();
            var one = new int[] { 1, 2, 3, 4, 5 };
            var two = new int[] { 1, 2, 3, 4, 6 };
            var three = new int[] { 2, 3, 4, 5, 6 };
            var four = new int[] { 1, 2, 3, 4 };
            var five = new int[] { 2, 3, 4, 5 };
            var six = new int[] { 1, 3, 4, 5, 6 };
            var seven = new int[] { 3, 4, 5, 6 };

            if (distinctList.SequenceEqual(one) || distinctList.SequenceEqual(two)
                || distinctList.SequenceEqual(three) || distinctList.SequenceEqual(four)
                || distinctList.SequenceEqual(five) || distinctList.SequenceEqual(six)
                || distinctList.SequenceEqual(seven))
                return 30;
            return 0;
        }

        public int Yahtzee()
        {
            if (mode == 5)
                return 50;
            return 0;
        }

        public int Chance()
        {
            return rollNumbers.Sum();
        }
    }
}

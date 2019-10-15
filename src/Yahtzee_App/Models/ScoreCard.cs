namespace Yahtzee_App
{
    public class ScoreCard<T>
    {
        public T Ones { get; set; }
        public T Twos { get; set; }
        public T Threes { get; set; }
        public T Fours { get; set; }
        public T Fives { get; set; }
        public T Sixes { get; set; }
        public T ThreeOfAKind { get; set; }
        public T FourOfAKind { get; set; }
        public T FullHouse { get; set; }
        public T SmallStraight { get; set; }
        public T LargeStraight { get; set; }
        public T Yahtzee { get; set; }
        public T Chance { get; set; }
    }
}

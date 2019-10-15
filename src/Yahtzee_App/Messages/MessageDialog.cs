namespace Yahtzee_App
{
    public static class MessageDialog
    {
        public static string RollClickedText_TwoRollsLeft()
        {
            return $"You have two rolls left. Select a scoring category or roll again.";
        }
        public static string RollClickedText_OneRollLeft()
        {
            return $"You have one roll left. Select a scoring category or roll again.";
        }

        public static string RollClickedText_NoRollsLeft()
        {
            return "You have no rolls left. Select a scoring category.";
        }

        public static string ScoreButtonClicked()
        {
            return "Click Roll to start another turn.";
        }

        public static string GameOverText(int finalScore)
        {
            return $"Game Over. Your final score is {finalScore}.\rClick Roll to play again.";
        }

        public static string Yahtzee()
        {
            return "YAHTZEE!!!\r";
        }
    }
}

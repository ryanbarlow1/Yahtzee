using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Yahtzee_App
{
    public partial class MainWindow : Window
    {
        private int[] rollNumbers;
        private int[] keepNumbers;
        private int numberOfTurns;
        private int numberOfRolls;
        private ScoreCard<bool> isCategoryScored;
        private ScoreCard<int> finalScores;
        private ScoreCard<int> lastRollScores;

        private readonly List<string> ScoreCardNames = new List<string> { "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes",
            "ThreeOfAKind", "FourOfAKind", "FullHouse", "SmallStraight", "LargeStraight", "Yahtzee", "Chance" };

        public MainWindow()
        {
            InitializeStats();
            InitializeComponent();
        }

        private void InitializeStats()
        {
            rollNumbers = new int[5];
            keepNumbers = new int[5];
            numberOfTurns = 0;
            numberOfRolls = 0;
            isCategoryScored = new ScoreCard<bool>();
            finalScores = new ScoreCard<int>();
            lastRollScores = new ScoreCard<int>();
        }

        private void RollButtonClick(object sender, RoutedEventArgs e)
        {
            if (numberOfTurns == 13)
                ResetGame();
            if (numberOfRolls == 0)
                MoveDiceToRollSection();

            numberOfRolls++;
            RollDice();
            CalculateScores();

            if (numberOfRolls == 1)
            {
                EnableScoreButtons();
                SetMessageBlockText(MessageDialog.RollClickedText_TwoRollsLeft());
            }
            if (numberOfRolls == 2)
                SetMessageBlockText(MessageDialog.RollClickedText_OneRollLeft());
            if (numberOfRolls == 3)
            {
                SetMessageBlockText(MessageDialog.RollClickedText_NoRollsLeft());
                RollButton.IsEnabled = false;
            }
        }

        private void RollDice()
        {
            Random random = new Random();
            Array.Sort(rollNumbers);
            Array.Reverse(rollNumbers);
            var keepDiceCount = rollNumbers.Where(n => n > 0).Count();

            for (int i = 0; i < 5; i++)
            {
                var rollImage = GetDiceImage("Roll", i);
                if (i < keepDiceCount)
                {
                    var randomNumber = random.Next(1, 7);
                    rollImage.Source = GetDieResource($"Die{randomNumber}");
                    rollNumbers[i] = randomNumber;
                }
                else
                    rollImage.Source = null;
            }
        }

        private ImageSource GetDieResource(string dieName)
        {
            return (ImageSource)FindResource(dieName);
        }

        private Image GetDiceImage(string area, int index)
        {
            var imageName = $"{area}Dice{index}";
            if (area == "Roll")
                return (Image)RollGrid.FindName(imageName);
            else if (area == "Keep")
                return (Image)KeepGrid.FindName(imageName);
            else
                throw new ArgumentException("Area must be Roll or Keep");
        }

        private void CalculateScores()
        {
            var diceNumbers = new List<int>();
            diceNumbers.AddRange(rollNumbers.Where(n => n > 0));
            diceNumbers.AddRange(keepNumbers.Where(n => n > 0));

            lastRollScores = GameScorer.Score(diceNumbers.ToArray());

            InputScores();
        }

        private void InputScores()
        {
            foreach (string name in ScoreCardNames)
            {
                if (IsCategoryScored(name))
                {
                    var label = GetLabelByName(name + "Label");
                    label.Content = GetScoreByPropertyName(lastRollScores, name);
                }
            }
        }

        private void DiceClick(object sender, MouseButtonEventArgs e)
        {
            var diceImage = (Image)sender;
            var diceNumber = GetDiceNumberValue(diceImage);
            var oldIndexNumber = GetDiceIndexNumber(diceImage);
            var newDiceImageArea = (diceImage.Name.Substring(0, 4)) == "Keep" ? "Roll" : "Keep";
            var newIndexNumber = GetFreeDiceNumber(newDiceImageArea);
            var newImage = GetDiceImage(newDiceImageArea, newIndexNumber);

            newImage.Source = diceImage.Source;
            diceImage.Source = null;

            if (newDiceImageArea == "Roll")
            {
                keepNumbers[oldIndexNumber] = 0;
                rollNumbers[newIndexNumber] = diceNumber;
            }
            else
            {
                rollNumbers[oldIndexNumber] = 0;
                keepNumbers[newIndexNumber] = diceNumber;
            }
        }

        private int GetDiceNumberValue(Image image)
        {
            var uriSource = ((BitmapImage)image.Source).UriSource.OriginalString;
            var number = uriSource.Substring(10, 1);
            return int.Parse(number);
        }

        private int GetDiceIndexNumber(Image image)
        {
            var imageName = image.Name.Substring(8);
            return int.Parse(imageName);
        }

        private int GetFreeDiceNumber(string area)
        {
            var dice = area == "Keep" ? keepNumbers : rollNumbers;
            return Array.IndexOf(dice, 0);
        }

        private void ScoreButtonClick(object sender, RoutedEventArgs e)
        {
            DisableScoreButtons();
            var button = (Button)sender;
            var categoryName = button.Name.Replace("Button", "");
            var score = GetScoreByPropertyName(lastRollScores, categoryName);
            SetScoreByPropertyName(finalScores, categoryName, score);
            MarkCategoryAsScored(categoryName);
            numberOfTurns++;
            numberOfRolls = 0;
            RollButton.IsEnabled = true;
            if (numberOfTurns == 13)
                EndOfGame();
            else if (categoryName == "Yahtzee")
                SetMessageBlockText(MessageDialog.Yahtzee() + MessageDialog.ScoreButtonClicked());
            else
                SetMessageBlockText(MessageDialog.ScoreButtonClicked());
        }

        private bool IsCategoryScored(string propName)
        {
            var isScored = (bool)isCategoryScored.GetType().GetProperty(propName).GetValue(isCategoryScored);
            return (isScored == false);
        }

        private Label GetLabelByName(string name)
        {
            return (Label)ScoreGrid.FindName(name);
        }

        private Button GetButtonByName(string name)
        {
            return (Button)ScoreGrid.FindName(name);
        }

        private int GetScoreByPropertyName(ScoreCard<int> scores, string propName)
        {
            return (int)scores.GetType().GetProperty(propName).GetValue(scores);
        }

        private void SetScoreByPropertyName(ScoreCard<int> scores, string propName, int value)
        {
            scores.GetType().GetProperty(propName).SetValue(scores, value);
        }

        private void MarkCategoryAsScored(string categoryName)
        {
            isCategoryScored.GetType().GetProperty(categoryName).SetValue(isCategoryScored, true);

            var label = GetLabelByName(categoryName + "Label");
            label.Foreground = Brushes.Blue;
        }

        private void EnableScoreButtons()
        {
            foreach (string name in ScoreCardNames)
            {
                if (IsCategoryScored(name))
                {
                    var button = GetButtonByName(name + "Button");
                    button.IsEnabled = true;
                }
            }
        }

        private void DisableScoreButtons()
        {
            var buttons = ScoreGrid.Children.OfType<Button>();
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void MoveDiceToRollSection()
        {
            for (int i = 0; i < 5; i++)
            {
                var keepImage = GetDiceImage("Keep", i);
                keepImage.Source = null;
                rollNumbers[i] = 8;
                keepNumbers[i] = 0;
            }
        }

        private void SetMessageBlockText(string message)
        {
            MessageBlock.Text = message;
        }

        private void EndOfGame()
        {
            var upperBonusScore = CalculateUpperBonus();
            var finalScore = FinalScoreSum() + upperBonusScore;
            SetMessageBlockText(MessageDialog.GameOverText(finalScore));
        }

        private int CalculateUpperBonus()
        {
            var sum = finalScores.Ones + finalScores.Twos + finalScores.Threes + finalScores.Fours
                + finalScores.Fives + finalScores.Sixes;
            UpperBonusLabel.Foreground = Brushes.Blue;
            if (sum >= 63)
            {
                UpperBonusLabel.Content = 35;
                return 35;
            }
            return 0;
        }

        private int FinalScoreSum()
        {
            int sum = 0;
            foreach (string name in ScoreCardNames)
            {
                sum += GetScoreByPropertyName(finalScores, name);
            }
            return sum;
        }

        private void ResetGame()
        {
            InitializeStats();

            foreach (Label label in ScoreGrid.Children.OfType<Label>())
            {
                label.Foreground = Brushes.Black;
            }
        }
    }
}

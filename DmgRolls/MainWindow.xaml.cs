using DmgRolls.Helpers;
using DmgRolls.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

/*
 * As I understand it, this code-behind basically functions as the controller?
 * Well, I'll use it as one, for what it's worth.
 * 
 * TODO:
 *  -calculate on Enter
 *  -update mu and sigma with other field adjustments
 */

namespace DmgRolls
{
    public partial class MainWindow : Window
    {
        public List<DiceRow> diceRows = new List<DiceRow>();
        private int staticModifier;
        private int lowerBound;
        private int upperBound;

        public MainWindow()
        {
            InitializeComponent();

            AddDie(2, 6);
            AdjustAndReadInputFields();
        }

        private void AddDie(int diceCount, int diceType)
        {
            var rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            DiceGrid.RowDefinitions.Add(rowDefinition);

            int rowIndex = diceRows.Count;
            DiceRow newDiceRow = new DiceRow(diceCount, diceType, this);
            diceRows.Add(newDiceRow);

            Grid.SetRow(newDiceRow.diceCountBox, rowIndex);
            Grid.SetRow(newDiceRow.dTextBlock, rowIndex);
            Grid.SetRow(newDiceRow.diceTypeBox, rowIndex);
            Grid.SetRow(newDiceRow.minusButton, rowIndex);

            Grid.SetColumn(newDiceRow.diceCountBox, 0);
            Grid.SetColumn(newDiceRow.dTextBlock, 1);
            Grid.SetColumn(newDiceRow.diceTypeBox, 2);
            Grid.SetColumn(newDiceRow.minusButton, 3);

            DiceGrid.Children.Add(newDiceRow.diceCountBox);
            DiceGrid.Children.Add(newDiceRow.dTextBlock);
            DiceGrid.Children.Add(newDiceRow.diceTypeBox);
            DiceGrid.Children.Add(newDiceRow.minusButton);
        }

        private void RemoveDie(int rowIndex)
        {
            DiceRow diceRow = diceRows[rowIndex];
            DiceGrid.Children.Remove(diceRow.diceCountBox);
            DiceGrid.Children.Remove(diceRow.dTextBlock);
            DiceGrid.Children.Remove(diceRow.diceTypeBox);
            DiceGrid.Children.Remove(diceRow.minusButton);
            DiceGrid.RowDefinitions.RemoveAt(rowIndex);

            // Decrement the row number of any control coming after the removed row
            for (int i = rowIndex + 1; i < diceRows.Count; i++)
            {
                DiceRow currentRow = diceRows[i];
                Grid.SetRow(currentRow.diceCountBox, i - 1);
                Grid.SetRow(currentRow.dTextBlock, i - 1);
                Grid.SetRow(currentRow.diceTypeBox, i - 1);
                Grid.SetRow(currentRow.minusButton, i - 1);
            }

            diceRows.RemoveAt(rowIndex);
        }

        private Dictionary<string, bool> ValidateInputFields()
        {
            bool isValidStaticModifier = int.TryParse(StaticModifierBox.Text, out this.staticModifier);
            bool isValidLowerBound = int.TryParse(LowerBoundBox.Text, out this.lowerBound);
            bool isValidUpperBound = int.TryParse(UpperBoundBox.Text, out this.upperBound);
            bool isValidDiceInput = true;

            foreach(DiceRow row in diceRows)
            {
                if (!int.TryParse(row.diceCountBox.Text, out _) || !int.TryParse(row.diceTypeBox.Text, out _))
                {
                    isValidDiceInput = false;
                    break;
                }
            }

            var results = new Dictionary<string, bool>();
            results.Add("staticModifier", isValidStaticModifier);
            results.Add("lowerBound", isValidLowerBound);
            results.Add("upperBound", isValidUpperBound);
            results.Add("diceInput", isValidDiceInput);
            return results;
        }

        private void AdjustAndReadInputFields()
        {
            //set the dice counts to 1 if they don't hold a valid number
            //set the dice types to 6 if they don't hold a valid number
            //set the modifier to 0 if it doesn't hold a valid number
            //set the bounds to min or max if they don't hold a valid number
            //set the bounds to min or max if they are too low or too high

            Dictionary<string, bool> validities = ValidateInputFields();

            if (!validities["diceInput"])
            {
                foreach (DiceRow row in diceRows)
                {
                    if (!int.TryParse(row.diceCountBox.Text, out _)) 
                    {
                        row.diceCountBox.Text = "1";
                    }
                    if (!int.TryParse(row.diceTypeBox.Text, out _))
                    {
                        row.diceTypeBox.Text = "6";
                    }
                }
            }

            if (!validities["staticModifier"])
            {
                this.staticModifier = 0;
                StaticModifierBox.Text = "0";
            }

            int minRoll = this.staticModifier;
            int maxRoll = this.staticModifier;
            foreach (DiceRow row in diceRows)
            {
                minRoll += int.Parse(row.diceCountBox.Text);
                maxRoll += int.Parse(row.diceCountBox.Text) * int.Parse(row.diceTypeBox.Text);
            }

            if (!validities["lowerBound"])
            {
                lowerBound = minRoll;
                LowerBoundBox.Text = minRoll.ToString();
            }
            if (!validities["upperBound"])
            {
                upperBound = maxRoll;
                UpperBoundBox.Text = maxRoll.ToString();
            }

            if (int.Parse(LowerBoundBox.Text) < minRoll)
            {
                lowerBound = minRoll;
                LowerBoundBox.Text = minRoll.ToString();
            }
            if (int.Parse(LowerBoundBox.Text) > maxRoll)
            {
                lowerBound = maxRoll;
                LowerBoundBox.Text = maxRoll.ToString();
            }
            if (int.Parse(UpperBoundBox.Text) < minRoll)
            {
                upperBound = minRoll;
                UpperBoundBox.Text = minRoll.ToString();
            }
            if (int.Parse(UpperBoundBox.Text) > maxRoll)
            {
                upperBound = maxRoll;
                UpperBoundBox.Text = maxRoll.ToString();
            }
        }

        public void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            AddDie(1, 6);
            AdjustAndReadInputFields();
        }

        public void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            RemoveDie(row);
            AdjustAndReadInputFields();
        }

        private void StaticModifierBox_LostFocus(object sender, RoutedEventArgs e)
        {
            AdjustAndReadInputFields();
        }

        private void lowerBoundBox_LostFocus(object sender, RoutedEventArgs e)
        {
            AdjustAndReadInputFields();
        }

        private void UpperBoundBox_LostFocus(object sender, RoutedEventArgs e)
        {
            AdjustAndReadInputFields();
        }

        public void diceField_LostFocus(object sender, RoutedEventArgs e)
        {
            AdjustAndReadInputFields();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            //adjust and read all input; initialize probability model; display calculated values
            AdjustAndReadInputFields();

            var dice = new List<int>();
            foreach(DiceRow row in diceRows)
            {
                for (int i = 0; i < int.Parse(row.diceCountBox.Text); i++)
                {
                    dice.Add(int.Parse(row.diceTypeBox.Text));
                }
            }

            var calculator = new DiceProbabilityModel(dice.ToArray(), this.staticModifier);
            string approximation = calculator.UseApproximation ? "~" : "";
            double mean = calculator.Mean;
            double standardDeviation = calculator.StandardDeviation;
            double probability = calculator.GetProbability(this.lowerBound, this.upperBound);

            MuTextBlock.Text = $"μ: {mean:N1}";
            SigmaTextBlock.Text = $"σ: {standardDeviation:N3}";
            ProbabilityTextBlock.Text = $"Probability: {approximation}{probability * 100:N1}%";
        }
    }
}

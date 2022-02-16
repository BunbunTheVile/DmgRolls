using DmgRolls.Helpers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

/*
 * As I understand it, this code-behind basically functions as the controller?
 * Well, I'll use it as one, for what it's worth.
 */

namespace DmgRolls
{
    public partial class MainWindow : Window
    {
        public List<DiceRow> diceRows = new List<DiceRow>();

        public MainWindow()
        {
            InitializeComponent();

            //MessageBox.Show(result.ToString());

            AddDie(4, 4);
            AddDie(3, 6);

        }

        private void AddDie(int diceCount, int diceType)
        {
            var rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            DiceGrid.RowDefinitions.Add(rowDefinition);

            int rowIndex = diceRows.Count;
            DiceRow newDiceRow = new DiceRow(diceCount, diceType);
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
    }
}

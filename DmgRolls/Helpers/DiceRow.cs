using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DmgRolls.Helpers
{
    public class DiceRow
    {
        Color borderColour = Color.FromRgb(20, 40, 120);
        Color foregroundColour = Color.FromRgb(40, 120, 255);
        Color backgroundColour = Color.FromRgb(10, 20, 40);

        public TextBox diceCountBox;
        public TextBlock dTextBlock;
        public TextBox diceTypeBox;
        public Button minusButton;

        private MainWindow controller;

        public DiceRow(int diceCount, int diceType, MainWindow controller)
        {
            this.controller = controller;

            this.diceCountBox = new TextBox();
            this.dTextBlock = new TextBlock();
            this.diceTypeBox = new TextBox();
            this.minusButton = new Button();

            diceCountBox.Text = diceCount.ToString();
            diceCountBox.TextAlignment = TextAlignment.Center;
            diceCountBox.Width = 40;
            diceCountBox.Margin = new Thickness(0, 5, 0, 0);
            diceCountBox.BorderBrush = new SolidColorBrush(borderColour);
            diceCountBox.Foreground = new SolidColorBrush(foregroundColour);
            diceCountBox.Background = new SolidColorBrush(backgroundColour);
            diceCountBox.LostFocus += this.controller.diceField_LostFocus;

            dTextBlock.Text = "d";
            dTextBlock.Margin = new Thickness(5, 7, 5, 0);

            diceTypeBox.Text = diceType.ToString();
            diceTypeBox.TextAlignment = TextAlignment.Center;
            diceTypeBox.Width = 40;
            diceTypeBox.Margin = new Thickness(0, 5, 0, 0);
            diceTypeBox.BorderBrush = new SolidColorBrush(borderColour);
            diceTypeBox.Foreground = new SolidColorBrush(foregroundColour);
            diceTypeBox.Background = new SolidColorBrush(backgroundColour);
            diceTypeBox.LostFocus += this.controller.diceField_LostFocus;

            minusButton.Content = "-";
            minusButton.Margin = new Thickness(25, 5, 5, 0);
            minusButton.BorderThickness = new Thickness(4);
            minusButton.Width = 32;
            minusButton.BorderBrush = new SolidColorBrush(borderColour);
            minusButton.Foreground = new SolidColorBrush(foregroundColour);
            minusButton.Background = new SolidColorBrush(backgroundColour);
            minusButton.Click += this.controller.MinusButton_Click;
        }
    }
}

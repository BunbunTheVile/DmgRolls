using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DmgRolls.Models;

/*
 * TODO:
 *  implement ApproximateProbabilityCalculator
 *  implement controller
 *  cleanup Main class file
 *  find a way to implement spinner boxes
 */

namespace DmgRolls
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int[] dice = new int[] { 6, 6, 6, 6, 6, 6};
            var x = new DiceProbabilityModel(dice, 5);

            var result = $"{x.GetProbability(30, 30):N3}";

            MessageBox.Show(result.ToString());
        }
    }
}

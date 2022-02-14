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

            int[] dice = new int[] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
            var x = new ExactProbabilityCalculator(dice, 0);
            string results = "";
            for (int i = 0; i < x.probabilities.Length; i++)
            {
                results += $"{i}\t{x.frequencies[i]}\t{x.probabilities[i] * 100:N3}%\n";
            }
            //MessageBox.Show(results);

            double prob = x.getProbability(25, 45);
            MessageBox.Show(prob.ToString());
        }
    }
}

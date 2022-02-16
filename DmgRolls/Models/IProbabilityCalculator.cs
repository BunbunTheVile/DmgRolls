using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmgRolls.Models
{
    public interface IProbabilityCalculator
    {
        double Mean { get; }
        double StandardDeviation { get; }
        double GetProbability(int lower, int upper);

        public static double GetMean(int die)
        {
            return (die + 1) / (double)2;
        }

        public static double GetVariance(int die)
        {
            return (die * die - 1) / (double)12;
        }
    }
}

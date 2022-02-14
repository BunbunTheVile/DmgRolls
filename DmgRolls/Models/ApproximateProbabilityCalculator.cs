using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmgRolls.Models
{
    public class ApproximateProbabilityCalculator : IProbabilityCalculator
    {
        public int[] dice;
        public int staticModifier;

        public double mean;
        public double variance;
        public double standardDeviation;

        public Normal normalDistribution;

        public ApproximateProbabilityCalculator(int[] dice, int staticModifier)
        {
            this.dice = dice;
            this.staticModifier = staticModifier;
            
            mean = 0.0;
            variance = 0.0;
            foreach (int die in dice)
            {
                mean += getMean(die);
                variance += getVariance(die);
            }
            standardDeviation = Math.Sqrt(variance);

            normalDistribution = new Normal(mean, standardDeviation);
        }

        public static double getMean(int die)
        {
            return (die + 1) / (double)2;
        }

        public static double getVariance(int die)
        {
            return (die * die - 1) / (double)12;
        }

        public double getProbability(int lower, int upper)
        {
            double upperProbability = normalDistribution.CumulativeDistribution((double)upper + 0.5);
            double lowerProbability = normalDistribution.CumulativeDistribution((double)lower - 0.5);
            return upperProbability - lowerProbability;
        }
    }
}

using MathNet.Numerics.Distributions;
using System;

namespace DmgRolls.Models
{
    public class ApproximateProbabilityCalculator : IProbabilityCalculator
    {
        public int[] dice;
        public int staticModifier;

        public double Mean { get; }
        public double Variance { get; }
        public double StandardDeviation { get; }

        public Normal normalDistribution;

        public ApproximateProbabilityCalculator(int[] dice, int staticModifier)
        {
            this.dice = dice;
            this.staticModifier = staticModifier;
            
            Mean = 0.0;
            Variance = 0.0;
            foreach (int die in dice)
            {
                Mean += IProbabilityCalculator.GetMean(die);
                Variance += IProbabilityCalculator.GetVariance(die);
            }
            StandardDeviation = Math.Sqrt(Variance);

            normalDistribution = new Normal(Mean, StandardDeviation);
        }

        public double GetProbability(int lower, int upper)
        {
            double upperProbability = normalDistribution.CumulativeDistribution((double)upper + 0.5);
            double lowerProbability = normalDistribution.CumulativeDistribution((double)lower - 0.5);
            return upperProbability - lowerProbability;
        }
    }
}

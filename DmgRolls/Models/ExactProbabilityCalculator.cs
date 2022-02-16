using System;

namespace DmgRolls.Models
{
    public class ExactProbabilityCalculator : IProbabilityCalculator
    {
        public int[] dice;
        public int staticModifier;

        public int maxRoll;
        public int minRoll;

        public double Mean { get; }
        public double Variance { get; }
        public double StandardDeviation { get; }

        public int[] frequencies;          // absolute frequencies of a every dice roll total
        public double[] probabilities;     // relative frequencies of every dice roll total
        public int possibilities;          // the total amount of possible dice rolls

        public ExactProbabilityCalculator(int[] dice, int staticModifier)
        {
            this.dice = dice;
            this.staticModifier = staticModifier;

            minRoll = dice.Length + staticModifier;
            maxRoll = staticModifier;
            possibilities = 1;
            foreach (int die in dice)
            {
                maxRoll += die;
                possibilities *= die;
            }
            frequencies = new int[maxRoll + 1];
            probabilities = new double[maxRoll + 1];

            Mean = (double)staticModifier;
            Variance = 0.0;
            foreach (int die in dice)
            {
                Mean += IProbabilityCalculator.GetMean(die);
                Variance += IProbabilityCalculator.GetVariance(die);
            }
            StandardDeviation = Math.Sqrt(Variance);
            if (dice.Length > 0)
            {
                this.initializeFrequencies(currentSum: staticModifier);
                this.initializeProbabilities();
            }
        }

        public void initializeFrequencies(int depth = 0, int currentSum = 0)
        {
            if (depth == 0 && depth != dice.Length - 1)
            {
                for (int i = 1; i <= dice[depth]; i++)
                {
                    int newSum = i + currentSum;
                    initializeFrequencies(depth + 1, newSum);
                }
            }
            else if (depth == dice.Length - 1)
            {
                for (int i = 1; i <= dice[depth]; i++)
                {
                    int newSum = i + currentSum;
                    frequencies[newSum]++;
                }
            }
            else
            {
                for (int i = 1; i <= dice[depth]; i++)
                {
                    int newSum = i + currentSum;
                    initializeFrequencies(depth + 1, newSum);
                }
            }
        }

        public void initializeProbabilities()
        {
            for (int i = 0; i < probabilities.Length; i++)
            {
                probabilities[i] = (double)frequencies[i] / possibilities;
            }
        }

        public double GetProbability(int lower, int upper)
        {
            if (!(dice.Length > 0)) return 1;
            if (lower > upper) return 0;

            lower = lower < minRoll ? minRoll : lower;
            lower = lower > maxRoll ? maxRoll : lower;
            upper = upper < minRoll ? minRoll : upper;
            upper = upper > maxRoll ? maxRoll : upper;

            double probability = 0;
            for (int i = lower; i <= upper; i++)
            {
                probability += probabilities[i];
            }
            return probability;
        }
    }
}

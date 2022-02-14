using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DmgRolls.Models
{
    public class ExactProbabilityCalculator : IProbabilityCalculator
    {
        public int[] dice;
        public int staticModifier;

        public int[] frequencies;          // absolute frequencies of a every dice roll total
        public double[] probabilities;     // relative frequencies of every dice roll total
        public int possibilities;          // the total amount of possible dice rolls

        public ExactProbabilityCalculator(int[] dice, int staticModifier)
        {
            this.dice = dice;
            this.staticModifier = staticModifier;

            int maxRoll = staticModifier;
            possibilities = 1;
            foreach (int die in dice)
            {
                maxRoll += die;
                possibilities *= die;
            }
            frequencies = new int[maxRoll + 1];
            probabilities = new double[maxRoll + 1];

            this.initializeFrequencies(currentSum: staticModifier);
            this.initializeProbabilities();
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

        public double getProbability(int lower, int upper)
        {
            double probability = 0;
            for (int i = lower; i <= upper; i++)
            {
                probability += probabilities[i];
            }
            return probability;
        }
    }
}

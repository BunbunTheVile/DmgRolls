using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmgRolls.Models
{
    public class DiceProbabilityModel
    {
        private int[] dice;
        private int staticModifier;
        private bool useApproximation;
        private IProbabilityCalculator probabilityCalculator;

        public DiceProbabilityModel(int[] dice, int staticModifier)
        {
            this.dice = dice;
            this.staticModifier = staticModifier;

            int calculationWeight = 0;
            foreach (int die in dice)
            {
                calculationWeight += die;
            }
            useApproximation = calculationWeight > 70;

            // initialize probabilityCalculator depending on the calculationWeight
        }
    }
}

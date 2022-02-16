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
            foreach (int die in this.dice)
            {
                calculationWeight += die;
            }
            useApproximation = calculationWeight > 70;        

            if (useApproximation)
            {
                this.probabilityCalculator = new ApproximateProbabilityCalculator(this.dice, this.staticModifier);
            }
            else
            {
                this.probabilityCalculator = new ExactProbabilityCalculator(this.dice, this.staticModifier);
            }
        }

        public double GetProbability(int lower, int upper)
        {
            double probability = this.probabilityCalculator.GetProbability(lower, upper);
            return probability;
        }
    }
}

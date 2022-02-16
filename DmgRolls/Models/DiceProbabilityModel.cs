namespace DmgRolls.Models
{
    public class DiceProbabilityModel
    {
        private int[] dice;
        private int staticModifier;
        private IProbabilityCalculator probabilityCalculator;

        public bool UseApproximation { get; }
        public double Mean { get; }
        public double StandardDeviation { get; }

        public DiceProbabilityModel(int[] dice, int staticModifier)
        {
            this.dice = dice;
            this.staticModifier = staticModifier;

            int calculationWeight = 0;
            foreach (int die in this.dice)
            {
                calculationWeight += die;
            }
            UseApproximation = calculationWeight > 70;        

            if (UseApproximation)
            {
                this.probabilityCalculator = new ApproximateProbabilityCalculator(this.dice, this.staticModifier);
            }
            else
            {
                this.probabilityCalculator = new ExactProbabilityCalculator(this.dice, this.staticModifier);
            }

            this.Mean = probabilityCalculator.Mean;
            this.StandardDeviation = probabilityCalculator.StandardDeviation;
        }

        public double GetProbability(int lower, int upper)
        {
            double probability = this.probabilityCalculator.GetProbability(lower, upper);
            return probability;
        }
    }
}

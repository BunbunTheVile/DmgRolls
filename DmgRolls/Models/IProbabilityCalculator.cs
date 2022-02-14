using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmgRolls.Models
{
    public interface IProbabilityCalculator
    {
        double getProbability(int lower, int upper);
    }
}

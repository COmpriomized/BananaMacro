using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaMacro.AI_MachineLearning.Utils
{
    public static class Metrics
    {
        public static double Accuracy(List<string> predicted, List<string> actual)
        {
            if (predicted.Count != actual.Count) return 0;
            int correct = predicted.Zip(actual, (p, a) => p == a ? 1 : 0).Sum();
            return (double)correct / predicted.Count;
        }
    }
}
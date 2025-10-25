using System.Collections.Generic;
using System.Linq;

namespace BananaMacro.AI_MachineLearning.Training
{
    public static class DataPreprocessor
    {
        public static List<string> Clean(List<string> raw)
        {
            return raw.Select(s => s.Trim().ToLower()).ToList();
        }

        public static List<string> Tokenize(List<string> cleaned)
        {
            return cleaned.Select(s => string.Join(" ", s.Split(' '))).ToList();
        }
    }
}
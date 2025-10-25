using System.Collections.Generic;

namespace BananaMacro.AppData
{
    public class AiTrainingSet
    {
        public List<TrainingExample> Examples { get; set; } = new();

        public void AddExample(string input, string expectedOutput)
        {
            Examples.Add(new TrainingExample { Input = input, ExpectedOutput = expectedOutput });
        }

        public class TrainingExample
        {
            public string Input { get; set; } = string.Empty;
            public string ExpectedOutput { get; set; } = string.Empty;
        }
    }
}
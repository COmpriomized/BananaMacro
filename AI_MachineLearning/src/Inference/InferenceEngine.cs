using System.Threading.Tasks;

namespace BananaMacro.AI_MachineLearning.Inference
{
    public class InferenceEngine
    {
        public async Task<PredictionResult> PredictAsync(string input)
        {
            await Task.Delay(100); // Simulate inference delay
            var label = input.ToLower().Contains("macro") ? "macro_trigger" : "no_action";
            var confidence = input.Length > 10 ? 0.9 : 0.6;

            return new PredictionResult
            {
                Label = label,
                Confidence = confidence,
                Raw = new { Echo = input }
            };
        }
    }

    public class PredictionResult
    {
        public string Label { get; set; } = "";
        public double Confidence { get; set; }
        public object? Raw { get; set; }
    }
}
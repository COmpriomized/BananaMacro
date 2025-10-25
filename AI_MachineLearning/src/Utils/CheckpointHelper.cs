using System;
using System.IO;

namespace BananaMacro.AI_MachineLearning.Utils
{
    public static class CheckpointHelper
    {
        public static void Save(string modelId, string content)
        {
            var path = Path.Combine("AI_MachineLearning", "models", "archived", $"{modelId}.txt");
            File.WriteAllText(path, content);
        }

        public static string Load(string modelId)
        {
            var path = Path.Combine("AI_MachineLearning", "models", "archived", $"{modelId}.txt");
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
        }
    }
}
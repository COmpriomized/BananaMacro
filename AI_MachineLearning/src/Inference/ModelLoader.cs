using System;
using System.IO;

namespace BananaMacro.AI_MachineLearning.Inference
{
    public static class ModelLoader
    {
        public static bool TryLoadModel(string path, out string reason)
        {
            reason = string.Empty;
            if (string.IsNullOrWhiteSpace(path)) { reason = "Path required"; return false; }
            if (!File.Exists(path)) { reason = "Model file missing"; return false; }
            return true;
        }
    }
}
using System.Collections.Generic;

namespace BananaMacro.AppData
{
    public class AiMemory
    {
        public Dictionary<string, string> Facts { get; set; } = new();
        public Dictionary<string, float[]> Embeddings { get; set; } = new();
        public List<string> ContextHistory { get; set; } = new();

        public void AddFact(string key, string value) => Facts[key] = value;

        public void AddEmbedding(string key, float[] vector) => Embeddings[key] = vector;

        public void AppendContext(string message) => ContextHistory.Add(message);
    }
}
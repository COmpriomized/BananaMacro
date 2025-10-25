namespace BananaMacro.AppData
{
    public class AiModelState
    {
        public string ModelName { get; set; } = "default";
        public string Version { get; set; } = "1.0.0";
        public bool IsFineTuned { get; set; } = false;
        public bool IsStreamingEnabled { get; set; } = false;
        public Dictionary<string, string> Flags { get; set; } = new();
    }
}
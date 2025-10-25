using System;
using System.Collections.Generic;

namespace BananaMacro.AppData
{
    public class AiTelemetry
    {
        public int SessionsStarted { get; set; }
        public int PromptsProcessed { get; set; }
        public TimeSpan TotalRuntime { get; set; }
        public List<string> Feedback { get; set; } = new();

        public void RecordSessionStart() => SessionsStarted++;

        public void RecordPrompt() => PromptsProcessed++;

        public void AddFeedback(string comment) => Feedback.Add(comment);
    }
}
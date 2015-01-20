using System.Collections.Generic;

using StyleCop;

namespace StyleCopCmd.Core
{
    public class ExecutionResult
    {
        public ExecutionResult()
        {
            this.Errors = new List<ViolationEventArgs>();
            this.Warnings = new List<ViolationEventArgs>();
        }

        public long WarningsCount { get; set; }
        
        public long ErrorsCount { get; set; }

        public List<ViolationEventArgs> Errors { get; set; }
        
        public List<ViolationEventArgs> Warnings { get; set; }

        public bool HasWarnings
        {
            get { return this.WarningsCount > 0; }
        }

        public bool HasErrors
        {
            get { return this.ErrorsCount > 0; }
        }
    }
}
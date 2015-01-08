using System;

namespace StyleCopCmd.Core
{
    public class ExecutorLoggingEventArgs : EventArgs
    {
        public string Level { get; set; }

        public string Message { get; set; }
    }
}
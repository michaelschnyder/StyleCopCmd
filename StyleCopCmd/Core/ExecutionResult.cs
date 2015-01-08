namespace StyleCopCmd.Core
{
    public class ExecutionResult
    {
        public bool HasWarnings
        {
            get { return this.WarningsCount > 0; }
        }

        public bool HasErrors
        {
            get { return this.ErrorsCount > 0; }
        }

        public long WarningsCount { get; set; }
        
        public long ErrorsCount { get; set; }
    }
}
using StyleCopCmd.Reader;

namespace StyleCopCmd.Core
{
    public abstract class StyleCopIssueReporter
    {
        public virtual void Report(string message)
        {
        }

        public virtual void ProjectAdded(CsProject project)
        {
        }

        public virtual void Started()
        {
        }

        public virtual void Completed(ExecutionResult result, string tempFileName)
        {
        }
    }
}
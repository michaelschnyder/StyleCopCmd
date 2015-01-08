namespace StyleCopCmd.Core
{
    public interface IStyleCopIssueReporter
    {
        void Report(string message);
    }
}
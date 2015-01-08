using System;
using System.Collections.Generic;

using StyleCop;

namespace StyleCopCmd.Core
{
    public class ExecutionContext
    {
        private readonly StyleCopExecutor executor;

        private readonly List<StyleCopIssueReporter> reporters;

        private readonly List<ViolationEventArgs> violationEvents = new List<ViolationEventArgs>();

        private ExecutionResult result;

        public ExecutionContext(StyleCopExecutor executor, IEnumerable<StyleCopIssueReporter> reporters)
        {
            this.executor = executor;
            this.reporters = new List<StyleCopIssueReporter>(reporters);

            this.result = new ExecutionResult();
        }

        public ExecutionResult Result 
        {
            get { return this.result; }
            set { this.result = value; }
        }

        public void ViolationEncountered(object sender, ViolationEventArgs args)
        {
            this.violationEvents.Add(args);

            if (args.Warning)
            {
                this.result.WarningsCount++;
            }
            else
            {
                this.result.ErrorsCount++;
            }

            foreach (var reporter in this.reporters)
            {
                try
                {
                    reporter.Report(string.Format("[{0}] {1} ({2}) in '{3}:{4}'", args.Warning ? "WARN" : "ERROR", args.Message, args.Violation.Rule.CheckId, args.Element.Document.SourceCode.Name, args.LineNumber));
                }
                catch (Exception e)
                {
                    this.executor.LogError("Reporter '{0}' has failed. Exception was: '{1}'", reporter.GetType().Name, e);
                }
            }
        }
    }
}
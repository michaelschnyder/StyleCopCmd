using System.IO;

using StyleCopCmd.Core;

namespace StyleCopCmd.Reporter
{
    public class StyleCopXmlReporter : StyleCopIssueReporter
    {
        private readonly string fileName;

        public StyleCopXmlReporter(string fileName)
        {
            this.fileName = fileName;
        }

        public override void Completed(ExecutionResult result, string tempFileName)
        {
            File.Copy(tempFileName, this.fileName);
        }
    }
}

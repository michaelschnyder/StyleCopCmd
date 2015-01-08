using System;

using StyleCopCmd.Core;
using StyleCopCmd.Reader;

namespace StyleCopCmd.Reporter
{
    public class ConsoleReporter : StyleCopIssueReporter
    {
        private int windowWidth = 150;

        public ConsoleReporter()
        {
            try
            {
                if (Console.CursorVisible)
                {
                    this.windowWidth = Console.WindowWidth;
                }
            }
            catch (Exception)
            {
            }
        }

        public override void Report(string message)
        {
            Console.WriteLine(message);
        }

        public override void ProjectAdded(CsProject project)
        {
            Console.WriteLine("Added Project '{0}' and its files ({1}) for validation.", project.AssemblyName, project.Files.Count);
        }

        public override void Started()
        {
            Console.Write(string.Empty.PadRight(this.windowWidth).Replace(' ', '-'));
        }

        public override void Completed(ExecutionResult result, string tempFileName)
        {
            Console.Write(string.Empty.PadRight(this.windowWidth).Replace(' ', '-'));
            Console.WriteLine("Errors:   {0}", result.ErrorsCount);
            Console.WriteLine("Warnings: {0}", result.WarningsCount);
        }
    }
}
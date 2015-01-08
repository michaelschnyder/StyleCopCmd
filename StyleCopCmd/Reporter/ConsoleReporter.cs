using System;

using StyleCopCmd.Core;
using StyleCopCmd.Reader;

namespace StyleCopCmd.Reporter
{
    public class ConsoleReporter : StyleCopIssueReporter
    {
        private readonly int windowWidth = 150;

        private readonly bool hasConsole = false;

        public ConsoleReporter()
        {
            try
            {
                if (Console.CursorVisible)
                {
                    this.windowWidth = Console.WindowWidth;
                    this.hasConsole = true;
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
            this.ConsoleHorizontalLine();
            Console.WriteLine("Errors:   {0}", result.ErrorsCount);
            Console.WriteLine("Warnings: {0}", result.WarningsCount);
        }

        private void ConsoleHorizontalLine()
        {
            string line = string.Empty.PadRight(this.windowWidth).Replace(' ', '-');

            if (this.hasConsole)
            {
                Console.Write(line);
            }
            else
            {
                Console.WriteLine(line);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using StyleCopCmd.Reader;
using StyleCopCmd.Reporter;

namespace StyleCopCmd
{
    using StyleCopCmd.Core;

    /// <summary>
    /// Simple example for running StyleCop environment.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main program entry.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            var options = new CommandLineOptions();

            var executor = new StyleCopExecutor();

            executor.Logging += ExecutorOnLogging;

            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                AddProjectsFromSolutionFiles(options.Solutions, executor);
                AddProjectFiles(options.Projects, executor);

                if (options.Console)
                {
                    executor.AddReporter(new ConsoleReporter());
                }

                executor.WarningsAsErrors = options.WarningsAsErrors;
            }

            var result = executor.Run();

            if (options.LogOnly || !(result.HasErrors || (options.WarningsAsErrors && result.HasWarnings)))
            {
                QuitApplication(0);
            }
            else
            {
                QuitApplication(1);
            }
        }

        private static void ExecutorOnLogging(object sender, ExecutorLoggingEventArgs executorLoggingEventArgs)
        {
            var args = executorLoggingEventArgs;

            switch (args.Level.ToUpperInvariant())
            {
                case "ERROR":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case "WARN":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case "INFO":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }

            Console.Write(args.Level);
            Console.ResetColor();
            Console.Write(" " + args.Message + "\n");
        }

        private static void QuitApplication(int exitCode)
        {
#if (DEBUG)
            if (Debugger.IsAttached) 
            {
                Console.Write("#ifdebug: ExitCode: {0}. Press any key to exit...", exitCode);
                Console.ReadKey();
            }
#endif
        }

        private static void AddProjectFiles(IEnumerable<string> projectFiles, StyleCopExecutor executore)
        {
            if (projectFiles == null)
            {
                return;
            }

            foreach (var projectFile in projectFiles)
            {
                if (File.Exists(projectFile))
                {
                    using (var reader = new ProjectReader(projectFile))
                    {
                        var project = reader.Read();
                        executore.AddProject(project);
                    }
                }
            }
        }

        private static void AddProjectsFromSolutionFiles(IEnumerable<string> solutionFiles, StyleCopExecutor executore)
        {
            if (solutionFiles == null)
            {
                return;
            }

            foreach (var solutionFile in solutionFiles)
            {
                if (File.Exists(solutionFile))
                {
                    using (var reader = new SolutionReader(solutionFile))
                    {
                        var solution = reader.Read();
                        foreach (var project in solution.Projects)
                        {
                            executore.AddProject(project);
                        }
                    }
                }
            }
        }
    }
}
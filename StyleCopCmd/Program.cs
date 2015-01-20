using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using StyleCopCmd.Core;

using StyleCopCmd.Reader;
using StyleCopCmd.Reporter;
using StyleCopCmd.Reporter.NUnit;
using StyleCopCmd.Reporter.TeamCity;

namespace StyleCopCmd
{
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

            if (args != null && args.Length > 0 && CommandLine.Parser.Default.ParseArguments(args, options))
            {
                var executor = new StyleCopExecutor();

                executor.Logging += ExecutorOnLogging;

                if (options.Console)
                {
                    executor.AddReporter(new ConsoleReporter());
                }

                if (!string.IsNullOrEmpty(options.StyleCopXml))
                {
                    executor.AddReporter(new StyleCopXmlReporter(options.StyleCopXml));
                }

                if (!string.IsNullOrEmpty(options.NUnitXml))
                {
                    executor.AddReporter(new NUnitReporter(options.NUnitXml));
                }

                if (options.TeamCityServiceMessages)
                {
                    executor.AddReporter(new TeamCityMessageReporter());
                }

                AddProjectsFromSolutionFiles(options.Solutions, executor);
                AddProjectFiles(options.Projects, executor);

                executor.WarningsAsErrors = options.WarningsAsErrors;

                var result = executor.Run();

                if (!options.EnableExitCode || !(result.HasErrors || (options.WarningsAsErrors && result.HasWarnings)))
                {
                    QuitApplication(0);
                }
                else
                {
                    QuitApplication(1);
                }
            }

            Console.WriteLine(options.GetUsage());
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
                Console.WriteLine();
                Console.Write("#ifdebug: ExitCode would be {0}. Press any key to exit...", exitCode);
                Console.ReadKey();
            }
#endif
            Environment.Exit(exitCode);
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
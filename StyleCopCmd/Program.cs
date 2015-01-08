using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using StyleCop;

using StyleCopCmd.Reader;

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
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public static void Main(string[] args)
        {
            var options = new CommandLineOptions();

            var executore = new StyleCopExecutor();

            executore.AddReporter(new ConsoleResultAdapter());

            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.Solutions != null)
                {
                    foreach (var solutionFile in options.Solutions)
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
                
                if (options.Projects != null)
                {
                    foreach (var projectFile in options.Projects)
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
            }

            executore.Run();

#if (DEBUG)
{  
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
}
#endif
        }
    }

    public class ConsoleResultAdapter : IStyleCopIssueReporter
    {
        public void Report(string message)
        {
            Console.WriteLine(message);
        }
    }
}
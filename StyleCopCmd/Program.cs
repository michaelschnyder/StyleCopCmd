using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using StyleCop;

using StyleCopCmd.Reader;

namespace StyleCopCmd
{
    using System.IO;
    using System.Linq;

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
            var projects = new List<CsProject>();

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
                                projects.AddRange(solution.Projects);
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

                                if (projects.All(p => p.Guid != project.Guid))
                                {
                                    projects.Add(project);
                                }
                                else
                                {
                                    // Ignored, already in list
                                }
                            }
                        }
                    }
                }
            }

            var console = new StyleCopConsole(null, false, null, null, true);
            var codeProjects = new List<CodeProject>();

            var projectIndex = 0;
            foreach (var csProject in projects)
            {
                var codeProject = new CodeProject(projectIndex++, csProject.Directory, new Configuration(null));

                foreach (var file in csProject.Files)
                {
                    console.Core.Environment.AddSourceCode(codeProject, file.FullName, null);
                }

                codeProjects.Add(codeProject);
            }

            console.ViolationEncountered += OnViolationEncountered;
            
            console.Start(codeProjects, true);

            console.ViolationEncountered -= OnViolationEncountered;

#if (DEBUG)
{  
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
}
#endif
        }

        private static void OnViolationEncountered(object sender, ViolationEventArgs e)
        {
            Console.WriteLine("[{0}:{1}] {2} {3}", e.Element.Document.SourceCode.Name, e.LineNumber, e.Violation.Rule.CheckId, e.Message);
        }
    }
}
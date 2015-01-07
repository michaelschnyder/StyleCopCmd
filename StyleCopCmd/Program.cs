using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using StyleCop;

namespace StyleCopCmd
{
    using System.Diagnostics;

    using StyleCopCmd.Reader;

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
            var projects = new List<CsProject>();

            var solutionFile = @"..\..\..\StyleCopCmd.sln";
            
            using (var reader = new SolutionReader(solutionFile))
            {
                var solution = reader.Read();
                projects.AddRange(solution.Projects);
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
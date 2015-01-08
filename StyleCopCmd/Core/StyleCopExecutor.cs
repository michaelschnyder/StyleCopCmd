using System;
using System.Collections.Generic;

using StyleCop;

using StyleCopCmd.Reader;
using System.Linq;

namespace StyleCopCmd.Core
{
    public class StyleCopExecutor
    {
        private readonly List<CsProject> projects = new List<CsProject>();

        private readonly List<IStyleCopIssueReporter> reporters = new List<IStyleCopIssueReporter>();

        public void AddProject(CsProject project)
        {
            if (this.projects.All(p => p.Guid != project.Guid))
            {
                this.projects.Add(project);
            }
            else
            {
                
            }
        }

        public void AddReporter(IStyleCopIssueReporter issueReporter)
        {
            this.reporters.Add(issueReporter);
        }

        public void Run()
        {
            var styleCopConsole = new StyleCopConsole(null, false, null, null, true);
            var codeProjects = new List<CodeProject>();

            var projectIndex = 0;

            foreach (var project in this.projects)
            {
                var codeProject = new CodeProject(projectIndex++, project.Directory, new Configuration(null));

                foreach (var file in project.Files)
                {
                    styleCopConsole.Core.Environment.AddSourceCode(codeProject, file.FullName, null);
                }

                codeProjects.Add(codeProject);
            }

            EventHandler<ViolationEventArgs> styleCopConsoleOnViolationEncountered = (sender, args) =>
                {
                    var issueReporters = new List<IStyleCopIssueReporter>(this.reporters);

                    foreach (var reporter in issueReporters)
                    {
                        try
                        {
                            reporter.Report(string.Format("[{0}:{1}] {2} {3}", args.Element.Document.SourceCode.Name, args.LineNumber, args.Violation.Rule.CheckId, args.Message));
                        }
                        catch
                        {
                        }
                    }
                };

            styleCopConsole.ViolationEncountered += styleCopConsoleOnViolationEncountered;

            styleCopConsole.Start(codeProjects, true);

            styleCopConsole.ViolationEncountered -= styleCopConsoleOnViolationEncountered;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using StyleCop;

using StyleCopCmd.Reader;

namespace StyleCopCmd.Core
{
    public class StyleCopExecutor
    {
        private readonly List<CsProject> projects = new List<CsProject>();

        private readonly List<StyleCopIssueReporter> reporters = new List<StyleCopIssueReporter>();

        public event EventHandler<ExecutorLoggingEventArgs> Logging;
        
        public bool WarningsAsErrors { get; set; }

        public void AddReporter(StyleCopIssueReporter issueReporter)
        {
            this.reporters.Add(issueReporter);
        }

        public void AddProject(CsProject project)
        {
            if (this.projects.All(p => p.Guid != project.Guid))
            {
                this.projects.Add(project);
                this.reporters.ForEach(r => r.ProjectAdded(project));
            }
            else
            {
                string assemblyName = project.AssemblyName;
                this.LogWarn("Project '{0}' from '{1}' has already been added. Skipping.", assemblyName);
            }
        }

        public ExecutionResult Run()
        {
            string tempFileName = Path.GetTempFileName();

            var styleCopConsole = new StyleCopConsole(null, false, tempFileName, null, true);
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
                this.reporters.ForEach(r => r.ProjectAdded(project));
            }

            var executionContext = new ExecutionContext(this, this.reporters);

            styleCopConsole.ViolationEncountered += executionContext.ViolationEncountered;
            this.reporters.ForEach(r => r.Started());

            styleCopConsole.Start(codeProjects, true);

            this.reporters.ForEach(r => r.Completed(executionContext.Result, tempFileName));
            styleCopConsole.ViolationEncountered -= executionContext.ViolationEncountered;

            if (File.Exists(tempFileName))
            {
                try
                {
                    File.Delete(tempFileName);
                }
                catch (Exception)
                {
                }
            }

            return executionContext.Result;
        }

        internal void LogWarn(string message, params object[] args)
        {
            this.Log("WARN", message, args);
        }

        internal void LogError(string message, params object[] args)
        {
            this.Log("ERROR", message, args);
        }

        protected virtual void OnLogging(ExecutorLoggingEventArgs e)
        {
            var handler = this.Logging;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void Log(string level, string message, object[] args)
        {
            this.OnLogging(new ExecutorLoggingEventArgs() { Level = level, Message = string.Format(message, args) });
        }
    }
}

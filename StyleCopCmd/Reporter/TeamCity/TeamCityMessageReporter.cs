using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using JetBrains.TeamCity.ServiceMessages.Write.Special;

using StyleCop;

using StyleCopCmd.Core;
using StyleCopCmd.Reader;

namespace StyleCopCmd.Reporter.TeamCity
{
    /// <summary>
    /// Reports StyleCop issues to TeamCity with Service Messages
    /// See <see cref="http://blog.jonnyzzz.name/2012/12/teeamcityservicemessages-v30.html"></see> and 
    /// <seealso cref="http://confluence.jetbrains.com/display/TCD7/Build+Script+Interaction+with+TeamCity#BuildScriptInteractionwithTeamCity-ReportingBuildProgress for more details"></seealso>
    /// </summary>
    public class TeamCityMessageReporter : StyleCopIssueReporter
    {
        private readonly ITeamCityWriter rootWriter;

        public TeamCityMessageReporter()
        {
            this.rootWriter = new TeamCityServiceMessages().CreateWriter();
        }

        public override void ProjectAdded(CsProject project)
        {
            this.rootWriter.WriteMessage(string.Format("Added Project '{0}' (From: '{1}', Guid: '{2}') for analysis.", project.AssemblyName, Path.Combine(project.Directory, project.File), project.Guid));
        }

        public override void Started()
        {
            this.rootWriter.WriteMessage("StyleCop is analyzing...");
        }

        public override void Completed(ExecutionResult result, string tempFileName)
        {
            this.ReportResults(result);

            this.PublishConclusion(result);
        }

        private void ReportResults(ExecutionResult result)
        {
            var violations = result.Errors.Union(result.Warnings).ToList();

            var topLevelGrouping = violations.GroupBy(v => v.Violation.SourceCode.Project.Location);

            using (var resultsBlock = this.rootWriter.OpenBlock("Results"))
            {
                foreach (var violationsInProject in topLevelGrouping)
                {
                    // This is the first grouping, which is by projectfile
                    var projectPath = violationsInProject.Key;
                    var displayNameForGroup = violationsInProject.Key.Substring(violationsInProject.Key.LastIndexOf('\\') + 1);

                    // TC: Open new Suite for each Project
                    using (var suite = resultsBlock.OpenTestSuite(displayNameForGroup))
                    {
                        this.PublishViolations(violationsInProject, projectPath, suite);
                    }
                }
            }
        }

        private void PublishViolations(IEnumerable<ViolationEventArgs> nameSpaceInProjectGroup, string rootPath, ITeamCityTestsSubWriter suiteWriter)
        {
            foreach (var violation in nameSpaceInProjectGroup.OrderBy(v => v.SourceCode.Path).ThenBy(v => v.LineNumber))
            {
                var styleCopId = violation.Violation.Rule.CheckId;
                var styleCopNameSpace = violation.Violation.Rule.Namespace;
                var ruleName = violation.Violation.Rule.Name;

                var text = violation.Message;
                var detail = violation.Violation.Rule.Description;
                var path = violation.SourceCode.Path + ":" + violation.LineNumber;
                var ruleUrl = string.Format("http://www.stylecop.com/docs/{0}.html", styleCopId);

                var testFileName = violation.SourceCode.Path.Replace(rootPath, string.Empty).Substring(1);
                var testName = styleCopId + "-" + ruleName + "(\"" + testFileName.Replace(".cs", ".cs") + ":" + violation.LineNumber + "\")";
                
                using (var test = suiteWriter.OpenTest(testName))
                {
                    test.WriteFailed(string.Format("{0}: {1} ({2})", styleCopId, text, styleCopNameSpace), string.Format("Reason: {0}\n\nFile: {1}\n\nSee: {2} for further explanation", detail, path, ruleUrl));
                }
            }
        }

        private void PublishConclusion(ExecutionResult result)
        {
            this.rootWriter.WriteBuildStatistics("StyleCopWarningsCount", result.WarningsCount.ToString(CultureInfo.InvariantCulture));
            this.rootWriter.WriteBuildStatistics("StyleCopErrorsCount", result.ErrorsCount.ToString(CultureInfo.InvariantCulture));

            if (result.HasErrors || result.HasWarnings)
            {
                this.rootWriter.WriteError(
                    string.Format("StyleCop failed with {0} Errors and {1} Warnings", result.ErrorsCount, result.WarningsCount));
            }
            else
            {
                this.rootWriter.WriteMessage("StyleCop completed sucessfully.");
            }
        }
    }
}

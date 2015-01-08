using System;
using System.Collections.Generic;
using System.Linq;

using StyleCop;

using StyleCopCmd.Core;

namespace StyleCopCmd.Reporter.NUnit
{
    public class NUnitReporter : StyleCopIssueReporter
    {
        private readonly string fileName;

        private NUnitResultWriter writer;

        private resultType root;

        private List<testsuiteType> testSuites = new List<testsuiteType>();

        public NUnitReporter(string fileName)
        {
            this.fileName = fileName;
            this.writer = new NUnitResultWriter(fileName);

            this.root = new resultType
                {
                    testsuite = new testsuiteType
                        {
                            results = new resultsType()
                        }
                };
        }

        public override void Started()
        {
            this.root.date = DateTime.Now.ToString("yyy-MM-dd");
            this.root.time = DateTime.Now.ToString("HH:mm:ss");

            this.writer.Write(this.root);
        }

        public override void Result(ViolationEventArgs @event)
        {
            if (!@event.Warning)
            {
                this.root.errors++;
            }
            else
            {
                this.root.ignored++;
            }

            this.root.total++;

            // Organize the viaolation inside test-suites (top level is the Namespace)
            var suiteForNameSpace = this.testSuites.FirstOrDefault(s => s.name == @event.Violation.Rule.Namespace);
            if (suiteForNameSpace == null)
            {
                suiteForNameSpace = new testsuiteType()
                    {
                        name = @event.Violation.Rule.Namespace, 
                        results = new resultsType()
                                      {
                                          Items = new List<testsuiteType>().Cast<object>().ToArray()
                                      }
                    };

                this.testSuites.Add(suiteForNameSpace);
            }

            var castedSuitesForRule = suiteForNameSpace.results.Items.Cast<testsuiteType>().ToList();
            var suiteForRule = castedSuitesForRule.FirstOrDefault(s => s.name == @event.Violation.Rule.Name);

            if (suiteForRule == null)
            {
                suiteForRule = new testsuiteType()
                    {
                        name = @event.Violation.Rule.Name,
                        results =
                            new resultsType()
                                {
                                    Items = new List<testcaseType>().Cast<object>().ToArray()
                                }
                    };

                castedSuitesForRule.Add(suiteForRule);
                suiteForNameSpace.results.Items = castedSuitesForRule.Cast<object>().ToArray();
            }

            var currentTestCases = suiteForRule.results.Items.Cast<testcaseType>().ToList();

            currentTestCases.Add(new testcaseType()
                {
                    name = @event.Element.FullyQualifiedName,
                    description = string.Format("{0}:{1} {2}", @event.SourceCode.Path, @event.LineNumber, @event.Message),
                    executed = "True",
                    result = "Failure",
                    success = "False"
                });

            suiteForRule.results.Items = currentTestCases.Cast<object>().ToArray();

            this.Save();
        }

        public override void Completed(ExecutionResult result)
        {
            this.Save();
        }

        private void Save()
        {
            this.root.testsuite.results.Items = this.testSuites.Cast<object>().ToArray();
            this.writer.Write(this.root);
        }
    }
}

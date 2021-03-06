﻿using System;
using System.Collections.Generic;
using System.Linq;

using StyleCop;

using StyleCopCmd.Core;

namespace StyleCopCmd.Reporter.NUnit
{
    public class NUnitReporter : StyleCopIssueReporter
    {
        private readonly NUnitResultWriter writer;

        private readonly resultType root;

        private readonly List<testsuiteType> testSuites = new List<testsuiteType>();

        public NUnitReporter(string fileName)
        {
            this.writer = new NUnitResultWriter(fileName);

            this.root = new resultType
                {
                    name = "StyleCopTest-Results",
                    environment = new environmentType()
                                      {
                                          machinename = Environment.MachineName,
                                          osversion = Environment.OSVersion.VersionString,
                                          user = Environment.UserName,
                                          userdomain = Environment.UserDomainName
                                          },

                    testsuite = new testsuiteType
                        {
                            name = "All",
                            type = "StyleCop",
                            executed = "True",
                            result = "Failure",
                            success = "True",
                            results = new resultsType()
                        }
                        };
        }

        public override void Started()
        {
            this.root.date = DateTime.Now.ToString("yyy-MM-dd");
            this.root.time = DateTime.Now.ToString("HH:mm:ss");

            this.Save();
        }

        public override void Result(ViolationEventArgs @event)
        {
            this.root.errors++;
            this.root.failures++;
            this.root.total++;

            // Organize the viaolation inside test-suites (top level is the Namespace)
            var suiteForNameSpace = this.testSuites.FirstOrDefault(s => s.name == @event.Violation.Rule.Namespace);
            if (suiteForNameSpace == null)
            {
                suiteForNameSpace = new testsuiteType()
                    {
                        name = @event.Violation.Rule.Namespace, 
                        type = "Namespace",
                        executed = "false",
                        result = "Failure",
                        success = "false",
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
                        type = "Namespace",
                        executed = "False",
                        result = "Failure",
                        success = "False",
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
                    name = @event.Violation.Rule.Name,
                    description = string.Format("{0}:{1} {2}", @event.SourceCode.Path, @event.LineNumber, @event.Message),
                    asserts = "0",
                    executed = "True",
                    result = "Error",
                    success = "False"
                });

            suiteForRule.results.Items = currentTestCases.Cast<object>().ToArray();

            this.Save();
        }

        public override void Completed(ExecutionResult result)
        {
            this.root.testsuite.executed = "True";
            this.root.testsuite.success = "False";
            this.root.testsuite.result = "Failure";

            foreach (var suiteForNameSpace in this.testSuites)
            {
                var castedSuitesForRule = suiteForNameSpace.results.Items.Cast<testsuiteType>().ToList();
                castedSuitesForRule.ForEach(ts => ts.executed = "True");
                suiteForNameSpace.executed = "true";
            }

            this.Save();
        }

        private void Save()
        {
            this.root.testsuite.results.Items = this.testSuites.Cast<object>().ToArray();
            this.writer.Write(this.root);
        }
    }
}

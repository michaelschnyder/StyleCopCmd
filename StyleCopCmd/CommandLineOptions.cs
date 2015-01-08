using System.Collections.Generic;

using CommandLine;
using CommandLine.Text;

namespace StyleCopCmd
{
    // Define a class to receive parsed values
    public class CommandLineOptions
    {
        public IList<string> Items { get; set; }

        [OptionArray('s', "solution", HelpText = "Define one or more solution for which the analysation should be done")]
        public string[] Solutions { get; set; }

        [OptionArray('p', "project", HelpText = "Define one or more projects for which the analysation should be done")]
        public List<string> Projects { get; set; }

        [Option('w', "strict", DefaultValue = true, HelpText = "Count warnings as errors")]
        public bool WarningsAsErrors { get; set; }

        [Option('d', "exitCode", HelpText = "Not only log to all reporters, in addition return an error code on exit if and violations has occured")]
        public bool EnableExitCode { get; set; }

        [Option('c', "console", DefaultValue = true, HelpText = "Report errors and warnings to console")]
        public bool Console { get; set; }

        [Option("styleCopXml", HelpText = "Specify a file to which the results should be saved in StyleCopXml-Format (ValidationResults.xml)")]
        public string StyleCopXml { get; set; }

        [Option("nUnitXml", HelpText = "Specify a file to which the results should be saved in NUnitTestXml-Format")]
        public string NUnitXml { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
